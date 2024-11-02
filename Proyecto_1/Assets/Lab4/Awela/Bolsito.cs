using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bolsito : MonoBehaviour
{
    public float monedicas = 20;
    public bool mehanrobado = false;
    public float wanderRadius = 5.0f; // Radio del wander
    public float wanderInterval = 2.0f; // Tiempo que quieres que pase entre cada wander

    private NavMeshAgent agent;
    private HideState ladronHideState;
    private Vector3 wanderTarget;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Wander());
    }

    void Update()
    {
        if (mehanrobado)
        {
            monedicas -= 1;
            Ladroon();
            mehanrobado = false;
        }
        //else
        //{
        //    if (ladronHideState != null && ladronHideState.isDetected)
        //    {
        //        mehanrobado = false; //Si el ladron esta escondido ya no le roban
        //    }
        //}
    }

    void Ladroon()
    {
        
        if (ladronHideState != null && !ladronHideState.isHidden)
        {
            agent.SetDestination(GameObject.Find("Ladron").transform.position);//Comprovar que el ladron este escondido
        }
    }
    IEnumerator Wander()
    {
        while (true)
        {
            if (!mehanrobado) // Que solo haga el wander si no le estan robando
            {
                wanderTarget = new Vector3(Random.Range(-wanderRadius, wanderRadius), 0, Random.Range(-wanderRadius, wanderRadius)) + transform.position;
                agent.SetDestination(wanderTarget);
            }

            yield return new WaitForSeconds(wanderInterval);
        }
    }

    public void StartBeingRobbed()
    {
        mehanrobado = true;
        
    }

    public void SetLadronHideState(HideState hideState)
    {
        ladronHideState = hideState;
    }
}
