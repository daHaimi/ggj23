using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float speed = .5f;
    public Transform playerUI;

    private Queue<Transform> floors = new();
    private float delta = 0;
    private Vector3 direction = Vector3.back;
    private FoodBar foodBar;
    
    // Start is called before the first frame update
    void Start()
    {
        foodBar = playerUI.GetComponent<FoodBar>();
        foreach (var child in transform)
        {
            floors.Enqueue(child as Transform);
        }
    }

    public void AddHealth(float amt)
    {
        foodBar.currentHealthPercent = Mathf.Min(100, foodBar.currentHealthPercent + amt);
    }

    public void AddEnergy(float amt)
    {
        // TODO: implement
    }
    
    // Update is called once per frame
    void Update()
    {
        AddHealth(-Time.deltaTime);
        var size = 3;
        var movement = Time.deltaTime * speed;
        delta += movement;
        foreach (var floor in floors)
        {
            floor.position += movement * direction;
        }

        if (delta > size)
        {
            var last = floors.Dequeue();
            last.position -= direction * (size * (floors.Count + 1));
            last.gameObject.GetComponent<FloorManager>().Populate();
            floors.Enqueue(last);
            delta = 0;
        }
    }
}
