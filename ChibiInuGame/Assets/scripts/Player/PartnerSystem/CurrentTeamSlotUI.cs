﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentTeamSlotUI : MonoBehaviour {


    public PartnerManager partnerManager;
    private Partner partnerW = null;
    private Partner partnerQ = null;
    //private Partner partnerE = null;

    public Image qImage;
    public Image wImage;
    //public Image eImage;

    private void OnEnable()
    {
        //UpdatePartners();
        //UpdateImages();
    }

    /*
    public void UpdatePartners()
    {
        for(int i = 0; i < partnerManager.partners.Count; ++i)
        {
            if (partnerManager.partners[i].K)
            {
                partnerW = partnerManager.partners[i];
            }

            if (partnerManager.partners[i].J)
            {
                partnerQ = partnerManager.partners[i];
            }

            
        }
    }


    public void UpdateImages()
    {
        if(partnerW != null)
        {
            wImage.sprite = partnerW.image;
            wImage.enabled = true;
        }
        if (partnerQ != null)
        {
            qImage.sprite = partnerQ.image;
            qImage.enabled = true;
        }
      
    }*/
}
