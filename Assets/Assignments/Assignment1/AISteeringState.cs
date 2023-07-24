using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASSIGNMENT1;

namespace ASSIGNMENT1
{
    public class AISteeringState : AIState
    {
        AIDetection detection;
        AISteering steering;

        public AISteeringState(AIStateMachine sm) : base(sm) { }

        public override void OnStart()
        {
            detection = stateMachine.GetComponent<AIDetection>();
            steering = stateMachine.GetComponent<AISteering>();
        }

        public override void OnExit()
        {
            steering.StopAllCoroutines();
        }

        public override void OnFixedUpdate()
        {
            steering.CompleteSteering(detection.obstacleOnLeft, detection.obstacleOnRight, detection.obstacleProximityFactor);
        }
    }
}
