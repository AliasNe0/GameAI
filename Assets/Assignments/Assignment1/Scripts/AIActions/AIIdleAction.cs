using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT1
{
    public class AIIdleAction : MonoBehaviour
    {
        [SerializeField] float idleDuration = 2f;
        public bool readyToPatrol { get; private set; }

        public void ResetIdle()
        {
            readyToPatrol = false;
        }

        public void Idle()
        {
            StartCoroutine(SuspendIdle());
        }

        IEnumerator SuspendIdle()
        {
            yield return new WaitForSeconds(idleDuration);
            readyToPatrol = true;
        }
    }
}
