using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT1
{
    public class AIChaseAction : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        [SerializeField] float maxDistatanceToCollectable = 1f;
        public bool TargetIsReached {  get; private set; }
        public void Chase(GameObject target)
        {
            if (target == null) Debug.Log("No target!");
            Vector3 direction = target.transform.position - transform.position;
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= maxDistatanceToCollectable)
            {
                TargetIsReached = true;
                return;
            }
            direction = Vector3.Normalize(direction);
            transform.position += speed * Time.deltaTime * direction;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
