using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance;

    public Action LoadingScreenLoaded;
    //public SceneNames SceneName;

    public Sprite BGOfLoadingScreen;
    public string NextSceneToLoad;
    public LightingSettings NextSceneLightingSettings;

    //public bool IsLoadingScreenPresent => FindObjectOfType<LoadingSceneHandler>();

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    public void LoadNextSceneAsync(string SceneToLoad)
    {
        NextSceneToLoad = SceneToLoad;
        SceneManager.LoadScene("Loading Screen", LoadSceneMode.Additive);
    }
    public void LoadNextSceneInstant(string SceneToLoad)
    {
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
    }

    public void UnloadThisScene()
    {
        LoadingScreenLoaded?.Invoke();
        Debug.Log(SceneManager.GetSceneByName("InGameUI").IsValid());
        if (SceneManager.GetSceneByName("InGameUI").IsValid()) SceneManager.UnloadSceneAsync("InGameUI");
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
