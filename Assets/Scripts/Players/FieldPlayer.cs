using UnityEngine;

/// <summary>
/// A player in the game field.  It only encapsulates
/// positions and boundaries of a single player.
/// </summary>
public class FieldPlayer : MonoBehaviour
{
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
}
