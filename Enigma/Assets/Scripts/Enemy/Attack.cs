using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator animator;

    public void AttackPlayer(SphereCollider sword) {
    
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
            sword.enabled = true;
        }
        else
        {
            sword.enabled = false;
        }

    }
}
