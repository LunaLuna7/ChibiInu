using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    public Animator animator;
    private int levelToLoad;

	void Update () {
        if (Input.GetKey(KeyCode.Alpha2))
        {
            FadeToLevel(4);
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            FadeToLevel(2);
        }
    }

    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToSameLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
        animator.Play("Fade_In");
    }

    public void FakeFade()
    {
        animator.Play("Fake_Fade_Out");
    }

    public void FakeFadeIn()
    { 
        animator.Play("Fade_In");
    }

    public void FakeDead()
    {

    }
}
