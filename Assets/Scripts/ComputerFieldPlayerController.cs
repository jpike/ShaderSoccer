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
    /// Units are Unity units (meters) per second.
    /// </summary>
    public float MaxVerticalMoveSpeedInMetersPerSecond = 5.0f;

    /// <summary>
    /// The minimum distance (in meters) between the ball and the
    /// player before the computer AI will attempts to move.
    /// </summary>
    public float MinBallToPlayerDistanceInMeters = 1.0f;
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
        // MAKE SURE THE DISTANCE BETWEEN THE BALL AND PLAYER POSITION MEETS SOME MINIMUM THRESHOLD.
        // This prevents a distracting jitter that occurs from the ball moving too frequently.
        Vector3 ballPosition = m_ball.Bounds.center;
        float ballToPlayerDistance = Mathf.Abs(ballPosition.y - transform.position.y);
        bool minDistanceReachedBetweenBallAndPlayer = (ballToPlayerDistance >= MinBallToPlayerDistanceInMeters);
        if (!minDistanceReachedBetweenBallAndPlayer)
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
