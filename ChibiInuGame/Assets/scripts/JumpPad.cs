using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {

    public float jumpPower;
    public Animator jumpAnim;
    public bool rightJumpad;
    public bool leftUpJumpda;
    
    private void Start()
    {
        jumpAnim = GetComponentInChildren<Animator>();
    }

    //============================================================
    //On Collision with player, set velocity to player's rigidBody
    //============================================================
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("satan is dumb");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("SATAN IS REALLY DUMB");
            if (Mathf.Abs(collision.transform.position.y - transform.position.y) < .7f) //hit on side
                return;

            SoundEffectManager.instance.Play("Trampoline");
            StartCoroutine(JumpAnimation());
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rightJumpad)
                rb.velocity = new Vector2(jumpPower, 0);

            else if (leftUpJumpda)
                rb.velocity = new Vector2(-jumpPower * 4, jumpPower);

            else
                rb.velocity = new Vector2(0, jumpPower);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterController2D cc = collision.gameObject.GetComponent<CharacterController2D>();
            StopCoroutine(cc.JumpadOffOverTime());
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
