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
    private SoundManager soundManager;

    private Patrol patrol;
    private Chase chase;
    private Investigate investigate;

    private GameObject player;
    private GameObject music;
    private NavMeshAgent agent;

    private float playerDistance;
    private float attackTimer = 0f;
    private float attackRange = 4f;
    private float investigateTimer = 0f;
    private float soundTimer = 0f;

    private AudioSource audioSource;

    private bool screamPlayed = false;

    private void Awake()
    {
        player = GameObject.Find("Player");
        music = GameObject.Find("Music");

        fieldOfView = GetComponent<FieldOfView>();
        stateAnimationController = GetComponent<StateAnimationController>();
        agent = GetComponent<NavMeshAgent>();
        patrol = GetComponent<Patrol>();
        chase = GetComponent<Chase>();
        investigate = GetComponent<Investigate>();
        audioSource = GetComponent<AudioSource>();
        soundManager = GetComponent<SoundManager>();

        state = State.Patrol;
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        fieldOfView.FindVisibleTarget();
        attackTimer += Time.deltaTime;
        soundTimer += Time.deltaTime;

        switch (state)
        {
            case State.Patrol:
                patrol.MoveToRandomWaypoint(waypoints, agent);
                IsTargetInFieldOfView();

                if (soundTimer > 5f)
                {
                    soundManager.PlayRandomSound(audioSource);
                    soundTimer = 0;
                }
                break;

            case State.Chase:
                chase.ChasePlayer(player, playerDistance, agent);

                if (!screamPlayed)
                {
                    soundManager.EnemyScream(audioSource, music);
                    screamPlayed = true;
                }

                IsTargetInFieldOfView();
                TargetInAttackRange();
                break;

            case State.Investigate:
                investigate.LookForTarget(agent, investigateTimer);
                investigateTimer += Time.deltaTime;

                if (screamPlayed == true)
                {
                    soundManager.EnemyInvestigating(audioSource, music);
                }
                screamPlayed = false;

                IsTargetInFieldOfView();
                break;

            case State.Attack:
                IsTargetInFieldOfView();
                TargetInAttackRange();

                if (attackTimer > 2f)
                {
                    soundManager.EnemyAttack(audioSource);
                    attackTimer = 0;
                }
                break;

            default:
                state = State.Patrol;
                break;
        }

        stateAnimationController.UpdateAnimationController(state.ToString(), attackTimer);
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
