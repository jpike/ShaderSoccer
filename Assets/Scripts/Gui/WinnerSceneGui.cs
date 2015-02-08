using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for initializing the text in the winner scene
/// along with background music.
/// </summary>
public class WinnerSceneGui : MonoBehaviour
{
    /// <summary>
    /// Initializes dynamic text in the winner scene
    /// along with background music.
    /// </summary>
	void Start()
    {
        // SET THE TEXT IDENTIFYING THE WINNING TEAM.
        Text winningTeamText = GameObject.Find("WinnerText").GetComponent<Text>();
        switch (Referee.Winner)
        {
            case Referee.WinnerType.LEFT_TEAM:
                winningTeamText.text = "LEFT TEAM WINS!";
                break;
            case Referee.WinnerType.RIGHT_TEAM:
                winningTeamText.text = "RIGHT TEAM WINS!";
                break;
            case Referee.WinnerType.TIE:
                winningTeamText.text = "TIED GAME";
                break;
            default:
                // Leave the text alone to make it easier to debug errors.
                break;
        }

        // SET THE LEFT TEAM'S SCORE.
        Text leftTeamScoreText = GameObject.Find("LeftTeamScoreText").GetComponent<Text>();
        leftTeamScoreText.text = "Left Team Score: " + Referee.LeftTeamScore.ToString();

        // SET THE RIGHT TEAM'S SCORE.
        Text rightTeamScoreText = GameObject.Find("RightTeamScoreText").GetComponent<Text>();
        rightTeamScoreText.text = "Right Team Score: " + Referee.RightTeamScore.ToString();
	}
}
