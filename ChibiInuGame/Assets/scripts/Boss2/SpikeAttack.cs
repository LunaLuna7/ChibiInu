using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeAttack : MonoBehaviour {

    public float speed;
    public float yOffset;
    public List<GameObject> spikes;

	// Use this for initialization
	void Start () {
        
    }

    private void OnEnable()
    {
        
        StartCoroutine(ActivateSpikes());
    }

    // Update is called once per frame
    void Update () {
        StartCoroutine(TurnOff());
        StartCoroutine(TriggerSpikes());
	}

    IEnumerator TriggerSpikes()
    {
        for(int i = 0; i < spikes.Capacity; ++i)
        {
            
            spikes[i].transform.position = Vector3.MoveTowards(spikes[i].transform.position, new Vector3(spikes[i].transform.position.x, yOffset, spikes[i].transform.position.z), speed * Time.deltaTime);
            yield return new WaitForSeconds(.05f);
        }
    }
    IEnumerator ActivateSpikes()
    {
        for (int i = 0; i < spikes.Capacity; ++i)
        {
            spikes[i].SetActive(true);
            yield return new WaitForSeconds(.05f);
        }
    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
