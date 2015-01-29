using UnityEngine;

/// <summary>
/// A GUI control for configuring the solid color shader.
/// This class encapsulates the underlying GUI controls
/// needed to configure the shader, allowing it to present
/// a simplified interface to receive the shader's configuration.
/// </summary>
public class SolidColorShaderConfigGui : MaterialConfiguration
{
    /// <summary>
    /// The material for the solid color shader.
    /// </summary>
    public Material SolidColorShaderMaterial = null;

    /// <summary>
    /// The selector for the solid color.
    /// </summary>
    private ColorSelector m_colorSelector = null;

    /// <summary>
    /// The example game object that gets updated with new shader
    /// settings configured in this GUI control.
    /// </summary>
    private PlayerExampleGameObject m_exampleGameObject = null;

    /// <summary>
    /// Initializes the configuration GUI to have
    /// access to the selector of the solid color
    /// for the shader and knowledge about the example
    /// game object.
    /// </summary>
    /// <param name="exampleGameObject">The example game object
    /// to update with new solid color settings configured
    /// in this control.</param>
    public void Initialize(GameObject exampleGameObject)
    {
        // STORE THE EXAMPLE GAME OBJECT.
        m_exampleGameObject = exampleGameObject.GetComponent<PlayerExampleGameObject>();

        // CLONE THE ATTACHED MATERIAL SO THAT WE DON'T MODIFY THE UNDERLYING MATERIAL ASSET.
        SolidColorShaderMaterial = new Material(SolidColorShaderMaterial);

        // REGISTER WITH THE COLOR SELECTOR TO BE NOTIFIED WHEN THE COLOR CHANGES.
        m_colorSelector = transform.Find("ColorSelector").GetComponent<ColorSelector>();
        m_colorSelector.Initialize();
        m_colorSelector.AddOnColorChangedListener(UpdateDisplayedColor);

        // INITIALIZE THE EXAMPLE GAME OBJECT WITH THE CURRENT SELECTED COLOR.
        UpdateDisplayedColor(m_colorSelector.Color);
    }

    /// <summary>
    /// Updates the color displayed in the example object based
    /// the provided color.
    /// </summary>
    /// <param name="color">The new solid color for the example object.</param>
    public void UpdateDisplayedColor(Color color)
    {
        // UPDATE THE SOLID COLOR IN THE MATERIAL.
        SolidColorShaderMaterial.SetColor("_Color", color);
        m_exampleGameObject.SetMaterial(SolidColorShaderMaterial);
    }

    /// <summary>
    /// Creates a copy of the configured solid color shader material.
    /// </summary>
    /// <returns>The configured solid color shader material.</returns>
    public override Material CreateMaterial()
    {
        return new Material(SolidColorShaderMaterial);
    }
}
