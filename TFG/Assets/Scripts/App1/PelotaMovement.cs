using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBallMovementXY : MonoBehaviour
{
    public Camera mainCamera;  // Cámara principal (asigna la cámara del jugador)
    public float speed = 2f;   // Velocidad de la pelota
    public Vector2 areaSize = new Vector2(0.2f, 0.2f); // Tamaño del área de movimiento (solo X e Y)
    public float movementDelay = 1f; // Tiempo entre cambios de dirección
    public float ballInFront = 2f;

    private Vector3 direction; // Dirección actual de la pelota
    private Vector3 areaCenter; // Centro del área de movimiento
    private float timer;       // Temporizador para cambio de dirección

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Asignar la cámara principal si no está configurada
        }

        // Calcular el centro del área de movimiento frente a la cámara
        areaCenter = mainCamera.transform.position + mainCamera.transform.forward * ballInFront; // 2 metros frente a la cámara
        ChangeDirection();
    }

    

    void Update()
    {
        timer += Time.deltaTime;

        // Cambiar la dirección después del retraso
        if (timer >= movementDelay)
        {
            ChangeDirection();
            timer = 0;
        }

        // Mover la pelota en la dirección actual
        transform.position += direction * speed * Time.deltaTime;

        // Restringir la posición dentro del área en X e Y
        ClampPositionToArea();
    }

    void ChangeDirection()
    {
        // Generar una nueva dirección aleatoria solo en X e Y
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
    }

    void ClampPositionToArea()
    {
        // Limitar la posición dentro del área definida en X e Y
        Vector3 clampedPosition = transform.position;
        //clampedPosition.x = Mathf.Clamp(transform.position.x, areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2);
        //clampedPosition.y = Mathf.Clamp(transform.position.y, areaCenter.y - areaSize.y / 2, areaCenter.y + areaSize.y / 2);
        clampedPosition.x = Mathf.Clamp(transform.position.x, -0.3f, 0.3f);
        clampedPosition.y = Mathf.Clamp(transform.position.y, 1f, 1.75f);
        // Mantener la posición en Z fija frente a la cámara
        clampedPosition.z = areaCenter.z;

        transform.position = clampedPosition;
    }
}
