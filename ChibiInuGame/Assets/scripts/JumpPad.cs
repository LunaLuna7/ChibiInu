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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))// || collision.gameObject.CompareTag("enemy"))
        {

            StartCoroutine(JumpAnimation());
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            CharacterController2D cc = collision.gameObject.GetComponent<CharacterController2D>();

            if(rightJumpad)
                rb.velocity = new Vector2(jumpPower, 0);

            else if (leftUpJumpda)
            {
                rb.velocity = new Vector2(-jumpPower * 4, jumpPower);
            }
            else
                rb.velocity = new Vector2(0, jumpPower);
            cc.JumpadOn();
        }
    }

   
    IEnumerator JumpAnimation()
    {
        jumpAnim.SetBool("PlayerJumped", false);
        jumpAnim.SetBool("PlayerJumped", true);
        yield return new WaitForSeconds(.1f);
        jumpAnim.SetBool("PlayerJumped", false);
    }
}
