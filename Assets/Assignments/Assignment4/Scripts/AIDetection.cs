using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT4
{
    public class AIDetection : MonoBehaviour
    {
        public bool CantFindCollectable { get; private set; }
        public bool CantFindMovable { get; private set; }
        public GameObject CollectableToPickUp { get; private set; }
        public GameObject Movable { get; private set; }

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
            if (Movable == null)
            {
                GameObject movable = GameObject.FindWithTag("Movable");
                if (movable)
                {
                    CantFindMovable = false;
                    Movable = movable;
                }
                else
                {
                    CantFindMovable = true;
                    Movable = null;
                }
            }
        }
    }
}
