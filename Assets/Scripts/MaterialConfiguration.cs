using UnityEngine;

/// <summary>
/// The configuration for a material.  Primarily intended
/// to be used as an interface that different times of 
/// shader configuration GUI classes can implement to
/// retrieve the particular material for their shader
/// configuration.  It is a class inheriting from MonoBehavior,
/// rather than in interface, to allow easy retrieval of
/// scripts inheriting from this class by calling
/// GetComponent on objects.
/// </summary>
public abstract class MaterialConfiguration : MonoBehaviour 
{
    /// <summary>
    /// An abstract method for creating a copy of the
    /// material in the configuration.  A copy ensures
    /// that the original material isn't accidentally
    /// modified.
    /// </summary>
    /// <returns>The configured material.</returns>
    public abstract Material CreateMaterial();
}
