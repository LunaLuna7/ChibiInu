using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Sciptable Object that contains Partner's data
/// </summary>


[CreateAssetMenu(menuName = "PartnerInfo")]
public class PartnerInfo : ScriptableObject {
    public string name;
    public int partnerId;
    [TextArea(3, 10)]
    public string skillInfo;
    public Sprite image;
    public Sprite skillImage;
    public RuntimeAnimatorController partnerAnim;
}
