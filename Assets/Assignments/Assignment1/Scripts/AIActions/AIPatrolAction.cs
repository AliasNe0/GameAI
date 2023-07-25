using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT1
{
    public class AIPatrolAction : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        [SerializeField] float baitDistance = 3f;
        [SerializeField] float baitRotationAngleRange = 90f;
        [SerializeField] float baitRotationMaxAngularSpeed = 1f;
        [SerializeField] float baitRotationMinDuration = .25f;
        [SerializeField] float baitRotationMaxDuration = .5f;
        [SerializeField] float collisionRotationAngularSpeed = 1f;
        [SerializeField] float collisionRotationDuration = .25f;
        [SerializeField] float deeadEndRotationAngularSpeed = 6f;
        [SerializeField] float deeadEndRotationDuration = .5f;
        [SerializeField] float maxProximityToObstacle = 2f;

        GameObject bait;
        Vector3 baitRotation;
        float baitRotationAngle;
        bool baitRotated = true;
        bool baitRotatedFromObstacle = true;
        bool baitRotatedFromDeadEnd = true;

        void InstantiateBait()
        {
            bait = Instantiate(new GameObject("Bait"));
            if (bait == null)
            {
                Debug.Log("Bait is missing");
                return;
            }
            bait.transform.parent = transform;
        }

        public void ResetPatrol()
        {
            StopAllCoroutines();
            if (bait == null) return;
            Destroy(bait);
            bait = null;
            baitRotated = true;
            baitRotatedFromObstacle = true;
            baitRotatedFromDeadEnd = true;
        }

        public void Patrol(bool obstacleOnLeft, bool obstacleOnRight, float distanceToObstacle)
        {
            if (bait == null) InstantiateBait();
            if (baitRotatedFromDeadEnd && baitRotatedFromObstacle) CalculateRotationParameters(obstacleOnLeft, obstacleOnRight);
            Vector3 direction = GetDirection();
            float speedReduction = Mathf.Clamp(-Mathf.Exp(-15f * distanceToObstacle / maxProximityToObstacle + 3f) + 1f, 0, 1f);
            transform.position += speed * speedReduction * Time.deltaTime * direction;
            transform.rotation = Quaternion.LookRotation(direction);
        }

        void CalculateRotationParameters(bool obstacleOnLeft, bool obstacleOnRight)
        {
            if (obstacleOnRight || obstacleOnRight)
            {
                if (!baitRotated)
                {
                    StopCoroutine(RotateBait());
                    baitRotated = true;
                }
                if (obstacleOnRight && obstacleOnRight)
                {
                    if (!baitRotatedFromObstacle)
                    {
                        StopCoroutine(RotateBaitFromObstacle());
                        baitRotatedFromObstacle = true;
                    }
                    int randomInt = Mathf.RoundToInt(Random.Range(0, 1f));
                    float baitRotationAngleSign = randomInt == 1 ? 1f : -1f;
                    baitRotationAngle = deeadEndRotationAngularSpeed * baitRotationAngleSign;
                    baitRotationAngle = Mathf.Clamp(baitRotationAngle, -baitRotationAngleRange / 2, baitRotationAngleRange / 2) * Mathf.Deg2Rad;
                    StartCoroutine(RotateBaitFromDeadEnd());
                }
                else if (obstacleOnRight)
                {
                    baitRotationAngle = -collisionRotationAngularSpeed;
                    baitRotationAngle = Mathf.Clamp(baitRotationAngle, -baitRotationAngleRange / 2, baitRotationAngleRange / 2) * Mathf.Deg2Rad;
                    StartCoroutine(RotateBaitFromObstacle());
                }
                else if (obstacleOnLeft)
                {
                    baitRotationAngle = collisionRotationAngularSpeed;
                    baitRotationAngle = Mathf.Clamp(baitRotationAngle, -baitRotationAngleRange / 2, baitRotationAngleRange / 2) * Mathf.Deg2Rad;
                    StartCoroutine(RotateBaitFromObstacle());
                }

            }
            else if (baitRotated)
            {
                float baitRotationAngleFactor = 2 * Random.Range(0, 2) - 1f;
                baitRotationAngle = baitRotationMaxAngularSpeed * baitRotationAngleFactor;
                baitRotationAngle = Mathf.Clamp(baitRotationAngle, -baitRotationAngleRange / 2, baitRotationAngleRange / 2) * Mathf.Deg2Rad;
                StartCoroutine(RotateBait());
            }
        }

        IEnumerator RotateBait()
        {
            baitRotated = false;
            float duration = Random.Range(baitRotationMinDuration, baitRotationMaxDuration);
            yield return new WaitForSeconds(duration);
            baitRotated = true;
        }
        IEnumerator RotateBaitFromObstacle()
        {
            baitRotatedFromObstacle = false;
            yield return new WaitForSeconds(collisionRotationDuration);
            baitRotatedFromObstacle = true;
        }
        IEnumerator RotateBaitFromDeadEnd()
        {
            baitRotatedFromDeadEnd = false;
            yield return new WaitForSeconds(deeadEndRotationDuration);
            baitRotatedFromDeadEnd = true;
        }

        Vector3 GetDirection()
        {
            Vector3 localBaitRotation = Vector3.Normalize(new Vector3(Mathf.Sin(baitRotationAngle), 0, Mathf.Cos(baitRotationAngle))); ;
            baitRotation = transform.TransformDirection(localBaitRotation);
            bait.transform.position = transform.position + baitRotation * baitDistance;
            return Vector3.Normalize(bait.transform.position - transform.position);
        }
    }
}
