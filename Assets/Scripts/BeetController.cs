using System.Collections.Generic;
using HTC.UnityPlugin.Vive;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class BeetController : MonoBehaviour
{
    public float fuelValue;
    public float healthValue;
    public float price;
    public float spawnFactor;
    public AudioSource digging;
    public List<AudioClip> gardenSounds;

    public ViveColliderEventCaster cec;
    
    private InputDevice rControl;
    private InputDevice lControl;
    
    // Start is called before the first frame update
    void Start()
    {
        rControl = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        lControl = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }

    void HapticPulseUnity(bool right)
    {
        if (right)
            rControl.SendHapticImpulse(0, 0.7f, 0.2f);
        else
            lControl.SendHapticImpulse(0, 0.7f, 0.2f);
    }
    
    public void StartDragging()
    {
        cec = GetComponent<BasicGrabbable>().currentGrabber.eventData.eventCaster as ViveColliderEventCaster;
        if (cec) HapticPulseUnity(cec.viveRole.IsRole(HandRole.RightHand));
        GetComponent<Rigidbody>().isKinematic = false;
        
        //
    }

}
