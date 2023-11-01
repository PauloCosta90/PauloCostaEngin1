using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform m_objectToLookAt;
    [SerializeField]
    private float m_rotationSpeed = 1.0f;
    [SerializeField]
    private Vector2 m_clampingXRotationValues = Vector2.zero;

    [SerializeField]
    private float maxScrollDist = 90;
    [SerializeField]
    private float minScrollDist = -90;

    private float m_desiredDistance = 8.0f;
    private float m_lerpSpeed = 0.1f;

    void Update()
    {
        UpdateHorizontalMove();
        UpdateVerticalMove();
        UpdateCameraScroll();
    }

    private void FixedUpdate()
    {
        FixedUpdateCameraLerp();
        FixedUpdateMoveCameraFromObstruction();
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

        if ((currentAngleY < 0 && comparisonAngle < m_clampingXRotationValues.x) || (currentAngleY > 0 && comparisonAngle > m_clampingXRotationValues.y))
        {
            return;
        }
        transform.RotateAround(m_objectToLookAt.position, transform.right, currentAngleY);

    }
    
    private void UpdateCameraScroll()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            // Done: a check of distance if reach max et min distance
            m_desiredDistance -= Input.mouseScrollDelta.y;
            m_desiredDistance = Mathf.Clamp(m_desiredDistance, minScrollDist, maxScrollDist);
        }
    }

    private void FixedUpdateCameraLerp()
    {
        // Done: lerp plutot que d'effectuer immédiatement la translation
        var desiredPosition = m_objectToLookAt.position - (transform.forward * m_desiredDistance);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, m_lerpSpeed);
    }
    
    private void FixedUpdateMoveCameraFromObstruction()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        //layerMask = ~layerMask;

        RaycastHit hit;

        var vecteurDiff = transform.position - m_objectToLookAt.position;
        var distance = vecteurDiff.magnitude;

        if (Physics.Raycast(m_objectToLookAt.position, vecteurDiff, out hit, distance, layerMask))
        {
            // object entre la caméra et le personnage
            Debug.DrawRay(m_objectToLookAt.position, vecteurDiff.normalized * hit.distance, Color.yellow);
            transform.SetPositionAndRotation(hit.point, transform.rotation);
        }

        else
        {
            // aucun
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
