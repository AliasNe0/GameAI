using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT2
{
    public abstract class AIState
    {
        protected AIStateMachine stateMachine;

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
