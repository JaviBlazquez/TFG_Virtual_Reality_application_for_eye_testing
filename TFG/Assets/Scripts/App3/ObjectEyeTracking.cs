using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;

public class ObjectEyeTracking : MonoBehaviour
{
    public GameObject leftEyeObject; 
    public GameObject rightEyeObject; 
    public GameObject MainCamera;
    public float fixedDistance = 5f; 

    void Update()
    {
        XR_HTC_eye_tracker.Interop.GetEyeGazeData(out XrSingleEyeGazeDataHTC[] out_gazes);
        XrSingleEyeGazeDataHTC leftGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
        XrSingleEyeGazeDataHTC rightGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC];
        if (leftGaze.isValid)
        {
            PositionEyeObject(leftGaze, leftEyeObject, -0.032f, Color.green);
        }
        if (rightGaze.isValid)
        {
            PositionEyeObject(rightGaze, rightEyeObject, 0.032f, Color.blue);
        }
    }

    void PositionEyeObject(XrSingleEyeGazeDataHTC gazeData, GameObject eyeObject, float eyeOffset, Color rayColor)
    {
        Quaternion orientation = gazeData.gazePose.orientation.ToUnityQuaternion();
        Vector3 direction = orientation * Vector3.forward;
        Vector3 origin = MainCamera.transform.position + new Vector3(eyeOffset, 0.0f, 0.0f);
        Vector3 endPoint = origin + direction * fixedDistance;
        eyeObject.transform.position = endPoint;
        Debug.DrawRay(origin, direction * fixedDistance, rayColor);
    }
}
