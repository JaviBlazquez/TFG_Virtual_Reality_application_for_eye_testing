using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;

public class EyeTracking : MonoBehaviour
{
    public GameObject ball; 
    public float gazeThreshold = 1f; 
    private float gazeTime = 0f; 
    private bool isTracking = false; 
    public GameObject MainCamera;

    public Material Green;
    public Material Red;
    void Start()
    {
         
    }

    void Update()
    {
        // Datos del eye tracking
        XR_HTC_eye_tracker.Interop.GetEyeGazeData(out XrSingleEyeGazeDataHTC[] out_gazes);
        XrSingleEyeGazeDataHTC leftGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
        XrSingleEyeGazeDataHTC rightGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC];

        bool leftHitBall = false;
        bool rightHitBall = false;

        if (leftGaze.isValid)
        {
            // Origen y dirección del ojo izquierdo
            Quaternion leftOrientation = leftGaze.gazePose.orientation.ToUnityQuaternion();
            Vector3 leftDirection = leftOrientation * Vector3.forward;
            Vector3 leftEyeOffset = new Vector3(-0.032f, 0.0f, 0.0f); // Offset ojo izquierdo
            Vector3 leftOrigin = MainCamera.transform.position + leftEyeOffset;

            // Raycast para el ojo izquierdo
            if (Physics.Raycast(leftOrigin, leftDirection, out RaycastHit leftHit, 100f))
            {
                if (leftHit.collider.gameObject == ball)
                {
                    leftHitBall = true;
                }
            }
            Debug.DrawRay(leftOrigin, leftDirection * 50f, Color.green);
        }

        if (rightGaze.isValid)
        {
            // Origen y dirección del ojo derecho
            Quaternion rightOrientation = rightGaze.gazePose.orientation.ToUnityQuaternion();
            Vector3 rightDirection = rightOrientation * Vector3.forward;
            Vector3 rightEyeOffset = new Vector3(0.032f, 0.0f, 0.0f); // Offset ojo derecho
            Vector3 rightOrigin = MainCamera.transform.position + rightEyeOffset;

            // Raycast para el ojo derecho
            if (Physics.Raycast(rightOrigin, rightDirection, out RaycastHit rightHit, 100f))
            {
                if (rightHit.collider.gameObject == ball)
                {
                    rightHitBall = true;
                }
            }
            Debug.DrawRay(rightOrigin, rightDirection * 50f, Color.blue);
        }

        if (leftHitBall || rightHitBall)
        {
            ball.GetComponent<Renderer>().material = Green;
            gazeTime += Time.deltaTime;

            if (!isTracking)
            {
                isTracking = true;
            }
        }
        else
        {
            ball.GetComponent<Renderer>().material = Red;
            if (isTracking)
            {
                isTracking = false;
            }
        }

        Debug.Log($"Tiempo siguiendo la pelota: {gazeTime:F2} segundos");
        PlayerPrefs.SetFloat("GazeTime", gazeTime);
        PlayerPrefs.Save();
    }
}


