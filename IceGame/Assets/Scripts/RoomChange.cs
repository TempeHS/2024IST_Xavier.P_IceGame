using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChange : MonoBehaviour
{
    public GameObject Player;
    private Respawn respawnScript; 

    void Start()
    {
        respawnScript = Player.GetComponent<Respawn>(); 
    }
    // Update is called once per frame
    void Update()
    {
        if (respawnScript.activeRoom != this.transform.position.x /24 && respawnScript.cameraMove == true)
        {
            transform.position = new Vector3(respawnScript.activeRoom * 24, this.transform.position.y, this.transform.position.z);
            respawnScript.cameraMove = false;
        } 
    }
    
}
