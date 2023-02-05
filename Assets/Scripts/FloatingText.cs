using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = .3f;
    public Vector3 offset = Vector3.up * .7f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += offset;
    }
}
