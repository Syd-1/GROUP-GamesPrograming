using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    private Transform targetWaypoint;
    private int randomIndex;

    private float targetDistance;
    private float targetThreshold = 5f;
    private float waitTimer = 0f;

    // Update is called once per frame
    public void MoveToRandomWaypoint(List<Transform> waypoints, NavMeshAgent agent)
    {
        agent.speed = 3f;

        // move to random initial waypoint
        if (!targetWaypoint)
        {
            randomIndex = Random.Range(0, waypoints.Count);
            targetWaypoint = waypoints[randomIndex];
        }

        targetDistance = Vector3.Distance(transform.position, targetWaypoint.position);
        waitTimer += Time.deltaTime;

        // Choose new waypoint if target reached
        if (targetDistance < targetThreshold && waitTimer > 20f)
        {
            randomIndex = Random.Range(0, waypoints.Count);
            targetWaypoint = waypoints[randomIndex];
            waitTimer = Random.Range(0, 10f);
        }

        Debug.Log(agent.isOnNavMesh);
        if (agent.isOnNavMesh)
        {
            agent.destination = targetWaypoint.position;
        }
    }
}
