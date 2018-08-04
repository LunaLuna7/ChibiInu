using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[System.Serializable]
[CreateAssetMenu(menuName = "Partner")]
public class Partner : ScriptableObject {

    public string partnerName;
    public int partnerID;
    public bool unlocked;
    public bool selected;
    public Sprite image;
    [TextArea(3,10)]
    public string skillInfo;
}
