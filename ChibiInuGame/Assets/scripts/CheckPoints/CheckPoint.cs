using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public Sprite activeCheckPoint;
    private SpriteRenderer checkPointImage;
    public int newCheckPoint;
    public GameObject book;
    public List<GameObject> enemies;

    public SoundEffectManager soundEffectManager;

    private bool onCheckPoint;


    public void ResetingLevel()
    {
        if (newCheckPoint == UpdateCheckPoint.currentCheckPoint)
        {
            for (int i = 0; i < enemies.Count; ++i)
            {
                enemies[i].SetActive(true);
            }
        }
    }


    public void Start()
    {
        checkPointImage = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (onCheckPoint)
        {
            if (Input.GetKeyDown(KeyCode.P) && !book.activeSelf)
                book.SetActive(true);

            else if (Input.GetKeyDown(KeyCode.P) && book.activeSelf)
                book.SetActive(false);

        
        }
    }

    public void SetCheckPointTo()
    {
        UpdateCheckPoint.currentCheckPoint = newCheckPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if(collision.gameObject.tag == "Player")
        {
            SetCheckPointTo();
            checkPointImage.sprite = activeCheckPoint;
        }*/
        if (collision.gameObject.tag == "Player")
        {
            SetCheckPointTo();
            onCheckPoint = true;
            checkPointImage.sprite = activeCheckPoint;
            soundEffectManager.Play("CheckPoint");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        /*
        if(collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.P) && !book.activeSelf)
                book.SetActive(true);
  
            else if (Input.GetKeyDown(KeyCode.P) && book.activeSelf)
                book.SetActive(false);
            
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onCheckPoint = false;
            book.SetActive(false);
        }
       /* if (collision.gameObject.tag == "Player")
        {
            book.SetActive(false);
        }*/
    }

}
