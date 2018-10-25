using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public LevelChanger levelChanger;
    public List<Transform> checkPointsPosition;
    public List<CheckPoint> checkPoints;
    public GameObject player;


    void Start()
    {
        player.transform.position = checkPointsPosition[UpdateCheckPoint.currentCheckPoint - 1].position;
    }

    public void GameOver(Transform playerTransform)
    {
        
        levelChanger.FakeFade();
        StartCoroutine(ToCheckPoint(playerTransform));
        for (int i = 0; i != checkPoints.Count; ++i)
        {
            checkPoints[i].ResetingLevel();
        }
    }
    
    IEnumerator ToCheckPoint(Transform playerTransform)
    {
        yield return new WaitForSeconds(1f);
        playerTransform.position = checkPointsPosition[UpdateCheckPoint.currentCheckPoint - 1].transform.position;

    }
}
