using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASSIGNMENT1;

namespace ASSIGNMENT1
{
    public class AISteering : MonoBehaviour
    {
        [SerializeField] GameObject target;
        [SerializeField] float speed = 2f;

        void FixedUpdate()
        {
            if (target)
            {
                Vector3 direction = Vector3.Normalize(target.transform.position - transform.position);
                transform.position += direction * speed * Time.deltaTime;
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
