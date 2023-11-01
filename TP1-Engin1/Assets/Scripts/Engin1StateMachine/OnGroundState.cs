using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundState : CharacterState
{
    private const float ONGROUND_STATE_EXIT_TIME = 2.0f;
    private float m_currentStateTimer = 0.0f;

    public override void OnEnter()
    {
        m_stateMachine.OnStunStimuliReceived = false;
        m_stateMachine.Animator.SetBool("IsStun", true);
        m_currentStateTimer = ONGROUND_STATE_EXIT_TIME;
        Debug.Log("On enter: crash state");
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
        m_stateMachine.Animator.SetBool("OnStun", false);
        Debug.Log("On exit: crash state");
    }

    public override bool CanEnter(IState currentState)
    {
        //return m_stateMachine.OnStunStimuliReceived;
        if (currentState is FallingState)
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
