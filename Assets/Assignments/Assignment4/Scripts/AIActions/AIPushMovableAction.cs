using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT4
{
    public class AIPushMovableAction : MonoBehaviour
    {
        public bool Active { get; private set; }

        public void ResetPushMovable()
        {
            Active = true;
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Movable"))
            {
                Active = false;
            }
        }
    }
}
