using UnityEngine;

public class ZombieVision : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 10f;

    private bool isPlayerDetected;

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (!isPlayerDetected)
            {
                isPlayerDetected = true;
                Debug.Log("¡Jugador detectado en el rango!");
            }
        }
        else
        {
            isPlayerDetected = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
