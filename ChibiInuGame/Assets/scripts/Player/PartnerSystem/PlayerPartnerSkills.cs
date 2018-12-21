using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPartnerSkills : MonoBehaviour {

    //=====================================================================================================
    //PlayerPartnerSkills handles the assinging what skill to what Skill object depending on scriptable Obj
    //=====================================================================================================    

    private PartnerManager partnerManager;

    private void Awake()
    {
        partnerManager = GetComponent<PartnerManager>();
    }

    //When player presses J 0r K the current partner method assing to JSkill() or/and KSkill() get call
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("Fire2"))
            partnerManager.JSkill();
        
        
        if (Input.GetKeyDown(KeyCode.K))
            partnerManager.KSkill();
    }
}
