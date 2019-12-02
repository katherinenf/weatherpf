using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public List<GameObject> cloudPrefabs;
    public float spawnsPerMeter;
    public float verticalExtent;
    public float prewarmDistance;
    public GameObject cloudParent;

    // The next x position that a cloud will spawn at
    float spawnNextX;

    void Start()
    {
        spawnNextX = transform.position.x - prewarmDistance;
    }

    void Update()
    {
        while (spawnsPerMeter > 0 && spawnNextX <= transform.position.x)
        {
            GameObject spawned = Instantiate(GetRandomCloud(), cloudParent.transform);
            spawned.transform.position = new Vector3(spawnNextX, GetRandomSpawnY(), transform.position.z);
            spawnNextX += spawnsPerMeter;
        }
    }

    GameObject GetRandomCloud()
    {
        if (cloudPrefabs.Count > 0)
        {
            return cloudPrefabs[Random.Range(0, cloudPrefabs.Count)];
        }
        return null;
    }

    float GetRandomSpawnY()
    {
        return transform.position.y + Random.Range(-verticalExtent, verticalExtent);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 0, 0.75f);
        Gizmos.DrawLine(
            transform.position - new Vector3(0, verticalExtent, 0),
            transform.position + new Vector3(0, verticalExtent, 0)
        );
        Gizmos.color = new Color(.5f, 1f, 0, 0.75f);
        Gizmos.DrawLine(
            transform.position - new Vector3(prewarmDistance, 0, 0),
            transform.position
        );
    }
}
