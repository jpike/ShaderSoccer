using UnityEngine;

/// <summary>
/// A GUI control for configuring the specular shader.
/// This class encapsulates the underlying GUI controls
/// needed to configure the shader, allowing it to present
/// a simplified interface to receive the shader's configuration.
/// </summary>
public class SpecularShaderConfigGui : MaterialConfiguration
{
    /// <summary>
    /// The material for the specular shader.
    /// </summary>
    public Material SpecularShaderMaterial = null;

    /// <summary>
    /// The selector for the diffuse color.
    /// </summary>
    private ColorSelector m_diffuseColorSelector = null;

    /// <summary>
    /// The selector for the specular color.
    /// </summary>
    private ColorSelector m_specularColorSelector = null;

    /// <summary>
    /// The selector for the specular power.
    /// </summary>
    private SpecularPowerSelector m_specularPowerSelector = null;

    /// <summary>
    /// The example game object that gets updated with new shader
    /// settings configured in this GUI control.
    /// </summary>
    private PlayerExampleGameObject m_exampleGameObject = null;

    /// <summary>
    /// Initializes the configuration GUI to have
    /// access to the selectors for the shader and 
    /// knowledge about the example game object.
    /// </summary>
    /// <param name="exampleGameObject">The example game object
    /// to update with new specular shader settings configured
    /// in this control.</param>
    public void Initialize(GameObject exampleGameObject)
    {
        // STORE THE EXAMPLE GAME OBJECT.
        m_exampleGameObject = exampleGameObject.GetComponent<PlayerExampleGameObject>();

        // CLONE THE ATTACHED MATERIAL SO THAT WE DON'T MODIFY THE UNDERLYING MATERIAL ASSET.
        SpecularShaderMaterial = new Material(SpecularShaderMaterial);

        // REGISTER WITH THE DIFFUSE COLOR SELECTOR TO BE NOTIFIED WHEN THE COLOR CHANGES.
        m_diffuseColorSelector = transform.Find("DiffuseColorSelector").GetComponent<ColorSelector>();
        m_diffuseColorSelector.Initialize();
        m_diffuseColorSelector.AddOnColorChangedListener(UpdateDisplayedDiffuseColor);

        // REGISTER WITH THE SPECULAR COLOR SELECTOR TO BE NOTIFIED WHEN THE COLOR CHANGES.
        m_specularColorSelector = transform.Find("SpecularColorSelector").GetComponent<ColorSelector>();
        m_specularColorSelector.Initialize();
        m_specularColorSelector.AddOnColorChangedListener(UpdateDisplayedSpecularColor);

        // REGISTER WITH THE SPECULAR POWER SELECTOR TO BE NOTIFIED WHEN THE POWER CHANGES.
        m_specularPowerSelector = transform.Find("SpecularPowerSelector").GetComponent<SpecularPowerSelector>();
        m_specularPowerSelector.AddOnPowerChangedListener(UpdateDisplayedSpecularPower);

        // INITIALIZE THE EXAMPLE GAME OBJECT WITH THE CURRENT SELECTED SHADER SETTINGS.
        UpdateDisplayedDiffuseColor(m_diffuseColorSelector.Color);
        UpdateDisplayedSpecularColor(m_specularColorSelector.Color);
        UpdateDisplayedSpecularPower(m_specularPowerSelector.SpecularPower);
    }

    /// <summary>
    /// Updates the diffuse color displayed in the example object based
    /// the provided color.
    /// </summary>
    /// <param name="color">The new diffuse color for the example object.</param>
    public void UpdateDisplayedDiffuseColor(Color color)
    {
        // UPDATE THE DIFFUSE COLOR IN THE MATERIAL.
        SpecularShaderMaterial.SetColor("_DiffuseColor", color);
        m_exampleGameObject.SetMaterial(SpecularShaderMaterial);
    }

    /// <summary>
    /// Updates the specular color displayed in the example object based
    /// the provided color.
    /// </summary>
    /// <param name="color">The new specular color for the example object.</param>
    public void UpdateDisplayedSpecularColor(Color color)
    {
        // UPDATE THE SPECULAR COLOR IN THE MATERIAL.
        SpecularShaderMaterial.SetColor("_SpecularColor", color);
        m_exampleGameObject.SetMaterial(SpecularShaderMaterial);
    }

    /// <summary>
    /// Updates the specular power in the shader for the displayed example object.
    /// </summary>
    /// <param name="power">The new specular power for the example object.</param>
    public void UpdateDisplayedSpecularPower(float power)
    {
        // UPDATE THE SPECULAR POWER IN THE MATERIAL.
        SpecularShaderMaterial.SetFloat("_SpecularPower", power);
        m_exampleGameObject.SetMaterial(SpecularShaderMaterial);
    }

    /// <summary>
    /// Creates a copy of the configured specular shader material.
    /// </summary>
    /// <returns>The configured specular shader material.</returns>
    public override Material CreateMaterial()
    {
        return new Material(SpecularShaderMaterial);
    }
}
