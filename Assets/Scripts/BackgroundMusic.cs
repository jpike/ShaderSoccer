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
    /// The minimum volume for the music.  When fading out,
    /// the music will fade out to this volume.
    /// </summary>
    public float MinVolume = 0.0f;

    /// <summary>
    /// The maximum volume for the music.  When fading in,
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
    /// If the music is currently being faded in.
    /// </summary>
    private bool m_isFadingIn = false;

    /// <summary>
    /// If the music is currently being faded out.
    /// </summary>
    private bool m_isFadingOut = false;

    /// <summary>
    /// Starts playing the background music.
    /// </summary>
    public void StartPlaying()
    {
        // Only start playing the music if it isn't already playing.
        // We don't want duplicate instances of the music playing.
        if (!Music.isPlaying)
        {
            Music.Play();
        }
    }

    /// <summary>
    /// Stops playing the background music.
    /// </summary>
    public void StopPlaying()
    {
        Music.Stop();
    }

    /// <summary>
    /// Starts fading in the background music over time.
    /// </summary>
    public void StartFadeIn()
    {
        // Make sure that fading in and out don't interfere
        // with each other.
        m_isFadingOut = false;

        // Even if the music is already fading in, toggle the
        // flag indicating that it is fading in to allow it
        // to continue to fade in if necessary.  Otherwise,
        // a scenario could occur where the background music
        // stops entirely.
        m_isFadingIn = true;
    }

    /// <summary>
    /// Starts fading out the background music over time.
    /// </summary>
    public void StartFadeOut()
    {
        // Make sure that fading in and out don't interfere
        // with each other.
        m_isFadingIn = false;

        // Start fading out the music.  This flag will
        // trigger the update loop to fade out the
        // music volume and then ultimately pause
        // the music.
        m_isFadingOut = true;
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
            m_isFadingIn = false;
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
            // so go ahead and stop fading out.
            m_isFadingOut = false;

            return;
        }

        // DECREASE THE VOLUME A BIT BASED ON TIME.
        Music.volume -= VolumeDecreasePerSecond * Time.deltaTime;
    }

    /// <summary>
    /// Updates the volume of the music depending on
    /// if playing of the music is fading in or out.
    /// </summary>
    private void Update()
    {
        if (m_isFadingIn)
        {
            FadeIn();
        }
        else if (m_isFadingOut)
        {
            FadeOut();
        }
    }
}
