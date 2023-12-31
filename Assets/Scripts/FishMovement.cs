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
    [SerializeField] bool isGood;
    enum State { RIGHT, LEFT, STOP};
    State state;
    Vector3 velo = Vector3.zero;
    float time;
    SpriteRenderer sr;
    private void Start()
    {
        caught = false;
        state = State.STOP;
        sr = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(StateMachine());
    }
    private void Update()
    {
        if (caught)
        {
            StopAllCoroutines();
            StartCoroutine(Caught());
            caught = false;
        }
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
        else ConsultingManager.SubFish();
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