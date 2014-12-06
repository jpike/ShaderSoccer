using UnityEngine;

/// <summary>
/// Defines the field where gameplay takes place.  This class
/// is intended to encapsulate handling collisions of objects
/// with the playing field.
/// 
/// It restricts objects to ensure that they don't leave the playing field.
/// 
/// Public fields exist for defining the boundaries of the playing field - 
/// it is expected that these boundaries are for objects that already have
/// colliders.  The colliders on these objects are used to automatically
/// handle collisions with other rigidbodies in the game that, unlike
/// field players, are processed by the physics engine
/// (the ball, for example).
/// </summary>
public class PlayField : MonoBehaviour
{
    #region Public Fields
    /// <summary>
    /// A transform defining the top boundary of the playing field.
    /// Only the y-coordinate matters.
    /// </summary>
    public Transform TopBoundary;
    /// <summary>
    /// A transform defining the bottom boundary of the playing field.
    /// Only the y-coordinate matters.
    /// </summary>
    public Transform BottomBoundary;
    /// <summary>
    /// A transform defining the left boundary of the playing field.
    /// Only the x-coordinate matters.
    /// </summary>
    public Transform LeftBoundary;
    /// <summary>
    /// A transform defining the right boundary of the playing field.
    /// Only the x-coordinate matters.
    /// </summary>
    public Transform RightBoundary;
    #endregion

    #region Private Fields
    /// <summary>
    /// The world boundaries of the playing field.
    /// </summary>
    private Bounds m_boundaries;

    /// <summary>
    /// The ball within the playing field.
    /// </summary>
    private Ball m_ball;
    #endregion

    #region Public Properties
    /// <summary>
    /// Accesses the boundaries of the playing field
    /// </summary>
    public Bounds Bounds
    {
        get
        {
            return m_boundaries;
        }
    }
    #endregion

    #region Initialization Methods
    /// <summary>
	/// Initializes the play field.
	/// </summary>
	private void Start() 
    {
        // INITIALIZE THE BOUNDARIES OF THE PLAYING FIELD.
        m_boundaries = CalculateBounds();

        // FIND THE BALL IN THE PLAYING FIELD.
        m_ball = gameObject.GetComponentInChildren<Ball>();
	}

    /// <summary>
    /// Calculates the world boundaries of the playing field.
    /// </summary>
    /// <returns>The world boundaries of the playing field.</returns>
    private Bounds CalculateBounds()
    {
        // CALCULATE THE CENTER OF THE PLAYING FIELD.
        // It should always be positioned at the world origin.
        Vector3 fieldCenter = Vector3.zero;

        // CALCULATE THE SIZE OF THE PLAYING FIELD.
        float fieldHeight = TopBoundary.position.y - BottomBoundary.position.y;
        float fieldWidth = RightBoundary.position.x - LeftBoundary.position.x;

        // The field depth should always be a constant.  Even though the game
        // uses 3D graphics, it effectively takes place on a 2D playing field,
        // so the field depth is largely irrelevant.  However, it is chosen to
        // be large enough to avoid accidental collisions in the z-direction.
        const float FIELD_DEPTH = 4.0f;

        Vector3 fieldSize = new Vector3(fieldWidth, fieldHeight, FIELD_DEPTH);

        // FORM THE BOUNDARIES.
        Bounds bounds = new Bounds(fieldCenter, fieldSize);
        return bounds;
    }
    #endregion

    #region Update Methods
    /// <summary>
    /// Updates the playing field, ensuring the ball stays confined.
    /// </summary>
    private void Update()
    {
        ConfineBallToField();
    }

    /// <summary>
    /// Ensures that the ball remains confined within the playing field.
    /// This method ensures that the ball gets reset if it escapes the playing field,
    /// which can happen if it ends up moving to fast in a particular direction
    /// (which can throw off the collision system).
    /// </summary>
    private void ConfineBallToField()
    {
        // CONFINE THE BALL TO WITHIN THE TOP BOUNDARY.
        ConfineBallToTopBoundary(m_ball);

        // CONFINE THE BALL TO WITHIN THE BOTTOM BOUNDARY.
        ConfineBallToBottomBoundary(m_ball);

        // CONFINE THE BALL TO WITHIN THE LEFT BOUNDARY.
        ConfineBallToLeftBoundary(m_ball);

        // CONFINE THE BALL TO WITHIN THE RIGHT BOUNDARY.
        ConfineBallToRightBoundary(m_ball);
    }

    /// <summary>
    /// Ensures that the provided ball is confined to within the top boundary
    /// of the playing field.
    /// </summary>
    /// <param name="ball">The ball to confine to the top boundary.
    /// It will be reset if it exceeds the boundary.</param>
    private void ConfineBallToTopBoundary(Ball ball)
    {
        // CHECK IF THE BALL'S TOP HAS EXCEEDED THE TOP BOUNDARY.
        // The bottom of the ball is checked against the top of the boundaries
        // so that resetting only occurs if the ball completely leaves the field
        // (as opposed to if just a single edge leaves the field).
        Bounds ballBounds = ball.Bounds;
        bool ballCollidedWithTop = ballBounds.min.y > m_boundaries.max.y;
        if (ballCollidedWithTop)
        {
            // RESET THE BALL SO THAT GAMEPLAY CAN CONTINUE.
            ball.Reset();
        }
    }

    /// <summary>
    /// Ensures that the provided ball is confined to within the bottom boundary
    /// of the playing field.
    /// </summary>
    /// <param name="ball">The ball to confine to the bottom boundary.
    /// It will be reset if it exceeds the boundary.</param>
    private void ConfineBallToBottomBoundary(Ball ball)
    {
        // CHECK IF THE BALL'S BOTTOM HAS EXCEEDED THE BOTTOM BOUNDARY.
        // The top of the ball is checked against the bottom of the boundaries
        // so that resetting only occurs if the ball completely leaves the field
        // (as opposed to if just a single edge leaves the field).
        Bounds ballBounds = ball.Bounds;
        bool ballCollidedWithBottom = ballBounds.max.y < m_boundaries.min.y;
        if (ballCollidedWithBottom)
        {
            // RESET THE BALL SO THAT GAMEPLAY CAN CONTINUE.
            ball.Reset();
        }
    }

    /// <summary>
    /// Ensures that the provided ball is confined to within the left boundary
    /// of the playing field.
    /// </summary>
    /// <param name="ball">The ball to confine to the left boundary.
    /// It will be reset if it exceeds the boundary.</param>
    private void ConfineBallToLeftBoundary(Ball ball)
    {
        // CHECK IF THE BALL'S LEFT HAS EXCEEDED THE LEFT BOUNDARY.
        // The right of the ball is checked against the left of the boundaries
        // so that resetting only occurs if the ball completely leaves the field
        // (as opposed to if just a single edge leaves the field).
        Bounds ballBounds = ball.Bounds;
        bool ballCollidedWithLeft = ballBounds.max.x < m_boundaries.min.x;
        if (ballCollidedWithLeft)
        {
            // RESET THE BALL SO THAT GAMEPLAY CAN CONTINUE.
            ball.Reset();
        }
    }

    /// <summary>
    /// Ensures that the provided ball is confined to within the right boundary
    /// of the playing field.
    /// </summary>
    /// <param name="ball">The ball to confine to the right boundary.
    /// It will be reset if it exceeds the boundary.</param>
    private void ConfineBallToRightBoundary(Ball ball)
    {
        // CHECK IF THE BALL'S LEFT HAS EXCEEDED THE RIGHT BOUNDARY.
        // The left of the ball is checked against the right of the boundaries
        // so that resetting only occurs if the ball completely leaves the field
        // (as opposed to if just a single edge leaves the field).
        Bounds ballBounds = ball.Bounds;
        bool ballCollidedWithRight = ballBounds.min.x > m_boundaries.max.x;
        if (ballCollidedWithRight)
        {
            // RESET THE BALL SO THAT GAMEPLAY CAN CONTINUE.
            ball.Reset();
        }
    }
    #endregion
}
