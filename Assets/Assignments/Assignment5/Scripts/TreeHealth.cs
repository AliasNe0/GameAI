using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT5
{
    public class TreeHealth : MonoBehaviour
    {
        [SerializeField] float treeHealth = 100f;

        public void DecreaseHealt(float hitPoints)
        {
            treeHealth -= hitPoints;
            if (treeHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
