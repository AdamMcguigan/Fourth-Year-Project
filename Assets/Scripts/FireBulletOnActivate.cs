using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public float shotSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();
        interactable.activated.AddListener(fireBullet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fireBullet(ActivateEventArgs arg)
    {
        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = firePoint.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity = firePoint.forward * shotSpeed;
        Destroy(spawnedBullet, 5);
    }
}
