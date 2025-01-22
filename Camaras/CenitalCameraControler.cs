using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenitalCameraControler : MonoBehaviour
{
    public GameObject player; // Referencia al jugador
    public float height = 20f; // Altura de la cámara
    public float rotationSpeed = 10f; // Velocidad de rotación alrededor del jugador

    void Update()
    {
        // Mantén la cámara encima del jugador
        Vector3 offset = new Vector3(0, height, 0);
        transform.position = player.transform.position + offset;

        // Rota alrededor del jugador
        transform.RotateAround(player.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        transform.LookAt(player.transform.position); // Mantén la cámara mirando al jugador
    }
}
