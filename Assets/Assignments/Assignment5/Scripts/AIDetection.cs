using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT5
{
    public class AIDetection : MonoBehaviour
    {
        public GameObject Tree { get; private set; }
        public bool HasPathToTree = true;

        void Update()
        {
            if (Tree == null)
            {
                GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
                if (trees.Length != 0)
                {
                    int i = Random.Range(0, trees.Length);
                    Tree = trees[i];
                }
                else
                {
                    Tree = null;
                }
            }
        }
    }
}
