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
    private float maxScrollDist = 5;
    [SerializeField]
    private float minScrollDist = -5;
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
        if (Input.mouseScrollDelta.y !=0)
        {
            // Todo: a check of distance if reach max et min distance
            // Todo: lerp plutot que d'effectuer immédiatement la translation
            float newDistance = Mathf.Clamp(transform.position.z + Input.mouseScrollDelta.y, minScrollDist, maxScrollDist);
            transform.Translate(Vector3.forward * Input.mouseScrollDelta.y, Space.Self);
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, newDistance);
            float lerpSpeed = 5.0f; // Adjust the speed as needed
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
        }
    }
    
    private void FixedUpdateTestCameraObstruction()
    {
        // Bit shift the index of the layer (8) to get a bit mask
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