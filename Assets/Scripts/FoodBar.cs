using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoodBar : MonoBehaviour
{
    public List<Transform> apples;
    public float currentHealthPercent = 100f;
    public int stateBefore = 8;

    public GameObject appleFull;
    public GameObject appleEmpty;

    private Mesh appleFullMesh;
    private Mesh appleEmptyMesh;
    public List<Animator> animators = new();
    public List<MeshFilter> meshes = new();
    
    // Start is called before the first frame update
    void Start()
    {
        appleFullMesh = appleFull.GetComponentInChildren<MeshFilter>().sharedMesh;
        appleEmptyMesh = appleEmpty.GetComponentInChildren<MeshFilter>().sharedMesh;

        foreach (var apple in apples)
        {
            var ani = apple.GetComponent<Animator>();
            ani.Rebind();
            ani.Update(0f);
            ani.enabled = false;
            animators.Add(ani);
            meshes.Add(apple.GetComponentInChildren<MeshFilter>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        var state = Mathf.FloorToInt(currentHealthPercent / 12.5f);
        if (state != stateBefore)
        {
            var dir = Math.Sign(state - stateBefore);
            for (var i = stateBefore; state != i;)
            {
                i += dir;
                switch (i)
                {
                    case 7:
                        animators[2].Rebind();
                        animators[2].Update(0f);
                        animators[2].enabled = false;
                        break;
                    case 6:
                        animators[2].enabled = true;
                        meshes[2].mesh = appleFullMesh;
                        break;
                    case 5:
                        animators[2].Rebind();
                        animators[2].Update(0f);
                        animators[2].enabled = false;
                        meshes[2].mesh = appleEmptyMesh;
                        animators[1].Rebind();
                        animators[1].Update(0f);
                        animators[1].enabled = false;
                        break;
                    case 4:
                        animators[1].enabled = true;
                        meshes[1].mesh = appleFullMesh;
                        break;
                    case 3:
                        animators[1].Rebind();
                        animators[1].Update(0f);
                        animators[1].enabled = false;
                        meshes[1].mesh = appleEmptyMesh;
                        animators[0].Rebind();
                        animators[0].Update(0f);
                        animators[0].enabled = false;
                        break;
                    case 2:
                        animators[0].enabled = true;
                        meshes[0].mesh = appleFullMesh;
                        break;
                    case 1:
                        animators[0].Rebind();
                        animators[0].Update(0f);
                        animators[0].enabled = false;
                        meshes[0].mesh = appleEmptyMesh;
                        break;
                }
            }

            stateBefore = state;
        }
    }
}
