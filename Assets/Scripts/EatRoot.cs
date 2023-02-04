using System.Collections;
using HTC.UnityPlugin.Vive;
using UnityEngine;

public class EatRoot : MonoBehaviour
{
    public GameManager gm;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Root")) return;
        gm.AddHealth(20f);
        collision.gameObject.GetComponent<BasicGrabbable>().ForceRelease();
        Destroy(collision.gameObject);
        
    }
}
