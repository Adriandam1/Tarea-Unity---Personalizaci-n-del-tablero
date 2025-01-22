using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraControler : MonoBehaviour
{
    // referencia al objeto jugador
    public GameObject player;
    // distancia entre la camara y el juegador
    private Vector3 offset;
    void Start()
    {

        // Calcula la posicion offset entre la camara y el jugador
        // Calcula el vector entre la camara y el jugador con las posiciones iniciales
        offset = transform.position - player.transform.position; 
    }

       void LateUpdate()
    {
        // Para mantener la posición de la camara con respecto al jugador
        transform.position = player.transform.position + offset; 
    }
}
