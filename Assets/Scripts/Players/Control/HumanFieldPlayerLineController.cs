using UnityEngine;

/// <summary>
/// A controller for moving a line of field players based on human user input.
/// It does not contain any update code itself - rather, another script should call public
/// methods in this class as desired.
/// </summary>
public class HumanFieldPlayerLineController : MonoBehaviour
{
    /// <summary>
    /// The name of the input axis for vertically moving a line of players on the left team.
    /// </summary>
    public const string LEFT_TEAM_VERTICAL_INPUT_AXIS_NAME = "LeftPlayerVertical";
    /// <summary>
    /// The name of the input axis for vertically moving a line of players on the right team.
    /// </summary>
    public const string RIGHT_TEAM_VERTICAL_INPUT_AXIS_NAME = "RightPlayerVertical";

    /// <summary>
    /// The name of the vertical input axis for moving the field player line.
    /// </summary>
    public string VerticalInputAxisName = "";

    /// <summary>
    /// The vertical speed at which the field player line moves.
    /// Units are Unity units (meters) per second.
    /// </summary>
    public float VerticalMoveSpeedInMetersPerSecond = 5.0f;

    /// <summary>
    /// The line of players controlled by this human user input controller.
    /// </summary>
    private FieldPlayerLine m_fieldPlayerLine = null;

    /// <summary>
    /// The field in which the game is played.
    /// </summary>
    private PlayField m_playField = null;

    /// <summary>
    /// Initializes the controller to know about necessary game objects.
    /// This method is intended to mimic a constructor.  An explicit
    /// initialize method was chosen over implementing the standard
    /// Start() method to better control when this component gets
    /// initialized.
    /// </summary>
    /// <param name="fieldPlayerLine">The line of players controlled by this controller.</param>
    /// <param name="playField">The playing field where the game is being played.</param>
    /// <param name="verticalInputAxisName">The name of the vertical input axis for moving the field player line.</param>
    public void Initialize(
        FieldPlayerLine fieldPlayerLine,
        PlayField playField,
        string verticalInputAxisName)
    {
        m_fieldPlayerLine = fieldPlayerLine;
        m_playField = playField;
        VerticalInputAxisName = verticalInputAxisName;
    }

    /// <summary>
    /// Moves the field player in response to user input.
    /// Intended to be called once per frame.
    /// </summary>
    public void MoveBasedOnInput()
    {
        // CALCULATE THE VERTICALLY MOVEMENT BASED ON USER INPUT AND TIME.
        float verticalAxisInput = Input.GetAxis(VerticalInputAxisName);
        float elapsedTimeInSeconds = Time.deltaTime;

        float verticalMovementInMeters = verticalAxisInput * VerticalMoveSpeedInMetersPerSecond * elapsedTimeInSeconds;
        Vector3 verticalMovement = verticalMovementInMeters * Vector3.up;

        // RESTRICT THE NEW POSITION FOR THE LINE OF PLAYERS TO THE PLAYING FIELD.
        Vector3 newPosition = ConfinePositionToPlayingField(verticalMovement);

        transform.position = newPosition;
    }

    /// <summary>
    /// Confines the position of the line of players to the playing field,
    /// taking into account the provided vertical movement and playing
    /// field boundaries.
    /// </summary>
    /// <param name="verticalMovement">The vertical movement to add to the
    /// current line position to determine the final position to confine
    /// to the playing field.</param>
    /// <returns>The new world position of the line of field players,
    /// after being moved by the provided amount but restricted to the
    /// boundaries of the playing field.</returns>
    /// @todo   Consider refactoring to move this to the FieldPlayerLine
    /// class because it this logic is currently duplicated across both
    /// this and the computer controller classes.
    private Vector3 ConfinePositionToPlayingField(Vector3 verticalMovement)
    {
        // GET THE MINIMUM AND MAXIMUM VALID POSITIONS FOR THE PLAYER LINE.
        float minValidCenterYPosition = m_playField.Bounds.min.y + m_fieldPlayerLine.Bounds.extents.y;
        float maxValidCenterYPosition = m_playField.Bounds.max.y - m_fieldPlayerLine.Bounds.extents.y;

        // TAKE THE VERTICAL MOVEMENT INTO ACCOUNT FOR THE PLAYER LINE'S POSITION.
        Vector3 newLinePosition = m_fieldPlayerLine.transform.position + verticalMovement;

        // RESTRICT THE NEW POSITION TO THE VALID RANGE.
        newLinePosition.y = Mathf.Clamp(newLinePosition.y, minValidCenterYPosition, maxValidCenterYPosition);
        return newLinePosition;
    }
}
