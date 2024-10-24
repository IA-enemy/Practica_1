using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 destPoint; // Punto de destino
    public float destDistance = 1.5f; // Distancia mínima para considerar que se a llegado el objetivo
    public float destSearch = 10f; // Rango de búsqueda aleatoria
    public Transform human; // El transform del "human" que queremos detectar
    public float AngleView = 110f; // Ángulo de visión en grados
    public float rangedetection = 15f; // Rango de detección
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
        //Patrol(); // Llamamos a la función Patrol
    }

    void Patrol()
    {
        // Comprobamos si el agente ha alcanzado el destino
        if (!agent.pathPending && agent.remainingDistance <= destDistance) //Pathpending es un bool que indica si el agente está calculando una ruta
        {
            // Si se alcanzó el destino llamamos a SetDest para asignar un nuevo destino
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
        Vector3 randomDirection = Random.insideUnitSphere * destSearch; //insideUnitSphere genera un vector de radio 1 y lo multiplica por el rango de búsqueda
        randomDirection += transform.position;

        agent.SetDestination(randomDirection);
    }
    bool HumanVisible()
    {
        //calculamos la distancia entre el zombie y el humano
        Vector3 directionHuman = human.position - transform.position;
        float distanceHuman = directionHuman.magnitude;
        //comprobamos si el humano esta dentro del rango de deteccion
        if (distanceHuman < rangedetection)
        {
            //calculamos el angulo entre el zombie y el humano
            float angle = Vector3.Angle(directionHuman, transform.forward);
            //comprobamos si el angulo esta dentro del campo de vision
            if (angle < AngleView * 0.5)
            {
                RaycastHit hit;
                //usamos el raycast para ver si hay obstaculos entre el zombie y el humano
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
        // creamos una esfera para detectar objetos cercanos
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, alertRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            // Verificamos si los objetos cercanos son zombies
            if (hitCollider.gameObject != this.gameObject)
            {
                // Enviamos un mensaje para que los zombies cercanos reciban la funcion BecomeAlerted
                hitCollider.gameObject.BroadcastMessage("BecomeAlerted", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    public void BecomeAlerted()
    {
        // Este zombie ahora también persigue al humano
        currentState = State.Chasing;
    }

}