using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float PlayerSpd;
    public bool isDialogueMode;
    float vAxis;
    Animator animator;
    
    // Start is called before the first frame update
    void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDialogueMode)
        {
            AnimParams();
            KeyInput();
            Move();
        }
        
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "KUI")
        {
            isDialogueMode = true;
        }
    }
}
