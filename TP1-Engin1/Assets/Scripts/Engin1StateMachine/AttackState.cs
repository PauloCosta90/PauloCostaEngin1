using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : CharacterState
{
    private const float ATTACK_STATE_EXIT_TIME = 0.8f;
    private float m_currentStateTimer = 0.0f;
    [SerializeField]
    private float m_slowMotionTime = 0.5f;
    private float m_normalTime = 1.0f;
    private float m_noTime = 0.0f;

    public override void OnEnter()
    {
        m_stateMachine.AttackTrigger();
        m_currentStateTimer = ATTACK_STATE_EXIT_TIME;
        m_stateMachine.EffectsController.PlaySound(EActionType.Attack);
        m_stateMachine.EffectsController.PlaySVFXSystem(EActionType.Attack);
        Time.timeScale = m_slowMotionTime;
        Debug.Log("On enter: attack state");
    }

    public override void OnFixedUpdate()
    {
       
    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
        if(m_currentStateTimer <= m_noTime)
        {
            Time.timeScale = m_normalTime;
        }
    }

    public override void OnExit()
    {
        m_stateMachine.OnEnabledHitBox(false);
        Debug.Log("On exit: attack state");
    }

    public override bool CanEnter(IState currentState)
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