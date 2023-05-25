using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using DG.Tweening;


public class SceneLoader : MonoBehaviour
{
    public enum LoadingStyle
    {
        INSTANT,
        FADE_IN
    }
    
    public static SceneLoader Instance;

    [Header("References")]
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject transitionPanel;

    [Header("Configuration")]
    [SerializeField] private string firstSceneId;

    private CanvasGroup transitionPanelCanvasGroup;
    private string[] currentSceneIds;

    #region Singleton
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        transitionPanelCanvasGroup = transitionPanel.GetComponent<CanvasGroup>();
    }
    #endregion

    void Start()
    {
        string[] scenes = { firstSceneId };

        LoadScene(scenes, LoadingStyle.INSTANT);
    }

    public void LoadScene(string[] sceneIds, LoadingStyle style)
    {
        switch (style)
        {
            case LoadingStyle.INSTANT:
                StartCoroutine(LoadSceneSequence(sceneIds));
                break;
                
            case LoadingStyle.FADE_IN:
                transitionPanel.SetActive(true);

                // Trigger Fade-Out Animation
                transitionPanelCanvasGroup.alpha = 0f;
                transitionPanelCanvasGroup.DOFade(1f, 1f).OnComplete(() => StartCoroutine(LoadSceneSequence(sceneIds)));
                break;
        }
    }

    private IEnumerator LoadSceneSequence(string[] sceneIds)
    {
        // Activate Loading Screen
        StartLoading();

        // Unload Current Scene if There are Any
        if (currentSceneIds != null)
        {
            foreach (string id in currentSceneIds)
                yield return SceneManager.UnloadSceneAsync(id);
        }

        // Unload Unused Assets
        Resources.UnloadUnusedAssets();
        yield return null;

        // Garbage Collection
        GC.Collect();
        yield return null;

        // Load the Scenes
        foreach (string id in sceneIds)
            yield return SceneManager.LoadSceneAsync(id, LoadSceneMode.Additive);

        // Deactivate Loading Screen
        EndLoading();

        // Set Loaded Scene to Current Scene
        currentSceneIds = sceneIds;
    }

    void StartLoading()
    {
        transitionPanel.SetActive(false);
        loadingPanel.SetActive(true);
    }

    void EndLoading()
    {
        loadingPanel.SetActive(false);

        transitionPanel.SetActive(true);

        // Trigger Fade-Out Animation
        transitionPanelCanvasGroup.alpha = 1f;
        transitionPanelCanvasGroup.DOFade(0, 1f).OnComplete(() => transitionPanel.SetActive(false));
    }
}