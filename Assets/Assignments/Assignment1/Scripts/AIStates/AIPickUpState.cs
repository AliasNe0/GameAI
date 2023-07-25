using ASSIGNMENT1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT1
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
            pickUpAction.PickUp(detection.CollectableToPickUp);
        }

        public override void OnExit()
        {
            pickUpAction.StopAllCoroutines();
        }

        public override void OnUpdate()
        {

        }

        public override void OnFixedUpdate()
        {
            detection.RunDetection();
        }
    }
}
