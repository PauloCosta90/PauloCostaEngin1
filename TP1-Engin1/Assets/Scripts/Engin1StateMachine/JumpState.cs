using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : CharacterState
{
    public override void OnEnter()
    {
        Debug.Log("On enter: jump state");

        //jump
        m_stateMachine.RB.AddForce(Vector3.up * m_stateMachine.JumpingValue,);
    }
    public override void OnFixedUpdate()
    {

    }
    public override void OnUpdate()
    {

    }
    public override void OnExit()
    {
        Debug.Log("On exit: jump state");

    }
    public override bool CanEnter()
    {
        //this must be run in update
        return Input.GetKeyDown(KeyCode.Space);
    }
    public override bool CanExit()
    {
        return true;
    }
}
