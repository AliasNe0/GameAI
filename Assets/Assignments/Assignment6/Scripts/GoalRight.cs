using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ASSIGNMENT6
{
    public class GoalRight : MonoBehaviour
    {
        TMP_Text score;
        void Awake()
        {
            score = GetComponentInChildren<TMP_Text>();
        }

        void Start()
        {
            score.text = ScoreBoard.Instance.Right.ToString();
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                ScoreBoard.Instance.Left += 1;
            }
        }
    }
}
