using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for initializing a team configuration panel
/// to its default state.
/// </summary>
public class TeamConfigurationPanel : MonoBehaviour
{
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

	    // SET THE DEFAULT SHADER.
        InitializeDefaultShader();
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
    /// Retrieves the material currently configured for the team.
    /// </summary>
    /// <returns>The material configured for the team.</returns>
    public Material GetMaterial()
    {
        return m_shaderConfigPanel.GetMaterial();
    }
}
