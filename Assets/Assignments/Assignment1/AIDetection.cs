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
        [SerializeField] float obstacleDetectionDistance = 1f;
        [SerializeField] float obstacleDetectionAngle = 45f;

        AIStateMachine stateMachine;
        GameObject rayOrigin;
        int collectableLayer;
        int obstacleLayer;
        List<int> sides = new() { 0, 1 };

        public bool obstacleOnLeft = false;
        public bool obstacleOnRight = false;
        public float obstacleProximityFactor = 1f;
        public GameObject collectableToPickUp;

        private void Awake()
        {
            stateMachine = GetComponent<AIStateMachine>();
            rayOrigin = Instantiate(new GameObject("RayOrigin"));
            rayOrigin.transform.parent = transform;
            collectableLayer = LayerMask.NameToLayer("Collectable");
            obstacleLayer = LayerMask.NameToLayer("Obstacle");
        }

        void FixedUpdate()
        {
            if (rayOrigin == null) return;
            rayOrigin.transform.position = transform.localPosition + new Vector3(0, rayOriginHeight, 0);
            Vector3 rayOriginPosition = rayOrigin.transform.position;
            float yRotationNormalized = (rayEndHeight - rayOriginHeight) / distanceRange;
            float distanceToObstacle = 0;
            bool obstacleOnLeftSide = false;
            bool obstacleOnRightSide = false;
            foreach (int side in sides)
            {
                int raysOnSide = Mathf.RoundToInt(rays / 2);
                for (float f = 0; f <= raysOnSide; f++)
                {
                    float angleClampFactor = 1f - f / raysOnSide;
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
                    bool rayCast = Physics.Raycast(rayOriginPosition, gloablRayRotation, out RaycastHit hit, distanceRange);
                    if (rayCast && hit.transform.gameObject.layer == collectableLayer)
                    {
                        Debug.DrawLine(rayOriginPosition, rayEndPosition, Color.green);
                        //collectableToPickUp = hit.transform.gameObject;
                    }
                    else if (rayCast && hit.transform.gameObject.layer == obstacleLayer && hit.distance <= obstacleDetectionDistance && Vector3.Angle(rayOrigin.transform.forward, gloablRayRotation) <= obstacleDetectionAngle / 2)
                    {
                        if (side == 0)
                        {
                            if (!obstacleOnLeftSide)
                            {
                                obstacleOnLeftSide = true;
                                Debug.DrawLine(rayOriginPosition, rayEndPosition, Color.red);
                            }
                        }
                        else if (side == 1)
                        {
                            if (!obstacleOnRightSide)
                            {
                                obstacleOnRightSide = true;
                                Debug.DrawLine(rayOriginPosition, rayEndPosition, Color.red);
                            }
                        }

                        if (distanceToObstacle > hit.distance) distanceToObstacle = hit.distance;
                    }
                }
            }
            obstacleProximityFactor = Mathf.Clamp(-Mathf.Exp(-2f * distanceToObstacle / obstacleDetectionDistance + 1f) + 2.1f, 0, 1f);
            obstacleOnLeft = obstacleOnLeftSide;
            obstacleOnRight = obstacleOnRightSide;
        }
    }
}
