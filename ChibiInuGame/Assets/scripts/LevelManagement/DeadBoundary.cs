using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBoundary : MonoBehaviour {

    private LevelChanger levelChanger;
    public List<Transform> checkPointsPosition;
    public List<CheckPoint> checkPoints;

	// Use this for initialization
	void Start () {
        levelChanger = GetComponentInParent<LevelChanger>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {
            //levelChanger.FadeToSameLevel();
            collision.gameObject.transform.position = checkPointsPosition[UpdateCheckPoint.currentCheckPoint - 1].transform.position;
            
            for(int i = 0; i != checkPoints.Count; ++i)
            {
                checkPoints[i].ResetingLevel();
            }
        }
    }
}
