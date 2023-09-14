using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : CharacterState
{
    public override void OnEnter()
    {
        Debug.Log("On enter: hit state");
    }

    public override void OnFixedUpdate()
    {
        if (Input.GetKey(KeyCode.X))
        {
            //Color.red;
            //m_stateMachine.transform;
        }
    }
    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        Debug.Log("On exit: hit state");
    }

    public override bool CanEnter()
    {
        //Je ne peux changer de state si je ne touche pas le sol 
        return m_stateMachine.IsOnContactWithFloor();
    }
    public override bool CanExit()
    {
        return true;
    }
}
