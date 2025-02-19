using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousCameraControler : MonoBehaviour
{
    public GameObject player; // Referencia al jugador
    public Transform[] waypoints; // Puntos por los que se moverá la cámara
    public float speed = 5f; // Velocidad de movimiento
    private int currentWaypointIndex = 0;

    void Update()
    {
        // Mantén la cámara siguiendo al jugador mientras se mueve entre waypoints
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);
        transform.LookAt(player.transform.position); // La cámara siempre apunta al jugador

        // Si llega al waypoint actual, pasa al siguiente
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}