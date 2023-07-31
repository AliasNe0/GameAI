using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ASSIGNMENT4
{
    public class AIPushMovableAction : MonoBehaviour
    {
        public bool Active { get; private set; }
        NavMeshAgent navigation;

        public void ResetPushMovable(NavMeshAgent agent, Animator animator)
        {
            Active = true;
            navigation = agent;
        }

        public void SetCollectablePath(GameObject collectable)
        {
            navigation.SetDestination(collectable.transform.position);
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Movable"))
            {
                Active = false;
            }
        }
    }
}
