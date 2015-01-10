using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script to handle GUI code for displaying appropriate
/// contents in the shader configuration panel.
/// </summary>
public class ShaderConfigurationPanel : MonoBehaviour
{
    /// <summary>
    /// The example game object that gets updated with new shader
    /// settings configured in this panel.
    /// </summary>
    public GameObject ExampleGameObject = null;

    /// <summary>
    /// The current shader configuration GUI prefab
    /// displayed in the panel.
    /// </summary>
    public GameObject CurrentShaderConfigGui = null;

    /// <summary>
    /// The toggle button for the solid color shader.
    /// </summary>
    public Toggle SolidColorShaderToggle = null;
    /// <summary>
    /// The prefab for the configuration GUI for the solid color shader.
    /// </summary>
    public GameObject SolidColorShaderConfigGuiPrefab = null;
    /// <summary>
    /// The material for the solid color shader.
    /// </summary>
    public Material SolidColorShaderMaterial = null;

    /// <summary>
    /// Displays the configuration settings for the solid
    /// color shader in the configuration panel, if the
    /// toggle is on.
    /// </summary>
	public void DisplaySolidColorShaderConfiguration()
    {
        Debug.Log("DisplaySolidColorShaderConfiguration");

        // CHECK IF THE SOLID COLOR SHADER TOGGLE WAS ENABLED.
        if (!SolidColorShaderToggle.isOn)
        {
            // There is nothing to do.  It is the responsibility
            // of other code in this class to clear the configuration
            // panel of any leftover GUI components.
            return;
        }

        // CLEAR THE SHADER CONFIGURATION PANEL.
        Destroy(CurrentShaderConfigGui);

        // POPULATE THE SOLID COLOR SHADER CONFIGURATION IN THE PANEL.
        CurrentShaderConfigGui = Instantiate(SolidColorShaderConfigGuiPrefab) as GameObject;
        CurrentShaderConfigGui.transform.SetParent(this.transform);
        CurrentShaderConfigGui.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        CurrentShaderConfigGui.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

        // UPDATE THE EXAMPLE GAME OBJECT TO USE THE SOLID COLOR SHADER.
        ExampleGameObject.renderer.material = SolidColorShaderMaterial;
    }
	
	
}
