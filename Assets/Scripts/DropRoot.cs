using System.Collections;
using HTC.UnityPlugin.Vive;
using UnityEngine;
using UnityEngine.XR;

public class DropRoot : MonoBehaviour
{
    public GameObject effectFire;
    public bool isSelling;
    public AudioSource actionSound;
    public GameObject gameManager;

    private InputDevice rControl;
    private InputDevice lControl;
    private GameManager gm;
    
    // Start is called before the first frame update
    void Start()
    {
        rControl = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        lControl = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        gm = gameManager.GetComponent<GameManager>();
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
        var bc = collision.gameObject.GetComponent<BeetController>();
        if (bg.isGrabbed)
        {
            HapticPulseUnity(bc.cec.viveRole.IsRole(HandRole.RightHand));
            bg.ForceRelease();
        }
        actionSound.Play();
        if (isSelling)
        {
            gm.AddMoney(bc.price);
        }
        else
        {
            gm.AddEnergy(bc.fuelValue);
        }

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
