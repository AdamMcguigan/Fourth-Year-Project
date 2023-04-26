using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject m_enemy;
    // Start is called before the first frame update
    void Start()
    {
        SpawnNewEnemy();
    }

  

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNewEnemy()
    {
        int rand = Mathf.RoundToInt(Random.RandomRange(0f, spawnPoints.Length - 1));
        Instantiate(m_enemy, spawnPoints[rand].transform.position, Quaternion.identity);
    }
}
