using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    [SerializeField] GameObject agentObject;
    [SerializeField] float interval = 30f;

    void Start()
    {
        if (agentObject == null) return;
        StartCoroutine(SpawnAgent());
    }

    IEnumerator SpawnAgent()
    {
        Instantiate(agentObject, transform.position, transform.rotation);
        yield return new WaitForSeconds(interval);
        StartCoroutine(SpawnAgent());
    }
}
