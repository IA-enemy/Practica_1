using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoberMachine : MonoBehaviour
{
    public SearchingState searchingState;
    public ChasingState chasingState;
    public HideState hideState;
    public enum State
    {
        Search,
        Chase,
        Hide
    }

    private State currentState;

    void Start()
    {
        TransitionToState(State.Search);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Search:
                if (searchingState.victimFound)
                {
                    TransitionToState(State.Chase);
                }
                break;

            case State.Chase:
                if (chasingState.inDistance)
                {
                    TransitionToState(State.Hide);
                }
                else if (!chasingState.isChasing)
                {
                    TransitionToState(State.Search);
                }
                break;

            case State.Hide:
                if (!hideState.isHidden)
                {
                    TransitionToState(State.Search);
                }
                break;

        }
}
    void TransitionToState(State newState)
    {
        // Finalizamos el estado actual
        switch (currentState)
        {
            case State.Search:
                searchingState.ExitState();
                break;
            case State.Chase:
                chasingState.ExitState();
                break;
            case State.Hide:
                hideState.ExitState();
                break;
        }

        // Activamos el nuevo estado
        currentState = newState;
        switch (currentState)
        {
            case State.Search:
                searchingState.EnterState();
                break;
            case State.Chase:
                chasingState.EnterState();
                break;
            case State.Hide:
                hideState.EnterState();
                break;
        }
    }
}
