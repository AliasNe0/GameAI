using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT1
{
    public class AIDetection : MonoBehaviour
    {
        [SerializeField] int rays = 35;
        [SerializeField] float rayOriginHeight = 1.5f;
        [SerializeField] float rayEndHeight = .2f;
        [SerializeField] float distanceRange = 3f;
        [SerializeField] float angleRange = 120f;
        [SerializeField] float obstacleDetectionDistance = 1f;
        [SerializeField] float obstacleDetectionAngle = 60f;

        GameObject rayOrigin;
        int collectableLayer;
        int obstacleLayer;
        readonly List<int> sides = new() { 0, 1 };

        public bool ObstacleOnLeft { get; private set; }
        public bool ObstacleOnRight { get; private set; }
        public float DistanceToObstacle { get; private set; }
        public GameObject CollectableToPickUp { get; private set; }

        void Awake()
        {
            rayOrigin = Instantiate(new GameObject("RayOrigin"));
            rayOrigin.transform.parent = transform;
            collectableLayer = LayerMask.NameToLayer("Collectable");
            obstacleLayer = LayerMask.NameToLayer("Obstacle");
        }

        public void RunDetection()
        {
            if (rayOrigin == null) return;
            rayOrigin.transform.position = transform.localPosition + new Vector3(0, rayOriginHeight, 0);
            Vector3 rayOriginPosition = rayOrigin.transform.position;
            float yRotationNormalized = (rayEndHeight - rayOriginHeight) / distanceRange;
            float distanceToObstacle = obstacleDetectionDistance;
            bool obstacleOnLeftSide = false;
            bool obstacleOnRightSide = false;
            foreach (int side in sides)
            {
                int raysOnSide = Mathf.RoundToInt(rays / 2);
                for (float f = 0; f <= raysOnSide; f++)
                {
                    float angleClampFactor = 1f - f / raysOnSide;
                    if (side == 0) angleClampFactor *= -1f;
                    float rayAngle = angleRange / 2 * angleClampFactor * Mathf.Deg2Rad;
                    Vector3 localRayRotation = Vector3.Normalize(new Vector3(Mathf.Sin(rayAngle), yRotationNormalized, Mathf.Cos(rayAngle)));
                    Vector3 gloablRayRotation = transform.TransformDirection(localRayRotation);
                    Vector3 rayEndPosition = rayOriginPosition + gloablRayRotation * distanceRange;
                    Debug.DrawLine(rayOriginPosition, rayEndPosition, Color.yellow);
                    if (Physics.Raycast(rayOriginPosition, gloablRayRotation, out RaycastHit hit, distanceRange))
                    {
                        if (hit.transform.gameObject.layer == collectableLayer)
                        {
                            Debug.DrawLine(rayOriginPosition, hit.point, Color.green);
                            CollectableToPickUp = hit.transform.gameObject;
                        }
                        if (hit.transform.gameObject.layer == obstacleLayer && hit.distance <= obstacleDetectionDistance && rayAngle <= obstacleDetectionAngle / 2)
                        {
                            Debug.DrawLine(rayOriginPosition, hit.point, Color.red);
                            if (side == 0)
                            {
                                if (!obstacleOnLeftSide)
                                {
                                    obstacleOnLeftSide = true;
                                }
                            }
                            else if (side == 1)
                            {
                                if (!obstacleOnRightSide)
                                {
                                    obstacleOnRightSide = true;
                                }
                            }
                            if (distanceToObstacle > hit.distance) distanceToObstacle = hit.distance;
                        }
                    }
                }
            }
            DistanceToObstacle = distanceToObstacle;
            ObstacleOnLeft = obstacleOnLeftSide;
            ObstacleOnRight = obstacleOnRightSide;
        }
    }
}
