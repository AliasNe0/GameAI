using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT2
{
    public class AIIdleAction : MonoBehaviour
    {
        [SerializeField] float idleDuration = 2f;
        public bool IdleFinished { get; private set; }

        public void ResetIdle()
        {
            StopAllCoroutines();
            IdleFinished = false;
        }

        public void Idle()
        {
            if (!IdleFinished) StartCoroutine(SuspendIdle());
        }

        IEnumerator SuspendIdle()
        {
            yield return new WaitForSeconds(idleDuration);
            IdleFinished = true;
        }
    }
}
