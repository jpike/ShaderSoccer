using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A GUI control for selecting a single component of
/// an RGBA color.  It is only intended to allow selection
/// of color values between 0.0 and 1.0.
/// </summary>
public class ColorComponentSelector : MonoBehaviour
{
    /// <summary>
    /// The GUI slider that allows a user to select the color value.
    /// Should only allow values between 0.0 and 1.0.
    /// </summary>
    public Slider ComponentSlider = null;

    /// <summary>
    /// The fill color to use for the slider.  Can help users
    /// more easily identify what color component they are changing.
    /// </summary>
    public Color SliderColor;

    /// <summary>
    /// The label to which to display the color component
    /// value as it is changed by the user.
    /// </summary>
    public Text Label = null;

    /// <summary>
    /// A textual prefix to include before the updated color
    /// value in the label.  For example, "Red: " could be
    /// provided to help indicate that the slider is for
    /// selecting red color components.
    /// </summary>
    public string LabelTextPrefix = null;

	/// <summary>
	/// Fills the attached slider with the configured color.
	/// </summary>
	private void Start()
    {
        // Fill the slider with the configured color.
        ComponentSlider.fillRect.GetComponentInChildren<Image>().color = SliderColor;

        // Initialize the label with the initial prefix text and color value.
        UpdateLabelWithColorValue();
	}
	
    /// <summary>
    /// Updates the label with the updated color value selected in the slider.
    /// </summary>
	public void UpdateLabelWithColorValue()
    {
        // Display the color value in the label with up to 2 decimal places.
        Label.text = LabelTextPrefix + ComponentSlider.value.ToString("0.00");
    }
}
