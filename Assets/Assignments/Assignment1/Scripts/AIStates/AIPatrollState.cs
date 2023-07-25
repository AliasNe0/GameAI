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
            detection.OnDetectionEnable();
        }

        public override void OnExit()
        {
            patrolAction.StopAllCoroutines();
        }

        public override void OnUpdate()
        {

        }

        public override void OnEnable()
        {
 
        }

        public override void OnDisable()
        {

        }

        public override void OnFixedUpdate()
        {
            patrolAction.Patrol(detection.ObstacleOnLeft, detection.ObstacleOnRight, detection.ObstacleProximityFactor);
            detection.RunDetection();
        }

        public override void OnLateUpdate()
        {

        }
    }
}
