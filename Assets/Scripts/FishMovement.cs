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
    enum State { RIGHT, LEFT, STOP};
    State state;
    Vector3 velo = Vector3.zero;
    float time;
    private void Start()
    {
        caught = false;
        state = State.STOP;
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
