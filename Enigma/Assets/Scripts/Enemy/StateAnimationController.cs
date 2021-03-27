using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateAnimationController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public void UpdateAnimationController(string state, float attackTimer)
    {
        switch (state)
        {
            case "Patrol":
                if (agent.velocity.magnitude > 0.1f)
                {
                    animator.SetFloat("MovementSpeed", 0.5f);
                }
                else
                {
                    animator.SetFloat("MovementSpeed", 0);
                }
                animator.SetBool("IsInvestigating", false);
                animator.SetBool("IsAttacking", false);
                break;

           case "Chase":
                animator.SetFloat("MovementSpeed", 1f);
                animator.SetBool("IsInvestigating", false);
                animator.SetBool("IsAttacking", false);
                break;

           case "Investigate":
                animator.SetBool("IsInvestigating", true);
                animator.SetBool("IsAttacking", false);
                break;

           case "Attack":
                if (attackTimer > 2f)
                {
                    animator.SetBool("IsAttacking", true);
                }
                animator.SetBool("IsInvestigating", false);
                break;
        }
    }
}
