using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampName : MonoBehaviour {

    public Text warningText;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void UpdateTalkPosition()
    {
        Vector2 talkPos = Camera.main.WorldToScreenPoint(this.transform.position);
        warningText.transform.position = talkPos;
    }

    public void ActiveMessage(bool isNormal, bool state)
    {
        if(state && !isNormal)
        {
            warningText.gameObject.SetActive(true);
            StartCoroutine(waitSec());
            
        }
        else
        {
            warningText.gameObject.SetActive(false);
        }
    }
    public void Message(string message)
    {
        warningText.text = message;
    }
    
    IEnumerator waitSec()
    {
        yield return new WaitForSeconds(2);
        warningText.gameObject.SetActive(false);
    }
}
