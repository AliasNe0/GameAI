using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ASSIGNMENT6
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] float acceleration = .1f;

        Vector3 velocity;
        void Start()
        {
            float yFactor = Random.Range(speed * .2f, speed * .8f) * GetRandomFactor();
            float xFactor = Mathf.Sqrt(Mathf.Pow(speed, 2) - Mathf.Pow(yFactor, 2)) * GetRandomFactor();
            velocity = new Vector3(xFactor, yFactor, 0);
        }

        float GetRandomFactor()
        {
            float factor = 1f;
            int i = 2 * Random.Range(0, 2) - 1;
            if (i < 0) factor = -1f;
            return factor;
        }

        void FixedUpdate()
        {
            velocity += velocity * acceleration * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
            if (Vector3.Distance(transform.parent.transform.position, transform.position) > 25f)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Goal"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (collision.gameObject.CompareTag("Player"))
            {
                velocity = new Vector3(-velocity.x, velocity.y, 0);
            }
            else if (collision.gameObject.CompareTag("Wall"))
            {
                velocity = new Vector3(velocity.x, -velocity.y, 0);
            }
        }
    }
}
