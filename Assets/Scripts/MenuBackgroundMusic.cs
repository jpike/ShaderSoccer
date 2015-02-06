using UnityEngine;

/// <summary>
/// Encapsulates the background music for menu screens
/// in the game.  This script allows easier control
/// of stopping/starting the menu screen background
/// music.  This class is intended to act as a
/// singleton throughout the entire game.  This
/// approach was taken only because it was a quick
/// and simple way to propagate consistent background
/// music across the multiple scenes.
/// </summary>
public class MenuBackgroundMusic : MonoBehaviour
{
    /// <summary>
    /// The audio source for the background music.
    /// Public so that it can be set from Unity's editor.
    /// Note that if this script is used in multiple places,
    /// only one instance of this audio will actually get
    /// played since playing of the audio is statically
    /// controlled.
    /// </summary>
    public AudioSource Audio;

    /// <summary>
    /// Whether or not the music is in the process of
    /// being started.
    /// </summary>
    private bool m_isStarting = false;
    /// <summary>
    /// Whether or not the music is in the process of
    /// being stopped.
    /// </summary>
    private bool m_isStopping = false;

    /// <summary>
    /// The singleton instance of this class.
    /// </summary>
    private static MenuBackgroundMusic Music;

    /// <summary>
    /// Initializes the the singleton instance
    /// of this class.  If this method is called
    /// multiple times, it will not overwrite
    /// any previous instance and will destroy
    /// any new instances.
    /// </summary>
    private void Awake()
    {
        // CHECK IF THE SINGLETON MUSIC INSTANCE ALREADY EXISTS.
        bool musicAlreadyExists = (Music != null);
        if (musicAlreadyExists)
        {
            // Another instance of this game object was created.
            // Since only a single instance should exist, destroy
            // the new instance.
            Destroy(this.gameObject);
        }
        else
        {
            // Initialize the singleton instance of the music for
            // the first time and ensure it persists between scenes.
            Music = this;
            DontDestroyOnLoad(this.gameObject);
        }

        // MAKE SURE THE MUSIC IS PLAYING.
        StartMusic();
    }

    /// <summary>
    /// Updates the volume of the music depending on
    /// if playing of the music is starting or stopping.
    /// </summary>
    private void Update()
    {
        if (m_isStarting)
        {
            FadeMusicIn();
        }
        else if (m_isStopping)
        {
            FadeMusicOut();
        }
    }

    /// <summary>
    /// Fades the music in by increasing its volume.
    /// </summary>
    private void FadeMusicIn()
    {
        // CHECK IF THE VOLUME HAS REACHED ITS MAXIMUM LEVEL.
        // The max volume is less than the absolute maximum volume
        // for audio because we don't want the background music
        // for menus to be as loud as that for the gameplay.
        const float MAX_VOLUME = 0.75f;
        bool maxVolumeReached = (Audio.volume >= MAX_VOLUME);
        if (maxVolumeReached)
        {
            // This volume doesn't need to be increased any more.
            m_isStarting = false;
            return;
        }

        // INCREASE THE VOLUME A BIT BASED ON TIME.
        const float VOLUME_INCREASE_PER_SECOND = 0.5f;
        Audio.volume += VOLUME_INCREASE_PER_SECOND * Time.deltaTime;
    }

    /// <summary>
    /// Fades the music out by decreasing its volume.
    /// </summary>
    private void FadeMusicOut()
    {
        // CHECK IF THE VOLUME HAS REACHED ITS MINIMUM LEVEL.
        const float MIN_VOLUME = 0.1f;
        bool minVolumeReached = (Audio.volume <= MIN_VOLUME);
        if (minVolumeReached)
        {
            // This volume doesn't need to be decreased any more,
            // so go ahead and stop it.
            m_isStopping = false;
            Audio.Pause();
            return;
        }

        // DECREASE THE VOLUME A BIT BASED ON TIME.
        const float VOLUME_DECREASE_PER_SECOND = 0.5f;
        Audio.volume -= VOLUME_DECREASE_PER_SECOND * Time.deltaTime;
    }

    /// <summary>
    /// Starts playing the background music.
    /// </summary>
    public static void StartMusic()
    {
        // Only start playing the music if it isn't already playing.
        // We don't want duplicate instances of the music playing.
        if (!Music.Audio.isPlaying)
        {
            // Make sure that starting/stopping don't interfere
            // with each other.
            Music.m_isStopping = false;

            // Reset the audio volume to zero to start fading
            // it in as the music plays.
            Music.m_isStarting = true;
            Music.Audio.volume = 0.0f;
            Music.Audio.Play();
        }
    }

    /// <summary>
    /// Stops playing the background music.
    /// </summary>
    public static void StopMusic()
    {
        // Make sure that starting/stopping don't interfere
        // with each other.
        Music.m_isStarting = false;

        // Start stopping the music.  This flag will
        // trigger the update loop to fade out the
        // music volume and then ultimately pause
        // the music.
        Music.m_isStopping = true;
    }
}
