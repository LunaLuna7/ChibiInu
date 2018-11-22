using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraSwitch : MonoBehaviour {

    private SwitchCamera sw;
    public CharacterController2D characterController2D;
    public int cameraRight;
    public int cameraLeft;

    // Use this for initialization
    void Start () {
        sw = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SwitchCamera>();

        characterController2D = GetComponent<CharacterController2D>();
	}
	
	// Update is called once per frame
	void Update () {

         if (!characterController2D.m_OnOtherCamera &&  characterController2D.m_FacingRight)
         {
            sw.ChangeCamera(cameraRight);
         }
            else if (!characterController2D.m_OnOtherCamera && !characterController2D.m_FacingRight)
         {
            sw.ChangeCamera(cameraLeft);
         }
	}
}
