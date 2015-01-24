using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// A GUI toggle button for switching the main camera from using
/// orthographic or perspective projections.  If toggled on,
/// perspective projection is used.  If toggled off, orthographic
/// projection is used.
/// </summary>
public class CameraProjectionToggle : MonoBehaviour
{
    /// <summary>
    /// The toggle button that indicates perspective projection should
    /// be used when the toggle is on.
    /// </summary>
    public Toggle PerspectiveWhenOnToggle = null;

    /// <summary>
    /// Toggles the camera's projection depending on whether the toggle is on.
    /// If on, perspective is used.  If off, orthographic is used.
    /// </summary>
    public void ToggleCameraProjection()
    {
        Camera.main.orthographic = !PerspectiveWhenOnToggle.isOn;
    }
}
