using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT1
{
    public class AIPickUpAction : MonoBehaviour
    {
        [SerializeField] float pickUpDuration = 5f;
        public void PickUp(GameObject collectable)
        {
            float duration = pickUpDuration * 10000f;
            float timer = 0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;
            }
            Destroy(collectable);
        }
    }
}
