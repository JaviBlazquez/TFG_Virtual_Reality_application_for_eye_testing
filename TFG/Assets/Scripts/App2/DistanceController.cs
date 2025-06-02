using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceController : MonoBehaviour
{
    public Transform quad;  // Referencia al Quad con el texto
    public Slider distanceSlider; // Referencia al Slider
    public Transform player; // Referencia al jugador (o cámara VR)

    private Vector3 initialOffset; // Offset inicial del Quad respecto a la cámara
    private float minDistance = 1f; // Distancia mínima permitida
    private float maxDistance = 50f; // Distancia máxima permitida

    void Start()
    {
        // Guarda la distancia inicial respecto al jugador
        initialOffset = quad.position - player.position;

        // Configura los valores mínimos y máximos del Slider
        distanceSlider.minValue = minDistance;
        distanceSlider.maxValue = maxDistance;
        distanceSlider.value = (minDistance + maxDistance) / 2; // Posición inicial del Slider

        // Asigna la función al Slider para que cambie la distancia
        distanceSlider.onValueChanged.AddListener(UpdateDistance);
    }

    void UpdateDistance(float value)
    {
        // Calcula la nueva posición respetando la distancia mínima
        Vector3 newPosition = player.position + initialOffset.normalized * Mathf.Max(value, minDistance);
        quad.position = newPosition;
    }
}


