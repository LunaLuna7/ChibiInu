using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlime : MonoBehaviour {

    public Animator anim;
    public GameObject hurtBox;
	void Start () {
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(FireAttackOff());
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator FireAttackOff()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            anim.SetBool("Fire", false);
            hurtBox.SetActive(true);
            yield return new WaitForSeconds(1f);
            anim.SetBool("Fire", true);
            hurtBox.SetActive(false);

        }
    }

    public void Die()
    {
        
    }
}
