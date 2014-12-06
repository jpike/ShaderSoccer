using UnityEngine;

/// <summary>
/// A controller for moving a line of field players based on computer artificial intelligence.
/// It does not contain any update code itself - rather, another script should call public
/// methods in this class as desired.
/// </summary>
public class ComputerFieldPlayerLineController : MonoBehaviour
{
    /// <summary>
    /// The minimum vertical speed at which the line of field players moves.
    /// Units are Units units (meters) per second.
    /// </summary>
    public float MinVerticalMoveSpeedInMetersPerSecond = 0.1f;

    /// <summary>
    /// The maximum vertical speed at which the line of field players moves.
    /// Units are Unity units (meters) per second.  The default value is
    /// slightly higher than the player's vertical move speed because it
    /// was observed that computer players would otherwise fail to move
    /// quickly enough to the ball.
    /// </summary>
    public float MaxVerticalMoveSpeedInMetersPerSecond = 6.0f;

    /// <summary>
    /// The minimum vertical distance (in meters) between the ball and the
    /// center of the line of players before the computer AI will attempt
    /// to move.  The ball be at least this far away from the line's center
    /// (vertically) before the AI will attempt to move.
    /// </summary>
    public float MinBallToLineVerticalDistanceInMeters = 0.5f;

    /// <summary>
    /// The maximum total distance (in meters) between the ball and the
    /// center of the line of players before the computer AI will attempt
    /// to move.  If the ball is further away than this (on the 2D plane),
    /// then no movement will occur.  The default is chosenn so that the
    /// line doesn't move too often if the ball is too far away.  This helps
    /// provide slightly different behavior for computer AI players on
    /// opposite sides of the fields - otherwise, both sides would often
    /// move synchronously.
    /// </summary>
    public float MaxBallToLineDistanceInMeters = 3.0f;

    /// <summary>
    /// The line of players controlled by this AI.
    /// </summary>
    private FieldPlayerLine m_fieldPlayerLine = null;

    /// <summary>
    /// The ball currently in play.  Used for determining which direction
    /// the AI should move the line of players.
    /// </summary>
    private Ball m_ball = null;

    /// <summary>
    /// The field in which the game is played.
    /// </summary>
    private PlayField m_playField = null;

    /// <summary>
    /// Initializes the computer AI to know about necessary game objects.
    /// This method is intended to mimic a constructor.  An explicit
    /// initialize method was chosen over implementing the standard
    /// Start() method to better control when this component gets
    /// initialized.
    /// </summary>
    /// <param name="fieldPlayerLine">The line of players controlled by this AI.</param>
    /// <param name="ball">The ball currently in play to be examined by the AI.</param>
    /// <param name="playField">The playing field where the game is being played.</param>
    public void Initialize(
        FieldPlayerLine fieldPlayerLine,
        Ball ball,
        PlayField playField)
    {
        m_fieldPlayerLine = fieldPlayerLine;
        m_ball = ball;
        m_playField = playField;
    }

    /// <summary>
    /// Moves the line of field players according to artificial intelligence.
    /// Intended to be called once per frame.
    /// </summary>
    public void MoveBasedOnAi()
    {
        // MAKE SURE BALL IS CLOSE ENOUGH.
        // This helps prevent scenarios where two AI player lines would move in similar patterns
        // due to both lines detecting the ball regardless of where it exists in the playing field.
        bool ballCloseEnoughToPlayerLine = BallCloseEnoughToPlayerLine();
        if (!ballCloseEnoughToPlayerLine)
        {
            // The ball is too far away for any movement.
            return;
        }

        // MAKE SURE THE MINIMUM VERTICAL DISTANCE THRESHOLD IS MET.
        // This prevents a distracting jitter that occurs from the ball moving too frequently.
        // If this minimum threshold weren't met, the players may be moved for small variations
        // in the ball position around the player line's center position, resulting in undesirable
        // up-and-down jittering.
        bool minVerticalDistanceMet = MinBallToLineCenterVerticalDistanceMet();
        if (!minVerticalDistanceMet)
        {
            // The ball is too far away for any movement.
            return;
        }

        // DETERMINE WHICH DIRECTION THE AI SHOULD MOVE THE LINE OF PLAYERS.
        float verticalDirection = CalculateMovementDirection();

        // DETERMINE THE SPEED AT WHICH TO MOVE THE LINE OF PLAYERS.
        float verticalMoveSpeedInMetersPerSecond = CalculateMovementSpeed();

        // CALCULATE THE VERTICAL MOVEMENT BASED ON ELAPSED TIME.
        float elapsedTimeInSeconds = Time.deltaTime;
        float verticalMovementInMeters = verticalDirection * verticalMoveSpeedInMetersPerSecond * elapsedTimeInSeconds;
        Vector3 verticalMovement = verticalMovementInMeters * Vector3.up;

        // RESTRICT THE NEW POSITION FOR THE LINE OF PLAYERS TO THE PLAYING FIELD.
        Vector3 newPosition = ConfinePositionToPlayingField(verticalMovement);
        
        transform.position = newPosition;
    }

    /// <summary>
    /// Determines if the ball is close enough to the line of players
    /// according to the distance configured for this object.
    /// </summary>
    /// <returns>True if the ball is close enough to the line of players;
    /// false otherwise.</returns>
    private bool BallCloseEnoughToPlayerLine()
    {
        // GET THE POSITION OF THE BALL AND PLAYER LINE.
        Vector3 ballPosition = m_ball.Bounds.center;
        Vector3 playerLinePosition = m_fieldPlayerLine.transform.position;

        // CALCULATE THE DISTANCE BETWEEN THE BALL AND PLAYER LINE.
        // The distance is checked in a 2D plane since that is where the gameplay
        // takes place.  Doing a 3D distance check may result in unexpected results.
        Vector2 ballPosition2d = new Vector2(ballPosition.x, ballPosition.y);
        Vector2 playerLinePosition2d = new Vector2(playerLinePosition.x, playerLinePosition.y);
        float ballToPlayerLineDistance = Vector2.Distance(ballPosition2d, playerLinePosition2d);

        // CHECK IF THE BALL IS CLOSE ENOUGH.
        bool ballCloseEnoughToPlayerLine = (ballToPlayerLineDistance <= MaxBallToLineDistanceInMeters);
        return ballCloseEnoughToPlayerLine;
    }

    /// <summary>
    /// Determines if the ball is close enough vertically to the center of
    /// the line of players according to the distance configured for this object.
    /// </summary>
    /// <returns>True if the ball is close enough vertically; false otherwise.</returns>
    private bool MinBallToLineCenterVerticalDistanceMet()
    {
        // GET THE POSITION OF THE BALL AND PLAYER LINE.
        Vector3 ballPosition = m_ball.Bounds.center;
        Vector3 playerLinePosition = m_fieldPlayerLine.transform.position;

        // CALCULATE THE VERTICAL DISTANCE BETWEEN THE BALL AND PLAYER LINE.
        float ballToPlayerLineVerticalDistance = Mathf.Abs(ballPosition.y - playerLinePosition.y);

        // CHECK IF THE MINIMUM VERTICAL DISTANCE WAS MET.
        bool minVerticalDistanceReached = (ballToPlayerLineVerticalDistance >= MinBallToLineVerticalDistanceInMeters);
        return minVerticalDistanceReached;
    }

    /// <summary>
    /// Calculates the vertical direction in which the line of players
    /// should move based on the position of the ball.
    /// </summary>
    /// <returns>The vertical direction in which the line of players should move.
    /// Will be 0 or a negative or positive 1.</returns>
    private float CalculateMovementDirection()
    {
        // GET THE POSITION OF THE BALL AND PLAYER LINE.
        Vector3 ballPosition = m_ball.Bounds.center;
        Vector3 playerLinePosition = m_fieldPlayerLine.transform.position;

        // DETERMINE WHICH DIRECTION THE AI SHOULD MOVE THE FIELD PLAYER LINE.
        // The player line should move closer toward the direction of the ball.
        // If the positions are equal, than the player line shouldn't move at all.
        // This is based simply on the center position of the line, as a simplification.
        float verticalDirection = 0.0f;
        bool ballAbovePlayer = (ballPosition.y > playerLinePosition.y);
        bool ballBelowPlayer = (ballPosition.y < playerLinePosition.y);
        if (ballAbovePlayer)
        {
            // Move upward to move closer to the ball.
            verticalDirection = 1.0f;
        }
        else if (ballBelowPlayer)
        {
            // Move downward to move closer to the ball.
            verticalDirection = -1.0f;
        }

        return verticalDirection;
    }

    /// <summary>
    /// Calculates the vertical movement speed for the line of players,
    /// based on the configured vertical speeds for this object.
    /// </summary>
    /// <returns>The vertical movement speed for the line of player.</returns>
    private float CalculateMovementSpeed()
    {
        // The speed is randomly calculated to add a little variation to gameplay.
        float verticalMoveSpeedInMetersPerSecond = Random.Range(
            MinVerticalMoveSpeedInMetersPerSecond,
            MaxVerticalMoveSpeedInMetersPerSecond);

        return verticalMoveSpeedInMetersPerSecond;
    }

    /// <summary>
    /// Confines the position of the line of players to the playing field,
    /// taking into account the provided vertical movement and playing
    /// field boundaries.
    /// </summary>
    /// <param name="verticalMovement">The vertical movement to add to the
    /// current line position to determine the final position to confine
    /// to the playing field.</param>
    /// <returns>The new world position of the line of field players,
    /// after being moved by the provided amount but restricted to the
    /// boundaries of the playing field.</returns>
    private Vector3 ConfinePositionToPlayingField(Vector3 verticalMovement)
    {
        // GET THE MINIMUM AND MAXIMUM VALID POSITIONS FOR THE PLAYER LINE.
        float minValidCenterYPosition = m_playField.Bounds.min.y + m_fieldPlayerLine.Bounds.extents.y;
        float maxValidCenterYPosition = m_playField.Bounds.max.y - m_fieldPlayerLine.Bounds.extents.y;

        // TAKE THE VERTICAL MOVEMENT INTO ACCOUNT FOR THE PLAYER LINE'S POSITION.
        Vector3 newLinePosition = m_fieldPlayerLine.transform.position + verticalMovement;

        // RESTRICT THE NEW POSITION TO THE VALID RANGE.
        newLinePosition.y = Mathf.Clamp(newLinePosition.y, minValidCenterYPosition, maxValidCenterYPosition);
        return newLinePosition;
    }
}
