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
    /// The toggle button for the diffuse shader.
    /// </summary>
    public Toggle DiffuseShaderToggle = null;
    /// <summary>
    /// The prefab for the configuration GUI for the diffuse shader.
    /// </summary>
    public GameObject DiffuseShaderConfigGuiPrefab = null;

    /// <summary>
    /// The toggle button for the specular shader.
    /// </summary>
    public Toggle SpecularShaderToggle = null;
    /// <summary>
    /// The prefab for the configuration GUI for the specular shader.
    /// </summary>
    public GameObject SpecularShaderConfigGuiPrefab = null;

    /// <summary>
    /// Retrieves the material currently configured for the team.
    /// </summary>
    /// <returns>The material configured for the team.</returns>
    public Material GetMaterial()
    {
        // RETRIEVE THE MATERIAL FROM THE CURRENT SHADER CONFIGURATION GUI.
        MaterialConfiguration materialConfiguration = CurrentShaderConfigGui.GetComponent<MaterialConfiguration>();
        return materialConfiguration.CreateMaterial();
    }

    /// <summary>
    /// Displays the configuration settings for the solid
    /// color shader in the configuration panel, if the
    /// toggle is on.
    /// </summary>
	public void DisplaySolidColorShaderConfiguration()
    {
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
        /// @todo   Research and document why these need to be set.
        CurrentShaderConfigGui.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        CurrentShaderConfigGui.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

        // INITIALIZE THE SOLID COLOR SHADER CONFIGURATION GUI.
        CurrentShaderConfigGui.GetComponent<SolidColorShaderConfigGui>().Initialize(
            ExampleGameObject);
    }

    /// <summary>
    /// Displays the configuration settings for the diffuse
    /// shader in the configuration panel, if the
    /// toggle is on.
    /// </summary>
    public void DisplayDiffuseShaderConfiguration()
    {
        // CHECK IF THE DIFFUSE COLOR SHADER TOGGLE WAS ENABLED.
        if (!DiffuseShaderToggle.isOn)
        {
            // There is nothing to do.  It is the responsibility
            // of other code in this class to clear the configuration
            // panel of any leftover GUI components.
            return;
        }

        // CLEAR THE SHADER CONFIGURATION PANEL.
        Destroy(CurrentShaderConfigGui);

        // POPULATE THE DIFFUSE SHADER CONFIGURATION IN THE PANEL.
        CurrentShaderConfigGui = Instantiate(DiffuseShaderConfigGuiPrefab) as GameObject;
        CurrentShaderConfigGui.transform.SetParent(this.transform);
        /// @todo   Research and document why these need to be set.
        CurrentShaderConfigGui.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        CurrentShaderConfigGui.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

        // INITIALIZE THE DIFFUSE SHADER CONFIGURATION GUI.
        CurrentShaderConfigGui.GetComponent<DiffuseShaderConfigGui>().Initialize(
            ExampleGameObject);
    }

    /// <summary>
    /// Displays the configuration settings for the specular
    /// shader in the configuration panel, if the
    /// toggle is on.
    /// </summary>
    public void DisplaySpecularShaderConfiguration()
    {
        // CHECK IF THE SPECULAR COLOR SHADER TOGGLE WAS ENABLED.
        if (!SpecularShaderToggle.isOn)
        {
            // There is nothing to do.  It is the responsibility
            // of other code in this class to clear the configuration
            // panel of any leftover GUI components.
            return;
        }

        // CLEAR THE SHADER CONFIGURATION PANEL.
        Destroy(CurrentShaderConfigGui);

        // POPULATE THE DIFFUSE SHADER CONFIGURATION IN THE PANEL.
        CurrentShaderConfigGui = Instantiate(SpecularShaderConfigGuiPrefab) as GameObject;
        CurrentShaderConfigGui.transform.SetParent(this.transform);
        /// @todo   Research and document why these need to be set.
        CurrentShaderConfigGui.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        CurrentShaderConfigGui.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

        // INITIALIZE THE SPECULAR SHADER CONFIGURATION GUI.
        CurrentShaderConfigGui.GetComponent<SpecularShaderConfigGui>().Initialize(
            ExampleGameObject);
    }
}
