using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFloorTrigger : MonoBehaviour
{
    public  bool IsOnFloor { get; private set; }

    private void OnTriggerStay(UnityEngine.Collider other)
    {
        if(!IsOnFloor)
        {
            Debug.Log("touching the ground");
        }
        IsOnFloor = true;
    }

    private void OnTriggerExit(UnityEngine.Collider other)
    {
       Debug.Log("left the ground");
       IsOnFloor = false;
    }
}