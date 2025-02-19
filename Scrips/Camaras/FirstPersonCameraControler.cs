using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraControler : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensibilidad del ratón
    public Transform playerBody; // Referencia al cuerpo del jugador
    public float distanceFromPlayer = 2f; // Distancia de la cámara respecto al jugador

    private float xRotation = 0f; // Rotación en el eje X (arriba y abajo)
    private float yRotation = 0f; // Rotación en el eje Y (izquierda y derecha)
    
    private Vector3 offset; // Offset para que la cámara se mantenga a una distancia fija del jugador

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor en el centro
        offset = transform.position - playerBody.position; // Calcula la distancia inicial entre cámara y jugador
    }

    void Update()
    {
        // Captura el movimiento del ratón
        // Time.deltaTime -> tiempo de cada frame
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Controla la rotación vertical (arriba/abajo)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita la rotación vertical
        

        // Controla la rotación horizontal (izquierda/derecha) alrededor del eje Y del cuerpo del jugador
        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        
        
        // Actualiza la posición de la cámara para que siga al jugador.
        FollowPlayer();
    }

    // Método para que la cámara siga al jugador
    void FollowPlayer()
    {
        // La cámara sigue al jugador con la misma distancia y offset que calculamos inicialmente
        transform.position = playerBody.position + offset.normalized * distanceFromPlayer;
    }
}
