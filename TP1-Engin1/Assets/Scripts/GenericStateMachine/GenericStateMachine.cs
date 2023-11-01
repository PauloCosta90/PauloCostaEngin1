using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericStateMachine<T> : MonoBehaviour where T : IState
{
    protected T m_currentState;
    protected List<T> m_possibleStates;

    protected virtual void Awake()
    {
        CreatePossibleStates();
    }

    protected virtual void CreatePossibleStates()
    {
       
    }

    protected virtual void Start()
    {
        foreach (T state in m_possibleStates)
        {
            state.OnStart();
        }
        m_currentState = m_possibleStates[0];
        m_currentState.OnEnter();
    }

    protected virtual void FixedUpdate()
    {
        m_currentState.OnFixedUpdate();
    }

    protected virtual void Update()
    {
        m_currentState.OnUpdate();
        TryToTransitionState();
    }

    private void TryToTransitionState()
    {
        if (!m_currentState.CanExit())
        {
            return;
        }

        // Je PEUX quitter le state actuel
        foreach (var state in m_possibleStates)
        {
            if (m_currentState.Equals(state))
            {
                continue;
            }

            if (state.CanEnter(m_currentState))
            {
                //Quitter le state actuel
                m_currentState.OnExit();
                m_currentState = state;
                //Rentrer dans le state state
                m_currentState.OnEnter();
                return;
            }
        }
    }
}
