using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Vector3 SpawnPoint;
    public float badY;
    public bool cameraMove = false;
    public int activeRoom = 1;
    void Update()
    {
        if (this.transform.position.y <= badY)
        {
            Debug.Log("bingo p1");
            playerRespawn();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        activeRoom = (int) (this.transform.position.x + 12) / 24 ;
        cameraMove = true;
        Debug.Log("bingus");
        Debug.Log(activeRoom);
    }

    void playerRespawn()
    {
        this.transform.position = new Vector3(SpawnPoint.x, SpawnPoint.y, 0f);
    }


}
