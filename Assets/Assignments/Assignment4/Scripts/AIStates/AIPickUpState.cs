using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT4
{
    public class AIPickUpState : AIState
    {
        AIPickUpAction pickUpAction;

        public AIPickUpState(AIStateMachine sm) : base(sm) { }

        public override void OnStart()
        {
            animator = stateMachine.AIAnimator;
            detection = stateMachine.Detection;
            pickUpAction = stateMachine.PickUpAction;
        }

        public override void OnEnter()
        {
            pickUpAction.ResetPickUp(animator);
        }

        public override void OnExit()
        {
            pickUpAction.StopAllCoroutines();
        }

        public override void OnUpdate()
        {
            if (detection.CollectableToPickUp) pickUpAction.PickUp(detection.CollectableToPickUp);
        }

        public override void OnFixedUpdate()
        {

        }
    }
}
