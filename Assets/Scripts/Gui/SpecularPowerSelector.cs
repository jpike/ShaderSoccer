using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// A GUI control for selecting the specular power for
/// a specular highlight shader.
/// </summary>
public class SpecularPowerSelector : MonoBehaviour
{
    /// <summary>
    /// The GUI slider that allows a user to select the specular power.
    /// </summary>
    public Slider PowerSlider = null;

    /// <summary>
    /// The label to which to display the specular power
    /// value as it is changed by the user.
    /// </summary>
    public Text Label = null;

    /// <summary>
    /// A textual prefix to include before the specular power
    /// value in the label.
    /// </summary>
    public string LabelTextPrefix = null;

    /// <summary>
    /// Retrieves the specular power value
    /// controlled by this slider.
    /// </summary>
    public float SpecularPower
    {
        get
        {
            return PowerSlider.value;
        }
    }

    /// <summary>
    /// Initializes the slider.
    /// </summary>
    private void Start()
    {
        // Initialize the label with the initial prefix text and color value.
        UpdateLabelWithPowerValue();

        // The font size is increased because the previous font size was too small for the web player.
        Label.fontSize = 12;

        // Set the minimum and maximum values for the slider.
        // The values are chosen to limit the range to values that work well.
        PowerSlider.minValue = 1.0f;
        PowerSlider.maxValue = 8.0f;
    }

    /// <summary>
    /// Updates the label with the updated power value selected in the slider.
    /// </summary>
    public void UpdateLabelWithPowerValue()
    {
        // Display the power value in the label.  It is limited to 2 decimal places to avoid
        // distracting shifting of the label text.
        Label.text = LabelTextPrefix + SpecularPower.ToString("0.00");
    }

    /// <summary>
    /// Adds an event listener to be notifed when the power in this selector changes.
    /// </summary>
    /// <param name="onPowerChanged">The event listener to be notified.</param>
    public void AddOnPowerChangedListener(UnityAction<float> onPowerChanged)
    {
        PowerSlider.onValueChanged.AddListener(onPowerChanged);
    }
}
