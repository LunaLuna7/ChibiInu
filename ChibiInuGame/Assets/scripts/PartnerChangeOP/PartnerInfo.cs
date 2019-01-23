using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PartnerInfo")]
public class PartnerInfo : ScriptableObject {
    public string name;
    public int partnerId;
    [TextArea(3, 10)]
    public string skillInfo;
    public Sprite image;

}
