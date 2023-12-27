using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float PlayerSpd;
    [SerializeField] float JumpPow;
    float vAxis;
    bool isJumping;
    Animator animator;
    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Awake()
    {
        animator = this.GetComponent<Animator>();
        rigid = this.GetComponent<Rigidbody2D>();
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        AnimParams();
        KeyInput();
        Move();
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    //������ transform���� �����ϰ� ���� ������ �� �ֱ⸸ �ϸ� ��
    //scale x ��ȣ �������� sprite ���� �ٲٱ�
    void Move()
    {
        Vector2 newPos = new Vector2(transform.position.x + vAxis, transform.position.y);
        transform.position = newPos;

        if(vAxis<0)
        {
            Vector3 newScale = new Vector3(-1, 1, 1);
            transform.localScale = newScale;
        }
        else if(vAxis>0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void KeyInput()
    {
        vAxis = Input.GetAxisRaw("Horizontal") * PlayerSpd * Time.deltaTime;
    }

    void AnimParams()
    {
        animator.SetBool("isWalking", vAxis != 0);
    }
    void Jump()
    {
        if (!isJumping)
        {
            rigid.AddForce(Vector2.up * JumpPow, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isJumping = false;
        }
    }
}
