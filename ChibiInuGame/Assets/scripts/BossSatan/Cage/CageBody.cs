using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageBody : MonoBehaviour {

	public SatanBossCage cage;

	public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("FireBall"))
        {
            if (cage.timeTrack <= Time.time)
            {
                cage.timeTrack = cage.timeBeforeDamageAgain + Time.time;
                cage.TakeDamage(1);
                //StartCoroutine(BlinkSprite());
            }
        }
    }
}
