using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace ASSIGNMENT4
{
    public class AIChaseState : AIState
    {
        AIChaseAction chaseAction;

        public AIChaseState(AIStateMachine sm) : base(sm) { }

        public override void OnStart()
        {
            animator = stateMachine.AIAnimator;
            detection = stateMachine.Detection;
            navigation = stateMachine.Navigation;
            chaseAction = stateMachine.ChaseAction;
            chaseAction.SetChase(navigation);
        }

        public override void OnEnter()
        {
            chaseAction.ResetChase(animator);
            if (detection.CollectableToPickUp)
            {
                navigation.isStopped = false;
                chaseAction.Chase(detection.CollectableToPickUp);
            }
        }

        public override void OnExit()
        {
            chaseAction.Active = false;
            navigation.isStopped = true;
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
