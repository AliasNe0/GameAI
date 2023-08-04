using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ASSIGNMENT7
{
    public class Detection : MonoBehaviour
    {
        [SerializeField] float minProximityToWaypoint = 3f;
        [SerializeField] float detectionAngle = 135f;
        [SerializeField] float detectionDistance = 45f;

        public GameObject NextWaypoint { get; private set; }
        public bool CantFindWaypoint { get; private set; }
        GameObject currentWaypoint;
        GameObject previousWaypoint;

        void Start()
        {
            UpdateWaypoints();
        }

        void Update()
        {
            if (CantFindWaypoint)
            {
                UpdateWaypoints();
            }
            float proximity = Vector3.Distance(NextWaypoint.transform.position, transform.position);
            if (proximity < minProximityToWaypoint)
            {
                UpdateWaypoints();
            }

        }

        void UpdateWaypoints()
        {
            previousWaypoint = currentWaypoint;
            currentWaypoint = NextWaypoint;
            FindNextWaypoint();
        }

        void FindNextWaypoint()
        {
            GameObject nextWaypoint = null;
            GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
            if (waypoints.Length != 0)
            {
                float minDistance = Mathf.Infinity;
                foreach (GameObject waypoint in waypoints)
                {
                    if (waypoint == currentWaypoint || waypoint == previousWaypoint) continue;
                    float angle = Vector3.Angle(transform.forward, waypoint.transform.position - transform.position);
                    if (angle > detectionAngle / 2) continue;
                    float distance = Vector3.Distance(waypoint.transform.position, transform.position);
                    if (distance < detectionDistance && distance < minDistance)
                    {
                        minDistance = distance;
                        nextWaypoint = waypoint;
                    }
                }
            }
            if (nextWaypoint == null)
            {
                CantFindWaypoint = true;
                Debug.Log("Cannot find next waypoint!");
            }
            else
            {
                if (CantFindWaypoint) CantFindWaypoint = false;
                NextWaypoint = nextWaypoint;
                Debug.Log(NextWaypoint.name);
            }            
        }
    }
}