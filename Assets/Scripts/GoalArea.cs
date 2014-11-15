using UnityEngine;

/// <summary>
/// Represents a goal on the playing field where the ball
/// may enter for a player to score points.  A goal area
/// is responsible for tracking the number of points scored
/// in it.
/// </summary>
public class GoalArea : MonoBehaviour
{
    #region Private Fields
    /// <summary>
    /// The number of points currently scored in the goal.
    /// </summary>
    private int m_pointsScored = 0;
    #endregion

    #region Collision Methods
    /// <summary>
    /// Called whenever another collider enters the goal area.
    /// This method only interacts with balls.  It resets the
    /// ball so that gameplay can continue and also increases
    /// the number of points scored in the goal.
    /// </summary>
    /// <param name="collider">The collider of the object that entered the goal.</param>
    private void OnTriggerEnter(Collider collider)
    {
        // CHECK IF THE ENTERED OBJECT WAS A BALL.
        Ball ball = collider.gameObject.GetComponent<Ball>();
        bool ballEnteredGoal = (ball != null);
        if (!ballEnteredGoal)
        {
            // Some object other than a ball entered the goal.
            // Since this method only handles balls, there is nothing to do.
            return;
        }

        // INCREMENT THE SCORE IN THE GOAL SINCE A BALL ENTERED IT.
        ++m_pointsScored;

        // RESET THE BALL SO THAT GAMEPLAY CAN CONTINUE.
        ball.Reset();
    }
    #endregion
}
