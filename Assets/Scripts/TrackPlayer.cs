using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    public Player p;
    float voff;

    void Start()
    {
        voff = transform.position.x - p.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(p.transform.position.x + voff, transform.position.y);
    }
}
