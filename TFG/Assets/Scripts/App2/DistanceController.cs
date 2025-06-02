using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceController : MonoBehaviour
{
    public Transform quad;  // Referencia al Quad con el texto
    public Slider distanceSlider; // Referencia al Slider
    public Transform player; // Referencia al jugador (o c�mara VR)

    private Vector3 initialOffset; // Offset inicial del Quad respecto a la c�mara
    private float minDistance = 1f; // Distancia m�nima permitida
    private float maxDistance = 50f; // Distancia m�xima permitida

    void Start()
    {
        // Guarda la distancia inicial respecto al jugador
        initialOffset = quad.position - player.position;

        // Configura los valores m�nimos y m�ximos del Slider
        distanceSlider.minValue = minDistance;
        distanceSlider.maxValue = maxDistance;
        distanceSlider.value = (minDistance + maxDistance) / 2; // Posici�n inicial del Slider

        // Asigna la funci�n al Slider para que cambie la distancia
        distanceSlider.onValueChanged.AddListener(UpdateDistance);
    }

    void UpdateDistance(float value)
    {
        // Calcula la nueva posici�n respetando la distancia m�nima
        Vector3 newPosition = player.position + initialOffset.normalized * Mathf.Max(value, minDistance);
        quad.position = newPosition;
    }
}


