using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpState : CharacterState
{
    public virtual void OnEnter()
    {
        Debug.Log("On enter: get up state");
    }
    public virtual void OnFixedUpdate()
    {

    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnExit()
    {
        Debug.Log("On exit: get up state");
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
