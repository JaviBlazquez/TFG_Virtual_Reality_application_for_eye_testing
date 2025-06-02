using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIVE.OpenXR.EyeTracker;
using VIVE.OpenXR;

public class Pruebas : MonoBehaviour
{
    public GameObject ball; // Referencia a la pelota
    public LineRenderer leftLineRenderer; // LineRenderer para el ojo izquierdo
    public LineRenderer rightLineRenderer; // LineRenderer para el ojo derecho
    public float gazeThreshold = 1f; // Umbral de distancia para determinar si los ojos están "siguiendo" la pelota
    private float gazeTime = 0f; // Tiempo acumulado siguiendo la pelota
    private float totalDuration = 60f; // Duración total del seguimiento
    private bool isTracking = false; // Bandera para saber si los ojos están mirando la pelota
    public GameObject MainCamera;

    void Update()
    {
        // Obtener datos del eye tracking
        XR_HTC_eye_tracker.Interop.GetEyeGazeData(out XrSingleEyeGazeDataHTC[] out_gazes);
        XrSingleEyeGazeDataHTC leftGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
        XrSingleEyeGazeDataHTC rightGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC];

        bool leftHitBall = false;
        bool rightHitBall = false;

        if (leftGaze.isValid)
        {
            // Obtener origen y dirección del ojo izquierdo
            Quaternion leftOrientation = leftGaze.gazePose.orientation.ToUnityQuaternion();
            Vector3 leftDirection = leftOrientation * Vector3.forward;
            Vector3 leftEyeOffset = new Vector3(-0.032f, 0.0f, 0.0f);
            Vector3 leftOrigin = MainCamera.transform.position + leftEyeOffset;
            //Vector3 leftOrigin = Camera.main.transform.position + Camera.main.transform.rotation * (leftGaze.gazePose.position.ToUnityVector() + leftEyeOffset);
            //Vector3 leftOrigin = MainCamera.transform.TransformPoint(leftGaze.gazePose.position.ToUnityVector());

            // Dibujar rayo del ojo izquierdo
            leftLineRenderer.SetPosition(0, leftOrigin);
            leftLineRenderer.SetPosition(1, leftOrigin + leftDirection * 50f);

            // Realizar Raycast para el ojo izquierdo
            if (Physics.Raycast(leftOrigin, leftDirection, out RaycastHit leftHit, 100f))
            {
                if (leftHit.collider.gameObject == ball)
                {
                    leftHitBall = true;
                }
            }
        }

        if (rightGaze.isValid)
        {
            // Obtener origen y dirección del ojo derecho
            Quaternion rightOrientation = rightGaze.gazePose.orientation.ToUnityQuaternion();
            Vector3 rightDirection = rightOrientation * Vector3.forward;
            Vector3 rightEyeOffset = new Vector3(0.032f, 0.0f, 0.0f);
            Vector3 rightOrigin = Camera.main.transform.position + rightEyeOffset;
            //Vector3 rightOrigin = Camera.main.transform.position + Camera.main.transform.rotation * (rightGaze.gazePose.position.ToUnityVector() + rightEyeOffset);
            //Vector3 rightOrigin = MainCamera.transform.TransformPoint(rightGaze.gazePose.position.ToUnityVector());
            // Dibujar rayo del ojo derecho
            rightLineRenderer.SetPosition(0, rightOrigin);
            rightLineRenderer.SetPosition(1, rightOrigin + rightDirection * 50f);

            // Realizar Raycast para el ojo derecho
            if (Physics.Raycast(rightOrigin, rightDirection, out RaycastHit rightHit, 100f))
            {
                if (rightHit.collider.gameObject == ball)
                {
                    rightHitBall = true;
                }
            }
        }

        // Si cualquiera de los dos ojos está mirando la pelota, acumula tiempo
        if (leftHitBall || rightHitBall)
        {
            gazeTime += Time.deltaTime;

            if (!isTracking)
            {
                isTracking = true;
                Debug.Log("Comenzaste a mirar la pelota.");
            }
        }
        else
        {
            if (isTracking)
            {
                isTracking = false;
                Debug.Log("Dejaste de mirar la pelota.");
            }
        }

        // Mostrar el tiempo acumulado en consola
        Debug.Log($"Tiempo siguiendo la pelota: {gazeTime:F2} segundos");

        // Termina el seguimiento después del tiempo total especificado
        if (gazeTime >= totalDuration)
        {
            Debug.Log("Fin del seguimiento ocular.");
            // Aquí podrías guardar los datos o cambiar de escena
        }
    }
}



