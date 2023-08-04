using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                    if (!trees[i].GetComponent<TreeHealth>().Fallen)
                    {
                        Tree = trees[i];
                    }
                    else
                    {
                        foreach (GameObject tree in trees)
                        {
                            if (!tree.GetComponent<TreeHealth>().Fallen)
                            {
                                Tree = trees[i];
                                return;
                            }
                        }
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                }
            }
            else if (Tree.GetComponent<TreeHealth>().Fallen)
            {
                Tree = null;
            }
        }
    }
}
