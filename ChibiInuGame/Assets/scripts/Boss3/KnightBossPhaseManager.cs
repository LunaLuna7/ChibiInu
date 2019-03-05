using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//describe the behaviour of each phase. The changing of phase is called by KnightBossHealth
public class KnightBossPhaseManager : MonoBehaviour {
	[Header("Phase Object")]
	public GameObject horizontalSheildGroup;
    public GameObject[] horizontalSheildList;
	public GameObject verticalSheildGroup;
    public GameObject[] verticalSheildList;
    public GameObject laserGroup;
    public GameObject[] laserList;


	[Header("Phase Info")]
	private int phase = 1;
    public int shieldSwitchingInterval;
	private float shieldSwitchingTimer;
	private bool currentShieldHorizontal = false;

	public float laserGroupRotateSpeed;
	private float laserXScale;
	private bool startRotatingLaser = false;


	void Awake () {
		laserXScale = laserList[0].transform.localScale.x;
	}
	
	void Start()
	{
		Reset();
	}

	// Update is called once per frame
	void Update () {
		if(phase == 2)
		{
			//check switching shield
			shieldSwitchingTimer += Time.deltaTime;
			if(shieldSwitchingTimer > shieldSwitchingInterval)
			{
				shieldSwitchingTimer = 0;
				SwitchShield();
			}
		}else if(phase == 3 && startRotatingLaser)
		{
			laserGroup.transform.Rotate(0, 0, laserGroupRotateSpeed* Time.deltaTime);
		}
	}

	public void EnterPhase(int num)
	{
		phase = num;
		if(phase == 2)
		{
			//phase 2
			SwitchShield();
		}else if(phase == 3){
			//phase 3
			StartCoroutine(TurnOnLaser());
		}
	}

	public int GetPhase()
	{
		return phase;
	}

	//reset to phase 1
	public void Reset()
	{
		phase = 1;
		//reset shield
		horizontalSheildGroup.SetActive(true);
		horizontalSheildList[0].SetActive(false);
		horizontalSheildList[0].SetActive(false);
		verticalSheildGroup.SetActive(true);
		verticalSheildList[0].SetActive(false);
		verticalSheildList[1].SetActive(false);
		shieldSwitchingTimer = 0;
		currentShieldHorizontal = false;
		//reset laser
		laserGroup.SetActive(false);
		laserGroup.transform.eulerAngles = Vector3.zero;
		startRotatingLaser = false;

	}

	//======================================================================
	//Phase2: Shield
	//======================================================================
	private void SwitchShield()
	{
		if(currentShieldHorizontal)
		{
			//hide horzontal shield and show vertical shield
			StartCoroutine(HideShield(horizontalSheildList[0]));
			StartCoroutine(HideShield(horizontalSheildList[1]));
			StartCoroutine(ShowShield(verticalSheildList[0]));
			StartCoroutine(ShowShield(verticalSheildList[1]));
		}else{
			//show horzontal shield and hide vertical shield
			StartCoroutine(ShowShield(horizontalSheildList[0]));
			StartCoroutine(ShowShield(horizontalSheildList[1]));
			StartCoroutine(HideShield(verticalSheildList[0]));
			StartCoroutine(HideShield(verticalSheildList[1]));
		}
		currentShieldHorizontal = !currentShieldHorizontal;
	}

	private IEnumerator ShowShield(GameObject shield)
	{
		float totalTime = 0.5f;
		shield.SetActive(true);
		//showing shield
		SpriteRenderer sprite = shield.GetComponent<SpriteRenderer>();
		Color currentColor = sprite.color;
		float timeInverval = 0.02f;

		currentColor.a = 0;
		sprite.color = currentColor;
		for(float x = 0; x< totalTime; x+= timeInverval)
		{
			currentColor.a = x/totalTime;
			sprite.color = currentColor;
			yield return new WaitForSeconds(timeInverval);
		}
		currentColor.a = 1;
		sprite.color = currentColor;
	}

	private IEnumerator HideShield(GameObject shield)
	{
		//if shield is already hiden, do nothing
		if(shield.activeSelf == false)
			yield break;
		float totalTime = 0.5f;
		//hiding shield
		SpriteRenderer sprite = shield.GetComponent<SpriteRenderer>();
		Color currentColor = sprite.color;
		float timeInverval = 0.02f;

		currentColor.a = 1;
		sprite.color = currentColor;
		for(float x = totalTime; x> 0; x-= timeInverval)
		{
			currentColor.a = x/totalTime;
			sprite.color = currentColor;
			yield return new WaitForSeconds(timeInverval);
		}
		currentColor.a = 0;
		sprite.color = currentColor;
		//inactive shield
		shield.SetActive(false);
	}

	//======================================================================
	//Phase3: Laser
	//======================================================================
	private IEnumerator TurnOnLaser()
	{
		laserGroup.SetActive(true);
		horizontalSheildGroup.SetActive(false);
		verticalSheildGroup.SetActive(false);
		//turn on all lasers
		float totalTime = 1;
		float timeInverval = 0.02f;
		//hide all lasers
		Vector3 scale = laserList[0].transform.localScale;
		scale.x = 0;
		for(int x = 0; x<laserList.Length; ++x)
		{
			laserList[x].transform.localScale = scale;
			//don't turn on laser at the beginning, so player has time to escape
			laserList[x].GetComponent<Collider2D>().enabled = false;
		}
		//turning all lasers on
		for(float time = 0; time < totalTime; time += timeInverval)
		{
			scale.x = laserXScale * time / totalTime;
			for(int x = 0; x<laserList.Length; ++x)
				laserList[x].transform.localScale = scale;
			yield return new WaitForSeconds(timeInverval);
		}
		scale.x = laserXScale;
		for(int x = 0; x<laserList.Length; ++x)
		{
			laserList[x].transform.localScale = scale;
			laserList[x].GetComponent<Collider2D>().enabled = true;
		}
		startRotatingLaser = true;
	}

}
