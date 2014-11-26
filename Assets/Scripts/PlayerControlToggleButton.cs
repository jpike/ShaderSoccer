using UnityEngine;

/// <summary>
/// A GUI toggle button for controlling whether or not a field player
/// is controlled by human user input or computer AI.
/// </summary>
public class PlayerControlToggleButton : MonoBehaviour
{
    #region Public Fields
    /// <summary>
    /// The game object for the field player controlled by this button.
    /// </summary>
    public GameObject FieldPlayerObject;

    /// <summary>
    /// Whether or not the toggle button is right-aligned on the screen.
    /// If not, the button will be left-aligned.
    /// </summary>
    public bool RightAligned = false;

    /// <summary>
    /// The style of the toggle button GUI.
    /// </summary>
    public GUIStyle ToggleButtonStyle;
    #endregion

    #region Private Fields
    /// <summary>
    /// The field player controlled by this button.
    /// </summary>
    private FieldPlayer m_player = null;

    /// <summary>
    /// Whether or not the attached player should be controlled by computer AI.
    /// </summary>
    private bool m_useComputerAi = false;
    #endregion


    #region Initialization Methods
    /// <summary>
    /// Finds the field player and determines the type of controller for its movement.
    /// </summary>
    private void Start()
    {
        // DETERMINE THE TYPE OF MOVEMENT CONTROLLER CURRENTLY USED FOR THE FIELD PLAYER.
        m_player = FieldPlayerObject.GetComponent<FieldPlayer>();
        switch (m_player.MovementControllerType)
        {
            case FieldPlayer.MovementControlType.HUMAN_CONTROL:
                m_useComputerAi = false;
                break;
            case FieldPlayer.MovementControlType.COMPUTER_CONTROL:
                m_useComputerAi = true;
                break;
            default:
                // Leave the setting for using computer AI alone since a valid
                // movement type couldn't be determined.
                break;
        }
	}
    #endregion

    #region GUI Methods
    /// <summary>
    /// Displays the toggle GUI button and allows users to alter
    /// its state to switch the attached player from being controlled
    /// by human user input or computer AI.
    /// </summary>
    private void OnGUI()
    {
        // CALCULATE THE BOUNDING RECTANGLE POSITION OF THE TOGGLE BUTTON.
        const float TOGGLE_BUTTON_TOP_Y_POSITION = 0.0f;
        const float TOGGLE_BUTTON_WIDTH = 128.0f;
        const float TOGGLE_BUTTON_HEIGHT = 64.0f;
        Rect toggleButtonBoundingRectangle;
        if (RightAligned)
        {
            // ALIGN THE TOGGLE BUTTON AT THE RIGHT OF THE SCREEN.
            float toggleButtonLeftXPosition = Screen.width - TOGGLE_BUTTON_WIDTH;
            toggleButtonBoundingRectangle = new Rect(
                toggleButtonLeftXPosition,
                TOGGLE_BUTTON_TOP_Y_POSITION,
                TOGGLE_BUTTON_WIDTH,
                TOGGLE_BUTTON_HEIGHT);
        }
        else
        {
            // ALIGN THE TOGGLE BUTTON AT THE LEFT OF THE SCREEN.
            float toggleButtonLeftXPosition = 0.0f;
            toggleButtonBoundingRectangle = new Rect(
                toggleButtonLeftXPosition,
                TOGGLE_BUTTON_TOP_Y_POSITION,
                TOGGLE_BUTTON_WIDTH,
                TOGGLE_BUTTON_HEIGHT);
        }

        // DRAW THE TOGGLE BUTTON BASED ON THE ATTACHED PLAYER'S CURRENT MOVEMENT SETTING.
        m_useComputerAi = GUI.Toggle(toggleButtonBoundingRectangle, m_useComputerAi, "CPU AI");

        // UPDATE THE ATTACHED PLAYER'S MOVEMENT SETTING.
        if (m_useComputerAi)
        {
            m_player.MovementControllerType = FieldPlayer.MovementControlType.COMPUTER_CONTROL;
        }
        else
        {
            m_player.MovementControllerType = FieldPlayer.MovementControlType.HUMAN_CONTROL;
        }
    }
    #endregion
}
