using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : CharacterState
{
    public override void OnEnter()
    {
        Debug.Log("On enter: falling state");
    }

    public override void OnFixedUpdate()
    {
        FixedUpdateApplyMovement();
    }

    public void FixedUpdateApplyMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            var vectorApplidedOnFloor = UnityEngine.Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, UnityEngine.Vector3.up);
            vectorApplidedOnFloor.Normalize();
            m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelerationValue, ForceMode.Acceleration);

            float verticalComponent = Vector3.Dot(m_stateMachine.RB.velocity, vectorApplidedOnFloor);
            m_stateMachine.UpdateAnimatorValues(new UnityEngine.Vector2(0, verticalComponent));

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                float combinedMaxVelocity = (m_stateMachine.MaxForwardVelocity + m_stateMachine.MaxSideVelocity) / 2;

                if (m_stateMachine.RB.velocity.magnitude > combinedMaxVelocity)
                {
                    m_stateMachine.RB.velocity = m_stateMachine.RB.velocity.normalized;
                    m_stateMachine.RB.velocity *= combinedMaxVelocity;
                }
            }

            if (Input.GetKeyUp(KeyCode.W))// décélération du character.
            {
                m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelerationValue * -1, ForceMode.Acceleration);
            }

            if (m_stateMachine.RB.velocity.magnitude > m_stateMachine.MaxForwardVelocity)
            {
                m_stateMachine.RB.velocity = m_stateMachine.RB.velocity.normalized;
                m_stateMachine.RB.velocity *= m_stateMachine.MaxForwardVelocity;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            var vectorApplidedOnFloor = UnityEngine.Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward * -1, UnityEngine.Vector3.up);
            vectorApplidedOnFloor.Normalize();
            m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelerationValue, ForceMode.Acceleration);

            float verticalComponent = Vector3.Dot(m_stateMachine.RB.velocity, vectorApplidedOnFloor);
            m_stateMachine.UpdateAnimatorValues(new UnityEngine.Vector2(0, verticalComponent));

            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                float combinedMaxVelocity = (m_stateMachine.MaxBackwardVelocity + m_stateMachine.MaxSideVelocity) / 2;

                if (m_stateMachine.RB.velocity.magnitude > combinedMaxVelocity)
                {
                    m_stateMachine.RB.velocity = m_stateMachine.RB.velocity.normalized;
                    m_stateMachine.RB.velocity *= combinedMaxVelocity;
                }
            }

            if (Input.GetKeyUp(KeyCode.S))// décélération du character.
            {
                m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelerationValue * -1, ForceMode.Acceleration);
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
            m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelerationValue, ForceMode.Acceleration);

            float horizontalComponent = Vector3.Dot(m_stateMachine.RB.velocity, vectorApplidedOnFloor);
            m_stateMachine.UpdateAnimatorValues(new UnityEngine.Vector2(horizontalComponent, 0));

            if (Input.GetKeyUp(KeyCode.D))// décélération du character.
            {
                m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelerationValue * -1, ForceMode.Acceleration);
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
            m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelerationValue, ForceMode.Acceleration);

            float horizontalComponent = Vector3.Dot(m_stateMachine.RB.velocity, vectorApplidedOnFloor);
            m_stateMachine.UpdateAnimatorValues(new UnityEngine.Vector2(horizontalComponent, 0));

            if (Input.GetKeyUp(KeyCode.A))// décélération du character.
            {
                m_stateMachine.RB.AddForce(vectorApplidedOnFloor * m_stateMachine.AccelerationValue * -1, ForceMode.Acceleration);
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
        m_stateMachine.EffectsController.PlaySound(EActionType.Landing);
        Debug.Log("On exit: falling state");
    }

    public override bool CanEnter(IState currentState)
    {
        if (currentState is JumpState)
        {
            return !m_stateMachine.IsOnContactWithFloor();
        }

        return false;
    }

    public override bool CanExit()
    {
        return true;
    }  
}
