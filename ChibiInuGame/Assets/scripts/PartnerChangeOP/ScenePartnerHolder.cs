using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePartnerHolder : MonoBehaviour {
	//the partner image players see during the game, not the actual data
	public Dictionary<SkillSlot, GameObject> activePartners = new Dictionary<SkillSlot, GameObject>();
	public GameObject partnerOne;
	public GameObject partnerTwo;

	// Use this for initialization
	public void Awake()
	{
		activePartners.Clear();
		activePartners.Add(SkillSlot.FirstSlot, partnerOne);
		activePartners.Add(SkillSlot.SecondSlot, partnerTwo);
	}

	public void ChangePartnerImage(SkillSlot slot, Sprite image)
	{
		if(activePartners.ContainsKey(slot))
			activePartners[slot].GetComponent<SpriteRenderer>().sprite = image;
		else
			Debug.LogError("ScenePartnerHolder: " + slot.ToString()+" is not in the keys");
	}
	
	//hide partners temporarily
	public void HidePartners()
	{
		foreach(GameObject partner in activePartners.Values)
		{
			try{
				partner.SetActive(false);
			}catch{
				Debug.LogError("ScenePartnerHolder:HidePartners(): Have you set the partner Images in inspector?");
			}
		}
	}

	//Show partners, called after hide
	public void ShowPartners()
	{
		foreach(GameObject partner in activePartners.Values)
		{
			try{
				partner.SetActive(true);
			}catch{
				Debug.LogError("ScenePartnerHolder:ShowPartners(): Have you set the partner Images in inspector?");
			}
		}
	}

}
