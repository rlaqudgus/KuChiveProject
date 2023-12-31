using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMove : MonoBehaviour
{
    public float tileSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movement = tileSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2(transform.position.x - movement, transform.position.y);
        transform.position = newPos;
    }
}
