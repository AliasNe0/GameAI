using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace ASSIGNMENT4
{
    public class AIPushMovableState : AIState
    {
        AIPushMovableAction pushMovableAction;

        public AIPushMovableState(AIStateMachine sm) : base(sm) { }

        public override void OnStart()
        {
            animator = stateMachine.AIAnimator;
            detection = stateMachine.Detection;
            navigation = stateMachine.Navigation;
            pushMovableAction = stateMachine.PushMovableAction;
        }

        public override void OnEnter()
        {
            pushMovableAction.ResetPushMovable(navigation, animator);
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {
            pushMovableAction.SetCollectablePath(detection.CollectableToPickUp);
        }

        public override void OnFixedUpdate()
        {

        }
    }
}
