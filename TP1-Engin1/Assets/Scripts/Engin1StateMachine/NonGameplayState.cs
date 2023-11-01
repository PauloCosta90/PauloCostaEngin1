using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonGameplayState : CharacterState
{
    public override void OnEnter()
    {
        Debug.Log("On enter: Non gameplay state");
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        Debug.Log("On exit: Non gameplay state");
    }

    public override bool CanEnter(IState currentState)
    {
        return m_stateMachine.InNonGameplayState;
    }

    public override bool CanExit()
    {
        return !m_stateMachine.InNonGameplayState;
    }
}
