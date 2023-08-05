using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace ASSIGNMENT4
{
    public class AIChaseAction : MonoBehaviour
    {
        [SerializeField] float stopChaseAtDistance = 1f;
        public bool Active = false;

        NavMeshAgent navigation;

        public void SetChase(NavMeshAgent agent)
        {
            navigation = agent;
        }

        public void ResetChase(Animator animator)
        {
            Active = true;
            animator.SetTrigger("Walk");
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Crouch");
        }

        public void Chase(GameObject target)
        {
            if (target.transform.position != navigation.destination)
            {
                navigation.SetDestination(target.transform.position);
                navigation.isStopped = false;
            }
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (navigation.pathStatus != NavMeshPathStatus.PathComplete || distanceToTarget <= stopChaseAtDistance)
            {
                Active = false;
                navigation.isStopped = true;
            }
        }
    }
}
