using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UVTileWithCamera : MonoBehaviour
{
    public float speed = 1f;

    RawImage image;

    void Start()
    {
        image = GetComponent<RawImage>();
    }

    void Update()
    {
        Rect uvs = image.uvRect;
        uvs.x += Time.deltaTime * speed;
        image.uvRect = uvs;
    }
}
