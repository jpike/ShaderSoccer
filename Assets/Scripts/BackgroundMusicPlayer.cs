using UnityEngine;

/// <summary>
/// A singleton class to control playing background music
/// and transitioning between background music for different
/// scenes.
/// </summary>
public class BackgroundMusicPlayer : MonoBehaviour
{
    /// <summary>
    /// The prefab for the menu background music.
    /// </summary>
    public GameObject MenuBackgroundMusicPrefab = null;

    /// <summary>
    /// The prefab for the gameplay background music.
    /// </summary>
    public GameObject GameplayBackgroundMusicPrefab = null;
    
    /// <summary>
    /// The singleton instance of the music player.
    /// </summary>
    private static BackgroundMusicPlayer m_musicPlayer = null;

    /// <summary>
    /// The instance of the menu background music used by this player.
    /// </summary>
    private GameObject m_menuBackgroundMusic = null;

    /// <summary>
    /// The instance of the gameplay background music used by this player.
    /// </summary>
    private GameObject m_gameplayBackgroundMusic = null;

    /// <summary>
    /// Initializes the the singleton instance
    /// of this class.  If this method is called
    /// multiple times, it will not overwrite
    /// any previous instance and will destroy
    /// any new instances.  The menu background
    /// music is started upon first instantiation
    /// since this music player is assumed to first
    /// be instantiated on the title screen.
    /// </summary>
    private void Awake()
    {
        // CHECK IF THE SINGLETON MUSIC PLAYER INSTANCE ALREADY EXISTS.
        bool musicPlayerAlreadyExists = (m_musicPlayer != null);
        if (musicPlayerAlreadyExists)
        {
            // Another instance of this game object was created.
            // Since only a single instance should exist, destroy
            // the new instance.
            Destroy(this.gameObject);
        }
        else
        {
            // Initialize the singleton instance of the music player for
            // the first time and ensure it persists between scenes.
            m_musicPlayer = this;
            DontDestroyOnLoad(this.gameObject);
            
            // Start playing the initial background music.
            // This is needed because the OnLevelWasLoaded callback
            // doesn't get called initially for the first scene.
            StartMenuBackgroundMusic();
        }
    }

    /// <summary>
    /// Switches the background music depending on which
    /// scene was loaded.
    /// </summary>
    /// <param name="sceneIndex"></param>
    private void OnLevelWasLoaded(int sceneIndex)
    {
        const int TITLE_SCENE_INDEX = 0;
        const int CREDITS_SCENE_INDEX = 1;
        const int GAME_SETUP_SCENE_INDEX = 2;
        const int GAMEPLAY_SCENE_INDEX = 3;
        const int WINNER_SCENE_INDEX = 4;

        // PLAY MUSIC APPROPRIATE FOR THE SCENE.
        switch (sceneIndex)
        {
            case TITLE_SCENE_INDEX:
                StopGameplayBackgroundMusic();
                StartMenuBackgroundMusic();
                break;
            case CREDITS_SCENE_INDEX:
                StopGameplayBackgroundMusic();
                StartMenuBackgroundMusic();
                break;
            case GAME_SETUP_SCENE_INDEX:
                StopGameplayBackgroundMusic();
                StartMenuBackgroundMusic();
                break;
            case GAMEPLAY_SCENE_INDEX:
                StopMenuBackgroundMusic();
                StartGameplayBackgroundMusic();
                break;
            case WINNER_SCENE_INDEX:
                StopGameplayBackgroundMusic();
                StartMenuBackgroundMusic();
                break;
        }
    }

    /// <summary>
    /// Instantiates the menu background music object (not playing)
    /// if it doesn't exist.
    /// </summary>
    private void CreateMenuBackgroundMusicIfNotExists()
    {
        // CHECK IF THE MENU BACKGROUND MUSIC ALREADY EXISTS.
        bool menuBackgroundMusicExists = (m_menuBackgroundMusic != null);
        if (!menuBackgroundMusicExists)
        {
            // CREATE THE MENU BACKGROUND MUSIC.
            // It is instantiated as a child to ensure that it doesn't
            // get destroyed when scenes change, which allows smoother
            // playing/resuming of music.
            m_menuBackgroundMusic = Instantiate(MenuBackgroundMusicPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            m_menuBackgroundMusic.transform.parent = this.transform;
        }
    }

    /// <summary>
    /// Starts playing the menu background music over time.
    /// </summary>
    private void StartMenuBackgroundMusic()
    {
        // ENSURE THAT THE BACKGROUND MUSIC EXISTS.
        CreateMenuBackgroundMusicIfNotExists();

        // START PLAYING THE BACKGROUND MUSIC.
        m_menuBackgroundMusic.GetComponent<BackgroundMusic>().StartPlaying();
    }

    /// <summary>
    /// Stops playing the menu background music over time.
    /// </summary>
    private void StopMenuBackgroundMusic()
    {
        // ENSURE THAT THE BACKGROUND MUSIC EXISTS.
        CreateMenuBackgroundMusicIfNotExists();

        // STOP PLAYING THE BACKGROUND MUSIC.
        m_menuBackgroundMusic.GetComponent<BackgroundMusic>().StopPlaying();
    }

    /// <summary>
    /// Instantiates the gameplay background music object (not playing)
    /// if it doesn't exist.
    /// </summary>
    private void CreateGamplayBackgroundMusicIfNotExists()
    {
        // CHECK IF THE GAMEPLAY BACKGROUND MUSIC ALREADY EXISTS.
        bool gameplayBackgroundMusicExists = (m_gameplayBackgroundMusic != null);
        if (!gameplayBackgroundMusicExists)
        {
            // CREATE THE GAMEPLAY BACKGROUND MUSIC.
            // It is instantiated as a child to ensure that it doesn't
            // get destroyed when scenes change, which allows smoother
            // playing/resuming of music.
            m_gameplayBackgroundMusic = Instantiate(GameplayBackgroundMusicPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            m_gameplayBackgroundMusic.transform.parent = this.transform;
        }
    }

    /// <summary>
    /// Starts playing the gameplay background music over time.
    /// </summary>
    private void StartGameplayBackgroundMusic()
    {
        // ENSURE THAT THE BACKGROUND MUSIC EXISTS.
        CreateGamplayBackgroundMusicIfNotExists();

        // START PLAYING THE BACKGROUND MUSIC.
        m_gameplayBackgroundMusic.GetComponent<BackgroundMusic>().StartPlaying();
    }

    /// <summary>
    /// Stops playing the gameplay background music over time.
    /// </summary>
    private void StopGameplayBackgroundMusic()
    {
        // ENSURE THAT THE BACKGROUND MUSIC EXISTS.
        CreateGamplayBackgroundMusicIfNotExists();

        // STOP PLAYING THE BACKGROUND MUSIC.
        m_gameplayBackgroundMusic.GetComponent<BackgroundMusic>().StopPlaying();
    }
}
