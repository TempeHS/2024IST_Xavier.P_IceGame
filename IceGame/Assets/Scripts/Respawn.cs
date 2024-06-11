using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Vector3 SpawnPoint;
    public float badY;
    void Update()
    {
        if (this.transform.position.y <= badY)
        {
            Debug.Log("bingo p1");
            playerRespawn();
        }
    }
    void playerRespawn()
    {
        this.transform.position = new Vector3(SpawnPoint.x, SpawnPoint.y, 0f);
    }
}
