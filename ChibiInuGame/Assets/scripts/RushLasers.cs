using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushLasers : MonoBehaviour {

    public float tempo;
    public List<GameObject> wave;
	
    
    // Use this for initialization
	void Start () {
        StartCoroutine(Alternate());
	}
	
	IEnumerator Alternate()
    {
        while (true)
        {
            for(int i = 0; i < wave.Count; i++)
            {
                wave[i].SetActive(true);
                yield return new WaitForSeconds(tempo);
                wave[i].SetActive(false);
            }
        }
    }
}
