using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public int newCheckPoint;
    public GameObject book;
    public List<GameObject> enemies;

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
        
    }

    public void SetCheckPointTo()
    {
        UpdateCheckPoint.currentCheckPoint = newCheckPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SetCheckPointTo();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                book.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            book.SetActive(false);
        }
    }

}
