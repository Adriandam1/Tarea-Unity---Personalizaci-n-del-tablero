using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    // referencia al objeto jugador
    public GameObject player;
    // distancia entre la camara y el juegador
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
}
