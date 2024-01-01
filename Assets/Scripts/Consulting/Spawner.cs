using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject FishPrefab_good;
    [SerializeField] float CoolTime;
    float time;
    private void Start()
    {
        time = CoolTime;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Alpha2) && time > CoolTime)
        {
            Spawn();
            time = 0;
        }
    }

    void Spawn()
    {
        GameObject Fish = Instantiate(FishPrefab_good);
        FishMovement Fish_M = Fish.GetComponent<FishMovement>();

        Fish.transform.position = transform.position;
        if (Random.Range(0, 2) == 0) Fish_M.Dive(true);
        else Fish_M.Dive(false);
    }


}
