using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurricane : MonoBehaviour
{
    // The extents used to find waterables during shape casts
    public float castExtents;

    // Update is called once per frame
    void Update()
    {
        DetectAndWaterIslands();
    }

    void DetectAndWaterIslands()
    {
        // Find island sections a storm passes over
        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            transform.position,
            new Vector2(castExtents * transform.localScale.x, 1f * transform.localScale.y),
            0f,
            Vector2.down
        );

        foreach (RaycastHit2D i in hits)
        {
            // If an island is hit, switch dry island sprite to wet island sprite
            if (!i.rigidbody.GetComponent<Island>())
                continue;

            Waterable[] waterables = i.collider.GetComponentsInChildren<Waterable>();
            foreach (Waterable w in waterables)
            {
                w.OnWatered(StormType.Hurricane);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.75f);
        Gizmos.DrawLine(
            transform.position - new Vector3(castExtents/2f * transform.localScale.x, 0, 0),
            transform.position + new Vector3(castExtents/2f * transform.localScale.x, 0, 0)
        );
    }
}
