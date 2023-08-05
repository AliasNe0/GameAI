using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace ASSIGNMENT4
{
    public class AIFollowMovableState : AIState
    {
        AIFollowMovableAction followMovableAction;

        public AIFollowMovableState(AIStateMachine sm) : base(sm) { }

        public override void OnStart()
        {
            animator = stateMachine.AIAnimator;
            detection = stateMachine.Detection;
            navigation = stateMachine.Navigation;
            navSurface = stateMachine.navSurface;
            followMovableAction = stateMachine.FollowMovableAction;
            followMovableAction.SetFollowMovable(navigation, navSurface);
        }

        public override void OnEnter()
        {
            followMovableAction.ResetFollowMovable(animator);
            if (detection.Movable)
            {
                navigation.isStopped = false;
                followMovableAction.FollowMovable(detection.Movable);
            }
        }

        public override void OnExit()
        {
            navigation.isStopped = true;
            followMovableAction.Active = false;
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
