using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraRotationDetector : MonoBehaviour
{
    public Canvas canvas;
    public TextMeshProUGUI instruction3;
    public GameObject buttom2;
    public GameObject buttom3;
    public GameObject camaraVR;
    private Quaternion initialRotation;

    public GameObject pelota;

    void Start()
    {
        initialRotation = camaraVR.transform.rotation;
    }

    void Update()
    {
        Quaternion currentRotation = camaraVR.transform.rotation;
        Quaternion deltaRotation = Quaternion.Inverse(initialRotation) * currentRotation;
        Vector3 deltaEulerAngles = deltaRotation.eulerAngles;
        deltaEulerAngles.x = NormalizeAngle(deltaEulerAngles.x);
        deltaEulerAngles.y = NormalizeAngle(deltaEulerAngles.y);
        deltaEulerAngles.z = NormalizeAngle(deltaEulerAngles.z);
        if (Mathf.Abs(deltaEulerAngles.x) > 20 || Mathf.Abs(deltaEulerAngles.y) > 20 || Mathf.Abs(deltaEulerAngles.z) > 20)
        {
            pelota.gameObject.SetActive(false);
            canvas.gameObject.SetActive(true);
            instruction3.gameObject.SetActive(true);
            buttom2.gameObject.SetActive(true);
            buttom3.gameObject.SetActive(true);
        }
    }
    private float NormalizeAngle(float angle)
    {
        if (angle > 180)
            angle -= 360;
        else if (angle < -180)
            angle += 360;

        return angle;
    }
}

