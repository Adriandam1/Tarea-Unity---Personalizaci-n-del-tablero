using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // AÑADIMOS para la AI


// Hemos arrastrado el GameObject "Player" al playerslot del objeto "Enemy"
public class EnemyMovement : MonoBehaviour
{

    public Transform Player;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        // asignamos la variable NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // indicamos al enemigo que debe ir a las coordenadas del player
        // la "destination" del enemigo se actualizara a la posicion de Player
        if (Player != null)
            {    
            navMeshAgent.SetDestination(Player.position);
            //Debug.Log(Player.position); // posicion del jugador en consola
            }
    }
}
