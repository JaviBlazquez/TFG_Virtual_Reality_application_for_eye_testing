using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBallMovementXY : MonoBehaviour
{
    public Camera mainCamera;  // C�mara principal (asigna la c�mara del jugador)
    public float speed = 2f;   // Velocidad de la pelota
    public Vector2 areaSize = new Vector2(0.2f, 0.2f); // Tama�o del �rea de movimiento (solo X e Y)
    public float movementDelay = 1f; // Tiempo entre cambios de direcci�n
    public float ballInFront = 2f;

    private Vector3 direction; // Direcci�n actual de la pelota
    private Vector3 areaCenter; // Centro del �rea de movimiento
    private float timer;       // Temporizador para cambio de direcci�n

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Asignar la c�mara principal si no est� configurada
        }

        // Calcular el centro del �rea de movimiento frente a la c�mara
        areaCenter = mainCamera.transform.position + mainCamera.transform.forward * ballInFront; // 2 metros frente a la c�mara
        ChangeDirection();
    }

    

    void Update()
    {
        timer += Time.deltaTime;

        // Cambiar la direcci�n despu�s del retraso
        if (timer >= movementDelay)
        {
            ChangeDirection();
            timer = 0;
        }

        // Mover la pelota en la direcci�n actual
        transform.position += direction * speed * Time.deltaTime;

        // Restringir la posici�n dentro del �rea en X e Y
        ClampPositionToArea();
    }

    void ChangeDirection()
    {
        // Generar una nueva direcci�n aleatoria solo en X e Y
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
    }

    void ClampPositionToArea()
    {
        // Limitar la posici�n dentro del �rea definida en X e Y
        Vector3 clampedPosition = transform.position;
        //clampedPosition.x = Mathf.Clamp(transform.position.x, areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2);
        //clampedPosition.y = Mathf.Clamp(transform.position.y, areaCenter.y - areaSize.y / 2, areaCenter.y + areaSize.y / 2);
        clampedPosition.x = Mathf.Clamp(transform.position.x, -0.3f, 0.3f);
        clampedPosition.y = Mathf.Clamp(transform.position.y, 1f, 1.75f);
        // Mantener la posici�n en Z fija frente a la c�mara
        clampedPosition.z = areaCenter.z;

        transform.position = clampedPosition;
    }
}
