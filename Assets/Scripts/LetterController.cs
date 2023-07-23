using UnityEngine;
using TMPro;

public class LetterController : MonoBehaviour
{
    [Header("Properties")]
    [TextArea(3, 10)] [SerializeField] private string[] notes;                         // Notes Array

    [Header("References")]
    [SerializeField] private TextMeshProUGUI noteText;
    
    // Start is called before the first frame update
    void Start()
    {
        PanelManager.Instance.ActivatePanel("Letter");
        WriteText();
    }

    void WriteText()
    {
        noteText.text = notes[PlayerManager.Instance.PlayerData.CurrentLevel - 1];
    }

    public void ProceedToGame()
    {
        // Load All Necessary Scenes for Gameplay
        string[] scenes = { "GameScene", "GameUIScene", "Level" + PlayerManager.Instance.PlayerData.CurrentLevel };

        // Load the Scenes with Fade In Transition
        SceneLoader.Instance.LoadScene(scenes, SceneLoader.LoadingStyle.FADE_IN);
    }
}
