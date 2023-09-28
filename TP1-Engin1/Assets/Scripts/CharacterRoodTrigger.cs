using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRoodTrigger : MonoBehaviour
{
    public bool IsTouchingRood { get; private set; }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("Rood"))
        {
            Debug.Log("touching the rood");
            IsTouchingRood = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log("left the rood");
        IsTouchingRood = false;
    }
}