using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A single vertical line of players in the game field.
/// This class encapsulates control and the bounding box
/// for a group of players in a single line.  Any number
/// of field players may be included in the line.
/// </summary>
public class FieldPlayerLine : MonoBehaviour
{
    /// <summary>
    /// Allows enabling or disabling of control of the line of players.
    /// </summary>
    public bool ControlEnabled { get; set; }

    /// <summary>
    /// Defines the type of controller for movement of the player.
    /// If human control, a human controller script should be attached.
    /// If computer control, a computer controller script should be attached.
    /// </summary>
    public PlayerMovementControlType MovementControllerType = PlayerMovementControlType.HUMAN_CONTROL;

    /// <summary>
    /// The world boundaries of the line of players.
    /// The bounds are stored as their own field because the bounds directly
    /// attached to a collider cannot be modified as desired.
    /// It is a public variable, rather than a property, to make it easier
    /// to modify center parts of the bounds.
    /// </summary>
    public Bounds Bounds;

    /// <summary>
    /// All of the field players included in this line.
    /// </summary>
    private List<FieldPlayer> m_fieldPlayers;

    /// <summary>
    /// The movement controller to use for human user input, if this line
    /// is controlled by a human.
    /// </summary>
    private HumanFieldPlayerLineController m_humanController;

    /// <summary>
    /// The movement controller to use for computer AI, if this line
    /// is controlled by the computer.
    /// </summary>
    private ComputerFieldPlayerLineController m_computerController;

    /// <summary>
    /// Initializes the line of players.
    /// This method is intended to mimic a constructor.  An explicit
    /// initialize method was chosen over implementing the standard
    /// Start() method to better control when this component gets
    /// initialized.
    /// </summary>
    /// <param name="fieldPlayers">All of the field players to include in the line.</param>
    /// <param name="humanController">The controller for the line to control movement based
    /// on human user input.  Required if the line is to be controlled by a human.</param>
    /// <param name="computerController">The controlller for the line to control movement based
    /// on computer AI.  Required if the line is to be controlled by the computer.</param>
    public void Initialize(
        List<FieldPlayer> fieldPlayers,
        HumanFieldPlayerLineController humanController,
        ComputerFieldPlayerLineController computerController)
    {
        // STORE THE FIELD PLAYERS.
        m_fieldPlayers = fieldPlayers;

        // STORE THE CONTROLLERS.
        m_humanController = humanController;
        m_computerController = computerController;

        // RESIZE THE BOUNDARIES OF THE LINE BASED ON THE ATTACHED PLAYERS.
        ResizeBounds();
    }

    /// <summary>
    /// Calculates the bounding box of the line based on the attached players.
    /// </summary>
    private void ResizeBounds()
    {
        // FIND THE MINIMUM AND MAXIMUM COORDINATES FOR THE BOUNDING BOX OF ALL ATTACHED PLAYERS.
        // Unity's Bounds class does not seem to work as expected when trying to grow it to encapsulate
        // other Bounds.  In particular, we cannot initialize an instance of it with a size of zero
        // and then gradually grow it to include other bounds.  Therefore, the minimum and maximum
        // boundaries of the players must be separately determined in order to construct the bounds
        // with proper values.  Since trying to determine the minimum or maximum against a zero vector
        // may not work, the vectors are nullable to allow detecting if they haven't been set yet.
        Vector3? boundingBoxMinimum = null;
        Vector3? boundingBoxMaximum = null;
        foreach (FieldPlayer currentPlayer in m_fieldPlayers)
        {
            // RETRIEVE THE BOUNDS FOR THE CURRENT PLAYER.
            Bounds playerBounds = currentPlayer.Bounds;

            // UPDATE THE MINIMUM BOUNDING BOX COORDINATES.
            if (!boundingBoxMinimum.HasValue)
            {
                // The minimum hasn't been set yet, so use the current bounds minimum.
                boundingBoxMinimum = playerBounds.min;
            }
            else
            {
                boundingBoxMinimum = Vector3.Min(boundingBoxMinimum.Value, playerBounds.min);
            }

            // UPDATE THE MAXIMUM BOUNDING BOX COORDINATES.
            if (!boundingBoxMaximum.HasValue)
            {
                // The maximum hasn't been set yet, so use the current bounds maximum.
                boundingBoxMaximum = playerBounds.max;
            }
            else
            {
                boundingBoxMaximum = Vector3.Max(boundingBoxMaximum.Value, playerBounds.max);
            }
        }

        // CALCULATE THE SIZE OF THE BOUNDING BOX.
        Vector3 size = boundingBoxMaximum.Value - boundingBoxMinimum.Value;

        // CREATE THE PROPER BOUNDING BOX FOR THE LINE OF PLAYERS.
        Bounds = new Bounds(transform.position, size);
    }

    /// <summary>
    /// Updates the line of players to move based on the configured controller.
    /// </summary>
    private void Update()
    {
        // UPDATES THE CENTER OF THE BOUNDING BOX.
        // The line of players may have moved since last update.
        Bounds.center = transform.position;

        // CHECK IF MOVEMENT CONTROL IS ENABLED.
        if (!ControlEnabled)
        {
            // No movement needs to be performed.
            return;
        }

        // MOVE BASED ON THE CONTROLLER TYPE.
        switch (MovementControllerType)
        {
            case PlayerMovementControlType.HUMAN_CONTROL:
                m_humanController.MoveBasedOnInput();
                break;
            case PlayerMovementControlType.COMPUTER_CONTROL:
                m_computerController.MoveBasedOnAi();
                break;
            default:
                // The controller type is invalid, so don't do anything.
                break;
        }
    }
}
