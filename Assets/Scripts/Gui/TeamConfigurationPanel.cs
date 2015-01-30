using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for initializing a team configuration panel
/// to its default state.
/// </summary>
public class TeamConfigurationPanel : MonoBehaviour
{
    /// <summary>
    /// The example game object showcasing how players in the team
    /// will look after configuring them in this panel.
    /// </summary>
    public PlayerExampleGameObject ExampleGameObject = null;

    /// <summary>
    /// The child panel for configuring the shader for this team.
    /// </summary>
    private ShaderConfigurationPanel m_shaderConfigPanel = null;

	/// <summary>
	/// Initializes the panel to its default state.
	/// </summary>
	private void Start()
    {
        // FIND THE CHILD SHADER CONFIGURATION PANEL.
        m_shaderConfigPanel = transform.Find("ShaderConfigurationPanel").GetComponent<ShaderConfigurationPanel>();

	    // INITIALIZE THE CONFIGURATION PANEL.
        InitializeDefaultShader();
        InitializeDefaultModel();
	}

    /// <summary>
    /// Initializes the default shader for the panel.
    /// </summary>
    private void InitializeDefaultShader()
    {
        // ENABLE THE SOLID COLOR SHADER AS THE INITIAL SELECTED ONE.
        Toggle solidColorShaderToggle = transform.Find("ShaderToggles/SolidColorShaderToggle").GetComponent<Toggle>();
        solidColorShaderToggle.isOn = true;

        // INITIALIZE THE SOLID COLOR SHADER CONFIGURATION GUI.
        ShaderConfigurationPanel shaderConfigPanel = transform.Find("ShaderConfigurationPanel").GetComponent<ShaderConfigurationPanel>();
        shaderConfigPanel.DisplaySolidColorShaderConfiguration();
    }

    /// <summary>
    /// Initializes the default model for the panel.
    /// </summary>
    private void InitializeDefaultModel()
    {
        // ENABLE THE BOX MODEL AS THE INITIAL SELECTED ONE.
        Toggle boxModelToggle = transform.Find("ModelToggles/BoxModelToggle").GetComponent<Toggle>();
        boxModelToggle.isOn = true;

        // DISPLAY THE BOX MODEL FOR THE EXAMPLE GAME OBJECT.
        ExampleGameObject.DisplayBoxModel();
    }

    /// <summary>
    /// Retrieves the material currently configured for the team.
    /// </summary>
    /// <returns>The material configured for the team.</returns>
    public Material GetMaterial()
    {
        return m_shaderConfigPanel.GetMaterial();
    }
}
