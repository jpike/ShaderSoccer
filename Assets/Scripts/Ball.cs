using UnityEngine;

/// <summary>
/// The ball of the game.  This class is primarily responsible
/// for handling movement of the ball.
/// </summary>
public class Ball : MonoBehaviour
{
    #region Public Constants
    /// <summary>
    /// The name of the ball game object.
    /// </summary>
    public const string BALL_OBJECT_NAME = "Ball";
    #endregion

    #region Public Fields
    /// <summary>
    /// The direction of the ball's movement.  It may be updated
    /// as the direction changes.
    /// </summary>
    public Vector3 Direction = Vector3.zero;

    /// <summary>
    /// The movement speed of the ball, in Unity units (meters) per second.
    /// </summary>
    public float MoveSpeedInMetersPerSecond = 2.0f;

    /// <summary>
    /// How much the speed should increase each time the ball collides.
    /// Units are the same as the overall speed for the ball.
    /// </summary>
    public float SpeedIncreasePerCollision = 0.1f;

    /// <summary>
    /// The maximum movement speed of the ball.  If this is too high,
    /// then collisions may not properly work.
    /// </summary>
    public float MaxSpeedInMetersPerSecond = 10.0f;
    #endregion

    #region Public Properties
    /// <summary>
    /// Retrieves the world boundaries of the ball.
    /// </summary>
    public Bounds Bounds
    {
        get
        {
            // RETRIEVE THE BOUNDS FROM THE ATTACHED COLLIDER IF POSSIBLE.
            bool colliderExists = (gameObject.collider != null);
            if (colliderExists)
            {
                return gameObject.collider.bounds;
            }
            else
            {
                // Return default, invalid bounds since no collider was attached to this object.
                return new Bounds();
            }
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Resets the ball to its initial position in the center of the playing
    /// field and starts it moving at a random direction.
    /// </summary>
    public void Reset()
    {
        // POSITION THE BALL IN THE CENTER OF THE PLAYING FIELD.
        gameObject.transform.position = Vector3.zero;

        // SELECT A RANDOM DIRECTION FOR THE BALL TO MOVE IN.
        // Since the game effectively occurs on a 2D X-Y plane,
        // the Z coordinate does not need to be randomized.
        const float NO_Z_DIRECTION = 0.0f;
        // Since random values may be zero but we want to ensure that the
        // ball does not get stuck moving completely horizontally or vertically,
        // a minimum direction value is specified.
        const float MIN_RANDOM_DIRECTION = 0.1f;
        // The maximum random direction is arbitrary, but it should not be made
        // too large.
        const float MAX_RANDOM_DIRECTION = 1.0f;

        float newXDirection = Random.Range(MIN_RANDOM_DIRECTION, MAX_RANDOM_DIRECTION);
        float newYDirection = Random.Range(MIN_RANDOM_DIRECTION, MAX_RANDOM_DIRECTION);

        // SET THE NEW DIRECTION FOR THE BALL.
        Direction = new Vector3(newXDirection, newYDirection, NO_Z_DIRECTION);
    }
    #endregion

    #region Update Methods
    /// <summary>
    /// Moves the ball based on elapsed time.  Since the ball interacts
    /// with the physics engine, this is done during a fixed update.
    /// </summary>
	private void FixedUpdate() 
    {
        // MOVE THE BALL BASED ON THE ELAPSED TIME.
        float elapsedTimeInSeconds = Time.deltaTime;
        Vector3 movementInMeters = MoveSpeedInMetersPerSecond * elapsedTimeInSeconds * Direction;
        Vector3 newBallPosition = rigidbody.position + movementInMeters;
        rigidbody.MovePosition(newBallPosition);
    }
    #endregion

    #region Collison Methods
    /// <summary>
    /// Handles collisions of the ball by reflecting its direction
    /// and increasing its speed (if there is room left to increase).
    /// </summary>
    /// <param name="collision">The collision that occurred.</param>
    private void OnCollisionEnter(Collision collision)
    {
        // GET THE NORMAL OF THE COLLISION SURFACE FOR REFLECTION.
        // Within this method, there is guaranteed to be at least 1 contact point.
        // Using the first contact point for the normal, should be sufficient.
        ContactPoint contactPoint = collision.contacts[0];
        Vector3 collidedSurfaceNormal = contactPoint.normal;

        // REFLECT THE DIRECTION OF THE BALL.
        Direction = Vector3.Reflect(Direction, collidedSurfaceNormal);

        // Force the Z coordinate to zero to ensure the ball stays in the 2D plane.
        // For some models, the surface normals may cause the ball to bounce in
        // different directions.
        Direction.z = 0.0f;

        // INCREASE THE MOVEMENT SPEED IF APPLICABLE.
        // The movement speed should only increase if the maximum limit hasn't been reached.
        MoveSpeedInMetersPerSecond += SpeedIncreasePerCollision;
        MoveSpeedInMetersPerSecond = Mathf.Clamp(MoveSpeedInMetersPerSecond, 0.0f, MaxSpeedInMetersPerSecond);
    }
    #endregion
}
