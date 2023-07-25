using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT1
{
    public class AIState
    {
        protected AIStateMachine stateMachine;

        public AIState() { }

        public AIState(AIStateMachine sm)
        {
            stateMachine = sm;
        }

        public virtual void OnStart() { }
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void OnUpdate() { }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
        public virtual void OnFixedUpdate() { }
        public virtual void OnLateUpdate() { }
    }
}
