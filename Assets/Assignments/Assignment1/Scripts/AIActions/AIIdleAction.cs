using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT1
{
    public class AIIdleAction : MonoBehaviour
    {
        [SerializeField] float idleDuration = 2f;
        public bool ReadyToPatrol { get; private set; }

        public void ResetIdle()
        {
            StopAllCoroutines();
            ReadyToPatrol = false;
        }

        public void Idle()
        {
            if (!ReadyToPatrol) StartCoroutine(SuspendIdle());
        }

        IEnumerator SuspendIdle()
        {
            yield return new WaitForSeconds(idleDuration);
            ReadyToPatrol = true;
        }
    }
}
