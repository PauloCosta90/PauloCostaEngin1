using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform m_objectToLookAt;
    [SerializeField]
    private float m_rotationSpeed = 1.0f;
    [SerializeField]
    private Vector2 m_clampingValue = Vector2.zero;
   
    void Update()
    {
        UpdateHorizontalMove();
        UpdateVerticalMove();
        UpdateCameraScroll();
    }
    private void FixedUpdate()
    {
        FixedUpdateTestCameraObstruction();
    }

    private void UpdateHorizontalMove()
    {
        // angle en degrée. Sin donnne la composante y du cercle et Cos donne la composante x du cercle
        float currentAngleX = Input.GetAxis("Mouse X") * m_rotationSpeed;
        transform.RotateAround(m_objectToLookAt.position, m_objectToLookAt.up, currentAngleX);
    }
    private void UpdateVerticalMove()
    {
        // angle en degrée. Sin donnne la composante y du cercle et Cos donne la composante x du cercle
        float currentAngleY = Input.GetAxis("Mouse Y") * m_rotationSpeed;
        float eulerAngleX = transform.rotation.eulerAngles.x;
        float comparisonAngle = eulerAngleX + currentAngleY;

        comparisonAngle = ClampAngle(comparisonAngle);

        if ((currentAngleY < 0 && comparisonAngle < m_clampingValue.x) || (currentAngleY > 0 && comparisonAngle > m_clampingValue.y))
        {
            return;
        }
        transform.RotateAround(m_objectToLookAt.position, transform.right, currentAngleY);

    }

    private void UpdateCameraScroll()
    {
        if(Input.mouseScrollDelta.y !=0)
        {
            // todo a check of distance if reach max et min distance and lerp plutot que d'effectuer immédiatement la translation
            transform.Translate(Vector3.forward * Input.mouseScrollDelta.y, Space.Self);
        }
    }

    private void FixedUpdateTestCameraObstruction()
    {
        int layerMask = 1 << 8;

        //layerMask = ~layerMask;

        RaycastHit hit;

        var vecteurDiff = transform.position - m_objectToLookAt.position;
        var distance = vecteurDiff.magnitude;

        if (Physics.Raycast(m_objectToLookAt.position, vecteurDiff, out hit, distance, layerMask))
        {
            // object entre la caméra et la capsule
            Debug.DrawRay(m_objectToLookAt.position, vecteurDiff.normalized * hit.distance, Color.yellow);
            transform.SetPositionAndRotation(hit.point, transform.rotation);
        }

        else
        {
            Debug.DrawRay(m_objectToLookAt.position, vecteurDiff, Color.white);
        }
    }
    private float ClampAngle(float angle)
    {
        if (angle > 180)
        {
            angle -= 360;
        }

        return angle;
    }
}
