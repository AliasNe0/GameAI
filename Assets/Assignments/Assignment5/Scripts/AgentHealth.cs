using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT5
{
    public class AgentHealth : MonoBehaviour
    {
        [SerializeField] float agentHealth = 100f;
        [SerializeField] float damageFromBullet = 20f;

        void DecreaseHealt(float hitPoints)
        {
            agentHealth -= hitPoints;
            if (agentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        void OnParticleCollision(GameObject other)
        {
            DecreaseHealt(damageFromBullet);
        }
    }
}
