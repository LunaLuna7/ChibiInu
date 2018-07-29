using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlime : MonoBehaviour {

    public Animator anim;
    public GameObject hurtBox;

    public List<GameObject> slimsOnTop;
    private bool triggeredOnce = false;

	void Start () {
        //anim = GetComponentInChildren<Animator>();
        //StartCoroutine(FireAttackOff());
       
	}
	
	// Update is called once per frame
	void Update () {

        int counter = 0;
        for(int i = 0; i < slimsOnTop.Count; ++i)
        {
            if (slimsOnTop[i] == null)
            {
                counter++;
            }
           
        }

        if(counter == slimsOnTop.Count && !triggeredOnce)
        {
            triggeredOnce = true;
            StartCoroutine(FireAttackOff());

        }
	}

    IEnumerator FireAttackOff()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            anim.SetBool("FireOn", false);
            hurtBox.SetActive(true);
            yield return new WaitForSeconds(1f);
            anim.SetBool("FireOn", true);
            hurtBox.SetActive(false);

        }
    }

    public void Die()
    {
        
    }
}
