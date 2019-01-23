using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePartnerHolder : MonoBehaviour {
	public Dictionary<SkillSlot, GameObject> activePartners = new Dictionary<SkillSlot, GameObject>();
	// Use this for initialization

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
			partner.SetActive(false);
		}
	}

	//Show partners, called after hide
	public void ShowPartners()
	{
		foreach(GameObject partner in activePartners.Values)
		{
			partner.SetActive(true);
		}
	}

}
