using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanBossHellBallState : IState {

    //components
    private SatanBossManager controller;

    //Stats
    private float projectileSpeed = 40;
    private float waitTime = .5f;

    
    public SatanBossHellBallState(SatanBossManager controller)
    {
        this.controller = controller;
    }

    public void EnterState()
    {
        controller.StartCoroutine(Skill(waitTime));
    }

    public void ExecuteState()
    {
        
    }

    public void ExitState()
    {
        
    }

    public IEnumerator Skill(float waitTime)
    {
        //create prefab
        GameObject obj = GameObject.Instantiate(controller.hellBall, controller.transform.position + Vector3.back, Quaternion.identity);
        obj.transform.SetParent(controller.skillObjectsGroup);
        

        yield return new WaitForSeconds(waitTime);

        //shoot towards player
        obj.transform.position = Vector3.RotateTowards(obj.transform.position, controller.player.transform.position, 20f * Time.deltaTime, 20f * Time.deltaTime);
        obj.GetComponent<SatanFireBall>().Shoot(projectileSpeed);
        
        yield return new WaitForSeconds(waitTime);
        controller.SwitchState();
    }
}
