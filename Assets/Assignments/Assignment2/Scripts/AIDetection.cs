using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT2
{
    public class AIDetection : MonoBehaviour
    {
        [SerializeField] List<GameObject> collectables;
        public bool ListIsEmpty { get; private set; }

        public GameObject CollectableToPickUp { get; private set; }

        private void Awake()
        {
            if (collectables.Count == 0)
            {
                Debug.Log("Define at least one collectable!");
                ListIsEmpty = true;
            }
            CollectableToPickUp = collectables[0];
        }

        void Update()
        {
            if (CollectableToPickUp == null && collectables.Count != 0)
            {
                ListIsEmpty = false;
                collectables.RemoveAt(0);
                if (collectables.Count != 0) CollectableToPickUp = collectables[0];
                else ListIsEmpty = true;
            }
        }
    }
}
