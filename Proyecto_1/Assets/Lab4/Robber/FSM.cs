using UnityEngine;
using System.Collections;
using UnityEngine.UIElements.Experimental;

public class FSM : MonoBehaviour
{
    private SearchingState searchingState;
    private ChasingState chasingState;
    private HideState hideState;

   
   
    private WaitForSeconds wait = new WaitForSeconds(0.05f); // == 1/20
    delegate IEnumerator State();
    private State currentState;

    IEnumerator Start()
    {
        searchingState = GetComponent<SearchingState>();
        chasingState = GetComponent<ChasingState>();
        hideState = GetComponent<HideState>();

        yield return wait;

        TransitionToSearching();

        while (enabled)
            yield return StartCoroutine(currentState());
    }
    private void TransitionToSearching()
    {
        searchingState.EnterState();
        currentState = Searching;
    }

    private void TransitionToChasing()
    {
        chasingState.EnterState();
        currentState = Chasing;
    }

    private void TransitionToHiding()
    {
        hideState.EnterState();
        currentState = Hiding;
    }
    IEnumerator Searching()
    {
        while (!searchingState.victimFound)
        {
            yield return wait;
        }

        searchingState.ExitState();
        TransitionToChasing();
    }

    IEnumerator Chasing()
    {

        while (!chasingState.inDistance)
        {
            yield return wait;
        }

        chasingState.ExitState();
        TransitionToHiding();
    }


    IEnumerator Hiding()
    {
        while (!hideState.isHidden)
        {
            yield return wait;
        }

        hideState.ExitState();
        TransitionToSearching();
    }
}