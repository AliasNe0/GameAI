using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ASSIGNMENT4
{
    public class AIFollowMovableState : AIState
    {
        NavMeshAgent navigation;
        AIDetection detection;
        AIFollowMovableAction followMovableAction;

        public AIFollowMovableState(AIStateMachine sm) : base(sm) { }

        public override void OnStart()
        {
            navigation = stateMachine.Navigation;
            detection = stateMachine.Detection;
            followMovableAction = stateMachine.FollowMovableAction;
        }

        public override void OnEnter()
        {
            followMovableAction.SetNavigation(navigation);
            followMovableAction.ResetFollowMovable();
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {
            if (detection.Movable)
            {
                followMovableAction.FollowMovable(detection.Movable);
            }
        }

        public override void OnFixedUpdate()
        {

        }
    }
}
