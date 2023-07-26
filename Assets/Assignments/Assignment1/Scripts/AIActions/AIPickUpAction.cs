using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT1
{
    public class AIPickUpAction : MonoBehaviour
    {
        [SerializeField] float pickUpDuration = .5f;
        public bool Active { get; private set; }
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
