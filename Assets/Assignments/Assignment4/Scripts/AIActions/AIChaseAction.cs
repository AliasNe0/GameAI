using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace ASSIGNMENT4
{
    public class AIChaseAction : MonoBehaviour
    {
        [SerializeField] float stopChaseAtDistance = 1f;
        public bool Active { get; private set; }

        AIDetection detection;
        NavMeshAgent navigation;
        NavMeshSurface navSurface;
        Vector3 targetLastPosition;

        public void SetChase(AIDetection AIdetection, NavMeshAgent agent, NavMeshSurface surface)
        {
            detection = AIdetection;
            navigation = agent;
            navSurface = surface;
        }

        public void ResetChase(Animator animator)
        {
            Active = true;
            navigation.isStopped = true;
            detection.HasPathToCollectable = false;
            animator.SetTrigger("Walk");
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Crouch");
        }

        public void Chase(GameObject target)
        {
            if (!target) return;
            if (navigation.isStopped || target.transform.position != targetLastPosition)
            {
                detection.HasPathToCollectable = true;
                navSurface.BuildNavMesh();
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
            if (!navigation.hasPath)
            {
                detection.HasPathToCollectable = false;
            }
        }
    }
}
