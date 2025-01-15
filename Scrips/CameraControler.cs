using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    // referencia al objeto jugador
    public GameObject player;
    // distancia entre la camara y el juegador
    private Vector3 offset;
    void Start()
    {

        // Calcula la posicion offset entre la camara y el jugador
        offset = transform.position - player.transform.position; 
    }

       void LateUpdate()
    {
            transform.position = player.transform.position + offset; 
    }
}
