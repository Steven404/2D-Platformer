using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesWithGround : MonoBehaviour
{
    private Animator animator;

    [SerializeField] public int NumberOfAnimation;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Anim();
    }

    void Anim() {
        animator.SetInteger("AnimNumber", NumberOfAnimation);
    }
}
