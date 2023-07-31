using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT5
{
    public class AIChopAction : MonoBehaviour
    {
        [SerializeField] float chopDuration = .5f;
        public bool Active {  get; private set; }
        float duration;
        float timer;

        public void ResetPickUp(Animator animator)
        {
            Active = true;
            duration = chopDuration;
            timer = 0f;
            animator.ResetTrigger("Walk");
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Chop");
        }

        public void PickUp(GameObject tree)
        {
            Vector3 direction =  transform.forward + .01f * Vector3.Normalize(tree.transform.position - transform.position);
            direction = Vector3.Normalize(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.LookRotation(direction);
            if (timer < duration)
            {
                timer += Time.deltaTime;
            }
            else
            {
                Destroy(tree);
                Active = false;
            }
        }
    }
}
