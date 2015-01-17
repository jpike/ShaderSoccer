using UnityEngine;

/// <summary>
/// The start button on the game setup screen.
/// This object encapsulates the behavior for clicking
/// on the start button and handles configuring the
/// game based on the parameters configured in the
/// game setup screen.
/// </summary>
public class GameSetupStartButton : MonoBehaviour
{
    /// <summary>
    /// The object to use to transition to the gameplay scene.
    /// </summary>
    public SceneLoader SceneLoader = null;

    /// <summary>
    /// Initializes and starts the main gameplay based on the
    /// options configured on this screen.
    /// </summary>
    public void StartGame()
    {
        // INITIALIZE THE LEFT TEAM.
        TeamConfigurationPanel leftTeamConfiguration = GameObject.Find("LeftTeamConfigurationPanel").GetComponent<TeamConfigurationPanel>();
        LeftTeamFactory.TeamMaterial = leftTeamConfiguration.GetMaterial();

        // INITIALIZE THE RIGHT TEAM.
        TeamConfigurationPanel rightTeamConfiguration = GameObject.Find("RightTeamConfigurationPanel").GetComponent<TeamConfigurationPanel>();
        RightTeamFactory.TeamMaterial = rightTeamConfiguration.GetMaterial();

        // START THE MAIN GAMEPLAY.
        SceneLoader.LoadScene("GameplayScene");
    }
}
