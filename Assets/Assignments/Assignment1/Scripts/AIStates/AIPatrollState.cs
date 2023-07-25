using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT1
{
    public class AIPatrolState : AIState
    {
        AIDetection detection;
        AIPatrolAction patrolAction;

        public AIPatrolState(AIStateMachine sm) : base(sm) { }

        public override void OnStart()
        {
            detection = stateMachine.Detection;
            patrolAction = stateMachine.PatrolAction;
        }

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {
            patrolAction.ResetPatrol();
        }

        public override void OnUpdate()
        {

        }

        public override void OnFixedUpdate()
        {
            detection.RunDetection();
            patrolAction.Patrol(detection.ObstacleOnLeft, detection.ObstacleOnRight, detection.DistanceToObstacle);
        }
    }
}
