using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] List<GameObject> GameTiles = new List<GameObject>();
    [SerializeField] float TileSpeed;
    [SerializeField] GameObject initPos;
    [SerializeField] Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateTile()
    {
        int randIndex = Random.Range(0, GameTiles.Count);
        GameObject tileObj = Instantiate(GameTiles[randIndex],initPos.transform.position, Quaternion.identity, parent);
        tileObj.GetComponent<TileMove>().tileSpeed = TileSpeed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Ground")
        {
            InstantiateTile();
        }
    }
}
