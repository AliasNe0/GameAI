using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT1
{
    public class AIPatrollingState : AIState
    {
        AIDetection detection;
        AIPatrolling patrolling;

        public AIPatrollingState(AIStateMachine sm) : base(sm) { }

        public override void OnStart()
        {
            detection = stateMachine.Detection;
            patrolling = stateMachine.Patrolling;
        }

        public override void OnExit()
        {
            patrolling.StopAllCoroutines();
        }

        public override void OnUpdate()
        {

        }

        public override void OnEnable()
        {
            detection.OnDetectionEnable();
        }

        public override void OnDisable()
        {

        }

        public override void OnFixedUpdate()
        {
            patrolling.CompleteSteering(detection.ObstacleOnLeft, detection.ObstacleOnRight, detection.ObstacleProximityFactor);
            detection.RunDetection();
        }

        public override void OnLateUpdate()
        {

        }
    }
}
