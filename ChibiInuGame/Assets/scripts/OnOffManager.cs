using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffManager : MonoBehaviour {

    public float alternateTime;
    public List<GameObject> firstOnObjects;
    public List<GameObject> secondOnObjects;

    private bool firstOn;
    
    // Use this for initialization
	void Start () {
        firstOn = true;
        StartCoroutine(AlternateObjects());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator AlternateObjects()
    {
        while(true){
            yield return new WaitForSeconds(alternateTime);
            SwitchOnObjects();
        }
    }

    void SwitchOnObjects()
    {
        if (firstOn)
        {
            foreach(GameObject each in firstOnObjects)
            {
                each.SetActive(true);
            }
            foreach(GameObject each in secondOnObjects)
            {
                each.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject each in firstOnObjects)
            {
                each.SetActive(false);
            }
            foreach (GameObject each in secondOnObjects)
            {
                each.SetActive(true);
            }
        }
        firstOn = !firstOn;
    }
}
