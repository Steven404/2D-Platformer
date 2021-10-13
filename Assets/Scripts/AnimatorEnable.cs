using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEnable : MonoBehaviour
{

    public GameObject obj;

    public float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnableAnimator());
    }

    private IEnumerator EnableAnimator() {
        yield return new WaitForSeconds(time);
        obj.SetActive(true);
    }

}
