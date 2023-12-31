using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpState : CharacterState
{
    private const float GETUP_EXIT_TIME = 1.0f;
    private float m_currentStateTimer = 0.0f;

    public override void OnEnter()
    {
        Debug.Log("On enter: get up state");
        m_currentStateTimer = GETUP_EXIT_TIME;
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
    }

    public override void OnExit()
    {
        Debug.Log("On exit: get up state");
        m_stateMachine.IsOnContactWithFloor();
    }

    public override bool CanEnter(IState currentState)
    {
        //this must be run in update
        if (currentState is OnGroundState)
        {
            return true;
        }

        return false;
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0;
    }
}
