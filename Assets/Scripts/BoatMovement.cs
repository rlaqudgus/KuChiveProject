using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public float BoatSpd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal") * BoatSpd * Time.deltaTime;
        float vAxis = Input.GetAxisRaw("Vertical") * BoatSpd * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + hAxis, transform.position.y + vAxis);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0.05f) pos.x = 0.05f;
        if (pos.x > 0.9f) pos.x = 0.9f;
        if (pos.y < -0.1f) pos.y = -0.1f;
        if (pos.y > 0.55f) pos.y = 0.55f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void Fishing()
    {

    }
}
