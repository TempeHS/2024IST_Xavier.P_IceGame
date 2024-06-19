using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChange : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (activeRoom != this.transform.position.x /24 + 12)
        {
            if (cameraMove = true)
            {
            this.transform.position.x = activeRoom * 24 - 12;
            }
        } 
    }
    
}
