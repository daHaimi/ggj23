using System.Collections;
using HTC.UnityPlugin.Vive;
using UnityEngine;
using UnityEngine.XR;

public class DropRoot : MonoBehaviour
{
    public GameObject effectFire;
    public bool isSelling;
    public AudioSource actionSound;

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
            rControl.SendHapticImpulse(0, 1f, 0.1f);
        else
            lControl.SendHapticImpulse(0, 1f, 0.1f);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Root")) return;
        
        var bg = collision.gameObject.GetComponent<BasicGrabbable>();
        if (bg.isGrabbed)
        {
            var bc = collision.gameObject.GetComponent<BeetController>();
            HapticPulseUnity(bc.cec.viveRole.IsRole(HandRole.RightHand));
            bg.ForceRelease();
        }
        actionSound.Play();

        Destroy(collision.gameObject);
        effectFire.SetActive(true);
        StartCoroutine(HideFireCoroutine());
    }

    IEnumerator HideFireCoroutine()
    {
        yield return new WaitForSeconds(.3f);
        effectFire.SetActive(false);
    }
}
