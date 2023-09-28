using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CharacterState
{
    private const float STATE_EXIT_TIMER = 1.0f;
    private float m_currentStateTimer = 0.0f;

    public override void OnEnter()
    {
        Debug.Log("On enter: attack state");
        m_stateMachine.AttackTrigger();
        m_currentStateTimer = STATE_EXIT_TIMER;
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
        Debug.Log("On exit: attack state");

    }

    public override bool CanEnter(CharacterState currentState)
    {
        //this must be run in update
        if (currentState is FreeState)
        {
            return Input.GetKeyDown(KeyCode.O);
        }

        return false;
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0;
    }
}
