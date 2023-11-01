using System.Collections;
using System.Collections.Generic;
using UnityEditor.DeviceSimulation;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HitState : CharacterState
{
    //private float m_noTimer = 0.0f;
    //private float m_shakeTimer = 1.2f;

    public override void OnEnter()
    {
        
        m_stateMachine.OnHitStimuliReceived = false;
        m_stateMachine.IsOnContactWithRood();
        m_stateMachine.EffectsController.PlaySound(EActionType.Hit);
        m_stateMachine.EffectsController.PlaySVFXSystem(EActionType.Hit);
        Debug.Log("On enter: hit state");
    }

    public override void OnFixedUpdate()
    {
       
    }
  
    public override void OnUpdate()
    {
        //m_shakeTimer -= Time.deltaTime;
        //if(m_shakeTimer <= m_noTimer)
        //{
        //    m_stateMachine;
        //}
    }

    public override void OnExit()
    {
        Debug.Log("On exit: hit state");
        m_stateMachine.IsOnContactWithRood();
    }

    public override bool CanEnter(IState currentState)
    {
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