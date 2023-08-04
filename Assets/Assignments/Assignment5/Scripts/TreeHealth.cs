using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT5
{
    public class TreeHealth : MonoBehaviour
    {
        [SerializeField] float treeHealth = 100f;

        public bool Fallen { get; private set; }

        private void Awake()
        {
            Fallen = false;
        }

        public void DecreaseHealt(float hitPoints)
        {
            treeHealth -= hitPoints;
            if (treeHealth <= 0)
            {
                Fallen = true;
                transform.rotation = Quaternion.LookRotation(-transform.up);
            }
        }
    }
}
