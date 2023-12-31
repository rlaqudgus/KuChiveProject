using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Vector3 pos1;
    Vector3 pos2;
    Vector3 target;
    [SerializeField] float moveSpd;
    [SerializeField] Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        pos1 = transform.position;
        pos2 = transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (pos1 == transform.position) { target = pos2; }

        if (pos2 == transform.position) { target = pos1; }

        transform.position = Vector2.MoveTowards(transform.position, target, moveSpd * Time.deltaTime);
    }
}
