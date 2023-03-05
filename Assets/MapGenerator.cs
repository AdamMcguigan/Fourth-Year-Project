//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MapGenerator : MonoBehaviour
//{
//    public GameObject treePrefab;
//    public int maxTrees = 100;
//    public int mapSize = 100;

//    // Start is called before the first frame update
//    void Start()
//    {
//        GenerateMap();    
//    }

//    void GenerateMap()
//    {
//        //create 2D array to represent the map
//        int[,] map = new int[mapSize, maxTrees];

//        for(int i = 0; i < mapSize; i++)
//        {
//            for(int y = 0; y < mapSize; y++)
//            {
//                map[i,y] = Random.Range(0, 2);
//            }
//        }

//        int treesSpawned = 0;
//        while(treesSpawned < maxTrees)
//        {
//            int x = Random.Range(0, mapSize);
//            int y = Random.Range(0, mapSize);

//            if (map[x,y] == 1)
//            {
//                Vector3 treePos = new Vector3(x, 0, y);
//                Instantiate(treePrefab, treePos, Quaternion.identity);

//                treesSpawned++;
//            }
//        }
//    }


//    // Update is called once per frame
//    void Update()
//    {

//    }
//}

using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Prefab for the tree
    public GameObject treePrefab;

    // Size of the map
    public int mapWidth = 50;
    public int mapHeight = 50;

    // Minimum and maximum number of trees to spawn
    public int minTrees = 10;
    public int maxTrees = 30;

    void Start()
    {
        // Create a plane to represent the ground
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.transform.position = new Vector3(mapWidth / 2, 0, mapHeight / 2);
        ground.transform.localScale = new Vector3(mapWidth, 1, mapHeight);

        // Randomly generate the number of trees to spawn
        int numTrees = Random.Range(minTrees, maxTrees);

        // Spawn the trees at random positions on the map
        for (int i = 0; i < numTrees; i++)
        {
            Vector3 treePos = new Vector3(Random.Range(0, mapWidth), 0, Random.Range(0, mapHeight));
            Instantiate(treePrefab, treePos, Quaternion.identity);
        }
    }
}

