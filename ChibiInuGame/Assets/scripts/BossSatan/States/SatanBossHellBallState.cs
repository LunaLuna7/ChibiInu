using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanBossHellBallState : IState {

    //components
    private SatanBossManager controller;

    //Stats
    private float projectileSpeed = 40;
    private float waitTimeShoot = 1f;
    private float waitTimeAfterShoot = 4f;

    
    public SatanBossHellBallState(SatanBossManager controller)
    {
        this.controller = controller;
    }

    public void EnterState()
    {
        controller.StartCoroutine(Skill(waitTimeShoot));
    }

    public void ExecuteState()
    {
        
    }

    public void ExitState()
    {
        
    }

    public IEnumerator Skill(float waitTime)
    {
        //make gesture
        controller.GetComponent<Animator>().SetTrigger("FireballSkill");
        yield return new WaitForSeconds(1);
        //create prefab
        GameObject obj = GameObject.Instantiate(controller.hellBall, controller.transform.position + Vector3.back, Quaternion.identity);
        obj.transform.SetParent(controller.skillObjectsGroup);
        obj.GetComponent<SatanFireBall>().RotateTowards(controller.player);
        

        yield return new WaitForSeconds(waitTime);

        //shoot towards player
        //obj.transform.eulerAngles = Vector3.RotateTowards(obj.transform.position, controller.player.transform.position, 20f * Time.deltaTime, 20f * Time.deltaTime);
        if(obj != null)
        {
            obj.GetComponent<SatanFireBall>().Shoot(projectileSpeed);
            SoundEffectManager.instance.Play("FireBall");
        }
        
        yield return new WaitForSeconds(waitTimeAfterShoot);
        controller.SwitchState();
    }
}
