using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHoi : MonoBehaviour
{
    [SerializeField] float HoiSpd;
    [SerializeField] float CommunicateTime;
    [SerializeField] float CoolTime;
    public bool isRight;
    bool useSkill;
    bool iscommunication;
    float time;

    private void Start()
    {
        isRight = true;
        useSkill = false;
        iscommunication = false;
        time = CoolTime;
    }

    private void Update()
    {
        time+= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Alpha1) && time > CoolTime) useSkill = true;
        if (Input.GetKey(KeyCode.Alpha1)&&useSkill) SkillMove();
        if (Input.GetKeyUp(KeyCode.Alpha1)&&useSkill) StartCoroutine(CommunicationSkill());
    }

    void SkillMove()
    {
        if(transform.position.x > 7f) isRight = false;
        else if(transform.position.x < -7f) isRight = true;

        if(isRight) transform.position = new Vector2(transform.position.x + HoiSpd * Time.deltaTime, transform.position.y);
        else transform.position = new Vector2(transform.position.x - HoiSpd * Time.deltaTime, transform.position.y);
    }

    IEnumerator LEFT()
    {
        iscommunication = false;
        while (transform.position.x > -7f)
        {
            transform.position = new Vector2(transform.position.x - HoiSpd * Time.deltaTime, transform.position.y);
            yield return null;
        }
        useSkill = false;
        time = 0;
    }
    IEnumerator CommunicationSkill()
    {
        iscommunication = true;
        while (this.transform.position.y > -4)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - HoiSpd * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(CommunicateTime);
        while (this.transform.position.y < 2)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + HoiSpd * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(LEFT());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fish" && !collision.gameObject.GetComponent<FishMovement>().isGood && iscommunication)
        {
            collision.gameObject.GetComponent<FishMovement>().CommunicationBall(this.transform.position + new Vector3(0, -4, 0));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fish" && !collision.gameObject.GetComponent<FishMovement>().isGood && !iscommunication)
        {
            collision.gameObject.GetComponent<FishMovement>().BallExit();
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
