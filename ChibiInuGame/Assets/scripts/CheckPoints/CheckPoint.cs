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
    public GameObject CheckPointParticleAura;
    public int previousCheckPoint;


    private bool onCheckPoint;
    private bool activated;

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
        soundEffectManager = GameObject.FindGameObjectWithTag("SoundEffect").GetComponent<SoundEffectManager>();
        activated = false;
        
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
        previousCheckPoint = UpdateCheckPoint.currentCheckPoint;
        UpdateCheckPoint.currentCheckPoint = newCheckPoint;
        if (previousCheckPoint > UpdateCheckPoint.currentCheckPoint)
            UpdateCheckPoint.currentCheckPoint = previousCheckPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            collision.gameObject.GetComponent<PlayerHealth>().HealDamage();
            SetCheckPointTo();
            onCheckPoint = true;
            checkPointImage.sprite = activeCheckPoint;
            soundEffectManager.Play("CheckPointAura");
            
            if (!activated)
            {
                CheckPointParticleAura.SetActive(true);
                soundEffectManager.Play("CheckPoint");
                activated = true;
            }
        }
    }

  

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            soundEffectManager.Stop("CheckPointAura");
            onCheckPoint = false;
            book.SetActive(false);
        }
       
    }

}
