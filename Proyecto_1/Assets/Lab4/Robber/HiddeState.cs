using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HideState : MonoBehaviour
{
    public bool isHidden = false; //Si el ladrón está escondido
    public bool isDetected = false; //Si el ladrón ha sido detectado

    private NavMeshAgent agent;
    public List<GameObject> hideSpots = new List<GameObject>();

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isDetected && !isHidden)
        {
            SearchForHideSpot();
        }
    }

    void SearchForHideSpot()
    {
        GameObject closestHideSpot = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject hideSpot in hideSpots)
        {
            float distance = Vector3.Distance(transform.position, hideSpot.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestHideSpot = hideSpot;
            }
        }

        if (closestHideSpot != null)
        {
            agent.SetDestination(closestHideSpot.transform.position);
        }
        else
        {
            Debug.LogWarning("No se encontraron puntos de escondite.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HidePoint"))
        {
            isHidden = true;
            isDetected = false;
            WaitOnSpot();
        }
    }

    void WaitOnSpot()
    {
        StartCoroutine(HideWaitRoutine());
        //Una vez acabe la cortiana es momento de cambiar al estado de searching
    }

    IEnumerator HideWaitRoutine()
    {
        yield return new WaitForSeconds(5f); 
        isHidden = false;
    }
}
