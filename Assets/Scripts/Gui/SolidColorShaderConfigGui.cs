using UnityEngine;

/// <summary>
/// A GUI control for configuring the solid color shader.
/// This class encapsulates the underlying GUI controls
/// needed to configure the shader, allowing it to present
/// a simplified interface to receive the shader's configuration.
/// </summary>
public class SolidColorShaderConfigGui : MonoBehaviour
{
    /// <summary>
    /// The selector for the solid color.
    /// </summary>
    private ColorSelector m_colorSelector = null;

    /// <summary>
    /// The material for the solid color shader.
    /// </summary>
    private Material m_solidColorShaderMaterial = null;

    /// <summary>
    /// The example game object that gets updated with new shader
    /// settings configured in this GUI control.
    /// </summary>
    private GameObject m_exampleGameObject = null;

    /// <summary>
    /// Initializes the configuration GUI to have
    /// access to the selector of the solid color
    /// for the shader and knowledge about the provided
    /// parameters.
    /// </summary>
    /// <param name="solidColorShaderMaterial">The material
    /// for the solid color shader.</param>
    /// <param name="exampleGameObject">The example game object
    /// to update with new solid color settings configured
    /// in this control.</param>
    public void Initialize(
        Material solidColorShaderMaterial,
        GameObject exampleGameObject)
    {
        // STORE THE PROVIDED PARAMETERS.
        m_solidColorShaderMaterial = solidColorShaderMaterial;
        m_exampleGameObject = exampleGameObject;

        // REGISTER WITH THE COLOR SELECTOR TO BE NOTIFIED WHEN THE COLOR CHANGES.
        m_colorSelector = transform.Find("ColorSelector").GetComponent<ColorSelector>();
        m_colorSelector.AddOnColorChangedListener(UpdateDisplayedColor);
    }

    /// <summary>
    /// Updates the color displayed in the example object based
    /// the provided color.
    /// </summary>
    /// <param name="color">The new solid color for the example object.</param>
    public void UpdateDisplayedColor(Color color)
    {
        // UPDATE THE SOLID COLOR IN THE MATERIAL.
        m_solidColorShaderMaterial.SetColor("_Color", color);
        m_exampleGameObject.renderer.material = m_solidColorShaderMaterial;
    }
}
