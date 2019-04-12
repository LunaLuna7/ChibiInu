using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// UIPartnerBook controls the Canvas UI Book and updates the book's pages depending on user input and partner's info
/// </summary>

    //TODO: make your own button active and deactivate based on if the partner is in used and update page accordingle!!!
public class UIPartnerBook : MonoBehaviour {

    public PartnerManager partnerManager;
    [HideInInspector] public Partner currentPartner;
    public CharacterController2D characterController;

    [Space]
    [Header("Book Page UI Elements")]
    public Text partnerName;
    public Image partnerPicture;
    public RawImage partnerSkillVideo;
    public Text partnerSkillInfo;
    public Button rightArrow;
    public Button leftArrow;
    public GameObject lBButton;
    public GameObject rBButton;
    public GameObject jKeyButton;
    public GameObject kKeyButton;
    public GameObject firstPartnerButtonSummon;
    public GameObject secondPartnerButtonSummon;
    public GameObject callBackPartnerButton;
    public Animator BookWindow;
    private VideoPlayer videoPlayer;
    private PlayVideoManager playVideoManager;
    
    private bool joyStickToNeutral;
    public bool deadInputTimeElapsed;
    public bool openWithXbox;
    public bool openWithKeyboard;

    private void Start()
    {
        playVideoManager = GetComponent<PlayVideoManager>();
        videoPlayer = GetComponent<VideoPlayer>();
        deadInputTimeElapsed = true;
        joyStickToNeutral = false;
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") == 0f)
            joyStickToNeutral = true;
        if (deadInputTimeElapsed && (Input.GetKeyDown(KeyCode.A) ||(Input.GetAxis("Horizontal") < 0 && joyStickToNeutral)))
        {
            deadInputTimeElapsed = false;
            joyStickToNeutral = false;
            LeftArrow();
        }

        else if (deadInputTimeElapsed && ( Input.GetKeyDown(KeyCode.D) || (Input.GetAxis("Horizontal") > 0 && joyStickToNeutral)))
        {
            deadInputTimeElapsed = false;
            joyStickToNeutral = false;
            RightArrow();
        }

        if (Input.GetButtonDown("PartnerA") || Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Z))
        {
            if (firstPartnerButtonSummon.activeSelf)
                SummonPartnerButton(0);
            else
                UnSummonPartnerButton();
        }
        else if (Input.GetButtonDown("PartnerB") || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.X))
        {
            if (secondPartnerButtonSummon.activeSelf)
                SummonPartnerButton(1);
            else
                UnSummonPartnerButton();
        }

        if (CheckPoint.onDialogue)
        {
            this.gameObject.SetActive(false);
        }
            
    }

        //Updates the individual UI objects on teh scene canvas in respect to the SCriptable Object of the current partner
    private void UpdatePartnerPage(int nextPartner)
    {
        StartCoroutine(PageFlip(nextPartner));
        StartCoroutine(UpdatePage(nextPartner));

    }

    IEnumerator PageFlip(int nextPartner)
    {
        if (currentPartner != partnerManager.partners[nextPartner])
            SoundEffectManager.instance.Play("PageFlip");
        BookWindow.SetBool("TurnPage", true);
        yield return new WaitForSeconds(.2f);
        BookWindow.SetBool("TurnPage", false);
    }

    IEnumerator UpdatePage(int nextPartner)
    {
        ActivePage(false);
       
        yield return new WaitForSeconds(.8f);
        ActivePage(true);
        //deadInputTimeElapsed = true;
        StopAllCoroutines();
        playVideoManager.StopVideo();
        StartCoroutine(DeadInputTime());
        StartCoroutine(playVideoManager.PlayVideo());
        currentPartner = partnerManager.partners[nextPartner];
        partnerName.text = currentPartner.partnerInfo.name.ToString();
        partnerPicture.sprite = currentPartner.partnerInfo.image;
        videoPlayer.clip = currentPartner.partnerInfo.skillVideo;
        partnerSkillInfo.text = currentPartner.partnerInfo.skillInfo;

        //Add control pictures
        if (openWithXbox)
        {
            lBButton.SetActive(true);
            rBButton.SetActive(true);
            jKeyButton.SetActive(false);
            kKeyButton.SetActive(false);
        }
        else if (openWithKeyboard)
        {
            lBButton.SetActive(false);
            rBButton.SetActive(false);
            jKeyButton.SetActive(true);
            kKeyButton.SetActive(true);
        }

        if (!currentPartner.inUse)
        {
            callBackPartnerButton.SetActive(false);
            firstPartnerButtonSummon.SetActive(true);
            if (partnerManager.secondPartnerSlotUnlock && !callBackPartnerButton.activeSelf)
                secondPartnerButtonSummon.SetActive(true);
            else
            {
                secondPartnerButtonSummon.SetActive(false);
                rBButton.SetActive(false);
                kKeyButton.SetActive(false);
            }
        }
        else
        {
            callBackPartnerButton.SetActive(true);
            firstPartnerButtonSummon.SetActive(false);
            secondPartnerButtonSummon.SetActive(false);
            lBButton.SetActive(false);
            rBButton.SetActive(false);
            jKeyButton.SetActive(false);
            kKeyButton.SetActive(false);
        }

    }

    private void ActivePage(bool active)
    {

        partnerName.gameObject.SetActive(active);
        partnerPicture.gameObject.SetActive(active);
        partnerSkillVideo.gameObject.SetActive(active);
        //partnerSkillInfo.gameObject.SetActive(active);
        firstPartnerButtonSummon.gameObject.SetActive(active);
        secondPartnerButtonSummon.gameObject.SetActive(active);
        callBackPartnerButton.gameObject.SetActive(active);

        lBButton.SetActive(active);
        rBButton.SetActive(active);
        jKeyButton.SetActive(active);
        kKeyButton.SetActive(active);

    }

    //Checks if there is at least the first player unlocked. If so then activates the Book on Canvas. Also freezes player movement
    void OnEnable()
    {
        characterController.m_Paralyzed = true;
        if (partnerManager.partners[0].unlocked)
        {
            SoundEffectManager.instance.Play("OpenBook");
            currentPartner = partnerManager.partners[0];
            UpdatePartnerPage(currentPartner.partnerInfo.partnerId);
            //if (partnerManager.secondPartnerSlotUnlock && !callBackPartnerButton.activeSelf)
                //secondPartnerButtonSummon.SetActive(true);
            gameObject.SetActive(true);
        }
        else
            gameObject.SetActive(false);
    }

    //Unfreeze player
    private void OnDisable()
    {
        characterController.m_Paralyzed = false;
        if (!partnerManager.IsActive(2))
            characterController.ShieldOff();
    }

    //Moves right(+1 index) through the partnerManager.cs allPartners list of Scriptable Partners objects and if unlock it updats the page and currentPartner
    public void RightArrow()
    {
        int nextPartner = (currentPartner.partnerInfo.partnerId + 1) % partnerManager.partners.Count;
        while (!partnerManager.partners[nextPartner].unlocked)
        {
            nextPartner = (nextPartner + 1) % partnerManager.partners.Count; //makes it so we dont indexOutOfBound and loops back through list index
        }

        UpdatePartnerPage(nextPartner);
    }

    //Moves Left(-1 index) through the partnerManager.cs allPartners list of Scriptable Partners objects and if unlock it updats the page and currentPartner
    public void LeftArrow()
    {
        int nextPartner;

        if (currentPartner.partnerInfo.partnerId == 0)
            nextPartner = partnerManager.partners.Count - 1;
        else
            nextPartner = (currentPartner.partnerInfo.partnerId - 1) % partnerManager.partners.Count; //makes it so we dont indexOutOfBound and loops back through list index

        while (!partnerManager.partners[nextPartner].unlocked)
            nextPartner = (nextPartner - 1) % partnerManager.partners.Count;
        
        UpdatePartnerPage(nextPartner);
    }

    //Button press on UI book that will call summonPartner from partnerManager with the currentpartner and skillslot base on 0 or 1 info as param
    public void SummonPartnerButton(int keySlot)
    {
        SoundEffectManager.instance.Play("PartnerSelect");
        if (keySlot == 0)
            partnerManager.SummonPartner(SkillSlot.FirstSlot, currentPartner);
        else
            partnerManager.SummonPartner(SkillSlot.SecondSlot, currentPartner);

        partnerManager.PartnerInUse(currentPartner);
        firstPartnerButtonSummon.SetActive(false);
        secondPartnerButtonSummon.SetActive(false);
        callBackPartnerButton.SetActive(true);
        partnerManager.LimitPlayerJump(partnerManager.TripleJumpPartnerCapacity());
    }

    //called when callback UI is press(ToDO: make it so either summonA button appears or summonB and summonA appears)
    public void UnSummonPartnerButton()
    {
        SoundEffectManager.instance.Play("PartnerDeselect");
        partnerManager.UnSummonPartner(currentPartner);
        callBackPartnerButton.SetActive(false);
        firstPartnerButtonSummon.SetActive(true);

        if (openWithKeyboard)
            jKeyButton.SetActive(true);
        else if (openWithXbox)
            lBButton.SetActive(true);

        if (partnerManager.secondPartnerSlotUnlock)
        {
            secondPartnerButtonSummon.SetActive(true);
            if (openWithKeyboard)
                kKeyButton.SetActive(true);
            else if (openWithXbox)
                rBButton.SetActive(true);
        }

        partnerManager.LimitPlayerJump(partnerManager.TripleJumpPartnerCapacity());
    }

    //Delay player able to flip page after the page data has loaded
    IEnumerator DeadInputTime()
    {
        yield return new WaitForSeconds(.2f);
        deadInputTimeElapsed = true;
    }
}
