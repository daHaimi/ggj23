using HTC.UnityPlugin.Vive;
using UnityEngine;
using UnityEngine.XR;

public class BeetController : MonoBehaviour
{
    
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
        //rControl.TryGetFeatureValue(CommonUsages.)
        GetComponent<BasicGrabbable>().grabbedEvent.currentInputModule.
        GetComponent<Rigidbody>().isKinematic = false;
    }

}
