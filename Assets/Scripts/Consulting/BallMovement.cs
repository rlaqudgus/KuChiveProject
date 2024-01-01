using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -15f) Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fish" && !collision.gameObject.GetComponent<FishMovement>().isGood)
        {
            rigid.velocity = Vector3.zero;
            rigid.gravityScale = 0.1f;
            collision.gameObject.GetComponent<FishMovement>().CommunicationBall(this.transform.position);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fish" && !collision.gameObject.GetComponent<FishMovement>().isGood)
        {
            collision.gameObject.GetComponent<FishMovement>().CommunicationBall(this.transform.position);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fish" && !collision.gameObject.GetComponent<FishMovement>().isGood)
        {
            collision.gameObject.GetComponent<FishMovement>().BallExit();
        }
    }
}
