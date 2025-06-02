using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlHeadMovement4 : MonoBehaviour
{
    public Canvas retry;
    public GameObject camara;
    private Quaternion rotacionInicial;

    public GameObject ball;

    void Start()
    {
        retry.gameObject.SetActive(false);
        rotacionInicial = camara.transform.rotation;
    }

    void Update()
    {
        Quaternion currentRotation = camara.transform.rotation;
        Quaternion deltaRotation = Quaternion.Inverse(rotacionInicial) * currentRotation;
        Vector3 deltaEulerAngles = deltaRotation.eulerAngles;
        deltaEulerAngles.x = Normalizar(deltaEulerAngles.x);
        deltaEulerAngles.y = Normalizar(deltaEulerAngles.y);
        deltaEulerAngles.z = Normalizar(deltaEulerAngles.z);
        if (Mathf.Abs(deltaEulerAngles.x) > 20 || Mathf.Abs(deltaEulerAngles.y) > 20 || Mathf.Abs(deltaEulerAngles.z) > 20)
        {
            ball.gameObject.SetActive(false);
            retry.gameObject.SetActive(true);

        }
    }
    private float Normalizar(float angle)
    {
        if (angle > 180)
            angle -= 360;
        else if (angle < -180)
            angle += 360;

        return angle;
    }
}
