using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASSIGNMENT1;

namespace ASSIGNMENT1
{
    public class AISteering : MonoBehaviour
    {
        [SerializeField] float speed = 2f;
        [SerializeField] float baitDistance = 3f;
        [SerializeField] float gizmoBaitHeight = 1.5f;
        [SerializeField] float gizmoBaitRadius = .25f;

        GameObject bait;

        private void Awake()
        {
            bait = Instantiate(new GameObject("Bait"));
            bait.transform.parent = transform;
        }

        void FixedUpdate()
        {
            if (bait)
            {
                bait.transform.position = transform.position + transform.forward * baitDistance;
                Vector3 direction = Vector3.Normalize(bait.transform.position - transform.position);
                transform.position += speed * Time.deltaTime * direction;
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }

        void OnDrawGizmos()
        {
            if (bait)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(bait.transform.position + bait.transform.up * gizmoBaitHeight, gizmoBaitRadius);
            }
        }
    }
}
