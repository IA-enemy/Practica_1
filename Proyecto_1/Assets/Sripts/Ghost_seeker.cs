using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishSeeker : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform Ghost;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Ghost = FindObjectOfType<GhostWanderer>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(Ghost.position);
    }
}

