using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] Vector3 offset;
    Transform playerPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = playerPos.position + offset;
    }
}
