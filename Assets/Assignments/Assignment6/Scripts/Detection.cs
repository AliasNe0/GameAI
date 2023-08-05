using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ASSIGNMENT6
{
    public class Detection : MonoBehaviour
    {
        [SerializeField] float minDetectionDistance = 6.5f;

        public GameObject Ball { get; private set; }

        float detectionDistance = 0;

        private void Awake()
        {
            detectionDistance = minDetectionDistance;
        }

        void Update()
        {
            if (Ball == null)
            {
                GameObject ball = GameObject.FindGameObjectWithTag("Ball");
                if (ball && Vector3.Distance(ball.transform.position, transform.position) < detectionDistance)
                {
                    detectionDistance = Random.Range(minDetectionDistance, 13f);
                    Ball = ball;
                }
            }
            else if (Vector3.Distance(Ball.transform.position, transform.position) > minDetectionDistance)
            {
                Ball = null;
            }
        }
    }
}
