using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public Vector3 targetLastKnownPosition;

    private Collider[] targetsInViewRaduius;
    private Transform target;
    private Vector3 directionToTarget;
    private float distanceToTarget;
    
    public List<Transform> visibleTargets;

    public void FindVisibleTarget()
    {
        visibleTargets.Clear();
        targetsInViewRaduius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        foreach (Collider collider in targetsInViewRaduius )
        {
            target = collider.transform;
            directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2) 
            {
                distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    targetLastKnownPosition = target.transform.position;
                }
            }
        }
    }

     public Vector3 DirectionFromAngle(float angle, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angle += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
