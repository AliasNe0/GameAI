using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace ASSIGNMENT4
{
    public class AIFollowMovableAction : MonoBehaviour
    {
        public bool Active { get; private set; }

        AIDetection detection;
        NavMeshAgent navigation;
        NavMeshSurface navSurface;
        Vector3 targetLastPosition;

        public void SetFollowMovable(AIDetection AIdetection, NavMeshAgent agent, NavMeshSurface surface)
        {
            detection = AIdetection;
            navigation = agent;
            navSurface = surface;
        }

        public void ResetFollowMovable()
        {
            Active = true;
            detection.HasPathToMovable = true;
            navigation.isStopped = true;
        }

        public void FollowMovable(GameObject target)
        {
            if (!target) return;
            if (navigation.isStopped || target.transform.position != targetLastPosition)
            {
                targetLastPosition = target.transform.position;
                navigation.SetDestination(targetLastPosition);
                navSurface.BuildNavMesh();
                navigation.isStopped = false;
            }
            if (!navigation.hasPath)
            {
                detection.HasPathToMovable = false;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Movable"))
            {
                Active = false;
                navigation.isStopped = true;
            }
        }
    }
}
