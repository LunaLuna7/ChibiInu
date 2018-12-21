using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPartnerSkills : MonoBehaviour {

    private PartnerManager partnerManager;

    private void Awake()
    {
        partnerManager = GetComponent<PartnerManager>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("Fire2"))
        {
            partnerManager.JSkill();
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            partnerManager.KSkill();
        }
       

    }

    
}
