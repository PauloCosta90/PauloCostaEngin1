using System.Collections;
using System.Collections.Generic;
using UnityEditor.DeviceSimulation;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HitState : CharacterState
{
    public override void OnEnter()
    {
        Debug.Log("On enter: hit state");
        m_stateMachine.IsOnContactWithRood();
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
      
    }

    public override void OnExit()
    {
        Debug.Log("On exit: hit state");
        m_stateMachine.IsOnContactWithRood();
    }

    public override bool CanEnter(CharacterState currentState)
    {
        //this must be run in update
        if (currentState is FreeState)
        {
            return m_stateMachine.IsOnContactWithRood();
        }

        return false;
    }

    public override bool CanExit()
    {
        return true;
    }
}
