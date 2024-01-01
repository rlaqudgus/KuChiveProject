using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] List<GameObject> GameTiles = new List<GameObject>();
    public float TileSpeed;
    [SerializeField] GameObject initPos;
    [SerializeField] Transform parent;
    public List<GameObject> instantiatedTiles = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var tile in instantiatedTiles) 
        {
            if (tile != null)
            {
                tile.GetComponent<TileMove>().tileSpeed = TileSpeed;

            }
        }
    }

    void InstantiateTile()
    {
        int randIndex = Random.Range(0, GameTiles.Count);
        GameObject tileObj = Instantiate(GameTiles[randIndex],initPos.transform.position, Quaternion.identity, parent);
        instantiatedTiles.Add(tileObj);
        //tileObj.GetComponent<TileMove>().tileSpeed = TileSpeed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Ground")
        {
            InstantiateTile();
        }
    }
}
