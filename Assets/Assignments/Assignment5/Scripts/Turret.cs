using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ASSIGNMENT5
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] float range = 15f;

        GameObject target;
        ParticleSystem bullets;

        private void Awake()
        {
            bullets = GetComponentInChildren<ParticleSystem>();
        }

        void FixedUpdate()
        {
            if (target == null)
            {
                FindTarget();
            }
            else
            {
                Shoot();
            }
        }

        void FindTarget()
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Agent");
            if (targets.Length == 0) return;
            float minDistance = Mathf.Infinity;
            foreach (GameObject tar in targets)
            {
                float distance = Vector3.Distance(tar.transform.position, transform.position);
                if (distance <= range && distance < minDistance)
                {
                    minDistance = distance;
                    target = tar;
                }
            }
        }

        void Shoot()
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (bullets.isPlaying && distance > range)
            {
                target = null;
                bullets.Stop();
                return;
            }
            else
            {
                if (!bullets.isPlaying) bullets.Play();
                RotateTurret();
            }
        }

        void RotateTurret()
        {
            float targetSpeed = 0f;
            NavMeshAgent navigation = target.GetComponent<NavMeshAgent>();
            if (!navigation.isStopped) targetSpeed = target.GetComponent<NavMeshAgent>().speed;
            Vector3 direction = bullets.main.startSpeed.constant * Vector3.Normalize(target.transform.position - transform.position) + target.transform.forward * targetSpeed;
            direction = new Vector3(direction.x, 0, direction.z);
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
