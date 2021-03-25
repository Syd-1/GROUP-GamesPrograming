using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    private float stopDistance = 2f;

    public void ChasePlayer(GameObject player, float playerDistance, NavMeshAgent agent)
    {
        agent.speed = 5.5f;

        if (agent.isOnNavMesh && playerDistance >= stopDistance)
        {
            agent.destination = player.transform.position;
        } 
        else if (agent.isOnNavMesh)
        {
            agent.destination = transform.position;
        }
    }
}
