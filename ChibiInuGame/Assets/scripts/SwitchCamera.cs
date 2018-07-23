using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour {

    //public GameObject cam1;
    //public GameObject cam2;
    public GameObject[] cameras;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
        

	}

    public void ChangeCamera(int activeCam)
    {
        if(activeCam < 1 || activeCam > cameras.Length)
        {
            Debug.LogError("active Cam: " + activeCam.ToString() + "out of cameras bound");
        }
        for(int i = 0; i != cameras.Length; i++)
        {
            cameras[i].SetActive(false);
        }
        cameras[activeCam - 1].SetActive(true);
        
       
    }
    
}
