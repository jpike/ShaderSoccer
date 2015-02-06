using UnityEngine;

/// <summary>
/// Controls background music for the main gameplay scene.
/// This script mainly serves to create a fade-in effect
/// for the background music.
/// </summary>
public class GameplayBackgroundMusic : MonoBehaviour
{
    /// <summary>
    /// The background music controlled by this script.
    /// </summary>
    public AudioSource Music;

    /// <summary>
    /// Ensures that the menu background music is no longer playing.
    /// </summary>
    private void Awake()
    {
        // STOP THE MENU BACKGROUND MUSIC FROM PLAYING.
        // It shouldn't play while the music for the main gameplay is also playing.
        MenuBackgroundMusic.StopMusic();
    }

    /// <summary>
    /// Increases the volume of the background music
    /// until the maximum volume is reached.
    /// </summary>
	private void Update() 
    {
	    // CHECK IF THE VOLUME HAS REACHED ITS MAXIMUM LEVEL.
        const float MAX_VOLUME = 1.0f;
        bool maxVolumeReached = (Music.volume >= MAX_VOLUME);
        if (maxVolumeReached)
        {
            // This volume doesn't need to be increased any more.
            return;
        }

        // INCREASE THE VOLUME A BIT BASED ON TIME.
        const float VOLUME_INCREASE_PER_SECOND = 0.1f;
        Music.volume += VOLUME_INCREASE_PER_SECOND * Time.deltaTime;
	}
}
