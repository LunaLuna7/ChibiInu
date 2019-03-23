using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour {

    public float lightTimeChange;
    public float offsetRange;
    Light light;
    private float originalRange;

	void Awake () {
        light = GetComponent<Light>();
        originalRange = light.range;
    }

	// Use this for initialization
    void Start()
    {
    }

    private void OnEnable()
    {
        StartCoroutine(PulseLight());    
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator PulseLight()
    {
        while (true)
        {
            light.range = originalRange + offsetRange;
            yield return new WaitForSeconds(lightTimeChange);
            light.range = originalRange;
            yield return new WaitForSeconds(lightTimeChange);
        }
    }
}
