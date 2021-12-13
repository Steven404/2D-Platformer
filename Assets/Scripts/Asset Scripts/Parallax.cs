using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos, startPosY;
    public GameObject cam;
    public float parallaxEffect;
    public float YEffect;

    // Start is called before the first frame update
    void Start() {
        startPosY = transform.position.y;
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        Debug.Log(length);
    }

    // Update is called once per frame
    private void Update() {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        float distY = (cam.transform.position.y * YEffect);


        transform.position = new Vector2(startPos + dist, startPosY + distY);
        if (temp > startPos + length - 5) startPos += length;
        else if (temp < startPos - length + 5) startPos -= length;
    }
}
