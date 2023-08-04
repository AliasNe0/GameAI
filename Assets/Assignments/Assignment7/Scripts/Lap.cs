using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ASSIGNMENT7
{
    public class Lap : MonoBehaviour
    {
        [SerializeField] TMP_Text lapText;
        int lap = 0;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Car"))
            {
                lap++;
                lapText.text = "Lap: " + lap.ToString();
            }
        }
    }
}
