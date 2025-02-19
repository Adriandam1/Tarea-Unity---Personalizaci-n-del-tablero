using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenitalCameraControler : MonoBehaviour
{
    public GameObject player; // Referencia al jugador
    public float height = 20f; // Altura de la cámara
    public float rotationSpeed = 10f; // Velocidad de rotación alrededor del jugador
    
    private Vector3 offset;
    // variable para indicar si el jugador existe o es null(false)
    private bool JugadorVivo = true;

    void Start()
    {

        // Calcula la posicion offset entre la camara y el jugador
        // Calcula el vector entre la camara y el jugador con las posiciones iniciales
        offset = transform.position - player.transform.position; 
    }

    void LateUpdate()
    {
        if (!JugadorVivo) return; // Si isActive es false, no ejecuta LateUpdate

        if (player != null)
        {
            transform.position = player.transform.position + offset;
        }
        else
        {
            Debug.LogWarning("Player es null en LateUpdate. Desactivando seguimiento.");
            JugadorVivo = false; // Desactiva LateUpdate si el player desaparece
        }
    }
    /*
    void Update()
    {
        // Mantén la cámara encima del jugador
        Vector3 offset = new Vector3(0, height, 0);
        transform.position = player.transform.position + offset;

        // Rota alrededor del jugador
        transform.RotateAround(player.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        transform.LookAt(player.transform.position); // Mantén la cámara mirando al jugador
    }
    */
}
