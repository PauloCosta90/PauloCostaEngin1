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
        //Done: avoir des vitesse de déplacements maximale différente avec les cotés et le back.
        //Done: lorsque aucun input est mis décelérer le character.
        //(composante x/ taille de votre vecteur) * vitesse de déplacements side +
        if (Input.GetKey(KeyCode.W))
        {
            var vectorApplidedOnFloor = UnityEngine.Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, UnityEngine.Vector3.up);
            vectorApplidedOnFloor.Normalize();
            //mon vecteur += (0,1)
            m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelarationValue, ForceMode.Acceleration);

            float fowardcomponent = Vector3.Dot(m_stateMachine.RB.velocity, vectorApplidedOnFloor);
            m_stateMachine.UpdateAnimatorValues(new Vector2(0, fowardcomponent));

            if (Input.GetKeyUp(KeyCode.W))// décélération du character.
            {
                m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelarationValue * -1, ForceMode.Acceleration);
            }

            if (m_stateMachine.RB.velocity.magnitude > m_stateMachine.MaxForwardVelocity)
            {
                m_stateMachine.RB.velocity = m_stateMachine.RB.velocity.normalized;
                m_stateMachine.RB.velocity *= m_stateMachine.MaxForwardVelocity;
            }
        }
        //if (m_stateMachine.RB.velocity.magnitude > m_stateMachine.MaxVelocity)
        //{
        //    m_stateMachine.RB.velocity = m_stateMachine.RB.velocity.normalized;
        //    m_stateMachine.RB.velocity *= m_stateMachine.MaxVelocity;
        //}

        if (Input.GetKey(KeyCode.S))
        {
            var vectorApplidedOnFloor = UnityEngine.Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward * -1, UnityEngine.Vector3.up);
            vectorApplidedOnFloor.Normalize();
            m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelarationValue, ForceMode.Acceleration);

            float fowardcomponent = Vector3.Dot(m_stateMachine.RB.velocity, vectorApplidedOnFloor);
            m_stateMachine.UpdateAnimatorValues(new Vector2(0, fowardcomponent));

            if (Input.GetKeyUp(KeyCode.S))// décélération du character.
            {
                m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelarationValue * -1, ForceMode.Acceleration);
            }

            if (m_stateMachine.RB.velocity.magnitude > m_stateMachine.MaxBackwardVelocity)
            {
                m_stateMachine.RB.velocity = m_stateMachine.RB.velocity.normalized;
                m_stateMachine.RB.velocity *= m_stateMachine.MaxBackwardVelocity;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            var vectorApplidedOnFloor = UnityEngine.Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.right, UnityEngine.Vector3.up);
            vectorApplidedOnFloor.Normalize();
            m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelarationValue, ForceMode.Acceleration);

            float fowardcomponent = Vector3.Dot(m_stateMachine.RB.velocity, vectorApplidedOnFloor);
            m_stateMachine.UpdateAnimatorValues(new Vector2(fowardcomponent,0));

            if (Input.GetKeyUp(KeyCode.D))// décélération du character.
            {
                m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelarationValue * -1, ForceMode.Acceleration);
            }

            if (m_stateMachine.RB.velocity.magnitude > m_stateMachine.MaxSideVelocity)
            {
                m_stateMachine.RB.velocity = m_stateMachine.RB.velocity.normalized;
                m_stateMachine.RB.velocity *= m_stateMachine.MaxSideVelocity;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            var vectorApplidedOnFloor = UnityEngine.Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.right * -1, UnityEngine.Vector3.down);
            vectorApplidedOnFloor.Normalize();
            m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelarationValue, ForceMode.Acceleration);

            float fowardcomponent = Vector3.Dot(m_stateMachine.RB.velocity, vectorApplidedOnFloor);
            m_stateMachine.UpdateAnimatorValues(new Vector2(fowardcomponent, 0));

            if (Input.GetKeyUp(KeyCode.A))// décélération du character.
            {
                m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelarationValue * -1, ForceMode.Acceleration);
            }

            if (m_stateMachine.RB.velocity.magnitude > m_stateMachine.MaxSideVelocity)
            {
                m_stateMachine.RB.velocity = m_stateMachine.RB.velocity.normalized;
                m_stateMachine.RB.velocity *= m_stateMachine.MaxSideVelocity;
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

    public override bool CanEnter(CharacterState currentState)
    {
        if(currentState is JumpState)
        {
            // si true,c'est que je suis présentement dans le jump state et je veux entrer dans free
            //Je ne peux changer de state si je ne touche pas le sol 
            return m_stateMachine.IsOnContactWithFloor();
        }

        return false;
    }

    public override bool CanExit()
    {
        return true;
    }
}