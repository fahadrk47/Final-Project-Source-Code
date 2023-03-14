using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    public GameObject[] enemy_tiles;
    public float zSpawn = 0;
    public float tileLength = 28;
    public int numberOfTiles = 5;
    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;

    public int tile_calculator = 0;
    public int hurdles_tiles;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, tilePrefabs.Length));

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 35 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();

        }
    }

    public void SpawnTile(int tileIndex)
    {
        if (tile_calculator > hurdles_tiles)
        {
            GameObject gObj = Instantiate(enemy_tiles[tileIndex], transform.forward * zSpawn, transform.rotation);
            activeTiles.Add(gObj);
            zSpawn += tileLength;
            tile_calculator++;
        }
        else
        {

            GameObject gObj = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
            activeTiles.Add(gObj);
            zSpawn += tileLength;
            tile_calculator++;
        }

    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
