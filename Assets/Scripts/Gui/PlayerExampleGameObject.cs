using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// An example game object for players on a team, as shown
/// in the game setup scene.  The example game object allows
/// users to quickly see how changes to the shader or model
/// configuration will affect the in-game representations of
/// the 3D models used for players on each team.
/// </summary>
public class PlayerExampleGameObject : MonoBehaviour
{
    /// <summary>
    /// The toggle for displaying the basic box model.
    /// </summary>
    public Toggle BoxModelToggle = null;
    /// <summary>
    /// The prefab to display for the basic box model.
    /// </summary>
    public GameObject BoxModelPrefab = null;
    /// <summary>
    /// The prefab for the goalie line of players using
    /// the box model.  Used for passing to the team
    /// factories.
    /// </summary>
    public GameObject BoxModelGoaliePrefab = null;
    /// <summary>
    /// The prefab for the midfielder line of players using
    /// the box model.  Used for passing to the team
    /// factories.
    /// </summary>
    public GameObject BoxModelMidfielderPrefab = null;
    /// <summary>
    /// The prefab for the forward line of players using
    /// the box model.  Used for passing to the team
    /// factories.
    /// </summary>
    public GameObject BoxModelForwardPrefab = null;

    /// <summary>
    /// The toggle for displaying the banana model.
    /// </summary>
    public Toggle BananaModelToggle = null;
    /// <summary>
    /// The prefab to display for the banana model.
    /// </summary>
    public GameObject BananaModelPrefab = null;
    /// <summary>
    /// The prefab for the goalie line of players using
    /// the banana model.  Used for passing to the team
    /// factories.
    /// </summary>
    public GameObject BananaModelGoaliePrefab = null;
    /// <summary>
    /// The prefab for the midfielder line of players using
    /// the banana model.  Used for passing to the team
    /// factories.
    /// </summary>
    public GameObject BananaModelMidfielderPrefab = null;
    /// <summary>
    /// The prefab for the forward line of players using
    /// the banana model.  Used for passing to the team
    /// factories.
    /// </summary>
    public GameObject BananaModelForwardPrefab = null;

    /// <summary>
    /// The toggle for displaying the foosball player model.
    /// </summary>
    public Toggle FoosballPlayerModelToggle = null;
    /// <summary>
    /// The prefab to display for the foosball player model.
    /// </summary>
    public GameObject FoosballPlayerModelPrefab = null;
    /// <summary>
    /// The prefab for the goalie line of players using
    /// the foosball player model.  Used for passing to the team
    /// factories.
    /// </summary>
    public GameObject FoosballPlayerModelGoaliePrefab = null;
    /// <summary>
    /// The prefab for the midfielder line of players using
    /// the foosball player model.  Used for passing to the team
    /// factories.
    /// </summary>
    public GameObject FoosballPlayerModelMidfielderPrefab = null;
    /// <summary>
    /// The prefab for the forward line of players using
    /// the foosball player model.  Used for passing to the team
    /// factories.
    /// </summary>
    public GameObject FoosballPlayerModelForwardPrefab = null;

    /// <summary>
    /// The toggle for displaying the jewel model.
    /// </summary>
    public Toggle JewelModelToggle = null;
    /// <summary>
    /// The prefab to display for the jewel model.
    /// </summary>
    public GameObject JewelModelPrefab = null;
    /// <summary>
    /// The prefab for the goalie line of players using
    /// the jewel model.  Used for passing to the team
    /// factories.
    /// </summary>
    public GameObject JewelModelGoaliePrefab = null;
    /// <summary>
    /// The prefab for the midfielder line of players using
    /// the jewel model.  Used for passing to the team
    /// factories.
    /// </summary>
    public GameObject JewelModelMidfielderPrefab = null;
    /// <summary>
    /// The prefab for the forward line of players using
    /// the jewel model.  Used for passing to the team
    /// factories.
    /// </summary>
    public GameObject JewelModelForwardPrefab = null;

    /// <summary>
    /// The current goalie line prefab.
    /// </summary>
    public GameObject CurrentGoalieLinePrefab { get; private set; }
    /// <summary>
    /// The current midfielder line prefab.
    /// </summary>
    public GameObject CurrentMidfielderLinePrefab { get; private set; }
    /// <summary>
    /// The current forward line prefab.
    /// </summary>
    public GameObject CurrentForwardLinePrefab { get; private set; }

    /// <summary>
    /// The model currently being displayed for the example player game object.
    /// </summary>
    private GameObject m_currentPlayerModel = null;
    /// <summary>
    /// The current material displayed for the example player game object.
    /// This is stored to allow quickly applying it to new models when
    /// the model is switched for the example game object.
    /// </summary>
    private Material m_currentMaterial = null;

    /// <summary>
    /// Sets the material used for the current example game object.
    /// </summary>
    /// <param name="material">The material to use for the current
    /// example game object.</param>
    public void SetMaterial(Material material)
    {
        // SAVE THE MATERIAL.
        m_currentMaterial = material;

        // UPDATE THE CURRENT PLAYER MODEL IF ONE EXISTS.
        bool currentPlayerModelExists = (null != m_currentPlayerModel);
        if (currentPlayerModelExists)
        {
            m_currentPlayerModel.renderer.material = material;
        }
    }

    /// <summary>
    /// Displays the box model example game object, if the
    /// corresponding toggle is on.
    /// </summary>
    public void DisplayBoxModel()
    {
        // CHECK IF THE BOX MODEL TOGGLE WAS ENABLED.
        if (!BoxModelToggle.isOn)
        {
            // There is nothing to do.  It is the responsibility
            // of other code in this class to clear the currently
            // displayed example model.
            return;
        }

        // CLEAR THE CURRENT EXAMPLE MODEL.
        Destroy(m_currentPlayerModel);

        // DISPLAY THE BOX MODEL.
        m_currentPlayerModel = Instantiate(BoxModelPrefab) as GameObject;
        m_currentPlayerModel.transform.position = this.transform.position;
        m_currentPlayerModel.renderer.material = m_currentMaterial;

        // SET THE CURRENT PLAYER LINE PREFABS.
        CurrentGoalieLinePrefab = BoxModelGoaliePrefab;
        CurrentMidfielderLinePrefab = BoxModelMidfielderPrefab;
        CurrentForwardLinePrefab = BoxModelForwardPrefab;
    }

    /// <summary>
    /// Displays the banana model example game object, if the
    /// corresponding toggle is on.
    /// </summary>
    public void DisplayBananaModel()
    {
        // CHECK IF THE BANANA MODEL TOGGLE WAS ENABLED.
        if (!BananaModelToggle.isOn)
        {
            // There is nothing to do.  It is the responsibility
            // of other code in this class to clear the currently
            // displayed example model.
            return;
        }

        // CLEAR THE CURRENT EXAMPLE MODEL.
        Destroy(m_currentPlayerModel);

        // DISPLAY THE BANANA MODEL.
        m_currentPlayerModel = Instantiate(BananaModelPrefab) as GameObject;
        m_currentPlayerModel.transform.position = this.transform.position;
        m_currentPlayerModel.renderer.material = m_currentMaterial;

        // SET THE CURRENT PLAYER LINE PREFABS.
        CurrentGoalieLinePrefab = BananaModelGoaliePrefab;
        CurrentMidfielderLinePrefab = BananaModelMidfielderPrefab;
        CurrentForwardLinePrefab = BananaModelForwardPrefab;
    }

    /// <summary>
    /// Displays the foosball player model example game object, if the
    /// corresponding toggle is on.
    /// </summary>
    public void DisplayFoosballPlayerModel()
    {
        // CHECK IF THE FOOSBALL PLAYER MODEL TOGGLE WAS ENABLED.
        if (!FoosballPlayerModelToggle.isOn)
        {
            // There is nothing to do.  It is the responsibility
            // of other code in this class to clear the currently
            // displayed example model.
            return;
        }

        // CLEAR THE CURRENT EXAMPLE MODEL.
        Destroy(m_currentPlayerModel);

        // DISPLAY THE FOOSBALL PLAYER MODEL.
        m_currentPlayerModel = Instantiate(FoosballPlayerModelPrefab) as GameObject;
        m_currentPlayerModel.transform.position = this.transform.position;
        m_currentPlayerModel.renderer.material = m_currentMaterial;

        // SET THE CURRENT PLAYER LINE PREFABS.
        CurrentGoalieLinePrefab = FoosballPlayerModelGoaliePrefab;
        CurrentMidfielderLinePrefab = FoosballPlayerModelMidfielderPrefab;
        CurrentForwardLinePrefab = FoosballPlayerModelForwardPrefab;
    }

    /// <summary>
    /// Displays the jewel model example game object, if the
    /// corresponding toggle is on.
    /// </summary>
    public void DisplayJewelModel()
    {
        // CHECK IF THE JEWEL MODEL TOGGLE WAS ENABLED.
        if (!JewelModelToggle.isOn)
        {
            // There is nothing to do.  It is the responsibility
            // of other code in this class to clear the currently
            // displayed example model.
            return;
        }

        // CLEAR THE CURRENT EXAMPLE MODEL.
        Destroy(m_currentPlayerModel);

        // DISPLAY THE JEWEL MODEL.
        m_currentPlayerModel = Instantiate(JewelModelPrefab) as GameObject;
        m_currentPlayerModel.transform.position = this.transform.position;
        m_currentPlayerModel.renderer.material = m_currentMaterial;

        // SET THE CURRENT PLAYER LINE PREFABS.
        CurrentGoalieLinePrefab = JewelModelGoaliePrefab;
        CurrentMidfielderLinePrefab = JewelModelMidfielderPrefab;
        CurrentForwardLinePrefab = JewelModelForwardPrefab;
    }

    /// <summary>
    /// Rotates the currently display model (if one exists),
    /// which helps provide a more visual indication to places of
    /// how the model changes with lighting.
    /// </summary>
    private void Update()
    {
        // CHECK IF A CURRENT PLAYER MODEL EXISTS.
        bool currentPlayerModelExists = (null != m_currentPlayerModel);
        if (!currentPlayerModelExists)
        {
            // No model exists that can be updated.
            return;
        }

        // ROTATE THE PLAYER MODEL AROUND THE WORLD Y-AXIS BASED ON THE AMOUNT OF ELAPSED TIME.
        const float ROTATE_SPEED_IN_DEGREES_PER_SECOND = 60.0f;
        float rotationInDegrees = ROTATE_SPEED_IN_DEGREES_PER_SECOND * Time.deltaTime;
        m_currentPlayerModel.transform.Rotate(0.0f, rotationInDegrees, 0.0f, Space.World);
    }
}
