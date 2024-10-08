using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderingMovementAI : MonoBehaviour
{
    NavMeshAgent agent;

    Vector3 destPoint; // Punto de destino
    public float destDistance = 1.5f; // Distancia m�nima para considerar que se a llegado el objetivo
    public float destSearch = 10f; // Rango de b�squeda aleatoria

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetDest(); // Inicializamos el primer destino
    }
    void Update()
    {
        Patrol(); // Llamamos a la funci�n Patrol
    }

    void Patrol()
    {
        // Comprobamos si el agente ha alcanzado el destino
        if (!agent.pathPending && agent.remainingDistance <= destDistance) //Pathpending es un bool que indica si el agente est� calculando una ruta
        {
            // Si se alcanz� el destino llamamos a SetDest para asignar un nuevo destino
            SetDest();
        }
    }

    void SetDest()
    {
        //Esto de aqui genera un punto de aleatorio
        Vector3 randomDirection = Random.insideUnitSphere * destSearch; //insideUnitSphere genera un vector de radio 1 y lo multiplica por el rango de b�squeda
        randomDirection += transform.position;

        agent.SetDestination(randomDirection);
    }
}
