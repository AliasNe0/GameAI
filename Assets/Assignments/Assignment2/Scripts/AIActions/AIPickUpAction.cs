using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT2
{
    public class AIPickUpAction : MonoBehaviour
    {
        [SerializeField] float pickUpDuration = .5f;
        public bool Active {  get; private set; }
        float duration;
        float timer;

        public void ResetPickUp()
        {
            Active = true;
            duration = pickUpDuration;
            timer = 0f;
        }

        public void PickUp(GameObject collectable)
        {
            Vector3 direction =  transform.forward + .01f * Vector3.Normalize(collectable.transform.position - transform.position);
            direction = Vector3.Normalize(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.LookRotation(direction);
            if (timer < duration)
            {
                timer += Time.deltaTime;
            }
            else
            {
                Destroy(collectable);
                Active = false;
            }
        }
    }
}
