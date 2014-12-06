using UnityEngine;

/// <summary>
/// Controls a fielded team of players based on human user input.
/// </summary>
public class HumanFieldTeamController : MonoBehaviour 
{
    /// <summary>
    /// The name of the input button for switching to the left line of players
    /// for the left team.
    /// </summary>
    public const string LEFT_TEAM_SWITCH_TO_LEFT_LINE_BUTTON_NAME = "LeftTeamHorizontalLeft";
    /// <summary>
    /// The name of the input button for switching to the right line of players
    /// for the left team.
    /// </summary>
    public const string LEFT_TEAM_SWITCH_TO_RIGHT_LINE_BUTTON_NAME = "LeftTeamHorizontalRight";
    /// <summary>
    /// The name of the input button for switching to the left line of players
    /// for the right team.
    /// </summary>
    public const string RIGHT_TEAM_SWITCH_TO_LEFT_LINE_BUTTON_NAME = "RightTeamHorizontalLeft";
    /// <summary>
    /// The name of the input button for switching to the right line of players
    /// for the right team.
    /// </summary>
    public const string RIGHT_TEAM_SWITCH_TO_RIGHT_LINE_BUTTON_NAME = "RightTeamHorizontalRight";

    /// <summary>
    /// The name of the input button for switching to a line of players
    /// to the left of the currently controlled line.
    /// </summary>
    public string SwitchToLeftLineButtonName = "";

    /// <summary>
    /// The name of the input button for switching to a line of players
    /// to the right of the currently controlled line.
    /// </summary>
    public string SwitchToRightLineButtonName = "";

    /// <summary>
    /// The team controlled by this controller.
    /// </summary>
    private FieldTeam m_team;

    /// <summary>
    /// Initializes the controller to know about necessary game objects.
    /// This method is intended to mimic a constructor.  An explicit
    /// initialize method was chosen over implementing the standard
    /// Start() method to better control when this component gets
    /// initialized.
    /// </summary>
    /// <param name="team">The team controlled by this controller.</param>
    /// <param name="switchToLeftLineButtonName">The name of the input button for
    /// switching to a line of players to the left of the currently controlled line.</param>
    /// <param name="switchToRightLineButtonName">The name of the input button for
    /// switching to a line of players to the right of the currently controlled line.</param>
    public void Initialize(
        FieldTeam team,
        string switchToLeftLineButtonName,
        string switchToRightLineButtonName)
    {
        m_team = team;
        SwitchToLeftLineButtonName = switchToLeftLineButtonName;
        SwitchToRightLineButtonName = switchToRightLineButtonName;
    }

    /// <summary>
    /// Handles user input to switch the field team to have
    /// the appropriate line of players controlled.
    /// </summary>
    public void HandleUserInput()
    {
        if (Input.GetButtonDown(SwitchToLeftLineButtonName))
        {
            m_team.SwitchToLeftLineOfPlayers();
        }
        else if (Input.GetButtonDown(SwitchToRightLineButtonName))
        {
            m_team.SwitchToRightLineOfPlayers();
        }
    }
}
