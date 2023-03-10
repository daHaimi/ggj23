using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public float rootDensity = 7;
    public float obstacleDensity = 3;

    public List<GameObject> roots;
    public List<GameObject> possObstacles;

    public GameObject rootsContainer;
    public GameObject obstaclesContainer;

    private BoxCollider bc;
    private Dictionary<float, GameObject> weighedRoots = new();
    private float maxWeight;
    
    // Start is called before the first frame update
    void Start()
    {
        if (roots.Count > 0)
        {
            // Prepare weighted roots map
            var i = 0f;
            foreach (var root in roots)
            {
                weighedRoots[i += root.GetComponent<BeetController>().spawnFactor] = root;
            }

            maxWeight = i;
        }

        bc = GetComponent<BoxCollider>();
        StartCoroutine(AsyncPopulate());
    }
    

    IEnumerator AsyncPopulate()
    {
        Populate();
        yield return null;
    }
    
    public void Populate()
    {
        foreach (Transform child in rootsContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in obstaclesContainer.transform)
        {
            Destroy(child.gameObject);
        }

        var s = bc.size / 2;
        for (int i = 0; i < rootDensity; i++)
        {
            var rand = Random.Range(0, maxWeight);
            GameObject root;
            do
            {
                root = weighedRoots.First(p => p.Key > rand).Value;
            } while (!root);
            var inst = Instantiate(root, rootsContainer.transform, true);
            inst.transform.localPosition = new Vector3(Random.Range(-s.x, s.x), 0, Random.Range(-s.z, s.z));
            inst.transform.Rotate(Vector3.up, Random.Range(0, 360));
            
        }
        for (int i = 0; i < obstacleDensity; i++)
        {
            var inst = Instantiate(possObstacles[Random.Range(0, possObstacles.Count)], obstaclesContainer.transform, true);
            inst.transform.localPosition = new Vector3(Random.Range(-s.x, s.x), 0.01f, Random.Range(-s.z, s.z));
            var mc = inst.GetComponent<MeshCollider>();
            if (mc) Destroy(mc);
            inst.transform.Rotate(Vector3.up, Random.Range(0, 360));
        }
    }
}
