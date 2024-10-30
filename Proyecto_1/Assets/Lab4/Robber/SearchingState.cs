using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class SearchingState : MonoBehaviour
{
    public bool victimFound = false;

    NavMeshAgent agent;
    private Transform player;

    public float destDistance = 1.5f; // Distancia m�nima para considerar que se a llegado el objetivo
    public float destSearch = 10f; // Rango de b�squeda aleatoria

    public float victimDistance = 10f; // Distancia a la que se detecta al jugador

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!victimFound)
        {
            Patrol();
        }
        if (Vector3.Distance(player.position, transform.position) < victimDistance)
        {
            victimFound = true; //Momento para cambiar de estado a chasing
        }

    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance <= destDistance)
        {
            SetDest();
        }
    }

    void SetDest()
    {
        Vector3 randomDirection = Random.insideUnitSphere * destSearch;
        randomDirection += transform.position;

        agent.SetDestination(randomDirection);
    }
}
