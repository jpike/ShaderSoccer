using System;
using UnityEngine;

/// <summary>
/// Responsible for monitoring the game and determining the winner.
/// Static variables exist to allow data about which team won
/// to persist from the gameplay scene to the winning scene.
/// </summary>
public class Referee : MonoBehaviour
{
    /// <summary>
    /// Defines the different possible winners of a game.
    /// </summary>
    public enum WinnerType
    {
        /// <summary>The game was a tie.</summary>
        TIE,
        /// <summary>The left team won.</summary>
        LEFT_TEAM,
        /// <summary>The right team won.</summary>
        RIGHT_TEAM
    }

    /// <summary>
    /// The winner as currently determined by the referee.
    /// </summary>
    public static WinnerType Winner;
    /// <summary>
    /// The left team's score, as determined at the end of the match.
    /// </summary>
    public static int LeftTeamScore;
    /// <summary>
    /// The right team's score, as determined at the end of the match.
    /// </summary>
    public static int RightTeamScore;

    /// <summary>
    /// Holds the scores that the referee monitors.
    /// </summary>
    private Scoreboard m_scoreboard;
    /// <summary>
    /// Holds the clock that the referee monitors.
    /// </summary>
    private Clock m_clock;

	/// <summary>
	/// Resets the winner of the game and finds game objects
    /// needed by the referee.
	/// </summary>
	void Start()
    {
	    // RESET THE CURRENT WINNER AND THE SCORES.
        // A new match is beginning, so no winner exists yet.
        Winner = WinnerType.TIE;
        LeftTeamScore = 0;
        RightTeamScore = 0;

        // FIND OBJECTS THAT THE REFEREE NEEDS TO MONITOR.
        m_scoreboard = GameObject.FindObjectOfType<Scoreboard>();
        m_clock = GameObject.FindObjectOfType<Clock>();
	}
	
	/// <summary>
	/// Checks the current clock to determine if the game is over,
    /// and checks the scoreboard to determine which team won.
	/// </summary>
	void Update()
    {
	    // CHECK IF THE CLOCK HAS RUN OUT OF TIME.
        // The match ends when the clock reaches zero.  To avoid issues
        // with comparing doubles for equality, the clock's time is
        // converted to an integer.
        int clockTimeInSeconds = Convert.ToInt32(m_clock.CurrentTimeInSeconds);
        bool matchOver = (clockTimeInSeconds <= 0);
        if (!matchOver)
        {
            // The match isn't over, so there is nothing more to do.
            return;
        }

        // CHECK WHICH TEAM WON THE MATCH.
        // The team with the most points won.  The static variables are populated
        // since they will need to be passed to the winning screen scene.
        LeftTeamScore = m_scoreboard.LeftTeamScore;
        RightTeamScore = m_scoreboard.RightTeamScore;
        bool leftTeamWon = (LeftTeamScore > RightTeamScore);
        bool rightTeamWon = (RightTeamScore > LeftTeamScore);
        if (leftTeamWon)
        {
            Winner = WinnerType.LEFT_TEAM;
        }
        else if (rightTeamWon)
        {
            Winner = WinnerType.RIGHT_TEAM;
        }
        else
        {
            Winner = WinnerType.TIE;
        }

        // SWITCH TO THE WINNER SCREEN.
        Application.LoadLevel("WinnerScreenScene");
	}
}
