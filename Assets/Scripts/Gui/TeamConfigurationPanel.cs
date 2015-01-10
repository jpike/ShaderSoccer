using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for initializing a team configuration panel
/// to its default state.
/// </summary>
public class TeamConfigurationPanel : MonoBehaviour
{
	/// <summary>
	/// Initializes the panel to its default state.
	/// </summary>
	private void Start()
    {
	    // SET THE DEFAULT SHADER.
        InitializeDefaultShader();
	}

    /// <summary>
    /// Initializes the default shader for the panel.
    /// </summary>
    private void InitializeDefaultShader()
    {
        // ENABLE THE SOLID COLOR SHADER AS THE INITIAL SELECTED ONE.
        // This should automatically switch out the shader configuration GUI components.
        
        Toggle solidColorShaderToggle = transform.Find("ShaderToggles/SolidColorShaderToggle").GetComponent<Toggle>();
        solidColorShaderToggle.isOn = true;
    }
}
