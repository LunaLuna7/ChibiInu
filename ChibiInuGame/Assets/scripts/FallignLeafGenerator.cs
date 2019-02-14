using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallignLeafGenerator : MonoBehaviour {

    public GameObject leaf;
    public Transform originPositionY;
    public Transform xLeftBoundary;
    public Transform xRightBoundary;

    public float leavesPerWave;
    public float intervalBetweenWaves;
    public float leavesFallingSpeed;

	// Use this for initialization
	void Start () {
        StartCoroutine(FallingLeaves());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator FallingLeaves()
    {
        while (true)
        {
            for(int i = 0; i < leavesPerWave; i++)
            {
                float xPosition = Random.Range(xLeftBoundary.transform.position.x, xRightBoundary.transform.position.x);

                Vector3 leafPosition = new Vector3(xPosition, originPositionY.position.y, 0f);
                Instantiate(leaf, leafPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(intervalBetweenWaves);

        }
    }
}
