using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public List<Transform> waypoints;

    private enum State
    {
        Patrol,
        Chase,
        Investigate,
        Attack
    }

    private State state;

    private FieldOfView fieldOfView;
    private StateAnimationController stateAnimationController;

    private Patrol patrol;
    private Chase chase;
    private Investigate investigate;
    private Attack attack;

    private GameObject player;
    private NavMeshAgent agent;
    private CapsuleCollider capCollider;

    private float playerDistance;
    private float attackTimer = 0f;
    private float attackRange = 2f;
    private float investigateTimer = 0f;

    private void Awake()
    {
        fieldOfView = GetComponent<FieldOfView>();
        stateAnimationController = GetComponent<StateAnimationController>();
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        capCollider = GetComponent<CapsuleCollider>();
        patrol = GetComponent<Patrol>();
        chase = GetComponent<Chase>();
        investigate = GetComponent<Investigate>();
        attack = GetComponent<Attack>();
        state = State.Patrol;
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        fieldOfView.FindVisibleTarget();
        attackTimer += Time.deltaTime;

        switch (state)
        {
            case State.Patrol:
                patrol.MoveToRandomWaypoint(waypoints, agent);
                IsTargetInFieldOfView();
                break;

            case State.Chase:
                chase.ChasePlayer(player, playerDistance, agent);
                IsTargetInFieldOfView();
                TargetInAttackRange();
                break;

            case State.Investigate:
                investigate.LookForTarget(agent, investigateTimer);
                investigateTimer += Time.deltaTime;
                IsTargetInFieldOfView();
                break;

            case State.Attack:
                IsTargetInFieldOfView();
                TargetInAttackRange();
                if (attackTimer > 2f)
                {
                    capCollider.radius = 0.15f; // sword reach
                    attackTimer = 0;
                }
                break;

            default:
                state = State.Patrol;
                break;
        }

        stateAnimationController.UpdateAnimationController(state.ToString());
        capCollider.radius = 0.05f;
    }

    private void TargetInAttackRange()
    {
        if (playerDistance < attackRange)
        {
            state = State.Attack;
        }
    }

    private void IsTargetInFieldOfView()
    {
        if (fieldOfView.visibleTargets.Count == 1)  {
            state = State.Chase;
        }

        if (fieldOfView.visibleTargets.Count == 0 && fieldOfView.targetLastKnownPosition != Vector3.zero) {
            agent.destination = fieldOfView.targetLastKnownPosition;

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (investigateTimer <= 5f)
                {
                    state = State.Investigate;
                }

                else
                {
                    investigateTimer = 0f;
                    fieldOfView.targetLastKnownPosition = Vector3.zero;
                    state = State.Patrol;
                }
            }
        }
    }
}
