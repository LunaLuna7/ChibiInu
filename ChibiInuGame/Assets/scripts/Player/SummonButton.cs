using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonButton : MonoBehaviour {

    public GameObject Q;
    public GameObject W;
    public GameObject E;

    public void OnMouseEnter()
    {
        
        Q.SetActive(true);
        W.SetActive(true);
        E.SetActive(true);
    }
}
