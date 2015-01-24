using UnityEngine;
using UnityEngine.Events;
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
    /// Retrieves the vaue of the color component
    /// controlled by this slider.
    /// </summary>
    public float ComponentValue
    {
        get
        {
            return ComponentSlider.value;
        }
    }

	/// <summary>
	/// Fills the attached slider with the configured color.
	/// </summary>
	private void Start()
    {
        // Fill the slider with the configured color.
        ComponentSlider.fillRect.GetComponentInChildren<Image>().color = SliderColor;

        // Initialize the label with the initial prefix text and color value.
        UpdateLabelWithColorValue();

        // ADJUST ADDITIONAL PROPERTIES OF THE LABEL.
        // These would normally be modified via the inspector.  However, due to the way
        // that prefabs are nested, modifying the original prefab for this script doesn't
        // appear to automatically updated nested versions of this prefab inside other
        // prefabs, so the modifications are done via code to avoid wasting time trying
        // to re-create the appropriate prefabs.

        // The font size is increased because the previous font size was too small for the web player.
        Label.fontSize = 12;

        // When the font size is adjusted, the veritical positioning of the label isn't well-aligned
        // with the color slider, so it must be manually fixed here.
        Label.rectTransform.position = new Vector3(
            Label.rectTransform.position.x,
            ComponentSlider.GetComponent<RectTransform>().position.y,//.rectTransform.position.y,
            Label.rectTransform.position.z);
	}
	
    /// <summary>
    /// Updates the label with the updated color value selected in the slider.
    /// </summary>
	public void UpdateLabelWithColorValue()
    {
        // Display the color value in the label with up to 2 decimal places.
        Label.text = LabelTextPrefix + ComponentSlider.value.ToString("0.00");
    }

    /// <summary>
    /// Adds a change listener to this selector for when its component
    /// value changes.
    /// </summary>
    /// <param name="changeListener">The listener function to call
    /// when the color component value changes.</param>
    public void AddOnValueChangedListener(UnityAction<float> changeListener)
    {
        ComponentSlider.onValueChanged.AddListener(changeListener);
    }
}
