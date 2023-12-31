using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunGamePlayer : MonoBehaviour
{
    [SerializeField] float jumpPower;
    Rigidbody2D rb;
    bool isGrounded;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        //AnimControl();
    }

    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGrounded = false;
            anim.SetBool("Jump",!isGrounded);
        }
    }

    void AnimControl()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Ground")
        {
            isGrounded = true;
            anim.SetBool("Jump", !isGrounded);
        }
    }
}
