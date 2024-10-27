using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bolsito : MonoBehaviour
{
    public float monedicas = 20;
    public bool mehanrobado = false;

    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (mehanrobado)
        {
            monedicas -= 1;
            Ladroon();
        }
    }

    void Ladroon()
    {
        agent.SetDestination(GameObject.Find("Ladron").transform.position);
        //Aqui hay que poner que si el ladron esta escondido se ponga mehanrobado en false
    }
}
