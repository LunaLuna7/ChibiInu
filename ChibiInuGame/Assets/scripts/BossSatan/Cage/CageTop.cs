using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the top of cage got hurt both when stepped and hitted by fireBall
public class CageTop : MonoBehaviour {
	public SatanBossCage cage;

	public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("FireBall") || collision.gameObject.CompareTag("Player"))
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
