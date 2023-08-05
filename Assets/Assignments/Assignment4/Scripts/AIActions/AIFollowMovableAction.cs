using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace ASSIGNMENT4
{
    public class AIFollowMovableAction : MonoBehaviour
    {
        public bool Active = false;

        NavMeshAgent navigation;
        NavMeshSurface navSurface;

        public void SetFollowMovable(NavMeshAgent agent, NavMeshSurface surface)
        {
            navigation = agent;
            navSurface = surface;
        }

        public void ResetFollowMovable(Animator animator)
        {
            Active = true;
            animator.SetTrigger("Walk");
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Crouch");
        }

        public void FollowMovable(GameObject target)
        {
            if (target.transform.position != navigation.destination)
            {
                navSurface.BuildNavMesh();
                navigation.SetDestination(target.transform.position);
                navigation.isStopped = false;
            }
            if (navigation.pathStatus == NavMeshPathStatus.PathInvalid)
            {
                navigation.isStopped = true;
                Active = false;
            }
        }
    }
}
