using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASSIGNMENT1;
using UnityEditor;

namespace ASSIGNMENT1
{
    public class AIDetection : MonoBehaviour
    {
        [SerializeField] int rays = 35;
        [SerializeField] float rayOriginHeight = 1.5f;
        [SerializeField] float rayEndHeight = .2f;
        [SerializeField] float distanceRange = 4f;
        [SerializeField] float angleRange = 120f;
        [SerializeField] float obstacleDetectionDistanceRange = 1.5f;
        [SerializeField] float obstacleDetectionAngleRange = 90f;

        AIStateMachine stateMachine;
        GameObject rayOrigin;
        int collectableLayer;
        int obstacleLayer;
        List<int> sides = new() { 0, 1 };

        public GameObject collectableToPickUp;
        public bool obstacleOnLeft;
        public bool obstacleOnRight;

        private void Awake()
        {
            stateMachine = GetComponent<AIStateMachine>();
            rayOrigin = Instantiate(new GameObject("RayOrigin"));
            rayOrigin.transform.parent = transform;
            collectableLayer = LayerMask.NameToLayer("Collectable");
            obstacleLayer = LayerMask.NameToLayer("Obstacle");
            obstacleOnLeft = false;
            obstacleOnRight = false;
        }

        void FixedUpdate()
        {
            rayOrigin.transform.position = transform.localPosition + new Vector3(0, rayOriginHeight, 0);
            Vector3 rayOriginPosition = rayOrigin.transform.position;
            float yRotationNormalized = (rayEndHeight - rayOriginHeight) / distanceRange;
            foreach (int side in sides)
            {
                int raysOnSide = Mathf.RoundToInt(rays / 2);
                for (float f = 0; f <= raysOnSide; f++)
                {
                    float angleClampFactor = f / raysOnSide;
                    float rayAngle = 0;
                    if (side == 0)
                    {
                        rayAngle = angleRange / 2 * -angleClampFactor * Mathf.Deg2Rad;
                    }
                    else if (side == 1)
                    {
                        rayAngle = angleRange / 2 * angleClampFactor * Mathf.Deg2Rad;
                    }
                    Vector3 localRayRotation = Vector3.Normalize(new Vector3(Mathf.Sin(rayAngle), yRotationNormalized, Mathf.Cos(rayAngle)));
                    Vector3 gloablRayRotation = transform.TransformDirection(localRayRotation);
                    Vector3 rayEndPosition = rayOriginPosition + gloablRayRotation * distanceRange;
                    Debug.DrawLine(rayOriginPosition, rayEndPosition, Color.yellow);
                    if (Physics.Raycast(rayOriginPosition, gloablRayRotation, out RaycastHit hit, distanceRange))
                    {
                        if (hit.transform.gameObject.layer == collectableLayer)
                        {
                            Debug.DrawLine(rayOriginPosition, rayEndPosition, Color.green);
                            collectableToPickUp = hit.transform.gameObject;
                            break;
                        }
                        else if (hit.transform.gameObject.layer == obstacleLayer && hit.distance <= obstacleDetectionDistanceRange && Vector3.Angle(rayOrigin.transform.forward, gloablRayRotation) <= obstacleDetectionAngleRange / 2)
                        {
                            if (side == 0)
                            {
                                obstacleOnLeft = true;
                            }
                            else if (side == 1)
                            {
                                obstacleOnRight = true;
                            }
                            Debug.DrawLine(rayOriginPosition, rayEndPosition, Color.red);
                            stateMachine.ChangeState(0);
                            break;
                        }
                        else
                        {
                            obstacleOnLeft = false;
                            obstacleOnRight = false;
                            stateMachine.ChangeState(0);
                        }
                    }
                }
            }
        }
    }
}
