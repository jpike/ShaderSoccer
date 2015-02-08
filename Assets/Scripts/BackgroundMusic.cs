using UnityEngine;

/// <summary>
/// An instance of background music.  Supports
/// fading-in and fading-out.
/// </summary>
public class BackgroundMusic : MonoBehaviour 
{
    /// <summary>
    /// The actual audio to play for the background music.
    /// </summary>
    public AudioSource Music = null;

    /// <summary>
    /// The minimum volume for the music.  When starting,
    /// the music will start at this volume.  When stopping,
    /// the music will fade out to this volume.
    /// </summary>
    public float MinVolume = 0.1f;

    /// <summary>
    /// The maximum volume for the music.  When starting,
    /// the music will fade in to this volume.
    /// </summary>
    public float MaxVolume = 1.0f;

    /// <summary>
    /// How fast the volume increases when fading in.
    /// </summary>
    public float VolumeIncreasePerSecond = 0.5f;

    /// <summary>
    /// How fast the volume decreases when fading out.
    /// </summary>
    public float VolumeDecreasePerSecond = 0.5f;

    /// <summary>
    /// If the music is currently being started (fading in).
    /// </summary>
    private bool m_isStarting = false;

    /// <summary>
    /// If the music is currently being stopped (fading out).
    /// </summary>
    private bool m_isStopping = false;

    /// <summary>
    /// Starts playing the background music over time.
    /// </summary>
    public void StartPlaying()
    {
        // Make sure that starting/stopping don't interfere
        // with each other.
        m_isStopping = false;

        // Even if the music is already playing, toggle the
        // flag indicating that it is starting to allow it
        // to continue to fade in if necessary.  Otherwise,
        // a scenario could occur where the background music
        // stops entirely.
        m_isStarting = true;

        // Only start playing the music if it isn't already playing.
        // We don't want duplicate instances of the music playing.
        if (!Music.isPlaying)
        {
            // Reset the audio volume to zero to start fading
            // it in as the music plays.
            Music.volume = MinVolume;
            Music.Play();
        }
    }

    /// <summary>
    /// Stops playing the background music over time.
    /// </summary>
    public void StopPlaying()
    {
        // Make sure that starting/stopping don't interfere
        // with each other.
        m_isStarting = false;

        // Start stopping the music.  This flag will
        // trigger the update loop to fade out the
        // music volume and then ultimately pause
        // the music.
        m_isStopping = true;
    }

    /// <summary>
    /// Fades the music in by increasing its volume.
    /// </summary>
    private void FadeIn()
    {
        // CHECK IF THE VOLUME HAS REACHED ITS MAXIMUM LEVEL.
        bool maxVolumeReached = (Music.volume >= MaxVolume);
        if (maxVolumeReached)
        {
            // This volume doesn't need to be increased any more.
            m_isStarting = false;
            return;
        }

        // INCREASE THE VOLUME A BIT BASED ON TIME.
        Music.volume += VolumeIncreasePerSecond * Time.deltaTime;
    }

    /// <summary>
    /// Fades the music out by decreasing its volume.
    /// </summary>
    private void FadeOut()
    {
        // CHECK IF THE VOLUME HAS REACHED ITS MINIMUM LEVEL.
        bool minVolumeReached = (Music.volume <= MinVolume);
        if (minVolumeReached)
        {
            // This volume doesn't need to be decreased any more,
            // so go ahead and stop it.
            m_isStopping = false;
            Music.Pause();
            return;
        }

        // DECREASE THE VOLUME A BIT BASED ON TIME.
        Music.volume -= VolumeDecreasePerSecond * Time.deltaTime;
    }

    /// <summary>
    /// Updates the volume of the music depending on
    /// if playing of the music is starting or stopping.
    /// </summary>
    private void Update()
    {
        if (m_isStarting)
        {
            FadeIn();
        }
        else if (m_isStopping)
        {
            FadeOut();
        }
    }
}
