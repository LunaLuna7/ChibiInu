using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlime : MonoBehaviour {

    public Animator anim;
    public GameObject hurtBox;
    public bool vunerable = true;
    public float startDelay = 0f;
    public List<GameObject>slimsOnTop;
    private bool triggeredOnce = false;

	void Start () {
        anim.SetBool("FireOn", true);
        hurtBox.SetActive(false);
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
            if(vunerable)
                StartCoroutine(FireAttackOff());

        }
	}

    IEnumerator FireAttackOff()
    {
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            anim.SetBool("FireOn", false);
            hurtBox.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            anim.SetBool("FireOn", true);
            hurtBox.SetActive(false);

        }
    }

    public void Die()
    {
        
    }
}
