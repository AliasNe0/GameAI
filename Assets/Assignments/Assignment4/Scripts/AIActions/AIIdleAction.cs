using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT4
{
    public class AIIdleAction : MonoBehaviour
    {
        [SerializeField] float idleDuration = 2f;
        public bool Active { get; private set; }

        public void ResetIdle()
        {
            Active = true;
        }

        public void Idle()
        {
            if (Active) StartCoroutine(SuspendIdle());
        }

        IEnumerator SuspendIdle()
        {
            yield return new WaitForSeconds(idleDuration);
            Active = false;
        }
    }
}
