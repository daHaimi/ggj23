using System.Collections;
using HTC.UnityPlugin.Vive;
using UnityEngine;

public class DropRoot : MonoBehaviour
{
    public GameObject effectFire;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Root")) return;
        collision.gameObject.GetComponent<BasicGrabbable>().ForceRelease();
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
