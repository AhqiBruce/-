using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkinganimation : MonoBehaviour
{
    public float speedTreshold = 0.1f;
    private Animator animator;
    private Vector3 previousPos;
    private VRrig vrRig;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        vrRig = transform.parent.GetComponent<VRrig>();
        previousPos = vrRig.head.vrTarget.position;
        animator.SetBool("isWalking", false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 headsetSpeed = (vrRig.head.vrTarget.position - previousPos) / Time.deltaTime;
        headsetSpeed.y = 0;

        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
        previousPos = vrRig.head.vrTarget.position;

        animator.SetBool("isWalking", headsetLocalSpeed.magnitude > speedTreshold);

    }
}
