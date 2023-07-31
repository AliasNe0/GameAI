using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace ASSIGNMENT5
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
            chaseAction = stateMachine.ChaseAction;
            chaseAction.SetChase(detection, navigation);
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
            if (detection.Tree)
            {
                chaseAction.Chase(detection.Tree);
            }
        }

        public override void OnFixedUpdate()
        {

        }
    }
}
