using UnityEngine;

/// <summary>
/// A controller for moving a field player based on a human's user input.
/// It does not contain any update code itself - rather, another script
/// should call public methods in this class as necessary.
/// </summary>
public class HumanFieldPlayerController : MonoBehaviour
{
    #region Public Fields
    /// <summary>
    /// The name of the vertical input axis for moving the field player.
    /// </summary>
    public string VerticalInputAxisName = "";

    /// <summary>
    /// The vertical speed at which the field player moves.
    /// Units are Unity units (meters) per second.
    /// </summary>
    public float VerticalMoveSpeedInMetersPerSecond = 5.0f;
    #endregion

    #region Methods
    /// <summary>
    /// Moves the field player in response to user input.
    /// Intended to be called once per frame.
    /// </summary>
    public void MoveBasedOnInput()
    {
        // MOVE THE FIELD PLAYER VERTICALLY BASED ON USER INPUT.
        float verticalAxisInput = Input.GetAxis(VerticalInputAxisName);
        float elapsedTimeInSeconds = Time.deltaTime;

        float verticalMovementInMeters = verticalAxisInput * VerticalMoveSpeedInMetersPerSecond * elapsedTimeInSeconds;
        Vector3 verticalMovement = verticalMovementInMeters * Vector3.up;

        transform.Translate(verticalMovement);
    }
    #endregion
}
