using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    public GameObject menu;
    public InputActionProperty showButton;

    public Transform head;
    public float spawnDistance = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(showButton.action.WasPerformedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);

            //This gets the menu to spawn in front of the player no matter where they are in the game
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }
        //This changes the rotation of the menu, so that it is always facing the player, then multiplying by -1 so its not flipped.
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }
}
