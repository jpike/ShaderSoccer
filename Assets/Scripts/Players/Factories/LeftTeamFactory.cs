using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A factory for constructing all components needed for the
/// left team in the playing field.  This handcoded logic
/// was chosen because it was easier to understand and
/// maintain than trying to hook up everything via
/// Unity's inspector.
/// </summary>
public class LeftTeamFactory : MonoBehaviour
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
    /// The material to use for shading the players in the team.
    /// It is static to allow passing the material from the game setup
    /// screen to the main gameplay screen.
    /// </summary>
    public static Material TeamMaterial = null;

    /// <summary>
    /// The world position of the goalie line for the team.
    /// </summary>
    private readonly Vector3 LEFT_TEAM_GOALIE_LINE_POSITION = new Vector3(-6.0f, 0.0f, 0.0f);
    /// <summary>
    /// The world position of the midfielder line for the team.
    /// </summary>
    private readonly Vector3 LEFT_TEAM_MIDFIELDER_LINE_POSITION = new Vector3(-4.0f, 0.0f, 0.0f);
    /// <summary>
    /// The world position of the forward line for the team.
    /// </summary>
    private readonly Vector3 LEFT_TEAM_FORWARD_LINE_POSITION = new Vector3(-2.0f, 0.0f, 0.0f);
    /// <summary>
    /// The world position of the center of the team.
    /// </summary>
    private readonly Vector3 LEFT_TEAM_POSITION = new Vector3(-4.0f, 0.0f, 0.0f);
    /// <summary>
    /// The world position of the control toggle button.
    /// </summary>
    private readonly Vector3 LEFT_TEAM_CONTROL_TOGGLE_BUTTON_POSITION = new Vector3(-8.0f, 4.0f, 0.0f);

    /// <summary>
    /// Creates the left team in the playing field.
    /// </summary>
    private void Start()
    {
        // FIND OBJECTS NEEDED BY MULTIPLE OBJECTS IN THE LEFT TEAM.
        PlayField playField = GameObject.FindObjectOfType<PlayField>();
        Ball ball = GameObject.FindObjectOfType<Ball>();

        // CREATE THE ROOT LEFT TEAM OBJECT.
        GameObject leftTeam = new GameObject(FieldTeam.LEFT_TEAM_OBJECT_NAME);
        leftTeam.transform.position = LEFT_TEAM_POSITION;
        leftTeam.AddComponent<FieldTeam>();

        // CREATE THE LEFT TEAM'S ROOT HUMAN CONTROLLER.
        leftTeam.AddComponent<HumanFieldTeamController>().Initialize(
            leftTeam.GetComponent<FieldTeam>(),
            HumanFieldTeamController.LEFT_TEAM_SWITCH_TO_LEFT_LINE_BUTTON_NAME,
            HumanFieldTeamController.LEFT_TEAM_SWITCH_TO_RIGHT_LINE_BUTTON_NAME);

        // CREATE THE LEFT TEAM'S ROOT COMPUTER CONTROLLER.
        leftTeam.AddComponent<ComputerFieldTeamController>().Initialize(
            leftTeam.GetComponent<FieldTeam>(),
            ball);
        
        // CREATE THE FIELD PLAYER LINES.
        List<FieldPlayerLine> fieldPlayerLines = CreateFieldPlayerLines(
            leftTeam.transform,
            playField,
            ball);

        // FINISH INITIALIZING THE LEFT TEAM.
        const int GOALIE_LINE_INDEX = 0;
        leftTeam.GetComponent<FieldTeam>().Initialize(
            fieldPlayerLines,
            leftTeam.GetComponent<HumanFieldTeamController>(),
            leftTeam.GetComponent<ComputerFieldTeamController>(),
            GOALIE_LINE_INDEX);

        // UPDATE THE MATERIAL FOR THE TEAM TO THE CONFIGURED MATERIAL.
        Renderer[] teamPlayerRenderers = leftTeam.GetComponentsInChildren<Renderer>();
        foreach (Renderer playerRenderer in teamPlayerRenderers)
        {
            playerRenderer.material = TeamMaterial;
        }

        // CREATE THE TOGGLE BUTTON TO CONTROL WHETHER THE TEAM IS CONTROLLED BY HUMAN INPUT OR CPU AI.
        GameObject controlToggleButton = Instantiate(ControlToggleButtonPrefab, LEFT_TEAM_CONTROL_TOGGLE_BUTTON_POSITION, Quaternion.identity) as GameObject;
        controlToggleButton.GetComponent<PlayerControlToggleButton>().Team = leftTeam.GetComponent<FieldTeam>();
    }

    /// <summary>
    /// Creates the field player lines for the left team.
    /// </summary>
    /// <param name="leftTeamTransform">The parent transform of the entire left team.</param>
    /// <param name="playField">The playing field in which to create the player lines.</param>
    /// <param name="ball">The ball in the playing field.</param>
    /// <returns>The field player lines for the left team.</returns>
    private List<FieldPlayerLine> CreateFieldPlayerLines(
        Transform leftTeamTransform,
        PlayField playField,
        Ball ball)
    {
        // CREATE THE GOALIE LINE.
        GameObject goalieLine = Instantiate(GoalieLinePrefab, LEFT_TEAM_GOALIE_LINE_POSITION, Quaternion.identity) as GameObject;
        InitializeGoalieLine(
            leftTeamTransform,
            playField,
            ball,
            goalieLine);

        // CREATE THE MIDFIELDER LINE.
        GameObject midfielderLine = Instantiate(MidfielderLinePrefab, LEFT_TEAM_MIDFIELDER_LINE_POSITION, Quaternion.identity) as GameObject;
        InitializeMidfielderLine(
            leftTeamTransform,
            playField,
            ball,
            midfielderLine);

        // CREATE THE FORWARD LINE.
        GameObject forwardLine = Instantiate(ForwardLinePrefab, LEFT_TEAM_FORWARD_LINE_POSITION, Quaternion.identity) as GameObject;
        InitializeForwardLine(
            leftTeamTransform,
            playField,
            ball,
            forwardLine);

        // ADD THE FIELD PLAYER LINES TO A LIST.
        List<FieldPlayerLine> fieldPlayerLines = new List<FieldPlayerLine>();

        fieldPlayerLines.Add(goalieLine.GetComponent<FieldPlayerLine>());
        fieldPlayerLines.Add(midfielderLine.GetComponent<FieldPlayerLine>());
        fieldPlayerLines.Add(forwardLine.GetComponent<FieldPlayerLine>());

        return fieldPlayerLines;
    }

    /// <summary>
    /// Initialize the goalie the field player line for the left team.
    /// </summary>
    /// <param name="leftTeamTransform">The parent transform of the entire left team.</param>
    /// <param name="playField">The playing field in which to create the player lines.</param>
    /// <param name="ball">The ball in the playing field.</param>
    /// <param name="goalieLine">The goalie line to initialize.</param>
    private void InitializeGoalieLine(
        Transform leftTeamTransform,
        PlayField playField,
        Ball ball,
        GameObject goalieLine)
    {
        // Set the parent of the goalie line so that it moves if the entire team moves.
        goalieLine.transform.parent = leftTeamTransform;

        // The goalie line starts out being controlled.
        goalieLine.GetComponent<FieldPlayerLine>().ControlEnabled = true;

        // Create the human controller for the goalie line.
        goalieLine.AddComponent<HumanFieldPlayerLineController>().Initialize(
            goalieLine.GetComponent<FieldPlayerLine>(),
            playField,
            HumanFieldPlayerLineController.LEFT_TEAM_VERTICAL_INPUT_AXIS_NAME);

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
    /// Initialize the midfielder the field player line for the left team.
    /// </summary>
    /// <param name="leftTeamTransform">The parent transform of the entire left team.</param>
    /// <param name="playField">The playing field in which to create the player lines.</param>
    /// <param name="ball">The ball in the playing field.</param>
    /// <param name="midfielderLine">The midfielder line to initialize.</param>
    private void InitializeMidfielderLine(
        Transform leftTeamTransform,
        PlayField playField,
        Ball ball,
        GameObject midfielderLine)
    {
        // Set the parent of the player line so that it moves if the entire team moves.
        midfielderLine.transform.parent = leftTeamTransform;

        // The midfielder line does not start out being controlled.
        midfielderLine.GetComponent<FieldPlayerLine>().ControlEnabled = false;

        // Create the human controller for the midfielder line.
        midfielderLine.AddComponent<HumanFieldPlayerLineController>().Initialize(
            midfielderLine.GetComponent<FieldPlayerLine>(),
            playField,
            HumanFieldPlayerLineController.LEFT_TEAM_VERTICAL_INPUT_AXIS_NAME);

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
    /// Initialize the forward the field player line for the left team.
    /// </summary>
    /// <param name="leftTeamTransform">The parent transform of the entire left team.</param>
    /// <param name="playField">The playing field in which to create the player lines.</param>
    /// <param name="ball">The ball in the playing field.</param>
    /// <param name="forwardLine">The forward line to initialize.</param>
    private void InitializeForwardLine(
        Transform leftTeamTransform,
        PlayField playField,
        Ball ball,
        GameObject forwardLine)
    {
        // Set the parent of the player line so that it moves if the entire team moves.
        forwardLine.transform.parent = leftTeamTransform;

        // The forward line does not start out being controlled.
        forwardLine.GetComponent<FieldPlayerLine>().ControlEnabled = false;

        // Create the human controller for the forward line.
        forwardLine.AddComponent<HumanFieldPlayerLineController>().Initialize(
            forwardLine.GetComponent<FieldPlayerLine>(),
            playField,
            HumanFieldPlayerLineController.LEFT_TEAM_VERTICAL_INPUT_AXIS_NAME);

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
