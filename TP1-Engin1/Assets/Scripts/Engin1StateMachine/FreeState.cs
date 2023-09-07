using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class FreeState : CharacterState
{

    public override void OnEnter()
    {
        Debug.Log("On enter: free state");
    }

    public override void OnUpdate()
    {
        
    }

    public override void  OnFixedUpdate()
    {
        //var vectorApplidedOnFloor = UnityEngine.Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, UnityEngine.Vector3.up);
        //vectorApplidedOnFloor.Normalize();

        if (Input.GetKey(KeyCode.W))
        {
            var vectorApplidedOnFloor = UnityEngine.Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, UnityEngine.Vector3.up);
            vectorApplidedOnFloor.Normalize();
            m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelarationValue, ForceMode.Acceleration);

        }
        if (m_stateMachine.RB.velocity.magnitude > m_stateMachine.MaxVelocity)
        {
            m_stateMachine.RB.velocity = m_stateMachine.RB.velocity.normalized;
            m_stateMachine.RB.velocity *= m_stateMachine.MaxVelocity;
        }
    
        //Todo: appliquer les déplacements relatifs a la caméra pour les autre directions.
        //Todo: avoir des vitesse de déplacements maximale différente avec les cotés et le back.
        //Todo: lorsque aucun input est mis décelérer le character.

        if (Input.GetKey(KeyCode.A))
        {
            var vectorApplidedOnFloor = UnityEngine.Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, UnityEngine.Vector3.left);
            vectorApplidedOnFloor.Normalize();
            m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelarationValue, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.S))
        {
            var vectorApplidedOnFloor = UnityEngine.Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, UnityEngine.Vector3.down);
            vectorApplidedOnFloor.Normalize();
            m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelarationValue, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.D))
        {
            var vectorApplidedOnFloor = UnityEngine.Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, UnityEngine.Vector3.right);
            vectorApplidedOnFloor.Normalize();
            m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelarationValue, ForceMode.Acceleration);
        }
    }

    public override void OnExit()
    {
        Debug.Log("On exit: free state");
    }

    public override bool CanEnter()
    {
     //Je ne peux changer de state si je ne touche pas le sol 
        return true;
    }
    public override bool CanExit()
    {
        return true;
    }
}