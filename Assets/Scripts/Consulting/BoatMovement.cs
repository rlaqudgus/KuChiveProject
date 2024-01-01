using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] float BoatSpd;
    [SerializeField] float CableSpd;
    [SerializeField] float CableEndPoint;
    [SerializeField] Camera MainCamera;
    public bool alive;
    enum State { BOAT, FISHING_DOWN, FISHING_UP, DOWN, UP};
    State state;
    Vector3 velo = Vector3.zero;
    Transform cable;
    Transform hook;
    float time;
    bool _catch;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        state = State.BOAT;
        StartCoroutine(StateMachine());
        cable = this.transform.Find("cable");
        hook = this.transform.Find("hook");
        time = 0;
        _catch = false;
    }
    IEnumerator StateMachine()
    {
        while (ConsultingManager.IsTutorial()) yield return null;
        while (alive)
        {
            yield return StartCoroutine(state.ToString());
        }
    }

    IEnumerator BOAT()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space)) ChangeState(State.DOWN);
        yield return null;
        _catch = false;
    }

    void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal") * BoatSpd * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + hAxis, -5);

        if (hAxis > 0) transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        else if (hAxis < 0) transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0.05f) pos.x = 0.05f;
        else if (pos.x > 0.9f) pos.x = 0.9f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
    IEnumerator DOWN()
    {
        Vector3 target = new Vector3(0, -3 + this.transform.position.y, -10);
        MainCamera.transform.position = Vector3.SmoothDamp(
           MainCamera.transform.position, target, ref velo, 0.1f);
        yield return null;
        if (MainCamera.transform.position == target) ChangeState(State.FISHING_DOWN);
    }
    IEnumerator FISHING_DOWN()
    {
        cable.localScale = new Vector3(1, 0.5f * (1f + time * CableSpd), 0);
        cable.localPosition = new Vector3(0, 3.3f - (time * CableSpd / 2f), 0);
        hook.localPosition = new Vector3(0, 2.8f - (time * CableSpd) * 0.93f, 0);
        time += Time.deltaTime;
        if (hook.localPosition.y <= CableEndPoint) ChangeState(State.FISHING_UP);
        if (_catch)
        {
            yield return new WaitForSeconds(1f);
            ChangeState(State.FISHING_UP);
            _catch = false;
        }
        yield return null;
    }
    IEnumerator FISHING_UP()
    {
        cable.localScale = new Vector3(1, 0.5f * (1f + time * CableSpd), 0);
        cable.localPosition = new Vector3(0, 3.3f - (time * CableSpd / 2f), 0);
        hook.localPosition = new Vector3(0, 2.8f - (time * CableSpd) * 0.93f, 0);
        time -= Time.deltaTime;
        if (hook.localPosition.y >= 2.8f) ChangeState(State.UP);
        if (_catch)
        {
            yield return new WaitForSeconds(1f);
            _catch = false;
        }
        yield return null;
    }
    IEnumerator UP()
    {
        time = 0;
        MainCamera.transform.position = Vector3.SmoothDamp(
           MainCamera.transform.position, new Vector3(0, 0, -10), ref velo, 0.1f);
        yield return null;
        if (MainCamera.transform.position == new Vector3(0, 0, -10))
        {
            ChangeState(State.BOAT);
            ConsultingManager.SubChance();
        }
    }

    void ChangeState(State new_state)
    {
        this.state = new_state;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Fish")
        {
            obj.GetComponent<FishMovement>().caught = true;
            _catch = true;
        }
    }
}