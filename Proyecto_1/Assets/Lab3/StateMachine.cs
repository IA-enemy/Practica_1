using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 destPoint; // Punto de destino
    public float destDistance = 1.5f; // Distancia m�nima para considerar que se a llegado el objetivo
    public float destSearch = 10f; // Rango de b�squeda aleatoria
    public Transform human; // El transform del "human" que queremos detectar
    public float AngleView = 110f; // �ngulo de visi�n en grados
    public float rangedetection = 15f; // Rango de detecci�n
    public float alertRadius = 20f;

    enum State { Wandering, Chasing } // Estados posibles
    State currentState; // El estado actual

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetDest(); // Inicializamos el primer destino
        currentState = State.Wandering; // Comenzamos en el estado de "Wandering"
    }
    void Update()
    {
        if (HumanVisible())
        {
            currentState = State.Chasing;
            AlertNearbyZombies();
        }
        else
        {
            currentState = State.Wandering;
        }
        switch (currentState)
        {
            case State.Wandering:
                Patrol();
                break;
            case State.Chasing:
                Chase();
                break;
        }
        //Patrol(); // Llamamos a la funci�n Patrol
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

    void Chase()
    {
        agent.SetDestination(human.position);
    }
    void SetDest()
    {
        //Esto de aqui genera un punto de aleatorio
        Vector3 randomDirection = Random.insideUnitSphere * destSearch; //insideUnitSphere genera un vector de radio 1 y lo multiplica por el rango de b�squeda
        randomDirection += transform.position;

        agent.SetDestination(randomDirection);
    }
    bool HumanVisible()
    {
        Vector3 directionHuman = human.position - transform.position;
        float distanceHuman = directionHuman.magnitude;
        if (distanceHuman < rangedetection)
        {
            float angle = Vector3.Angle(directionHuman, transform.forward);
            if (angle < AngleView * 0.5)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up, directionHuman.normalized, out hit, rangedetection))
                {
                    if (hit.transform == human)
                    {
                        return true;
                    }
                }

            }
        }
        return false;
    }

    void AlertNearbyZombies()
    {
        // Crear una esfera para detectar otros zombies cercanos
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, alertRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            // Verificar si los objetos detectados son otros zombies
            StateMachine otherZombie = hitCollider.GetComponent<StateMachine>();

            if (otherZombie != null && otherZombie != this) // No queremos avisarnos a nosotros mismos
            {
                otherZombie.BecomeAlerted(); // Llamar a la funci�n para alertar al otro zombie
            }
        }
    }
    public void BecomeAlerted()
    {
        // Este zombie ahora tambi�n persigue al humano
        currentState = State.Chasing;
    }

}