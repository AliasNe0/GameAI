using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace ASSIGNMENT5
{
    public class AIChaseAction : MonoBehaviour
    {
        [SerializeField] float stopChaseAtDistance = 1f;
        public bool Active { get; private set; }

        AIDetection detection;
        NavMeshAgent navigation;

        public void SetChase(AIDetection AIdetection, NavMeshAgent agent)
        {
            detection = AIdetection;
            navigation = agent;
        }

        public void ResetChase(Animator animator)
        {
            Active = true;
            navigation.isStopped = true;
            detection.HasPathToTree = false;
            animator.SetTrigger("Walk");
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Chop");
        }

        public void Chase(GameObject target)
        {
            if (!target) return;
            if (navigation.isStopped)
            {
                detection.HasPathToTree = true;
                navigation.SetDestination(target.transform.position);
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
                detection.HasPathToTree = false;
            }
        }
    }
}
