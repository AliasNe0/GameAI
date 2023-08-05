using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace ASSIGNMENT4
{
    public class AIDetection : MonoBehaviour
    {
        public GameObject CollectableToPickUp { get; private set; }
        public GameObject Movable { get; private set; }

        void Update()
        {
            if (CollectableToPickUp == null)
            {
                GameObject collectable = GameObject.FindWithTag("Collectable");
                if (collectable)
                {
                    CollectableToPickUp = collectable;
                }
            }

            if (Movable == null)
            {
                GameObject movable = GameObject.FindWithTag("Movable");
                if (movable)
                {
                    Movable = movable;
                }
            }
        }
    }
}
