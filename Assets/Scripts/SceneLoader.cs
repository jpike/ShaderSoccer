using UnityEngine;

/// <summary>
/// Responsible for loading different scenes.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Loads the scene with the specified name.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public void LoadScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }
}
