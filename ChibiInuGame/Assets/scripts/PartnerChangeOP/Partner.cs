using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Partner contains the scriptable Object partner data for eahc object, as well as the information that is dependant on each save data file 
/// </summary>

[System.Serializable]
public class Partner {

    public Partner(bool i, bool u)
    {
        inUse = i;
        unlocked = u;

    }

    public bool inUse;
    public bool unlocked;
    public PartnerInfo partnerInfo;

}