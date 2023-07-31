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
            detection = stateMachine.Detection;
            animator = stateMachine.AIAnimator;
            navigation = stateMachine.Navigation;
            navSurface = stateMachine.navSurface;
            chaseAction = stateMachine.ChaseAction;
            chaseAction.SetChase(detection, navigation, navSurface);
        }

        public override void OnEnter()
        {
            chaseAction.ResetChase(animator);
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
