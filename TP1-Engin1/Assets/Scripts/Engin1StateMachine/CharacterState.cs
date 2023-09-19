using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState : IState
{
    protected MainCharacterControllerStateMachine m_stateMachine;
    public void OnStart(MainCharacterControllerStateMachine stateMachine)
    {
        m_stateMachine = stateMachine;
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
    public virtual bool CanEnter(CharacterState state)
    {
        throw new System.NotImplementedException();
    }
    public virtual bool CanExit()
    {
        throw new System.NotImplementedException();
    }
}