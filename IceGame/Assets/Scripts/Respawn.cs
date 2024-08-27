using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Vector3 SpawnPoint;
    public float badY;
    public bool cameraMove = false;
    public int activeRoom = 0;
    public GameObject camera;
    public float CamPos;
    void Update()
    {
        CamPos = camera.transform.position.x;
        if (this.transform.position.y <= badY)
        {
            playerRespawn();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("RoomTrigger")) 
        {
            Debug.Log("Pre-room: " + activeRoom);
            if (CamPos <= this.transform.position.x) 
            {
                activeRoom = activeRoom + 1;
            }
            else if (CamPos > this.transform.position.x)
            {
                activeRoom = activeRoom - 1;
            }
            cameraMove = true;
            Debug.Log("Room Triggered: " + activeRoom);
        }
        if (other.CompareTag("Blob"))
        {
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
         if (other.CompareTag("RoomTrigger")) 
        {
            Debug.Log("Pre-exit: " + activeRoom);
            if (CamPos <= this.transform.position.x) 
            {
                activeRoom = activeRoom + 1;
            }
            else if (CamPos > this.transform.position.x)
            {
                activeRoom = activeRoom - 1;
            }
            cameraMove = true;
            Debug.Log("Room Exit: " + activeRoom);
        }
        if (other.CompareTag("Blob"))
        {
            
        }
    }

    void playerRespawn()
    {
        this.transform.position = new Vector3(SpawnPoint.x, SpawnPoint.y, 0f);
    }


}
