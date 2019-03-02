using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarSound : MonoBehaviour {

    bool play = false;
    private int currentSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            play = true;
            StartCoroutine(PlayGuitar());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            play = false;
            SoundEffectManager.instance.Stop("Bard" + currentSound);
        }
    }

    IEnumerator PlayGuitar()
    {
        while (play)
        {
            currentSound = Random.Range(1, 4);
            SoundEffectManager.instance.Play("Bard" + currentSound);
            yield return new WaitForSeconds(1f);
        }

        
    }
}
