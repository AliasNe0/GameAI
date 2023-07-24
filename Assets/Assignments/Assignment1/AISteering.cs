using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASSIGNMENT1;

namespace ASSIGNMENT1
{
    public class AISteering : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        [SerializeField] float baitDistance = 3f;
        [SerializeField] float baitRotationAngleRange = 90f;
        [SerializeField] float baitRotationMaxAngularSpeed = 1f;
        [SerializeField] float baitRotationMinDuration = .25f;
        [SerializeField] float baitRotationMaxDuration = .5f;
        [SerializeField] float collisionRotationAngularSpeed = 2f;
        [SerializeField] float collisionRotationDuration = .25f;
        [SerializeField] float deeadEndRotationAngularSpeed = 2f;
        [SerializeField] float deeadEndRotationDuration = 1f;

        GameObject bait;
        Vector3 baitRotation;
        float baitRotationAngle;
        bool baitRotated = true;
        bool baitRotatedFromObstacle = true;
        bool baitRotatedFromDeadEnd = true;

        void Awake()
        {
            bait = Instantiate(new GameObject("Bait"));
            if (bait == null) return;
            bait.transform.parent = transform;
        }

        public void CompleteSteering(bool obstacleOnLeft, bool obstacleOnRight, float obstacleProximityFactor)
        {
            if (bait == null) return;
            if (baitRotatedFromDeadEnd && baitRotatedFromObstacle) UpdateRotationAngle(obstacleOnLeft, obstacleOnRight);
            if (baitRotatedFromDeadEnd && baitRotatedFromObstacle && !obstacleOnLeft && !obstacleOnRight) obstacleProximityFactor = 1f;
            Vector3 direction = GetDirection();
            transform.position += speed * obstacleProximityFactor * Time.deltaTime * direction;
            transform.rotation = Quaternion.LookRotation(direction);
        }

        void UpdateRotationAngle(bool obstacleOnLeft, bool obstacleOnRight)
        {
            if (obstacleOnRight || obstacleOnRight)
            {
                if (!baitRotated) StopCoroutine(RotateBait());
                if (obstacleOnRight && obstacleOnRight)
                {
                    if (!baitRotatedFromObstacle) StopCoroutine(RotateBaitFromObstacle());
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
