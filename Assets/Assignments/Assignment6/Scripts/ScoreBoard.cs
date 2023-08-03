using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ASSIGNMENT6
{
    public class ScoreBoard : MonoBehaviour
    {
        public static ScoreBoard Instance { get ; private set; }
        public int Left = 0;
        public int Right = 0;

        void Awake()
        {
            ScoreBoard scoreBoard = FindObjectOfType<ScoreBoard>();
            if (scoreBoard == this)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
