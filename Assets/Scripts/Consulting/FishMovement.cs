using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FishMovement : MonoBehaviour
{
    public bool caught;
    [SerializeField] float FishSpd;
    [SerializeField] float Duration;
    [SerializeField] float CoolTime;
    [SerializeField] float ShadowTime;
    [SerializeField] Sprite bad_img;
    public bool isGood;
    enum State { RIGHT, LEFT, STOP};
    State state;
    Vector3 velo = Vector3.zero;
    float time;
    float skill_cool;
    SpriteRenderer sr;
    GameObject shadow;
    private void Start()
    {
        caught = false;
        state = State.STOP;
        skill_cool = CoolTime;
        if (isGood)
        {
            shadow = transform.Find("Shadow").gameObject;
            shadow.SetActive(false);
        }
        sr = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(StateMachine());
    }
    private void Update()
    {
        skill_cool += Time.deltaTime;
        if (caught)
        {
            StopAllCoroutines();
            StartCoroutine(Caught());
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            caught = false;
        }
        if (isGood && Input.GetKeyDown(KeyCode.Alpha3) && skill_cool > CoolTime) 
        {
            StartCoroutine(ViewShadow());
        }
    }
    IEnumerator ViewShadow()
    {
        shadow.transform.position = new Vector2(transform.position.x, -4f);
        shadow.SetActive(true);
        yield return new WaitForSeconds(ShadowTime);
        shadow.SetActive(false);
    }
    public void Dive(bool B_good)
    {
        StopAllCoroutines();
        if (!B_good) GetComponent<SpriteRenderer>().sprite = bad_img;
        StartCoroutine(DIVE(B_good));
    }
    IEnumerator DIVE(bool B_good)
    {
        float dive_depth = Random.Range(-6.0f, -8.0f);
        while (transform.position.y > dive_depth)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - FishSpd * Time.deltaTime * 3);
            yield return null;
        }
        if (!B_good) isGood = false;
        StartCoroutine(StateMachine());
    }
    public void CommunicationBall(Vector3 pos)
    {
        if (!caught && transform.position.y >-9f)
        {
            StopAllCoroutines();
            StartCoroutine(Hoi(pos));
        }
    }
    IEnumerator Hoi(Vector3 pos)
    {
        while(transform.position != pos)
        {
            transform.position = new Vector2(transform.position.x + Time.deltaTime * (transform.position.x < pos.x ? 1 : -1),
                transform.position.y + Time.deltaTime * (transform.position.y < pos.y && transform.position.y < -6f ? 1 : -1));
            yield return null;
        }
    }
    public void BallExit()
    {
        StopAllCoroutines();
        StartCoroutine(StateMachine());
    }
    IEnumerator RIGHT()
    {
        transform.localScale = new Vector3(-1, 1, 1);
        float run_time = 0f;
        while (run_time < time)
        {
            run_time += Time.deltaTime;
            transform.position = new Vector2(transform.position.x + FishSpd * Time.deltaTime, transform.position.y);
            yield return null;
        }
        State next_state = (State)Random.Range(0, 3);
        if (transform.position.x > 7) next_state = State.LEFT;
        ChangeState(next_state);
    }
    IEnumerator LEFT()
    {
        transform.localScale = new Vector3(1, 1, 1);
        float run_time = 0f;
        while (run_time < time)
        {
            run_time += Time.deltaTime;
            transform.position = new Vector2(transform.position.x - FishSpd * Time.deltaTime, transform.position.y);
            yield return null;
        }
        State next_state = (State)Random.Range(0, 3);
        if (transform.position.x < -7) next_state = State.RIGHT;
        ChangeState(next_state);
    }
    IEnumerator STOP()
    {
        yield return new WaitForSeconds(time);
        State next_state = (State)Random.Range(0, 3);
        ChangeState(next_state);
    }
    IEnumerator Caught()
    {
        if (isGood) shadow.SetActive(false);
        float run_time = 0f;
        while (run_time < 0.4f)
        {
            run_time += Time.deltaTime;
            transform.Rotate(Vector3.forward * Time.deltaTime * -100f*transform.localScale.x);
            yield return null;
        }
        for (int i=0; i<2; i++)
        {
            run_time = 0f;
            while (run_time < 0.1f)
            {
                run_time += Time.deltaTime;
                transform.Rotate(Vector3.forward * Time.deltaTime * 100f * transform.localScale.x);
                yield return null;
            }
            run_time = 0f;
            while (run_time < 0.1f)
            {
                run_time += Time.deltaTime;
                transform.Rotate(Vector3.forward * Time.deltaTime * -100f * transform.localScale.x);
                yield return null;
            }
        }
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        for (int i=10;i>=0;i--)
        {
            float f = i / 10f;
            Color c = sr.material.color;
            c.a = f;
            sr.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
        if (isGood) ConsultingManager.AddFish();
    }
    IEnumerator StateMachine()
    {
        while (!caught)
        {
            time = Random.Range(0.0f, Duration);
            yield return StartCoroutine(state.ToString());
        }
    }

    void ChangeState(State new_state)
    {
        state = new_state;
    }
}