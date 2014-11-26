using UnityEngine;

/// <summary>
/// A controller for moving a field player based on computer artificial intelligence.
/// It does not contain any update code itself - rather, another script
/// should call public methods in this class as necessary.
/// </summary>
public class ComputerFieldPlayerController : MonoBehaviour 
{
    #region Public Fields
    /// <summary>
    /// The minimum vertical speed at which the field player moves.
    /// Units are Unity units (meters) per second.
    /// </summary>
    public float MinVerticalMoveSpeedInMetersPerSecond = 0.1f;

    /// <summary>
    /// The maximum vertical speed at which the field player moves.
    /// Units are Unity units (meters) per second.  The default
    /// value is slightly higher than the player's vertical move
    /// speed because it was observed that the the computer players
    /// would otherwise fail to move quickly enough to the ball.
    /// </summary>
    public float MaxVerticalMoveSpeedInMetersPerSecond = 6.0f;

    /// <summary>
    /// The minimum vertical distance (in meters) between the ball and the
    /// player before the computer AI will attempt to move.  The ball must
    /// be at least this far away from the player (vertically) before
    /// the AI will attempt to move.
    /// </summary>
    public float MinBallToPlayerVerticalDistanceInMeters = 0.5f;

    /// <summary>
    /// The maximum total distance (in meters) between the ball and the
    /// player before the computer AI will attempt to move.  If the ball is
    /// further away than this (on the 2D plane), then no movement will
    /// occur.  The default is chosen to be slightly less than half the width
    /// of the playing field so that the player only moves when the ball gets
    /// approximately on the same half of the field that the attached player
    /// is on.  This helps provide slightly different behavior for computer AI
    /// players - otherwise, they would often both move synchronously.
    /// </summary>
    public float MaxBallToPlayerTotalDistanceInMeters = 7.0f;
    #endregion

    #region Private Fields
    /// <summary>
    /// The ball currently in play.  Used for determining
    /// which direction the AI should move the field player.
    /// </summary>
    private Ball m_ball;
    #endregion

    #region Initialization Methods
    /// <summary>
    /// Initializes the computer AI to know where the ball is.
    /// </summary>
    private void Start()
    {
        // FIND THE BALL.
        GameObject ballGameObject = GameObject.Find(Ball.BALL_OBJECT_NAME);
        m_ball = ballGameObject.GetComponent<Ball>();
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Moves the field player according to artificial intelligence.
    /// Intended to be called once per frame.
    /// </summary>
    public void MoveBasedOnAi()
    {
        // MAKE SURE THE VERTICAL DISTANCE BETWEEN THE BALL AND PLAYER POSITION MEETS SOME MINIMUM THRESHOLD.
        // This prevents a distracting jitter that occurs from the ball moving too frequently.  If this minimum
        // threshold weren't met, the player may be moved for small variations in the ball position around
        // the player's center position, resulting in undesirable up-and-down jittering.
        Vector3 ballPosition = m_ball.Bounds.center;
        float ballToPlayerVerticalDistance = Mathf.Abs(ballPosition.y - transform.position.y);
        bool minVerticalDistanceReachedBetweenBallAndPlayer = (ballToPlayerVerticalDistance >= MinBallToPlayerVerticalDistanceInMeters);
        if (!minVerticalDistanceReachedBetweenBallAndPlayer)
        {
            // Return early since no movement is needed.
            return;
        }

        // MAKE SURE THE BALL IS CLOSE ENOUGH TO THE PLAYER.
        // This helps prevent scenarios where two AI players would move in similar patterns
        // due to both detecting the ball regardless of where it exists in the playing field.
        Vector2 ballPosition2d = new Vector2(ballPosition.x, ballPosition.y);
        Vector2 playerPosition2d = new Vector2(transform.position.x, transform.position.y);
        float ballToPlayerDistance = Vector2.Distance(ballPosition2d, playerPosition2d);
        bool ballTooFarAwayFromPlayer = (ballToPlayerDistance > MaxBallToPlayerTotalDistanceInMeters);
        if (ballTooFarAwayFromPlayer)
        {
            // Return early since no movement is needed.
            return;
        }

        // DETERMINE WHICH DIRECTION THE AI SHOULD MOVE THE FIELD PLAYER.
        // The player should move closer toward the direction of the ball.
        // If the positions are equal, than the player shouldn't move at all.
        float verticalDirection = 0.0f;
        bool ballAbovePlayer = (ballPosition.y > transform.position.y);
        bool ballBelowPlayer = (ballPosition.y < transform.position.y);
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

        // DETERMINE THE MAGNITUDE WHICH TO MOVE THE PLAYER.
        float verticalMoveSpeedInMetersPerSecond = Random.Range(
            MinVerticalMoveSpeedInMetersPerSecond, 
            MaxVerticalMoveSpeedInMetersPerSecond);

        // MOVE THE FIELD PLAYER VERTICALLY BASED ON THE ELAPSED TIME AND CALCULATED DIRECTION.
        float elapsedTimeInSeconds = Time.deltaTime;
        float verticalMovementInMeters = verticalDirection * verticalMoveSpeedInMetersPerSecond * elapsedTimeInSeconds;
        Vector3 verticalMovement = verticalMovementInMeters * Vector3.up;

        transform.Translate(verticalMovement);
    }
    #endregion
}
