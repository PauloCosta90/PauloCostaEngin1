using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFloorTrigger : MonoBehaviour
{
    public  bool IsOnFloor { get; private set; }
    private void OnTriggerStay(Collision other)
    {
        if(!IsOnFloor)
        {
            Debug.Log("touch the ground");
        }
        IsOnFloor = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsOnFloor)
        {
            Debug.Log("left the ground");
        }
        IsOnFloor = false;
    }
}
