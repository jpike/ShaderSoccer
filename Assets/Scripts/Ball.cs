using UnityEngine;

/// <summary>
/// The ball of the game.  This class is primarily responsible
/// for handling movement of the ball.
/// </summary>
public class Ball : MonoBehaviour
{
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

        // INCREASE THE MOVEMENT SPEED IF APPLICABLE.
        // The movement speed should only increase if the maximum limit hasn't been reached.
        MoveSpeedInMetersPerSecond += SpeedIncreasePerCollision;
        MoveSpeedInMetersPerSecond = Mathf.Clamp(MoveSpeedInMetersPerSecond, 0.0f, MaxSpeedInMetersPerSecond);
    }
    #endregion
}
