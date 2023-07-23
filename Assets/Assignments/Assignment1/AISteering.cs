using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASSIGNMENT1;

namespace ASSIGNMENT1
{
    public class AISteering : MonoBehaviour
    {
        [SerializeField] float speed = 1.5f;
        [SerializeField] float baitDistance = 3f;
        [SerializeField] float baitGizmoPositionHeight = 1.5f;
        [SerializeField] float baitGizmoSphereRadius = .25f;
        [SerializeField] float baitRotationAngleRange = 90f;
        [SerializeField] float baitRotationMinAngularSpeed = .25f;
        [SerializeField] float baitRotationMaxAngularSpeed = 1f;
        [SerializeField] float baitRotationMinDuration = 1f;
        [SerializeField] float baitRotationMaxDuration = 3f;

        GameObject bait;
        float baitAngle;
        Vector3 localBaitRotation;
        bool baitRotated = true;

        private void Awake()
        {
            bait = Instantiate(new GameObject("Bait"));
            if (bait == null) return;
            bait.transform.parent = transform;
        }

        void FixedUpdate()
        {
            if (bait == null) return;
            if (baitRotated) StartCoroutine(UpdateRotation());
            Vector3 baitRotation = transform.TransformDirection(localBaitRotation);
            bait.transform.position = transform.position + baitRotation * baitDistance;
            Vector3 direction = Vector3.Normalize(bait.transform.position - transform.position);
            transform.position += speed * Time.deltaTime * direction;
            transform.rotation = Quaternion.LookRotation(direction);
        }

        void OnDrawGizmos()
        {
            if (bait == null) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(bait.transform.position + bait.transform.up * baitGizmoPositionHeight, baitGizmoSphereRadius);
        }

        IEnumerator UpdateRotation()
        {
            baitRotated = false;
            baitAngle += Random.Range(baitRotationMinAngularSpeed, baitRotationMaxAngularSpeed) * (2 * Random.Range(0, 2) - 1f);
            baitAngle = Mathf.Clamp(baitAngle, -baitRotationAngleRange / 2, baitRotationAngleRange / 2) * Mathf.Deg2Rad;
            localBaitRotation = Vector3.Normalize(new Vector3(Mathf.Sin(baitAngle), 0, Mathf.Cos(baitAngle)));
            float duration = Random.Range(baitRotationMinDuration, baitRotationMaxDuration);
            yield return new WaitForSeconds(duration);
            baitRotated = true;
        }
    }
}
