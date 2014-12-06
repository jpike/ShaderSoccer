﻿using UnityEngine;

/// <summary>
/// Controls a fielded team of players based on computer artifical intelligence.
/// Whether it attempts to switch lines of players is determined based on some
/// thresholds (between 0 and 100).  If neither threshold is met, then no switching 
/// will occur.  Switching is also controlled in part by the position of the ball.
/// </summary>
public class ComputerFieldTeamController : MonoBehaviour
{
    /// <summary>
    /// The minimum threshold for which the computer AI will try and switch
    /// to to the line of players on the right.  If a random number generated
    /// is greater than or equal to this value, then the AI may attempt to
    /// switch to the right line of players.
    /// </summary>
    public int SwitchRightMinRandomThreshold = 60;

    /// <summary>
    /// The maximum threshold for which the computer AI will try and switch
    /// to to the line of players on the left.  If a random number generated
    /// is less than or equal to this value, then the AI may attempt to
    /// switch to the left line of players.
    /// </summary>
    public int SwitchLeftMaxRandomThreshold = 40; 

    /// <summary>
    /// The team controlled by this controller.
    /// </summary>
    private FieldTeam m_team;

    /// <summary>
    /// The ball currently in play.
    /// </summary>
    private Ball m_ball;

    /// <summary>
    /// Initializes the controller to know about necessary game objects.
    /// This method is intended to mimic a constructor.  An explicit
    /// initialize method was chosen over implementing the standard
    /// Start() method to better control when this component gets
    /// initialized.
    /// </summary>
    /// <param name="team">The team controlled by this script.</param>
    /// <param name="ball">The ball currently in play.</param>
    public void Initialize(FieldTeam team, Ball ball)
    {
        m_team = team;
        m_ball = ball;
    }

    /// <summary>
    /// Executes the AI algorithm to potentially switch the controlled
    /// team to potentially move a different line of players.
    /// </summary>
    public void SwitchPlayerLineBasedOnAi()
    {
        // GET THE CURRENT ACTIVE LINE OF PLAYERS FOR THE TEAM.
        FieldPlayerLine currentFieldPlayerLine = m_team.GetCurrentFieldPlayerLine();
        
        // CALCULATE A RANDOM CHANCE FOR SWITCHING TO THE LEFT OR RIGHT LINE OF PLAYERS.
        int randomSwitchChance = Random.Range(0, 100);

        // CHECK IF THE AI SHOULD ATTEMPT TO SWITCH TO THE LEFT LINE OF PLAYERS.
        bool switchLeftThresholdMet = (randomSwitchChance <= SwitchLeftMaxRandomThreshold);
        if (switchLeftThresholdMet)
        {
            // MAKE SURE THE BALL IS FURTHER TO THE LEFT THAN THE CURRENT LINE OF PLAYERS.
            // If the ball isn't to the left, then there isn't much of a strategic reason
            // to switch left, so we don't want to switch in that case.
            bool ballLeftOfCurrentPlayerLine = (m_ball.transform.position.x <= currentFieldPlayerLine.transform.position.x);
            if (ballLeftOfCurrentPlayerLine)
            {
                m_team.SwitchToLeftLineOfPlayers();
            }
        }

        // CHECK IF THE AI SHOULD ATTEMPT TO SWITCH TO THE RIGHT LINE OF PLAYERS.
        bool switchRightThresholdMet = (randomSwitchChance >= SwitchRightMinRandomThreshold);
        if (switchRightThresholdMet)
        {
            // MAKE SURE THE BALL IS FURTHER TO THE RIGHT THAN THE CURRENT LINE OF PLAYERS.
            // If the ball isn't to the right, then there isn't much of a strategic reason
            // to switch right, so we don't want to switch in that case.
            bool ballRightOfCurrentPlayerLine = (m_ball.transform.position.x >= currentFieldPlayerLine.transform.position.x);
            if (ballRightOfCurrentPlayerLine)
            {
                m_team.SwitchToRightLineOfPlayers();
            }
        }
    }
}
