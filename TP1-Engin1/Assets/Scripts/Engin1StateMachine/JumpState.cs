using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : CharacterState
{
    private const float STATE_EXIT_TIMER = 1.0f;
    private float m_currentStateTimer = 0.0f;

    public override void OnEnter()
    {
        Debug.Log("On enter: jump state");

        //jump
        m_stateMachine.RB.AddForce(Vector3.up * m_stateMachine.JumpIntensity * m_stateMachine.AccelarationValue/2, ForceMode.Acceleration);
        m_stateMachine.JumpTrigger();
        //m_stateMachine.IsInMidair();
        m_currentStateTimer = STATE_EXIT_TIMER;
    }

    public override void OnFixedUpdate()
    {
        ////if (Input.GetKey(KeyCode.W))
        ////{
        ////    var vectorApplidedOnFloorUp = UnityEngine.Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, UnityEngine.Vector3.up);
        ////    vectorApplidedOnFloorUp.Normalize();
        ////    m_stateMachine.RB.AddForce(vectorApplidedOnFloorUp * m_stateMachine.AccelarationValue, ForceMode.Acceleration);
        ////    m_stateMachine.RB.MovePosition(vectorApplidedOnFloorUp);
        ////}
    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
    }

    public override void OnExit()
    {
        Debug.Log("On exit: jump state");
    }

    public override bool CanEnter(CharacterState currentState)
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