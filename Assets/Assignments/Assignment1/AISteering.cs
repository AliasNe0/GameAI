using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASSIGNMENT1;

namespace ASSIGNMENT1
{
    public class AISteering : MonoBehaviour
    {
        [SerializeField] float speed = 2f;

        GameObject bait;

        private void Awake()
        {
            bait = Instantiate(new GameObject("Bait"));
            bait.transform.position = transform.position + transform.forward * 3f;
            bait.transform.parent = transform;
        }

        void FixedUpdate()
        {
            if (bait)
            {                
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
                Gizmos.DrawWireSphere(bait.transform.position + bait.transform.up, 0.25f);
            }
        }
    }
}
