using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderScript : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotSpeed = 100f;

    private bool iswandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool iswalking = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (iswandering == false) ;
        {
            StartCoroutine(Wander());
        }

        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * moveSpeed);
        }

        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -moveSpeed);
        }

        if (iswalking == true)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }


    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 2);
        int rotateLorR = Random.Range(1, 2);
        int walkWait = Random.Range(1, 2);
        int walkTime = Random.Range(1, 4);

        iswandering = true;

        yield return new WaitForSeconds(walkWait);
        iswalking = true;

        yield return new WaitForSeconds(walkTime);
        iswalking = false;

        yield return new WaitForSeconds(rotateWait);

        if (rotateLorR == 1)
        {
            isRotatingRight = true;
        }

        yield return new WaitForSeconds(rotTime);
        {
            isRotatingRight = false;
        }

        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
        }

        yield return new WaitForSeconds(rotTime);
        {
            isRotatingLeft = false;
        }
        iswandering = false;
    }
}

