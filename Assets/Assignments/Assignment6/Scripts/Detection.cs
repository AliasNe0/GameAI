using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ASSIGNMENT6
{
    public class Detection : MonoBehaviour
    {
        [SerializeField] float detectionRange = 10f;

        public GameObject Ball { get; private set; }

        void Update()
        {
            if (Ball == null)
            {
                GameObject ball = GameObject.FindGameObjectWithTag("Ball");
                if (ball && Vector3.Distance(ball.transform.position, transform.position) < detectionRange)
                {
                    Ball = ball;
                }
            }
            else if (Vector3.Distance(Ball.transform.position, transform.position) > detectionRange)
            {
                Ball = null;
            }
        }
    }
}
