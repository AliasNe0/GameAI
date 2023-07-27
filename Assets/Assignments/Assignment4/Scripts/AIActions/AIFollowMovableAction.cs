using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace ASSIGNMENT4
{
    public class AIFollowMovableAction : MonoBehaviour
    {
        [SerializeField] NavMeshSurface navSurface;
        public bool Active { get; private set; }

        NavMeshAgent navigation;
        Vector3 targetLastPosition;

        public void ResetFollowMovable()
        {
            Active = true;
            navigation.isStopped = true;
            navSurface.BuildNavMesh();
        }
        public void SetNavigation(NavMeshAgent agent)
        {
            navigation = agent;
        }

        public void FollowMovable(GameObject target)
        {
            if (!target) return;
            if (navigation.isStopped || target.transform.position != targetLastPosition)
            {
                targetLastPosition = target.transform.position;
                navigation.SetDestination(targetLastPosition);
                navigation.isStopped = false;
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
