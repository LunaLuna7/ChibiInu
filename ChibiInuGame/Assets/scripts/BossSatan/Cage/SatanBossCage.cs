using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatanBossCage : MonoBehaviour {

    [SerializeField]private float health;
	public float maxHealth;
	[System.NonSerialized]public float timeTrack;
	public float timeBeforeDamageAgain;
	private bool broken = false;
	public SatanBossPhaseManager satanBossPhaseManager;
    private SpriteRenderer spriteRender;

    //movement 
	public bool moving;
	public float moveSpeed;
    private float originalSpeed;
	public Transform[] positionList;
	private int currentPositionIndex;

    [Header("UI")]
    public Image healthBar;
    public GameObject healthUI;

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        if (spriteRender == null)
            Debug.LogError("Need sprite component");
        health = maxHealth;
        healthBar.fillAmount = health / maxHealth;
    }

    void OnEnable()
	{
		Reset();
	}

	private float ratio = 0;
	private float totalLength;
	public void Update()
	{
		if(moving && positionList.Length > 1)
		{
			int next = (currentPositionIndex + 1)% positionList.Length;
			transform.position = Vector3.Lerp(positionList[currentPositionIndex].position, positionList[next].position, ratio);
			//change ratio
			ratio += (moveSpeed * Time.deltaTime)/totalLength;
			if(ratio > 1)
			{
				ratio = 0;
				currentPositionIndex = next;
				next = (currentPositionIndex + 1)% positionList.Length;
				totalLength = Vector2.Distance(positionList[currentPositionIndex].position, positionList[next].position);
			}
		}
	}
	
	public void Reset()
	{
        spriteRender.enabled = true;
        originalSpeed = moveSpeed;
		health = maxHealth;
        healthBar.fillAmount = health / maxHealth;
		broken = false;
		currentPositionIndex = 0;
		ratio = 0;
		if(moving && positionList.Length > 0)
		{
			transform.position = positionList[0].position;
			int next = (currentPositionIndex + 1)% positionList.Length;
			totalLength = Vector2.Distance(positionList[currentPositionIndex].position, positionList[next].position);
		}
	}

	public void TakeDamage(int amount)
	{
		health -= amount;
        SoundEffectManager.instance.Play("CageDamage");
        healthBar.fillAmount = health / maxHealth;
        if (health > 0)
        {
            StartCoroutine(BlinkSprite());
            StartCoroutine(RunAway());
        }

        if (health <= 0 && !broken)
		{
			satanBossPhaseManager.GoToNextPhase();
            if (satanBossPhaseManager.GetPhaseMap() == 3)
                healthUI.SetActive(false);
			broken = true;
		}
	}

    IEnumerator RunAway()
    {
        moveSpeed = originalSpeed*3;
        yield return new WaitForSeconds(.5f);
        moveSpeed = originalSpeed;
    }

    IEnumerator BlinkSprite()
    {
        for (int i = 0; i < 8; ++i)
        {
            yield return new WaitForSeconds(.05f);
            if (spriteRender.enabled == true)
            {
                spriteRender.enabled = false;
            }
            else
            {
                spriteRender.enabled = true;
            }
        }
        spriteRender.enabled = true;
    }

}
