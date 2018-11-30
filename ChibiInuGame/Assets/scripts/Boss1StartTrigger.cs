using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1StartTrigger : MonoBehaviour {

    public bool wallDown;
    public float moveSpeed;
    public List<GameObject> slimesType; 
    public SoundEffectManager soundEffect;
    private bool musicPlaying;

    void Start()
    {
        soundEffect = GameObject.FindGameObjectWithTag("SoundEffect").GetComponent<SoundEffectManager>();

        musicPlaying = false;
        for (int i = 0; i != slimesType.Count; ++i)
        {
            StateController sc;
            sc = slimesType[i].GetComponent<StateController>();
            sc.enemyStats.moveSpeed = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (wallDown)
        {
            
            for (int i = 0; i != slimesType.Capacity; ++i)
            {
                StateController sc;
                sc = slimesType[i].GetComponent<StateController>();
                sc.enemyStats.moveSpeed = moveSpeed;
            }
            

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!musicPlaying)
            {
                soundEffect.Play("Boss");
                musicPlaying = true;
            }
            wallDown = true;
        }
    }
}
