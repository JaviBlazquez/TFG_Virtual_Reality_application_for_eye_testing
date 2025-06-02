using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;
using System;
using System.Globalization;

public class EyeTrackingLogger : MonoBehaviour
{
    private string playerName;
    private string leftEyeFilePath;
    private string rightEyeFilePath;

    void Start()
    {
        playerName = PlayerPrefs.GetString("UserName", "DefaultPlayer");
        leftEyeFilePath = Application.persistentDataPath + "/" + playerName + "_LeftEye.csv";
        rightEyeFilePath = Application.persistentDataPath + "/" + playerName + "_RightEye.csv";

        // Crear archivos y agregar encabezados si no existen
        if (!File.Exists(leftEyeFilePath))
        {
            File.WriteAllText(leftEyeFilePath, "X;Y" + Environment.NewLine);
        }

        if (!File.Exists(rightEyeFilePath))
        {
            File.WriteAllText(rightEyeFilePath, "X;Y" + Environment.NewLine);
        }
    }

    void Update()
    {
        XR_HTC_eye_tracker.Interop.GetEyeGazeData(out XrSingleEyeGazeDataHTC[] out_gazes);
        XrSingleEyeGazeDataHTC leftGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
        XrSingleEyeGazeDataHTC rightGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC];

        //float timeStamp = Time.time;

        if (leftGaze.isValid)
        {
            Vector3 leftPosition = leftGaze.gazePose.position.ToUnityVector();
            string leftData = $"{leftPosition.x};{leftPosition.y}{Environment.NewLine}";
            File.AppendAllText(leftEyeFilePath, leftData);
        }

        if (rightGaze.isValid)
        {
            Vector3 rightPosition = rightGaze.gazePose.position.ToUnityVector();
            string rightData = $"{rightPosition.x};{rightPosition.y}{Environment.NewLine}";
            File.AppendAllText(rightEyeFilePath, rightData);
        }
    }
}

