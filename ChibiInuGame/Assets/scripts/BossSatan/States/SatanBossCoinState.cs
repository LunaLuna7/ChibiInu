using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanBossCoinState : IState {

    SatanBossManager controller;
    float timeWait = 1.5f;

    public SatanBossCoinState(SatanBossManager satanBossManager)
    {
        controller = satanBossManager;
    }
    public void EnterState()
    {
        controller.StartCoroutine(Skill());
    }

    public void ExecuteState()
    {
        
    }

    public void ExitState()
    {
        
    }

    
    IEnumerator Skill()
    {
        //get the current phase map
        int currentPhase = controller.gameObject.GetComponent<SatanBossPhaseManager>().GetPhaseMap();
        //get a random location from the specific map list of possible locations to spawn coin
        Transform coinToSummonLocation = ChooseRandomLocation(controller.allCoinSpawns[currentPhase]);
        //create coin
        GameObject coin = GameObject.Instantiate(controller.coinAttack, coinToSummonLocation.position, Quaternion.identity);

        yield return new WaitForSeconds(timeWait);
        controller.SwitchState();
    }

    Transform ChooseRandomLocation(List<Transform> t)
    {
        return t[Random.Range(0, t.Count-1)];
    }

    
}
