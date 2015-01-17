using UnityEngine;

/// <summary>
/// A GUI control for configuring the diffuse shader.
/// This class encapsulates the underlying GUI controls
/// needed to configure the shader, allowing it to present
/// a simplified interface to receive the shader's configuration.
/// </summary>
public class DiffuseShaderConfigGui : MaterialConfiguration
{
    /// <summary>
    /// The material for the diffuse shader.
    /// </summary>
    public Material DiffuseShaderMaterial = null;

    /// <summary>
    /// The selector for the diffuse color.
    /// </summary>
    private ColorSelector m_colorSelector = null;

    /// <summary>
    /// The example game object that gets updated with new shader
    /// settings configured in this GUI control.
    /// </summary>
    private GameObject m_exampleGameObject = null;

    /// <summary>
    /// Initializes the configuration GUI to have
    /// access to the selector of the diffuse color
    /// for the shader and knowledge about the example
    /// game object.
    /// </summary>
    /// <param name="exampleGameObject">The example game object
    /// to update with new diffuse color settings configured
    /// in this control.</param>
    public void Initialize(GameObject exampleGameObject)
    {
        // STORE THE EXAMPLE GAME OBJECT.
        m_exampleGameObject = exampleGameObject;

        // CLONE THE ATTACHED MATERIAL SO THAT WE DON'T MODIFY THE UNDERLYING MATERIAL ASSET.
        DiffuseShaderMaterial = new Material(DiffuseShaderMaterial);

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
    /// <param name="color">The new diffuse color for the example object.</param>
    public void UpdateDisplayedColor(Color color)
    {
        // UPDATE THE DIFFUSE COLOR IN THE MATERIAL.
        DiffuseShaderMaterial.SetColor("_DiffuseColor", color);
        m_exampleGameObject.renderer.material = DiffuseShaderMaterial;
    }

    /// <summary>
    /// Creates a copy of the configured diffuse shader material.
    /// </summary>
    /// <returns>The configured diffuse shader material.</returns>
    public override Material CreateMaterial()
    {
        return new Material(DiffuseShaderMaterial);
    }
}
