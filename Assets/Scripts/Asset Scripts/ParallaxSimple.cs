using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxSimple : MonoBehaviour {
    private float length, startPos, startPosY;
    public GameObject cam;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start() {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        
    }

    // Update is called once per frame
    private void Update() {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);


        transform.position = new Vector2(startPos + dist, transform.position.y);
        if (temp > startPos + length - 5) startPos += length;
        else if (temp < startPos - length + 5) startPos -= length;
    }
}
