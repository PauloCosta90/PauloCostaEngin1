using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CharacterState
{
    
    public virtual void OnEnter()
    {
        Debug.Log("On enter: attack state");
    }
    public virtual void OnFixedUpdate()
    {

    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnExit()
    {
        Debug.Log("On exit: attack state");
    }
    public virtual bool CanEnter()
    {
        return true;
    }
    public virtual bool CanExit()
    {
        return true;
    }
}
