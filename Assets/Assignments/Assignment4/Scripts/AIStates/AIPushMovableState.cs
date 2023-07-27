using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT4
{
    public class AIPushMovableState : AIState
    {
        AIPushMovableAction pushMovableAction;

        public AIPushMovableState(AIStateMachine sm) : base(sm) { }

        public override void OnStart()
        {
            pushMovableAction = stateMachine.PushMovableAction;
        }

        public override void OnEnter()
        {
            pushMovableAction.ResetPushMovable();
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {

        }

        public override void OnFixedUpdate()
        {

        }
    }
}
