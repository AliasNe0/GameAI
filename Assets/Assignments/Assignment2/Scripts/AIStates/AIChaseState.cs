using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT2
{
    public class AIChaseState : AIState
    {
        AIDetection detection;
        AIChaseAction chaseAction;

        public AIChaseState(AIStateMachine sm) : base(sm) { }

        public override void OnStart()
        {
            detection = stateMachine.Detection;
            chaseAction = stateMachine.ChaseAction;
        }

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {
            chaseAction.ResetChase();
        }

        public override void OnUpdate()
        {

        }

        public override void OnFixedUpdate()
        {
            chaseAction.Chase(detection.CollectableToPickUp);
        }
    }
}
