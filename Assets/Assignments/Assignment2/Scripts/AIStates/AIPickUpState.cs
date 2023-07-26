using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT2
{
    public class AIPickUpState : AIState
    {
        AIDetection detection;
        AIPickUpAction pickUpAction;

        public AIPickUpState(AIStateMachine sm) : base(sm) { }

        public override void OnStart()
        {
            detection = stateMachine.Detection;
            pickUpAction = stateMachine.PickUpAction;
        }

        public override void OnEnter()
        {
            pickUpAction.ResetPickUp();
        }

        public override void OnExit()
        {
            pickUpAction.StopAllCoroutines();
        }

        public override void OnUpdate()
        {
            pickUpAction.PickUp(detection.CollectableToPickUp);
        }

        public override void OnFixedUpdate()
        {

        }
    }
}
