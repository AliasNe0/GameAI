using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT1
{
    public class AIIdleState : AIState
    {
        AIDetection detection;
        AIIdleAction idleAction;

        public AIIdleState(AIStateMachine sm) : base(sm) { }

        public override void OnStart()
        {
            detection = stateMachine.Detection;
            idleAction = stateMachine.IdleAction;
        }

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {

        }

        public override void OnEnable()
        {

        }

        public override void OnDisable()
        {

        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnLateUpdate()
        {

        }
    }
}
