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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            partnerManager.QSkill();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            partnerManager.WSkill();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            partnerManager.ESkill();
        }

    }

    
}
