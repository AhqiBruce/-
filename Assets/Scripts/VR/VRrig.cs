using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffest;
    public Vector3 trackingRotationOffest;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffest);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffest);
    }
}

public class VRrig : MonoBehaviour
{
    public float turnSmoothness = 1;
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;
    //public VRMap body;
    private PhotonView photonView;
    public Transform headConstraint;
    private Vector3 headBodyOffest;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        headBodyOffest = transform.position - headConstraint.position;
        //if (PhotonNetwork.IsMasterClient == true)
        //{
            head.vrTarget = GameObject.Find("xrrig/Camera Offset/Main Camera").transform;
            leftHand.vrTarget = GameObject.Find("xrrig/Camera Offset/LeftHand Controller").transform;
            rightHand.vrTarget = GameObject.Find("xrrig/Camera Offset/RightHand Controller").transform;
        //}
        //else if (!PhotonNetwork.IsMasterClient == false)
        //{
        //    head.vrTarget = GameObject.Find("XRRig/Camera Offset/Main Camera").transform;
        //    leftHand.vrTarget = GameObject.Find("XRRig/Camera Offset/LeftHand Controller").transform;
        //    rightHand.vrTarget = GameObject.Find("XRRig/Camera Offset/RightHand Controller").transform;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            transform.position = headConstraint.position + headBodyOffest;
            transform.forward = Vector3.Lerp(transform.forward,
            Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, turnSmoothness);

            head.Map();
            leftHand.Map();
            rightHand.Map();
        }
    }
}
