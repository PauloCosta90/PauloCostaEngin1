using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState : IState
{
    protected MainCharacterControllerStateMachine m_stateMachine;

    public void OnStart(MainCharacterControllerStateMachine stateMachine)
    {
        //TODO call and send the state machine to characterstate
        m_stateMachine = stateMachine;
    }

    public void OnStart()
    {
       
    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnFixedUpdate()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {

    }

    public virtual bool CanEnter(IState currentState)
    {
        throw new System.NotImplementedException();
    }

    public virtual bool CanExit()
    {
        throw new System.NotImplementedException();
    }
}