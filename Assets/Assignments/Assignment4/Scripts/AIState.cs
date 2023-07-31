using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace ASSIGNMENT4
{
    public abstract class AIState
    {
        protected AIStateMachine stateMachine;
        protected AIDetection detection;
        protected Animator animator;
        protected NavMeshAgent navigation;
        protected NavMeshSurface navSurface;

        public AIState() { }

        public AIState(AIStateMachine sm)
        {
            stateMachine = sm;
        }

        public abstract void OnStart();
        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();
    }
}
