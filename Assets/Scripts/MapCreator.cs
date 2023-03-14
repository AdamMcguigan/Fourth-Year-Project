using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//                   _____      _____  __________        _________ __________ ___________   _____ ___________________  __________ 
//                  /     \    /  _  \ \______   \       \_   ___ \\______   \\_   _____/  /  _  \\__    ___/\_____  \ \______   \
//                 /  \ /  \  /  /_\  \ |     ___/       /    \  \/ |       _/ |    __)_  /  /_\  \ |    |    /   |   \ |       _/
//                /  / Y    \/    |    \|    |           \     \____|    |   \ |        \/    |    \|    |   /    |    \|    |   \
//               \____ |__  /\____|__  /|____|            \______  /|____|_  //_______  /\____|__  /|____|   \_______  /|____|_  /
//                        \/         \/                          \/        \/         \/         \/                  \/        \/ 
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//  Dear Adam:
//  if you end up looking at this code...
//  When I wrote this code, only god and 
//  I knew how it worked.
//  Now only god knows it!
//
//  Therefore, if you are trying to optimize
//  this code/functions and it fails (most surely)
//  please increase this counter as a
//  warning for next person:
//
//  total_hours_wasted_here = 15
//



[RequireComponent(typeof(MeshFilter))]
public class MapCreator : MonoBehaviour
{

    Mesh mesh;                                  // Variables we need for the map generator
    private int MESH_SCALE = 15;
    public GameObject[] objects;
    [SerializeField] private AnimationCurve heightCurve;
    private Vector3[] vertices;
    private int[] triangles;

    private Color[] colors;
    [SerializeField] private Gradient gradient;

    private float minTerrainheight;
    private float maxTerrainheight;

    public int xSize;
    public int zSize;
    // this is to controll the noise and vertices spikiniess
    public float scale;
    public int octaves;
    public float lacunarity;

    public int seed;

    private float lastNoiseHeight;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateNewMap();
    }

    private void SetNullProperties()                //only use this when i dont use properties in inspector
    {
        if (xSize <= 0) xSize = 100;
        if (zSize <= 0) zSize = 100;
        if (octaves <= 0) octaves = 5;
        if (lacunarity <= 0) lacunarity = 2;
        if (scale <= 0) scale = 70;
    }

    public void CreateNewMap()                      //runss all the functions in order to create a map (absolute god method)
    {
        CreateMeshShape();
        CreateTriangles();
        ColorMap();

        UpdateMesh();
    }

    private void CreateMeshShape()
    {
        Vector2[] octaveOffsets = GetOffsetSeed();     // Creates random seed here
        if (scale <= 0) scale = 0.0001f;

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];        // Create vertices here for the triangles
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float noiseHeight = GenerateNoiseHeight(z, x, octaveOffsets);                // Set height of vertices depending on the noise
                SetMinMaxHeights(noiseHeight);
                vertices[i] = new Vector3(x, noiseHeight, z);
                i++;

            }
        }
    }

    private Vector2[] GetOffsetSeed()
    {
        seed = Random.Range(0, 1000);

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int o = 0; o < octaves; o++)
        {
            float offsetX = prng.Next(-100000, 100000);
            float offsetY = prng.Next(-100000, 100000);
            octaveOffsets[o] = new Vector2(offsetX, offsetY);

        }
        return octaveOffsets;
    }

    private float GenerateNoiseHeight(int z, int x, Vector2[] octaveOffsets)    //we create noise map here like a boss
    {
        float amplitude = 20;
        float frequency = 1;
        float persistence = 0.5f;
        float noiseHeight = 0;


        for (int y = 0; y < octaves; y++)    // loop over octaves here boss
        {
            float mapZ = z / scale * frequency + octaveOffsets[y].y;
            float mapX = x / scale * frequency + octaveOffsets[y].x;

            //The *2-1 is to create a flat floor level
            float perlinValue = (Mathf.PerlinNoise(mapZ, mapX)) * 2 - 1;
            noiseHeight += heightCurve.Evaluate(perlinValue) * amplitude;
            frequency *= lacunarity;
            amplitude *= persistence;
        }
        return noiseHeight;
    }

    private void SetMinMaxHeights(float noiseHeight)   // we set big colours here with gradient
    {
        if (noiseHeight > maxTerrainheight)
            maxTerrainheight = noiseHeight;
        if (noiseHeight < minTerrainheight)
            minTerrainheight = noiseHeight;
    }


    private void CreateTriangles()
    {
        triangles = new int[xSize * zSize * 6]; //2 triangles = 1 quad = 6 points

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < xSize; z++)             //  (ty brackeys)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    private void ColorMap()
    {
        colors = new Color[vertices.Length];
        for (int i = 0, z = 0; z < vertices.Length; z++)        // here we go through points and get colour depending on height
        {
            float height = Mathf.InverseLerp(minTerrainheight, maxTerrainheight, vertices[i].y);
            colors[i] = gradient.Evaluate(height);
            i++;
        }
    }

    private void MapEmbellishments()                //this is for objects <<----- will need to modify this for trees and diffeenrt objects to have much rarer chance of spawning :) (dont forget to attach script to objects fifi)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPt = transform.TransformPoint(mesh.vertices[i]);  // find actual position of vertices in the game
            var noiseHeight = worldPt.y;
            if (System.Math.Abs(lastNoiseHeight - worldPt.y) < 25) // Stop generation if height difference between 2 vertices is too steep
            {

                if (noiseHeight > 10 && noiseHeight < 160)                       // min height for object generation
                {

                    //NORMAL TREE
                    if (Random.Range(1, 8) == 1)             // Chance to generate & change the random range to add more trees to the scene.
                    {
                        GameObject objectToSpawn = objects[0];      //NORMAL TREE
                        var spawnAboveTerrainBy = noiseHeight * 2;
                        Instantiate(objectToSpawn, new Vector3(mesh.vertices[i].x * MESH_SCALE, spawnAboveTerrainBy, mesh.vertices[i].z * MESH_SCALE), Quaternion.identity);
                    }


                    //BIRCH TREE
                    if (Random.Range(1, 20) == 1)             // Chance to generate
                    {
                        GameObject objectToSpawn = objects[1];      //BIRCH TREE
                        var spawnAboveTerrainBy = noiseHeight * 2;
                        Instantiate(objectToSpawn, new Vector3(mesh.vertices[i].x * MESH_SCALE, spawnAboveTerrainBy, mesh.vertices[i].z * MESH_SCALE), Quaternion.identity);
                    }

                }

                //SNOWY TREE
                if (noiseHeight > 160)                       // min height for object generation
                {
                    if (Random.Range(1, 8) == 1)             // Chance to generate
                    {
                        GameObject objectToSpawn = objects[2];      //SNOWY TREE
                        var spawnAboveTerrainBy = noiseHeight * 2;
                        Instantiate(objectToSpawn, new Vector3(mesh.vertices[i].x * MESH_SCALE, spawnAboveTerrainBy, mesh.vertices[i].z * MESH_SCALE), Quaternion.identity);
                    }
                }


            }
            if (System.Math.Abs(lastNoiseHeight - worldPt.y) < 1) // Stop generation if height difference between 2 vertices is too steep
            {
                if (noiseHeight > 10 && noiseHeight < 20)                       // min height for object generation
                {
                    if (Random.Range(1, 200) == 1)             // Chance to generate
                    {
                        GameObject objectToSpawn = objects[3];      //NORMAL TREE
                        var spawnAboveTerrainBy = noiseHeight * 2;          //Quaternion.LookRotation(rayHit.normal)
                        Instantiate(objectToSpawn, new Vector3(mesh.vertices[i].x * MESH_SCALE, spawnAboveTerrainBy, mesh.vertices[i].z * MESH_SCALE), Quaternion.identity);
                    }
                }
            }
            lastNoiseHeight = noiseHeight;
        }
    }

    private void UpdateMesh()           //name of function pretty much self explanitory 
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        GetComponent<MeshCollider>().sharedMesh = mesh;
        gameObject.transform.localScale = new Vector3(MESH_SCALE, MESH_SCALE, MESH_SCALE);
        MapEmbellishments();
    }
}
