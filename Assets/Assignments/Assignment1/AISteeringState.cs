using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASSIGNMENT1;

namespace ASSIGNMENT1
{
    public class AISteeringState : AIState
    {
        [SerializeField] float speed = 1.5f;
        [SerializeField] float baitDistance = 3f;
        [SerializeField] float baitRotationAngleRange = 180f;
        [SerializeField] float baitRotationMinAngularSpeed = .25f;
        [SerializeField] float baitRotationMaxAngularSpeed = 1f;
        [SerializeField] float baitRotationMinDuration = 1f;
        [SerializeField] float baitRotationMaxDuration = 3f;

        AIDetection detection;
        GameObject bait;
        float baitRotationAngle;
        float baitRotationAngleSign;
        Vector3 localBaitRotation;
        Vector3 direction;
        bool baitRotated;
        bool stop;
        GameObject AIAgent;

        public AISteeringState(AIStateMachine sm) : base(sm) { }

        public override void OnStart()
        {
            bait = GameObject.Instantiate(new GameObject("Bait"));
            detection = stateMachine.gameObject.GetComponent<AIDetection>();
            if (bait == null) return;
            AIAgent = stateMachine.gameObject;
            bait.transform.parent = AIAgent.transform;
            baitRotated = true;
            stop = false;
        }

        public override void OnDisable()
        {
            if (!baitRotated) stateMachine.StopCoroutine(RotateBait());
        }

        public override void OnFixedUpdate()
        {
            if (bait == null || detection == null) return;
            CompleteSteering();
        }

        void CompleteSteering()
        {
            if (detection.obstacleOnLeft || detection.obstacleOnRight)
            {
                stop = true;
            }
            else
            {
                stop = false;
            }
            if (baitRotated) UpdateRotationAngle();
            UpdateDirection();
            if (!stop) AIAgent.transform.position += speed * Time.deltaTime * direction;
            AIAgent.transform.rotation = Quaternion.LookRotation(direction);
        }

        void UpdateRotationAngle()
        {
            if (detection.obstacleOnLeft && !detection.obstacleOnRight)
            {
                baitRotationAngleSign = 1f;
            }
            else if (detection.obstacleOnRight && !detection.obstacleOnLeft)
            {
                baitRotationAngleSign = -1f;
            }
            else
            {
                baitRotationAngleSign = 2 * Random.Range(0, 2) - 1f;
            }
            baitRotationAngle += Random.Range(baitRotationMinAngularSpeed, baitRotationMaxAngularSpeed) * baitRotationAngleSign;
            baitRotationAngle = Mathf.Clamp(baitRotationAngle, -baitRotationAngleRange / 2, baitRotationAngleRange / 2) * Mathf.Deg2Rad;
            stateMachine.StartCoroutine(RotateBait());
        }

        IEnumerator RotateBait()
        {
            baitRotated = false;
            float duration = Random.Range(baitRotationMinDuration, baitRotationMaxDuration);
            yield return new WaitForSeconds(duration);
            baitRotated = true;
        }

        void UpdateDirection()
        {
            localBaitRotation = Vector3.Normalize(new Vector3(Mathf.Sin(baitRotationAngle), 0, Mathf.Cos(baitRotationAngle))); ;
            Vector3 baitRotation = AIAgent.transform.TransformDirection(localBaitRotation);
            bait.transform.position = AIAgent.transform.position + baitRotation * baitDistance;
            direction = Vector3.Normalize(bait.transform.position - AIAgent.transform.position);
        }
    }
}
