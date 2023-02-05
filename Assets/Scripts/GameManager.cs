using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float speed = .5f;
    public Transform playerUI;
    public Transform statsBar;
    public Transform fireUI;
    public float energy = 100;
    public float difficulty = 1;

    private Queue<Transform> floors = new();
    private float delta = 0;
    private Vector3 direction = Vector3.back;
    private FoodBar foodBar;
    private MoneyControl moneyCtrl;
    private Light fireLight;
    private Color fireOrigColor;
    private float fireOrigIntensity;
    private Color fireAlertColor = Color.red;
    private float fireAlertIntensity = 5f;

    private const float DifficultyPickupRaise = 0.05f;
    private const float DifficultyMinuteRaiseInv = 240; // 0.25 per Minute
    
    // Start is called before the first frame update
    void Start()
    {
        foodBar = playerUI.GetComponent<FoodBar>();
        moneyCtrl = statsBar.GetComponent<MoneyControl>();
        fireLight = fireUI.GetComponentInChildren<Light>();
        fireOrigColor = fireLight.color;
        fireOrigIntensity = fireLight.intensity;
        foreach (var child in transform)
        {
            floors.Enqueue(child as Transform);
        }
    }

    public void AddHealth(float amt)
    {
        if (amt > 0)
        {
            difficulty += DifficultyPickupRaise;
        }
        foodBar.currentHealthPercent = Mathf.Min(100, foodBar.currentHealthPercent + amt);
        if (foodBar.currentHealthPercent < 0)
        {
            Lose();
        }
    }

    public void AddEnergy(float amt)
    {
        energy += amt;
        var scale = fireUI.localScale;
        scale.y = 50 / energy;
        fireUI.localScale = scale;
        switch (energy)
        {
            case < 0:
                Lose();
                break;
            case < 20:
                fireLight.color = fireAlertColor;
                fireLight.intensity = fireAlertIntensity;
                break;
            default:
                fireLight.color = fireOrigColor;
                fireLight.intensity = fireOrigIntensity;
                break;
        }
    }

    public void AddMoney(float amt)
    {
        moneyCtrl.AddCurrency(amt);
    }

    void Lose()
    {
        // Todo: Implement
    }
    
    // Update is called once per frame
    void Update()
    {
        AddHealth(-Time.deltaTime);
        AddEnergy(-Time.deltaTime);
        difficulty += Time.deltaTime / DifficultyMinuteRaiseInv;
        var size = 3;
        var movement = Time.deltaTime * speed * difficulty;
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
