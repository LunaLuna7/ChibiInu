using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public LevelChanger levelChanger;
    public List<Transform> checkPointsPosition;
    public List<CheckPoint> checkPoints;
    public GameObject player;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        UpdateCheckPoint.currentCheckPoint = 1;
    }

    void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        player.transform.position = checkPointsPosition[UpdateCheckPoint.currentCheckPoint - 1].position;
    }

    public void GameOver(Transform playerTransform)
    {
        playerHealth.HPLeft = 0;
        levelChanger.FakeFade();
        StartCoroutine(ToCheckPoint(playerTransform));
        StartCoroutine(DelayStart());
    }
    
    IEnumerator ToCheckPoint(Transform playerTransform)
    {
        yield return new WaitForSeconds(1f);
        playerTransform.position = checkPointsPosition[UpdateCheckPoint.currentCheckPoint - 1].transform.position;

        playerHealth.ResetPlayer();

    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(1.1f);
        for (int i = 0; i != checkPoints.Count; ++i)
        {
            checkPoints[i].ResetingLevel();
        }
    }
}
