using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT6
{
    public class Chase : MonoBehaviour
    {
        [SerializeField] float chaseSpeed = 3f;

        Detection detection;

        void Awake()
        {
            detection = GetComponent<Detection>();
        }

        void FixedUpdate()
        {
            if (!detection.Ball)
            {
                return;
            }
            else
            {
                float dot = Vector3.Dot(transform.up, detection.Ball.transform.position - transform.position);
                Vector3 direction = Vector3.up * Mathf.Clamp(dot, -1f, 1f);
                Vector3 playerPosition = transform.position + chaseSpeed * Time.deltaTime * direction;
                transform.position = new Vector3(playerPosition.x, Mathf.Clamp(playerPosition.y, 1.5f, 7.5f), playerPosition.z);
            }
        }
    }
}
