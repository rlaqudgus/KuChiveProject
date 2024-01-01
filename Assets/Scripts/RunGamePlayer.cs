using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunGamePlayer : MonoBehaviour
{
    [SerializeField] float jumpPower;
    [SerializeField] float shakeTime;
    [SerializeField] float shakeAmount;
    [SerializeField] int jumpCount;
    [SerializeField] ProgressManager pm;
    [SerializeField] GameObject shield;
    int jumpInit;
    Rigidbody2D rb;
    bool isGrounded;
    bool isShieldState;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpInit = jumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    void Jump()
    {
        if (jumpCount != 0 && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpCount--;
            isGrounded = false;
            anim.SetBool("Jump",!isGrounded);
        }
    }

    void DecreaseBar()
    {
        if (pm.fillImg.rectTransform.offsetMax.x - 50 < -700) 
        {
            pm.fillImg.rectTransform.offsetMax = new Vector2(-700, 0);
            return;
        }
        pm.fillImg.rectTransform.offsetMax -= new Vector2(50,0);
    }

    void IncreaseBar()
    {
        if (pm.fillImg.rectTransform.offsetMax.x + 50 > 0)
        {
            pm.fillImg.rectTransform.offsetMax = new Vector2(0, 0);
            return;
        }
        pm.fillImg.rectTransform.offsetMax += new Vector2(50, 0);
    }

    IEnumerator CameraShake(float ShakeAmount, float shakeTime)
    {
        float timer = 0;
        while(timer<=shakeTime)
        {
            Vector2 randomPos = Random.insideUnitCircle * shakeAmount;
            Camera.main.transform.position =
                new Vector3(randomPos.x,randomPos.y,-10);
            timer += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = new Vector3(0, 0, -10);
    }

    IEnumerator ShieldPower()
    {
        isShieldState = true;
        shield.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        shield.gameObject.SetActive(false);
        isShieldState = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            jumpCount = jumpInit;
            anim.SetBool("Jump", !isGrounded);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="PotionItem") 
        {
            collision.gameObject.SetActive(false);
            IncreaseBar();
        }
        if (collision.gameObject.tag == "ShieldItem")
        {
            collision.gameObject.SetActive(false);
            StartCoroutine(ShieldPower());
        }
        if (collision.gameObject.tag == "Enemy")
        {
            if (isShieldState || pm.isFinished)
            {
                return;
            }
            StartCoroutine(CameraShake(shakeTime, shakeAmount));
           
            DecreaseBar();
        }

    }


}
