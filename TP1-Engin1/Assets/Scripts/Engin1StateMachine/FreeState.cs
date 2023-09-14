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

    public override void OnFixedUpdate()
    {
        //31 aout
        //Done: appliquer les déplacements relatifs a la caméra pour les autre directions.
        //Todo: avoir des vitesse de déplacements maximale différente avec les cotés et le back.
        //Done: lorsque aucun input est mis décelérer le character.
        //(composante x/ taille de votre vecteur) * vitesse de déplacements side +
        if (Input.GetKey(KeyCode.W))
        {
            var vectorApplidedOnFloorUp = UnityEngine.Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, UnityEngine.Vector3.up);
            vectorApplidedOnFloorUp.Normalize();
            //mon vecteur += (0,1)
            m_stateMachine.RB.AddForce(vectorApplidedOnFloorUp * m_stateMachine.AccelarationValue, ForceMode.Acceleration);

            if (Input.GetKeyUp(KeyCode.W))// décélération du character.
            {
                m_stateMachine.RB.AddForce(vectorApplidedOnFloorUp * m_stateMachine.AccelarationValue * -1, ForceMode.Acceleration);
            }

        }
        if (m_stateMachine.RB.velocity.magnitude > m_stateMachine.MaxVelocity)
        {
            m_stateMachine.RB.velocity = m_stateMachine.RB.velocity.normalized;
            m_stateMachine.RB.velocity *= m_stateMachine.MaxVelocity;
        }

        if (Input.GetKey(KeyCode.S))
        {
            var vectorApplidedOnFloor = UnityEngine.Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward * -1, UnityEngine.Vector3.up);
            vectorApplidedOnFloor.Normalize();
            m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelarationValue, ForceMode.Acceleration);

            if (Input.GetKeyUp(KeyCode.S))// décélération du character.
            {
                m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelarationValue * -1, ForceMode.Acceleration);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            var vectorApplidedOnFloor = UnityEngine.Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.right, UnityEngine.Vector3.up);
            vectorApplidedOnFloor.Normalize();
            m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelarationValue, ForceMode.Acceleration);

            if (Input.GetKeyUp(KeyCode.D))// décélération du character.
            {
                m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelarationValue * -1, ForceMode.Acceleration);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            var vectorApplidedOnFloorDown = UnityEngine.Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.right * -1, UnityEngine.Vector3.down);
            vectorApplidedOnFloorDown.Normalize();
            m_stateMachine.RB.AddForce(vectorApplidedOnFloorDown * m_stateMachine.AccelarationValue, ForceMode.Acceleration);

            if (Input.GetKeyUp(KeyCode.A))// décélération du character.
            {
                m_stateMachine.RB.AddForce(vectorApplidedOnFloorDown * m_stateMachine.AccelarationValue * -1, ForceMode.Acceleration);
            }
        }
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        Debug.Log("On exit: free state");
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