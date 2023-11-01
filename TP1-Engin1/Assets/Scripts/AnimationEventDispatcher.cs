using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventDispatcher : MonoBehaviour
{
    private MainCharacterControllerStateMachine m_stateMachineRef;

    private void Awake()
    {
        m_stateMachineRef = GetComponent<MainCharacterControllerStateMachine>();
    }

    public void ActivateHitBox()
    {
        m_stateMachineRef.OnEnabledHitBox(true);
    }

    public void DeactivateHitBox()
    {
        m_stateMachineRef.OnEnabledHitBox(false);
    }
}
