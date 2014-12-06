using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// An entire team of players in the main gameplay field.
/// This class manages switching of control across different
/// lines of players for a team.
/// </summary>
public class FieldTeam : MonoBehaviour
{
    /// <summary>
    /// The name for the game object for the team on the left side of the playing field.
    /// </summary>
    public const string LEFT_TEAM_OBJECT_NAME = "LeftTeam";
    /// <summary>
    /// The name for the game object for the team on the right side of the playing field.
    /// </summary>
    public const string RIGHT_TEAM_OBJECT_NAME = "RightTeam";

    /// <summary>
    /// How movement is controlled for the team.
    /// </summary>
    private PlayerMovementControlType m_movementControlType = PlayerMovementControlType.HUMAN_CONTROL;

    /// <summary>
    /// The line of field players currently being controlled.
    /// </summary>
    private int m_currentPlayerLineIndex = 0;

    /// <summary>
    /// The lines of field players in the team.
    /// Sorted by increasing X position.
    /// </summary>
    private List<FieldPlayerLine> m_fieldPlayerLines = null;

    /// <summary>
    /// The controller for controlling the team based on human user input.
    /// </summary>
    private HumanFieldTeamController m_humanController = null;

    /// <summary>
    /// The controller for controlling the team based on computer AI.
    /// </summary>
    private ComputerFieldTeamController m_computerController = null;

    /// <summary>
    /// The minimum valid player line index.
    /// </summary>
    private int MinPlayerLineIndex
    {
        get
        {
            return 0;
        }
    }

    /// <summary>
    /// The maximum valid player line index.
    /// </summary>
    private int MaxPlayerLineIndex
    {
        get
        {
            return m_fieldPlayerLines.Count - 1;
        }
    }

    /// <summary>
    /// Initializes the controller to know about necessary game objects.
    /// This method is intended to mimic a constructor.  An explicit
    /// initialize method was chosen over implementing the standard
    /// Start() method to better control when this component gets
    /// initialized.
    /// </summary>
    /// <param name="fieldPlayerLines">The lines of field players to include in the team.</param>
    /// <param name="humanController">The human controller for the team.
    /// Required if human user control is used.</param>
    /// <param name="computerController">The computer controller for the team.
    /// Required if computer AI control is used.</param>
    /// <param name="startingActivePlayerLineIndex">The starting 0-based index for the
    /// line of players that is currently being controlled for the team.  0 represents
    /// the leftmost line, and increasing indices go to lines further on the right.</param>
    public void Initialize(
        List<FieldPlayerLine> fieldPlayerLines,
        HumanFieldTeamController humanController,
        ComputerFieldTeamController computerController,
        int startingActivePlayerLineIndex)
    {
        // SORT THE PLAYERS BASED ON X POSITION.
        // Sorting based on X is needed to properly determine which lines are left and right.
        m_fieldPlayerLines = fieldPlayerLines;
        m_fieldPlayerLines = m_fieldPlayerLines.OrderBy(playerLine => playerLine.transform.position.x).ToList();

        // Set the index for the currently active line of players.
        m_currentPlayerLineIndex = startingActivePlayerLineIndex;

        // STORE THE CONTROLLERS.
        m_humanController = humanController;
        m_computerController = computerController;
    }

    /// <summary>
    /// Retrieves the currently active field player line.
    /// </summary>
    /// <returns>The current field player line.</returns>
    public FieldPlayerLine GetCurrentFieldPlayerLine()
    {
        return m_fieldPlayerLines[m_currentPlayerLineIndex];
    }

    /// <summary>
    /// Switches to make the line of players to the left of the current line
    /// to be the active line that may be moved.
    /// </summary>
    public void SwitchToLeftLineOfPlayers()
    {
        // SWITCH TO THE LEFT PLAYER LINE INDEX.
        // It needs to be clamped to ensure it is still valid.
        --m_currentPlayerLineIndex;
        m_currentPlayerLineIndex = Mathf.Clamp(m_currentPlayerLineIndex, MinPlayerLineIndex, MaxPlayerLineIndex);

        // SWITCH MOVEMENT CONTROL TO APPLY TO ONLY THE CURRENT LINE OF PLAYERS.
        EnableControlOnlyOfCurrentLine();
    }

    /// <summary>
    /// Switches to make the line of players to the right of the current line
    /// to be the active line that may be moved.
    /// </summary>
    public void SwitchToRightLineOfPlayers()
    {
        // SWITCH TO THE RIGHT PLAYER LINE INDEX.
        // It needs to be clamped to ensure it is still valid.
        ++m_currentPlayerLineIndex;
        m_currentPlayerLineIndex = Mathf.Clamp(m_currentPlayerLineIndex, MinPlayerLineIndex, MaxPlayerLineIndex);

        // SWITCH MOVEMENT CONTROL TO APPLY TO ONLY THE CURRENT LINE OF PLAYERS.
        EnableControlOnlyOfCurrentLine();
    }

    /// <summary>
    /// Switches the team to be controlled by human user input.
    /// </summary>
    public void UseHumanControl()
    {
        // UPDATE THE MOVEMENT CONTROL TYPE FOR THE OVERALL TEAM.
        m_movementControlType = PlayerMovementControlType.HUMAN_CONTROL;
        
        // UPDATE THE MOVEMENT CONTROL TYPE FOR INDIVIDUAL LINES OF PLAYERS.
        foreach (FieldPlayerLine playerLine in m_fieldPlayerLines)
        {
            playerLine.MovementControllerType = PlayerMovementControlType.HUMAN_CONTROL;
        }
    }

    /// <summary>
    /// Switches the team to be controlled by computer AI.
    /// </summary>
    public void UseComputerControl()
    {
        // UPDATE THE MOVEMENT CONTROL TYPE FOR THE OVERALL TEAM.
        m_movementControlType = PlayerMovementControlType.COMPUTER_CONTROL;

        // UPDATE THE MOVEMENT CONTROL TYPE FOR INDIVIDUAL LINES OF PLAYERS.
        foreach (FieldPlayerLine playerLine in m_fieldPlayerLines)
        {
            playerLine.MovementControllerType = PlayerMovementControlType.COMPUTER_CONTROL;
        }
    }

    /// <summary>
    /// Updates the field team to potentially switch active player lines
    /// based on the type of control configured for the team.
    /// </summary>
    private void Update()
    {
        // MOVE THE TEAM BASED ON THE MOVEMENT CONTROLLER TYPE.
        switch (m_movementControlType)
        {
            case PlayerMovementControlType.HUMAN_CONTROL:
                m_humanController.HandleUserInput();
                break;
            case PlayerMovementControlType.COMPUTER_CONTROL:
                m_computerController.SwitchPlayerLineBasedOnAi();
                break;
        }
    }

    /// <summary>
    /// Enables control only for the currently active line of players,
    /// disabling control for other lines.
    /// </summary>
    private void EnableControlOnlyOfCurrentLine()
    {
        // DISABLE CONTROL FOR ALL LINES.
        foreach (FieldPlayerLine playerLine in m_fieldPlayerLines)
        {
            playerLine.ControlEnabled = false;
        }

        // ENABLE CONTROL ONLY FOR THE CURRENT LINE.
        m_fieldPlayerLines[m_currentPlayerLineIndex].ControlEnabled = true;
    }
}
