using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT1
{
    public class AIChaseAction : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        [SerializeField] float stopChaseAtDistance = 1.5f;
        [SerializeField] float rotationSpeed = .1f;
        public bool Active { get; private set; }

        public void ResetChase()
        {
            Active = true;
        }
        public void Chase(GameObject target)
        {
            if (target == null) Debug.Log("No target!");
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= stopChaseAtDistance)
            {
                Active = false;
                return;
            }
            Vector3 direction = transform.forward + rotationSpeed * Vector3.Normalize(target.transform.position - transform.position);
            direction = Vector3.Normalize(new Vector3(direction.x, 0, direction.z));
            transform.position += speed * Time.deltaTime * direction;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
