using UnityEngine;

/// <summary>
/// A player in the game field.  A HumanFieldPlayerController script may
/// be attached to the object for this player to have the it
/// controlled by the human user input.
/// </summary>
public class FieldPlayer : MonoBehaviour
{
    #region Public Properties
    /// <summary>
    /// Retrieves the world boundaries of the field player.
    /// </summary>
    public Bounds Bounds
    {
        get
        {
            // RETRIEVE THE BOUNDS FROM THE ATTACHED COLLIDER IF POSSIBLE.
            bool colliderExists = (gameObject.collider != null);
            if (colliderExists)
            {
                return gameObject.collider.bounds;
            }
            else
            {
                // Return default, invalid bounds since no collider was attached to this object.
                return new Bounds();
            }
        }
    }

    /// <summary>
    /// Retrieves the transform of the field player.
    /// </summary>
    public Transform Transform
    {
        get
        {
            // A transform should be attached to every game object.
            return transform;
        }
    }
    #endregion

    #region Private Fields
    /// <summary>
    /// A game controller for a human supplying input to move the field player.
    /// If null, it is assumed that no human is controlling the field player.
    /// </summary>
    private HumanFieldPlayerController m_humanPlayerController;
    #endregion

    #region Initialization Methods
    /// <summary>
    /// Initalizes the field player to have references to all dependent objects.
    /// </summary>
	private void Start() 
    {
        // GET ANY HUMAN CONTROLLER FOR THE PLAYER.
        m_humanPlayerController = gameObject.GetComponent<HumanFieldPlayerController>();
	}
    #endregion

    #region Update Methods
    /// <summary>
    /// Updates the field player once per frame to respond to user input.
    /// </summary>
	private void Update()
    {
        // MOVE THE PLAYER BASED ON ANY HUMAN USER INPUT.
        bool humanControllingPlayer = (m_humanPlayerController != null);
        if (humanControllingPlayer)
        {
            m_humanPlayerController.MoveBasedOnInput();
        }
    }
    #endregion
}
