using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chase : MonoBehaviour
{
    public GameObject player;
    float MoveSpeed = 2;
    float MaxDist = 10;
    float MinDist = 10;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        if(Vector3.Distance(transform.position, player.transform.position) <= MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
          
        }


        
    }
}
