using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT4
{
    public class AIFollowMovableState : AIState
    {
        AIFollowMovableAction followMovableAction;

        public AIFollowMovableState(AIStateMachine sm) : base(sm) { }

        public override void OnStart()
        {
            followMovableAction = stateMachine.FollowMovableAction;
        }

        public override void OnEnter()
        {

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
