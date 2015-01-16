using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A GUI control that allows users to select
/// components of an RGBA color.
/// </summary>
public class ColorSelector : MonoBehaviour
{
    /// <summary>
    /// The selector for the red color component.
    /// </summary>
    private ColorComponentSelector m_redSelector = null;
    /// <summary>
    /// The selector for the green color component.
    /// </summary>
    private ColorComponentSelector m_greenSelector = null;
    /// <summary>
    /// The selector for the blue color component.
    /// </summary>
    private ColorComponentSelector m_blueSelector = null;
    /// <summary>
    /// The selector for the alpha color component.
    /// </summary>
    private ColorComponentSelector m_alphaSelector = null;

    /// <summary>
    /// Callback functions to notify when the color in the selector changes.
    /// </summary>
    private List<UnityAction<Color>> m_onColorChangedListeners = new List<UnityAction<Color>>();

    /// <summary>
    /// Retrieves the color currently in this selector.
    /// </summary>
    public Color Color
    {
        get
        {
            Color color = new Color(
                m_redSelector.ComponentValue,
                m_greenSelector.ComponentValue,
                m_blueSelector.ComponentValue,
                m_alphaSelector.ComponentValue);

            return color;
        }
    }

    /// <summary>
    /// Initializes the color selector to have
    /// knowledge about all of the child selectors
    /// for each component of an RGBA color.
    /// </summary>
    private void Start()
    {
        // GET ALL OF THE CHILD COLOR COMPONENT SELECTORS.
        m_redSelector = transform.Find("RedColorSelector").GetComponent<ColorComponentSelector>();
        m_greenSelector = transform.Find("GreenColorSelector").GetComponent<ColorComponentSelector>();
        m_blueSelector = transform.Find("BlueColorSelector").GetComponent<ColorComponentSelector>();
        m_alphaSelector = transform.Find("AlphaColorSelector").GetComponent<ColorComponentSelector>();

        // REGISTER WITH THE COLOR COMPONENT SELECTORS TO BE NOTIFIED WHEN THEIR VALUES CHANGE.
        m_redSelector.AddOnValueChangedListener(OnColorChanged);
        m_greenSelector.AddOnValueChangedListener(OnColorChanged);
        m_blueSelector.AddOnValueChangedListener(OnColorChanged);
        m_alphaSelector.AddOnValueChangedListener(OnColorChanged);
    }

    /// <summary>
    /// A callback function to be called whenever an individual component of
    /// the color changes.  This callback allows propagating changes upward
    /// to other objects that have registered with this selector to receive
    /// notifications of when the color changes.
    /// </summary>
    /// <param name="colorComponentValue">The color component value that changed.
    /// The exact color component is unknown, so this parameter is ignored.</param>
    public void OnColorChanged(float colorComponentValue)
    {
        // NOTIFY ALL REGISTERED LISTENERS OF THE CHANGED COLOR.
        foreach (UnityAction<Color> onColorChanged in m_onColorChangedListeners)
        {
            onColorChanged(Color);
        }
    }

    /// <summary>
    /// Adds an event listener to be notifed when the color in this selector changes.
    /// </summary>
    /// <param name="onColorChanged">The event listener to be notified.</param>
    public void AddOnColorChangedListener(UnityAction<Color> onColorChanged)
    {
        m_onColorChangedListeners.Add(onColorChanged);
    }
}
