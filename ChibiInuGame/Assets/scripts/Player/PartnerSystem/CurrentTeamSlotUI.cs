using System.Collections;
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
        UpdatePartners();
        UpdateImages();
    }

    public void UpdatePartners()
    {
        for(int i = 0; i < partnerManager.allPartners.Count; ++i)
        {
            if (partnerManager.allPartners[i].W)
            {
                partnerW = partnerManager.allPartners[i];
            }

            if (partnerManager.allPartners[i].Q)
            {
                partnerQ = partnerManager.allPartners[i];
            }

            /*if (partnerManager.allPartners[i].E)
            {
                partnerE = partnerManager.allPartners[i];
            }*/
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
        /*if (partnerE != null)
        {
            eImage.sprite = partnerE.image;
            eImage.enabled = true;
        }*/
    }
}
