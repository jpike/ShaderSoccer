using System.Collections.Generic;
using UnityEngine;

/// A factory for constructing all components needed for the
/// right team in the playing field.  This handcoded logic
/// was chosen because it was easier to understand and
/// maintain than trying to hook up everything via
/// Unity's inspector.
public class RightTeamFactory : MonoBehaviour 
{
    /// <summary>
    /// The prefab for the player line containing the goalie.
    /// </summary>
    public GameObject GoalieLinePrefab = null;
    /// <summary>
    /// The prefab for the player line containing midfielders.
    /// </summary>
    public GameObject MidfielderLinePrefab = null;
    /// <summary>
    /// The prefab for the player line containing forwards.
    /// </summary>
    public GameObject ForwardLinePrefab = null;
    /// <summary>
    /// The prefab for the GUI toggle button for controlling
    /// whether the team is controlled by human user input
    /// or computer AI.
    /// </summary>
    public GameObject ControlToggleButtonPrefab = null;
    /// <summary>
    /// The prefab for showing which line of players in the team is
    /// currently active.
    /// </summary>
    public GameObject ActivePlayerLineVisualIndicatorPrefab = null;

    /// <summary>
    /// The material to use for shading the players in the team.
    /// It is static to allow passing the material from the game setup
    /// screen to the main gameplay screen.
    /// </summary>
    public static Material TeamMaterial = null;

    /// <summary>
    /// The world position of the goalie line for the team.
    /// </summary>
    private readonly Vector3 RIGHT_TEAM_GOALIE_LINE_POSITION = new Vector3(6.0f, 0.0f, 0.0f);
    /// <summary>
    /// The world position of the midfielder line for the team.
    /// </summary>
    private readonly Vector3 RIGHT_TEAM_MIDFIELDER_LINE_POSITION = new Vector3(4.0f, 0.0f, 0.0f);
    /// <summary>
    /// The world position of the forward line for the team.
    /// </summary>
    private readonly Vector3 RIGHT_TEAM_FORWARD_LINE_POSITION = new Vector3(2.0f, 0.0f, 0.0f);
    /// <summary>
    /// The world position of the center of the team.
    /// </summary>
    private readonly Vector3 RIGHT_TEAM_POSITION = new Vector3(4.0f, 0.0f, 0.0f);
    /// <summary>
    /// The world position of the control toggle button.
    /// </summary>
    private readonly Vector3 RIGHT_TEAM_CONTROL_TOGGLE_BUTTON_POSITION = new Vector3(8.0f, 4.0f, 0.0f);

    /// <summary>
    /// Creates the left team in the playing field.
    /// </summary>
	private void Start() 
    {
        // FIND OBJECTS NEEDED BY MULTIPLE OBJECTS IN THE RIGHT TEAM.
        PlayField playField = GameObject.FindObjectOfType<PlayField>();
        Ball ball = GameObject.FindObjectOfType<Ball>();

        // CREATE THE ROOT RIGHT TEAM OBJECT.
        GameObject rightTeam = new GameObject(FieldTeam.RIGHT_TEAM_OBJECT_NAME);
        rightTeam.transform.position = RIGHT_TEAM_POSITION;
        rightTeam.AddComponent<FieldTeam>();

        // CREATE THE RIGHT TEAM'S ROOT HUMAN CONTROLLER.
        rightTeam.AddComponent<HumanFieldTeamController>().Initialize(
            rightTeam.GetComponent<FieldTeam>(),
            HumanFieldTeamController.RIGHT_TEAM_SWITCH_TO_LEFT_LINE_BUTTON_NAME,
            HumanFieldTeamController.RIGHT_TEAM_SWITCH_TO_RIGHT_LINE_BUTTON_NAME);

        // CREATE THE RIGHT TEAM'S ROOT COMPUTER CONTROLLER.
        rightTeam.AddComponent<ComputerFieldTeamController>().Initialize(
            rightTeam.GetComponent<FieldTeam>(),
            ball);

        // CREATE THE FIELD PLAYER LINES.
        List<FieldPlayerLine> fieldPlayerLines = CreateFieldPlayerLines(
            rightTeam.transform,
            playField,
            ball);

        // CREATE THE VISUAL INDICATOR FOR WHICH LINE IS ACTIVE.
        GameObject activePlayerLineIndicator = Instantiate(ActivePlayerLineVisualIndicatorPrefab) as GameObject;
        activePlayerLineIndicator.renderer.material = new Material(TeamMaterial);

        // FINISH INITIALIZING THE RIGHT TEAM.
        int goalieLineIndex = fieldPlayerLines.Count - 1;
        rightTeam.GetComponent<FieldTeam>().Initialize(
            fieldPlayerLines,
            rightTeam.GetComponent<HumanFieldTeamController>(),
            rightTeam.GetComponent<ComputerFieldTeamController>(),
            goalieLineIndex,
            activePlayerLineIndicator);

        // UPDATE THE MATERIAL FOR THE TEAM TO THE CONFIGURED MATERIAL.
        Renderer[] teamPlayerRenderers = rightTeam.GetComponentsInChildren<Renderer>();
        foreach (Renderer playerRenderer in teamPlayerRenderers)
        {
            playerRenderer.material = TeamMaterial;
        }

        // CREATE THE TOGGLE BUTTON TO CONTROL WHETHER THE TEAM IS CONTROLLED BY HUMAN INPUT OR CPU AI.
        GameObject controlToggleButton = Instantiate(ControlToggleButtonPrefab, RIGHT_TEAM_CONTROL_TOGGLE_BUTTON_POSITION, Quaternion.identity) as GameObject;
        controlToggleButton.GetComponent<PlayerControlToggleButton>().Team = rightTeam.GetComponent<FieldTeam>();
        controlToggleButton.GetComponent<PlayerControlToggleButton>().RightAligned = true;
	}

    /// <summary>
    /// Creates the field player lines for the right team.
    /// </summary>
    /// <param name="rightTeamTransform">The parent transform of the entire right team.</param>
    /// <param name="playField">The playing field in which to create the player lines.</param>
    /// <param name="ball">The ball in the playing field.</param>
    /// <returns>The field player lines for the right team.</returns>
    private List<FieldPlayerLine> CreateFieldPlayerLines(
        Transform rightTeamTransform,
        PlayField playField,
        Ball ball)
    {
        // CREATE THE GOALIE LINE.
        GameObject goalieLine = Instantiate(GoalieLinePrefab, RIGHT_TEAM_GOALIE_LINE_POSITION, Quaternion.identity) as GameObject;
        InitializeGoalieLine(
            rightTeamTransform,
            playField,
            ball,
            goalieLine);

        // CREATE THE MIDFIELDER LINE.
        GameObject midfielderLine = Instantiate(MidfielderLinePrefab, RIGHT_TEAM_MIDFIELDER_LINE_POSITION, Quaternion.identity) as GameObject;
        InitializeMidfielderLine(
            rightTeamTransform,
            playField,
            ball,
            midfielderLine);

        // CREATE THE FORWARD LINE.
        GameObject forwardLine = Instantiate(ForwardLinePrefab, RIGHT_TEAM_FORWARD_LINE_POSITION, Quaternion.identity) as GameObject;
        InitializeForwardLine(
            rightTeamTransform,
            playField,
            ball,
            forwardLine);

        // ADD THE FIELD PLAYER LINES TO A LIST.
        List<FieldPlayerLine> fieldPlayerLines = new List<FieldPlayerLine>();

        // Player lines are added in left-to-right order so that the lowest
        // index in the list refers to the left-most line, which helps ensure
        // that switching between lines works as expected.
        fieldPlayerLines.Add(forwardLine.GetComponent<FieldPlayerLine>());
        fieldPlayerLines.Add(midfielderLine.GetComponent<FieldPlayerLine>());
        fieldPlayerLines.Add(goalieLine.GetComponent<FieldPlayerLine>());

        return fieldPlayerLines;
    }

    /// <summary>
    /// Initialize the goalie the field player line for the right team.
    /// </summary>
    /// <param name="rightTeamTransform">The parent transform of the entire right team.</param>
    /// <param name="playField">The playing field in which to create the player lines.</param>
    /// <param name="ball">The ball in the playing field.</param>
    /// <param name="goalieLine">The goalie line to initialize.</param>
    private void InitializeGoalieLine(
        Transform rightTeamTransform,
        PlayField playField,
        Ball ball,
        GameObject goalieLine)
    {
        // Set the parent of the goalie line so that it moves if the entire team moves.
        goalieLine.transform.parent = rightTeamTransform;

        // The goalie line starts out being controlled.
        goalieLine.GetComponent<FieldPlayerLine>().ControlEnabled = true;

        // Create the human controller for the goalie line.
        goalieLine.AddComponent<HumanFieldPlayerLineController>().Initialize(
            goalieLine.GetComponent<FieldPlayerLine>(),
            playField,
            HumanFieldPlayerLineController.RIGHT_TEAM_VERTICAL_INPUT_AXIS_NAME);

        // Create the computer controller for the goalie line.
        goalieLine.AddComponent<ComputerFieldPlayerLineController>().Initialize(
            goalieLine.GetComponent<FieldPlayerLine>(),
            ball,
            playField);

        // Finish initializing the goalie line.
        goalieLine.GetComponent<FieldPlayerLine>().Initialize(
            new List<FieldPlayer>(goalieLine.GetComponentsInChildren<FieldPlayer>()),
            goalieLine.GetComponent<HumanFieldPlayerLineController>(),
            goalieLine.GetComponent<ComputerFieldPlayerLineController>());
    }

    /// <summary>
    /// Initialize the midfielder the field player line for the right team.
    /// </summary>
    /// <param name="rightTeamTransform">The parent transform of the entire right team.</param>
    /// <param name="playField">The playing field in which to create the player lines.</param>
    /// <param name="ball">The ball in the playing field.</param>
    /// <param name="midfielderLine">The midfielder line to initialize.</param>
    private void InitializeMidfielderLine(
        Transform rightTeamTransform,
        PlayField playField,
        Ball ball,
        GameObject midfielderLine)
    {
        // Set the parent of the player line so that it moves if the entire team moves.
        midfielderLine.transform.parent = rightTeamTransform;

        // The midfielder line does not start out being controlled.
        midfielderLine.GetComponent<FieldPlayerLine>().ControlEnabled = false;

        // Create the human controller for the midfielder line.
        midfielderLine.AddComponent<HumanFieldPlayerLineController>().Initialize(
            midfielderLine.GetComponent<FieldPlayerLine>(),
            playField,
            HumanFieldPlayerLineController.RIGHT_TEAM_VERTICAL_INPUT_AXIS_NAME);

        // Create the computer controller for the midfielder line.
        midfielderLine.AddComponent<ComputerFieldPlayerLineController>().Initialize(
            midfielderLine.GetComponent<FieldPlayerLine>(),
            ball,
            playField);

        // Finish initializing the midfielder line.
        midfielderLine.GetComponent<FieldPlayerLine>().Initialize(
            new List<FieldPlayer>(midfielderLine.GetComponentsInChildren<FieldPlayer>()),
            midfielderLine.GetComponent<HumanFieldPlayerLineController>(),
            midfielderLine.GetComponent<ComputerFieldPlayerLineController>());
    }

    /// <summary>
    /// Initialize the forward the field player line for the right team.
    /// </summary>
    /// <param name="rightTeamTransform">The parent transform of the entire right team.</param>
    /// <param name="playField">The playing field in which to create the player lines.</param>
    /// <param name="ball">The ball in the playing field.</param>
    /// <param name="forwardLine">The forward line to initialize.</param>
    private void InitializeForwardLine(
        Transform rightTeamTransform,
        PlayField playField,
        Ball ball,
        GameObject forwardLine)
    {
        // Set the parent of the player line so that it moves if the entire team moves.
        forwardLine.transform.parent = rightTeamTransform;

        // The forward line does not start out being controlled.
        forwardLine.GetComponent<FieldPlayerLine>().ControlEnabled = false;

        // Create the human controller for the forward line.
        forwardLine.AddComponent<HumanFieldPlayerLineController>().Initialize(
            forwardLine.GetComponent<FieldPlayerLine>(),
            playField,
            HumanFieldPlayerLineController.RIGHT_TEAM_VERTICAL_INPUT_AXIS_NAME);

        // Create the computer controller for the forward line.
        forwardLine.AddComponent<ComputerFieldPlayerLineController>().Initialize(
            forwardLine.GetComponent<FieldPlayerLine>(),
            ball,
            playField);

        // Finish initializing the forward line.
        forwardLine.GetComponent<FieldPlayerLine>().Initialize(
            new List<FieldPlayer>(forwardLine.GetComponentsInChildren<FieldPlayer>()),
            forwardLine.GetComponent<HumanFieldPlayerLineController>(),
            forwardLine.GetComponent<ComputerFieldPlayerLineController>());
    }
}
