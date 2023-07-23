using TMPro;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] private GameObject debugPanel;
    [SerializeField] private GameObject pausePanel;

    [Header("Debug Menu Pages")]
    [SerializeField] private GameObject[] pages;

    private int currentPage;

    private TextMeshProUGUI debugMenuTitle;

    void Awake()
    {
        debugPanel.SetActive(false);
    }

    public void SetDebugMenuTitle(TextMeshProUGUI value)
    {
        debugMenuTitle = value;
    }

    private void OnEnable()
    {
        currentPage = 0;
        ActivatePage(currentPage);
    }

    public void ActivatePage(int pageNumber)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }

        pages[pageNumber].SetActive(true);
    }

    public void OnPreviousContextMenuClicked()
    {
        currentPage--;

        if (currentPage < 0)
        {
            currentPage = pages.Length - 1;
        }

        ActivatePage(currentPage);
    }

    public void OnNextContextMenuClicked()
    {
        currentPage++;

        if (currentPage >= pages.Length)
        {
            currentPage = 0;
        }

        ActivatePage(currentPage);
    }

    private void Update()
    {
        if (!debugPanel.activeInHierarchy) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenPausePanel();
        }
    }

    private void OpenPausePanel()
    {
        debugPanel.SetActive(false);
        pausePanel.SetActive(true);
    }


    public void OnPreviousLevelClicked()
    {
        Debug.Log("PREVIOUS LEVEL CLICKED");

        if (Time.timeScale <= 0f) Time.timeScale = 1f;

        PlayerManager.Instance.PlayerData.CurrentLevel--;

        PlayerManager.Instance.PlayerData.CurrentLevel =
                    Mathf.Clamp(PlayerManager.Instance.PlayerData.CurrentLevel, 1, 3);

        // Load All Necessary Scenes for Gameplay
        string[] scenes = { "LetterScene" };

        // Load the Scenes with Fade In Transition
        SceneLoader.Instance.LoadScene(scenes, SceneLoader.LoadingStyle.FADE_IN);
    }

    public void OnRestartLevelClicked()
    {
        if (Time.timeScale <= 0f) Time.timeScale = 1f;

        string[] scenes = { "LetterScene" };

        // Load the Scenes with Fade In Transition
        SceneLoader.Instance.LoadScene(scenes, SceneLoader.LoadingStyle.FADE_IN);
    }

    public void OnNextLevelClicked()
    {
        if (Time.timeScale <= 0f) Time.timeScale = 1f;

        Debug.Log("NEXT LEVEL CLICKED");

        PlayerManager.Instance.PlayerData.CurrentLevel++;

        PlayerManager.Instance.PlayerData.CurrentLevel =
                    Mathf.Clamp(PlayerManager.Instance.PlayerData.CurrentLevel++, 1, 3);

        // Load All Necessary Scenes for Gameplay
        string[] scenes = { "LetterScene" };

        // Load the Scenes with Fade In Transition
        SceneLoader.Instance.LoadScene(scenes, SceneLoader.LoadingStyle.FADE_IN);
    }

    public void OnResetPlayerSanityClicked()
    {
        Debug.Log("RESET SANITY CLICKED");

        GameManager.Instance.SetGameState(GameManager.GameState.NO_MONSTER);
        PlayerEvents.Instance.PlayerStabilized();
    }

    public void OnMakePlayerInsaneClicked()
    {
        Debug.Log("PLAYER IS INSANE CLICKED");

        GameManager.Instance.SetGameState(GameManager.GameState.MONSTER_PRESENT);
        PlayerEvents.Instance.PlayerInsane();
    }

    public void OnDisableMovementClicked()
    {
        Debug.Log("DISABLE MOVEMENT CLICKED");

        PlayerEvents.Instance.SetPlayerEnable(false);
    }

    public void OnEnableMovementClicked()
    {
        Debug.Log("DISABLE MOVEMENT CLICKED");

        PlayerEvents.Instance.SetPlayerEnable(true);
    }
}
