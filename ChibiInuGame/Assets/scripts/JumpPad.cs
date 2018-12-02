using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {


    public float jumpPower;
    public Animator jumpAnim;
    public bool rightJumpad;
    public bool leftUpJumpda;
    private SoundEffectManager soundEffectManager;


    private void Start()
    {
        soundEffectManager = GameObject.FindGameObjectWithTag("SoundEffect").GetComponent<SoundEffectManager>();
        jumpAnim = GetComponentInChildren<Animator>();
    }

    //============================================================
    //On Collision with player, add velocity to player's rigidBody
    //============================================================
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            soundEffectManager.Play("Trampoline");
            StartCoroutine(JumpAnimation());
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            CharacterController2D cc = collision.gameObject.GetComponent<CharacterController2D>();

            if(rightJumpad)
                rb.velocity = new Vector2(jumpPower, 0);

            else if (leftUpJumpda)
                rb.velocity = new Vector2(-jumpPower * 4, jumpPower);
            
            else
                rb.velocity = new Vector2(0, jumpPower);

            cc.JumpadOn();
        }
    }

    //==============================================
    //Handles animator booleans to trigger animation
    //==============================================
    IEnumerator JumpAnimation()
    {
        jumpAnim.SetBool("PlayerJumped", false);
        jumpAnim.SetBool("PlayerJumped", true);
        yield return new WaitForSeconds(.1f);
        jumpAnim.SetBool("PlayerJumped", false);
    }
}
