using System;
using UnityEngine;

/// <summary>
/// The clock that tracks and displays the time for a match in the game.
/// It may be configured to count upwards or downwards.
/// </summary>
public class Clock : MonoBehaviour
{
    #region Public Fields
    /// <summary>
    /// Whether or not the clock should count down.
    /// If false, the clock will count up.
    /// </summary>
    public bool CountDown = false;
    /// <summary>
    /// The starting time on the clock (in seconds).
    /// </summary>
    public float StartTimeInSeconds = 0.0f;
    /// <summary>
    /// The style of the clock GUI.
    /// </summary>
    public GUIStyle ClockStyle;
    #endregion

    #region Private Fields
    /// <summary>
    /// The current time displayed for the clock.  If counting up,
    /// this is the total elapsed time.  If counting down, this is
    /// the time remaining.
    /// </summary>
    private float m_currentTimeInSeconds = 0.0f;
    #endregion

    #region Initialization Methods
    /// <summary>
    /// Initializes the clock with its starting time.
    /// </summary>
	private void Start() 
    {
        m_currentTimeInSeconds = StartTimeInSeconds;
	}
    #endregion

    #region Update Methods
    /// <summary>
    /// Updates the time for the clock.
    /// </summary>
	private void Update()
    {
        // UPDATE THE CLOCK DEPENDING ON WHICH DIRECTION IT IS COUNTING.
        if (CountDown)
        {
            m_currentTimeInSeconds -= Time.deltaTime;

            // Make sure the current time doesn't go negative.
            const float MIN_TIME_IN_SECONDS = 0.0f;
            m_currentTimeInSeconds = Mathf.Max(MIN_TIME_IN_SECONDS, m_currentTimeInSeconds);
        }
        else
        {
            m_currentTimeInSeconds += Time.deltaTime;
        }
    }
    #endregion

    #region GUI Methods
    /// <summary>
    /// Displays the clock's current time.
    /// </summary>
    private void OnGUI()
    {
        // CREATE A MORE INTUITIVE DISPLAY OF THE CLOCK'S TIME.
        // It will be displayed in MM:SS format (2 digits each).  While we could theoretically
        // display higher units (like hours), this is likely unnecessary.  If
        // some plays a game that long, then display a 3-digit hour is acceptable.
        // Unity does not seem to currently have a version of the .NET framework
        // that supports TimeSpan format strings.
        const string TIME_DISPLAY_FORMAT_STRING = "{0:00}:{1:00}";
        TimeSpan currentClockTime = TimeSpan.FromSeconds(m_currentTimeInSeconds);
        string timeDisplayString = string.Format(
            TIME_DISPLAY_FORMAT_STRING,
            currentClockTime.Minutes,
            currentClockTime.Seconds);
        
        // CALCULATE THE BOUNDING RECTANGLE FOR THE CLOCK'S TIME.
        // The clock's width is a constant that should be wide enough to handle
        // expected minutes and seconds.
        int CLOCK_WIDTH = 128;
        int CLOCK_HALF_WIDTH = CLOCK_WIDTH / 2;
        // The clock should be centered on screen.
        int screen_half_width = Screen.width / 2;
        int clockLeftXPosition = (screen_half_width - CLOCK_HALF_WIDTH);
        // The clock's should appear at the top of the screen.
        int CLOCK_TOP_Y_POSITION = 0;
        // The clock's height is a constant that should be tall enough
        // to handle the height of text.
        int CLOCK_HEIGHT = 32;
        Rect clockBoundingRect = new Rect(
            clockLeftXPosition,
            CLOCK_TOP_Y_POSITION,
            CLOCK_WIDTH,
            CLOCK_HEIGHT);
        
        // DRAW THE CLOCK'S CURRENT TIME ON SCREEN.
        GUI.Label(clockBoundingRect, timeDisplayString, ClockStyle);
    }
    #endregion
}
