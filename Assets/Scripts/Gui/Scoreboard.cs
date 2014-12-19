using UnityEngine;

/// <summary>
/// The scoreboard for the game.  Scores for each player are displayed.
/// </summary>
public class Scoreboard : MonoBehaviour
{
    #region Public Fields
    /// <summary>
    /// The style of the scoreboard GUI.
    /// </summary>
    public GUIStyle ScoreboardStyle;
    #endregion

    #region Private Fields
    /// <summary>
    /// The goal of the left team.
    /// </summary>
    private GoalArea LeftTeamGoal = null;
    /// <summary>
    /// The goal of the right time.
    /// </summary>
    private GoalArea RightTeamGoal = null;
    #endregion

    #region Properties
    /// <summary>
    /// Retrieves the left team's score.
    /// </summary>
    public int LeftTeamScore
    {
        get
        {
            // The left team's score is defined by the number of points
            // scored in the right team's goal.
            return RightTeamGoal.PointsScored;
        }
    }

    /// <summary>
    /// Retrieves the right team's score.
    /// </summary>
    public int RightTeamScore
    {
        get
        {
            // The right team's score is defined by the number of points
            // scored in the left team's goal.
            return LeftTeamGoal.PointsScored;
        }
    }
    #endregion

    #region Initialization Methods
    /// <summary>
    /// Initializes the scoreboard to have access to the goals of each team.
    /// </summary>
    private void Start()
    {
        // FIND THE GOAL AREAS.
        LeftTeamGoal = GameObject.Find(GoalArea.LEFT_GOAL_OBJECT_NAME).GetComponent<GoalArea>();
        RightTeamGoal = GameObject.Find(GoalArea.RIGHT_GOAL_OBJECT_NAME).GetComponent<GoalArea>();
    }
    #endregion

    #region GUI Methods
    /// <summary>
    /// Draws the GUI for the scoreboard.
    /// </summary>
    private void OnGUI()
    {
        // DEFINE COORDINATES AND DIMENSIONS USED TO POSITION THE SCORE'S OF ALL TEAMS.
        const int SCOREBOARD_TOP_Y_POSITION = 0;
        
        // The scores are drawn 1/3 from the edges of the screen.
        int third_of_screen_width = Screen.width / 3;

        // THe boxes for each score have an arbitrary but consistent size.
        const int SCORE_WIDTH = 64;
        const int SCORE_HEIGHT = 32;

        // DRAW THE LEFT TEAM'S SCORE.
        int leftTeamScoreLeftXPosition = third_of_screen_width;

        Rect leftTeamScoreBoundingRectangle = new Rect(
            leftTeamScoreLeftXPosition, 
            SCOREBOARD_TOP_Y_POSITION,
            SCORE_WIDTH,
            SCORE_HEIGHT);

        // The left team's score is tracked by the points scored in the right team's goal.
        string leftTeamScore = RightTeamGoal.PointsScored.ToString();
        GUI.Label(leftTeamScoreBoundingRectangle, leftTeamScore, ScoreboardStyle);

        // DRAW THE RIGHT TEAM'S SCORE.
        int rightTeamScoreRightXPosition = Screen.width - third_of_screen_width;
        int rightTeamScoreLeftXPosition = rightTeamScoreRightXPosition - SCORE_WIDTH;

        Rect rightTeamScoreBoundingRectangle = new Rect(
            rightTeamScoreLeftXPosition,
            SCOREBOARD_TOP_Y_POSITION,
            SCORE_WIDTH,
            SCORE_HEIGHT);

        // The right team's score is tracked by the points scored in the left team's goal.
        string rightTeamScore = LeftTeamGoal.PointsScored.ToString();
        GUI.Label(rightTeamScoreBoundingRectangle, rightTeamScore, ScoreboardStyle);
    }
    #endregion
}
