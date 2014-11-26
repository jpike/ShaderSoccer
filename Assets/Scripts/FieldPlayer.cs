using UnityEngine;

/// <summary>
/// A player in the game field.  It may be controlled by human user input
/// or computer AI depending on the movement controller type.  A controller
/// script of the corresponding type must be attached based on the movement
/// controller type.  Multiple controller scripts may be attached at once
/// to allow easy switching by simply changing an enumeration field.
/// </summary>
public class FieldPlayer : MonoBehaviour
{
    #region Public Enumerations
    /// <summary>
    /// Defines the type of controller for movement of the player.
    /// </summary>
    public enum MovementControlType
    {
        /// <summary>
        /// Movement is controlled by human user input.
        /// </summary>
        HUMAN_CONTROL,
        /// <summary>
        /// Movement is controlled by computer AI.
        /// </summary>
        COMPUTER_CONTROL
    };
    #endregion

    #region Public Fields
    /// <summary>
    /// Defines the type of controller for movement of the player.
    /// If human control, a human controller script should be attached.
    /// If computer contorl, a computer controller script should be attached.
    /// </summary>
    public MovementControlType MovementControllerType = MovementControlType.HUMAN_CONTROL;
    #endregion

    #region Public Properties
    /// <summary>
    /// Retrieves the world boundaries of the field player.
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

    /// <summary>
    /// Retrieves the transform of the field player.
    /// </summary>
    public Transform Transform
    {
        get
        {
            // A transform should be attached to every game object.
            return transform;
        }
    }
    #endregion

    #region Private Fields
    /// <summary>
    /// A game controller for a human supplying input to move the field player.
    /// Must be set if human control is set for the player.
    /// </summary>
    private HumanFieldPlayerController m_humanPlayerController;

    /// <summary>
    /// A game controller for a computer AI controlling movement of the field player.
    /// Must be set if computer control is set for the player.
    /// </summary>
    private ComputerFieldPlayerController m_computerPlayerController;
    #endregion

    #region Initialization Methods
    /// <summary>
    /// Initalizes the field player to have references to all dependent objects.
    /// </summary>
	private void Start() 
    {
        // GET ANY HUMAN CONTROLLER FOR THE PLAYER.
        m_humanPlayerController = gameObject.GetComponent<HumanFieldPlayerController>();

        // GET ANY COMPUTER CONTROLLER FOR THE PLAYER.
        m_computerPlayerController = gameObject.GetComponent<ComputerFieldPlayerController>();
	}
    #endregion

    #region Update Methods
    /// <summary>
    /// Moves the field player once per frame according to the type of movement controller.
    /// </summary>
	private void Update()
    {
        // MOVE THE PLAYER BASED ON THE CONTROLLER TYPE.
        switch (MovementControllerType)
        {
            case MovementControlType.HUMAN_CONTROL:
                // MOVE THE PLAYER BASED ON ANY HUMAN USER INPUT.
                m_humanPlayerController.MoveBasedOnInput();
                break;
            case MovementControlType.COMPUTER_CONTROL:
                // MOVE THE PLAYER BASED ON COMPUTER AI.
                m_computerPlayerController.MoveBasedOnAi();
                break;
            default:
                // Do nothing since an invalid control type was specified.
                break;
        }
    }
    #endregion
}
