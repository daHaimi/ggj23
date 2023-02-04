using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodBar : MonoBehaviour
{
    public List<Transform> apples;
    public float currentHealthPercent = 100f;
    public float stateBefore = 8;

    public GameObject appleFull;
    public GameObject appleEmpty;

    private Mesh appleFullMesh;
    private Mesh appleEmptyMesh;
    public List<Animator> animators = new();
    
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        var state = Mathf.Ceil(currentHealthPercent / 12.5f);
        if (Math.Abs(state - stateBefore) > .1)
        {
            switch (state)
            {
                case 8:
                    animators[2].Rebind();
                    animators[2].Update(0f);
                    animators[2].enabled = false;
                    break;
                case 7:
                    animators[2].enabled = true;
                    apples[2].GetComponentInChildren<MeshFilter>().mesh = appleFullMesh;
                    break;
                case 6:
                    animators[2].Rebind();
                    animators[2].Update(0f);
                    animators[2].enabled = false;
                    apples[2].GetComponentInChildren<MeshFilter>().mesh = appleEmptyMesh;
                    break;
                case 5:
                    animators[1].Rebind();
                    animators[1].Update(0f);
                    animators[1].enabled = false;
                    break;
                case 4:
                    animators[1].enabled = true;
                    apples[1].GetComponentInChildren<MeshFilter>().mesh = appleFullMesh;
                    break;
                case 3:
                    animators[1].Rebind();
                    animators[1].Update(0f);
                    animators[1].enabled = false;
                    apples[1].GetComponentInChildren<MeshFilter>().mesh = appleEmptyMesh;
                    break;
                case 2:
                    animators[0].Rebind();
                    animators[0].Update(0f);
                    animators[0].enabled = false;
                    break;
                case 1:
                    animators[0].enabled = true;
                    apples[0].GetComponentInChildren<MeshFilter>().mesh = appleFullMesh;
                    break;
                case 0:
                    animators[0].Rebind();
                    animators[0].Update(0f);
                    animators[0].enabled = false;
                    apples[0].GetComponentInChildren<MeshFilter>().mesh = appleEmptyMesh;
                    break;
            }

            stateBefore = state;
        }
    }
}
