using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingState : MonoBehaviour
{
    public bool isChasing = false; //Si el ladrón está persiguiendo al jugador
    public bool isDetected = false; //Si el jugador ha detectado al ladrón
    public bool inDistance = false; //Si el ladron está a una distancia determinada del jugador

    public float distanceToChase = 5.0f; //Distancia a la que el ladrón persigue al jugador

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
        //Quitas algo al jugador y este te detecta/ Momento para cambiar a hidding
    }
}
