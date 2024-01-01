using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shieldcircle : MonoBehaviour
{
    [SerializeField] Transform rotationCenter;
    [SerializeField] float rotationRadius = 2f, angularSpeed = 2f;
    float angle = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Cos(angle) * rotationRadius;
        float y = Mathf.Sin(angle) * rotationRadius;

        transform.position = rotationCenter.position + new Vector3(x, y, 0);

        angle+= angularSpeed*Time.deltaTime;

        if (angle > Mathf.PI * 2)
        {
            angle -= Mathf.PI * 2;
        }
    }
}
