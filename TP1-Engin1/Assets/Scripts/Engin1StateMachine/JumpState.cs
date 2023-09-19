using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.2f;
    private float m_currentStateTimer = 0.0f;
    //private float m_maximumHeight = 20.0f;

    public override void OnEnter()
    {
        Debug.Log("On enter: jump state");

        //jump
        m_stateMachine.RB.AddForce(Vector3.up * m_stateMachine.JumpIntensity * m_stateMachine.AccelarationValue/2, ForceMode.Acceleration);
        m_currentStateTimer = STATE_EXIT_TIMER;
        //if (Input.GetKey(KeyCode.W))
        //{
        //    var vectorApplidedOnFloorUp = UnityEngine.Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, UnityEngine.Vector3.up);
        //    vectorApplidedOnFloorUp.Normalize();
        //    m_stateMachine.RB.AddForce(vectorApplidedOnFloorUp * m_stateMachine.AccelarationValue, ForceMode.Acceleration);
        //    m_stateMachine.RB.MovePosition(vectorApplidedOnFloorUp);
        //}
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
    public override bool CanEnter()
    {
        //this must be run in update
        return Input.GetKeyDown(KeyCode.Space);
    }
    public override bool CanExit()
    {
        return m_currentStateTimer <= 0;
    }
}