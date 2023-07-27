using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ASSIGNMENT4
{
    public class AIChaseState : AIState
    {
        NavMeshAgent navigation;
        AIDetection detection;
        AIChaseAction chaseAction;

        public AIChaseState(AIStateMachine sm) : base(sm) { }

        public override void OnStart()
        {
            navigation = stateMachine.Navigation;
            detection = stateMachine.Detection;
            chaseAction = stateMachine.ChaseAction;
        }

        public override void OnEnter()
        {
            chaseAction.SetNavigation(navigation);
            chaseAction.ResetChase();
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {
            if (detection.CollectableToPickUp)
            {
                chaseAction.Chase(detection.CollectableToPickUp);
            }
        }

        public override void OnFixedUpdate()
        {

        }
    }
}
