using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT4
{
    public class AIIdleState : AIState
    {
        AIIdleAction idleAction;

        public AIIdleState(AIStateMachine sm) : base(sm) { }

        public override void OnStart()
        {
            animator = stateMachine.AIAnimator;
            idleAction = stateMachine.IdleAction;
        }

        public override void OnEnter()
        {
            idleAction.ResetIdle(animator);
            idleAction.Idle();
        }

        public override void OnExit()
        {
            idleAction.StopAllCoroutines();
        }

        public override void OnUpdate()
        {

        }

        public override void OnFixedUpdate()
        {

        }
    }
}
