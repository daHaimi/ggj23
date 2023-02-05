using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainingRoots : MonoBehaviour
{
    public List<GameObject> roots;

    private BoxCollider coll;
    
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) > 0) return;
        var r = Instantiate(
            roots[Random.Range(0, roots.Count)],
            new Vector3(
                Random.Range(coll.bounds.min.x, coll.bounds.max.x),
                Random.Range(coll.bounds.min.y, coll.bounds.max.y),
                Random.Range(coll.bounds.min.z, coll.bounds.max.z)
            ),
            Quaternion.identity
        );
        r.transform.localScale = Vector3.one * 5f;
        var rb = r.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddTorque(new Vector3(
                Random.Range(0, 1f),
                Random.Range(0, 1f),
                Random.Range(0, 1f)
            ));
    }
}
