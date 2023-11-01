using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : CharacterState
{
    private const float JUMP_STATE_EXIT_TIME = 0.5f;
    private float m_currentStateTimer = 0.0f;

    public override void OnEnter()
    {
        Debug.Log("On enter: jump state");

        //jump
        m_stateMachine.RB.AddForce(Vector3.up * m_stateMachine.JumpIntensity * m_stateMachine.AccelerationValue, ForceMode.Acceleration);
        m_currentStateTimer = JUMP_STATE_EXIT_TIME;
        m_stateMachine.EffectsController.PlaySound(EActionType.Jump);
        m_stateMachine.JumpTrigger();
        m_stateMachine.IsInMidair();
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
        Debug.Log("On exit: jump state");
    }

    public override bool CanEnter(IState currentState)
    {
        //this must be run in update
        if (currentState is FreeState)
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        return false;
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0;
    }
}