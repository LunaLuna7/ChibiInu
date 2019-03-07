using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu: MonoBehaviour{

    public GameObject arrow;
    public GameObject[] buttons;

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
        if(pauseWindow.activeSelf)
            CheckMainInput();
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

    private int mainArrowIndex = 0;
    private float timer = 5;
    public void CheckMainInput()
    {

        if (Input.GetButtonDown("Pause"))
            mainArrowIndex = 0;
        //when press up button
        if (MenuInputManager.CheckUp() && mainArrowIndex > 0)
        {
            SoundEffectManager.instance.Play("MenuScroll");
            --mainArrowIndex;
            UpdateArrow(mainArrowIndex);
        }
        //when press down
        else if (MenuInputManager.CheckDown() && mainArrowIndex < buttons.Length - 1)
        {
            SoundEffectManager.instance.Play("MenuScroll");
            ++mainArrowIndex;
            UpdateArrow(mainArrowIndex);
        }
        //when press Space
        else if (Input.GetButtonDown("Submit"))
        {
            Debug.Log(mainArrowIndex);
            SoundEffectManager.instance.Play("MenuSelect");
            switch (mainArrowIndex)
            {
                //start game
                case 0:
                    DeactivatePause();
                    break;
                case 1:
                    LoadLevelSelect();
                    break;
                //quit game
                case 2:
                    LoadMainMenu();
                    break;
            }
        }
    }

    private void UpdateArrow(int index)
    {
        arrow.GetComponent<RectTransform>().position = new Vector3(arrow.GetComponent<RectTransform>().position.x,
                                                              buttons[index].GetComponent<RectTransform>().position.y,
                                                              arrow.GetComponent<RectTransform>().position.z);

    }
}
