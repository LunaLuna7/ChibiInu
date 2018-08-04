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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (partnerManager.QSkill == null)
                Debug.LogError("QSkill has not being assigned in PartnerManager script");

            partnerManager.QSkill();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (partnerManager.WSkill == null)
                Debug.LogError("WSkill has not being assigned in PartnerManager script");
            partnerManager.WSkill();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (partnerManager.ESkill == null)
                Debug.LogError("ESkill has not being assigned in PartnerManager script");
            partnerManager.ESkill();
        }

    }

    
}
