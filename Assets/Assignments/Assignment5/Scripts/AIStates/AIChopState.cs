using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT5
{
    public class AIChopState : AIState
    {
        AIChopAction chopAction;

        public AIChopState(AIStateMachine sm) : base(sm) { }

        public override void OnStart()
        {
            animator = stateMachine.AIAnimator;
            detection = stateMachine.Detection;
            chopAction = stateMachine.ChopAction;
        }

        public override void OnEnter()
        {
            chopAction.ResetPickUp(animator);
        }

        public override void OnExit()
        {
            chopAction.StopAllCoroutines();
        }

        public override void OnUpdate()
        {
            if (detection.Tree) chopAction.PickUp(detection.Tree);
        }

        public override void OnFixedUpdate()
        {

        }
    }
}
