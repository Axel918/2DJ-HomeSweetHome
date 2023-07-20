using TMPro;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    [Header("Debug Menu Panel")]
    [SerializeField] private GameObject debugPanel;

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


    public void OnPreviousLevelClicked()
    {
        Debug.Log("PREVIOUS LEVEL CLICKED");
    }

    public void OnNextLevelClicked()
    {
        Debug.Log("NEXT LEVEL CLICKED");
    }

    public void OnDisableMonsterClicked()
    {
        Debug.Log("DISABLE MONSTER CLICKED");
    }

    public void OnResetSanityClicked()
    {
        Debug.Log("RESET SANITY CLICKED");
    }

    public void OnDisableMovementClicked()
    {
        Debug.Log("DISABLE MOVEMENT CLICKED");
    }

}
