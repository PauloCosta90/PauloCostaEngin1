using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : CharacterState
{
    float m_scaryHeight = 20;
    float m_midairHeight;

    public override void OnEnter()
    {
        Debug.Log("On enter: falling state");
        m_midairHeight = m_stateMachine.transform.position.y;
    }

    public override void OnFixedUpdate()
    { 

    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        Debug.Log("On exit: falling state");

        float landingHeight = m_stateMachine.transform.position.y;
        float heightReached = m_midairHeight - landingHeight;
        Debug.Log("we falled from " + m_midairHeight + " and landed at " + landingHeight);
        if (heightReached > m_scaryHeight)
        {
            m_stateMachine.CrashTrigger();
        }

        else
        {
            m_stateMachine.LandTrigger();
            m_stateMachine.IsOnContactWithFloor();
        }
    }

    public override bool CanEnter(CharacterState currentState)
    {
        //this must be run in update
        if (currentState is JumpState)
        {
            return true;
        }

        return false;
    }

    public override bool CanExit()
    {
        return true;
    }
}
