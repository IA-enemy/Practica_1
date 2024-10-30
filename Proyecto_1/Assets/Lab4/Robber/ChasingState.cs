using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingState : MonoBehaviour
{
    public bool isChasing = false; //Si el ladr�n est� persiguiendo al jugador
    public bool isDetected = false; //Si el jugador ha detectado al ladr�n
    public bool inDistance = false; //Si el ladron est� a una distancia determinada del jugador

    public float distanceToChase = 5.0f; //Distancia a la que el ladr�n persigue al jugador

    private NavMeshAgent agent;
    private Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!isDetected)
        {
            if (!isChasing)
            {
                isChasing = true;
            }

            Chase();
        }
    }

    void Chase()
    {
        agent.SetDestination(player.position);

        if (Vector3.Distance(transform.position, player.position) < distanceToChase)
        {
            inDistance = true;
            Borrow();
        }
    }

    void Borrow()
    {
        //Quitas algo al jugador y este te detecta
        Bolsito abuela = GameObject.FindGameObjectWithTag("Abuela").GetComponent<Bolsito>();
        if (abuela != null)
        {
            abuela.StartBeingRobbed(); //Enviar a Abuela que le estan empezando a robar
        }
    }
}
