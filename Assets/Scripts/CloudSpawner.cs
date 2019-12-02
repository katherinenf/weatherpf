using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    // The random list of clouds prefabs to spawn
    public List<GameObject> cloudPrefabs;

    // Distance between cloud spawns
    public float spawnDistance;

    // Vertical extents that clouds will be spawned
    public float verticalExtent;

    // Horizontal distance to retro-actively spawn clouds on start
    public float prewarmDistance;

    // A gameobject the clouds will parent too
    public GameObject cloudParent;

    // The next x position that a cloud will spawn at
    float spawnNextX;

    void Start()
    {
        spawnNextX = transform.position.x - prewarmDistance;
    }

    void Update()
    {
        while (spawnDistance > 0 && spawnNextX <= transform.position.x)
        {
            GameObject spawned = Instantiate(GetRandomCloud(), cloudParent.transform);
            spawned.transform.position = new Vector3(spawnNextX, GetRandomSpawnY(), transform.position.z);
            spawnNextX += spawnDistance;
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
