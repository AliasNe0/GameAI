using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASSIGNMENT1;

namespace ASSIGNMENT1
{
    public class AIDetection : MonoBehaviour
    {
        [SerializeField] int rays = 35;
        [SerializeField] float rayOriginHeight = 1.5f;
        [SerializeField] float rayEndHeight = .2f;
        [SerializeField] float distanceRange = 7f;
        [SerializeField] float angleRange = 120f;

        int collectableLayer;
        int obstacleLayer;

        public bool obstacleOnLeft;
        public bool obstacleOnRight;

        private void Awake()
        {
            collectableLayer = LayerMask.NameToLayer("Collectable");
            obstacleLayer = LayerMask.NameToLayer("Obstacle");
        }

        void FixedUpdate()
        {
            Vector3 rayOrigin = transform.localPosition + new Vector3(0, rayOriginHeight, 0);
            float yRotationNormalized = (rayEndHeight - rayOriginHeight) / distanceRange;
            for (int i = 0; i <= rays; i++)
            {
                float angleClampFactor = 2f * i / rays - 1f;
                float rayAngle = angleRange / 2 * angleClampFactor * Mathf.Deg2Rad;
                Vector3 localRayRotation = Vector3.Normalize(new Vector3(Mathf.Sin(rayAngle), yRotationNormalized, Mathf.Cos(rayAngle)));
                Vector3 rayRotation = transform.TransformDirection(localRayRotation);
                Vector3 rayEndPosition = rayOrigin + rayRotation * distanceRange;
                Debug.DrawLine(rayOrigin, rayEndPosition, Color.yellow);
                if (Physics.Raycast(rayOrigin, rayRotation, out RaycastHit hit, distanceRange))
                {
                    if (hit.transform.gameObject.layer == collectableLayer)
                    {
                        Debug.DrawLine(rayOrigin, rayEndPosition, Color.green);
                    }
                    else if (hit.transform.gameObject.layer == obstacleLayer)
                    {
                        Debug.DrawLine(rayOrigin, rayEndPosition, Color.red);
                    }
                }
            }
        }
    }
}
