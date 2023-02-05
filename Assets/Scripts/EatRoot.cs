using System.Collections;
using System.Collections.Generic;
using HTC.UnityPlugin.Vive;
using UnityEngine;
using UnityEngine.XR;

public class EatRoot : MonoBehaviour
{
    public GameManager gm;
    public AudioSource abgeschmatzt;
    public List<AudioClip> eatingSounds;
    public Transform sabbern;

    private InputDevice rControl;
    private InputDevice lControl;
    private ParticleSystem psys;
    
    // Start is called before the first frame update
    void Start()
    {
        rControl = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        lControl = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        psys = sabbern.GetComponent<ParticleSystem>();
    }
    void HapticPulseUnity(bool right)
    {
        if (right)
            rControl.SendHapticImpulse(0, 1f, 0.3f);
        else
            lControl.SendHapticImpulse(0, 1f, 0.3f);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Root")) return;
        var bc = collision.gameObject.GetComponent<BeetController>(); 
        gm.AddHealth(bc.healthValue);
        abgeschmatzt.clip = eatingSounds[Random.Range(0, eatingSounds.Count)];
        abgeschmatzt.Play();
        psys.Play();
        HapticPulseUnity(bc.cec.viveRole.IsRole(HandRole.RightHand));
        collision.gameObject.GetComponent<BasicGrabbable>().ForceRelease();
        Destroy(collision.gameObject);
        
    }
}
