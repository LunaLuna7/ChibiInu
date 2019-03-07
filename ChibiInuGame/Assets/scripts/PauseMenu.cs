using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu: MonoBehaviour{

    public LevelChanger levelChanger;
    public GameObject pauseWindow;
    private bool loadingScene;

    private void Start()
    {
        loadingScene = false;
        if (levelChanger == null)
            levelChanger = GameObject.FindObjectOfType<LevelChanger>();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || (Input.GetButtonDown("Pause"))) && !loadingScene)
        {
            if (!pauseWindow.activeSelf)
                ActivatePause();
            else
                DeactivatePause();
        }
    }

    public void ActivatePause()
    {
        pauseWindow.SetActive(true);
        Time.timeScale = 0;
    }

    public void DeactivatePause()
    {
        pauseWindow.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadLevelSelect()
    {
        loadingScene = true;
        levelChanger.FadeToLevel(1);
        DeactivatePause();
    }

    public void LoadMainMenu()
    {
        loadingScene = true;
        levelChanger.FadeToLevel(0);
        DeactivatePause();
    }

}
