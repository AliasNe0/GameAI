using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ASSIGNMENT2
{
    public class AIChaseAction : MonoBehaviour
    {
        [SerializeField] float stopChaseAtDistance = 1f;
        public bool Active { get; private set; }

        Vector3 targetLastPosition;

        public void ResetChase(NavMeshAgent navigation)
        {
            Active = true;
            navigation.isStopped = true;
        }
        public void Chase(GameObject target, NavMeshAgent navigation)
        {
            if (!target) return;
            if (navigation.isStopped || target.transform.position != targetLastPosition)
            {
                targetLastPosition = target.transform.position;
                navigation.SetDestination(targetLastPosition);
                navigation.isStopped = false;
            }
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= stopChaseAtDistance)
            {
                Active = false;
                navigation.isStopped = true;
            }
        }
    }
}
