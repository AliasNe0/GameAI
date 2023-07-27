using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT2
{
    public class AIDetection : MonoBehaviour
    {
        public bool CantFindCollectable { get; private set; }
        public GameObject CollectableToPickUp { get; private set; }

        private void Awake()
        {
            CantFindCollectable = false;
        }

        void Update()
        {
            if (CollectableToPickUp == null)
            {
                GameObject collectable = GameObject.FindWithTag("Collectable");
                if (collectable)
                {
                    CantFindCollectable = false;
                    CollectableToPickUp = collectable;
                }
                else
                {
                    CantFindCollectable = true;
                    CollectableToPickUp = null;
                }
            }
        }
    }
}
