using UnityEngine;

/// <summary>
/// A GUI toggle button for controlling whether or not a field player
/// is controlled by human user input or computer AI.
/// </summary>
public class PlayerControlToggleButton : MonoBehaviour
{
    #region Public Fields
    /// <summary>
    /// Whether or not the toggle button is right-aligned on the screen.
    /// If not, the button will be left-aligned.
    /// </summary>
    public bool RightAligned = false;

    /// <summary>
    /// The style of the toggle button GUI.
    /// </summary>
    public GUIStyle ToggleButtonStyle;

    /// <summary>
    /// The field player controlled by this button.
    /// </summary>
    public FieldTeam Team;
    #endregion

    #region Private Fields
    /// <summary>
    /// Whether or not the attached player should be controlled by computer AI.
    /// </summary>
    private bool m_useComputerAi = false;
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
            Team.UseComputerControl();
        }
        else
        {
            Team.UseHumanControl();
        }
    }
    #endregion
}
