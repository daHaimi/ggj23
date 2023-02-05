using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float speed = .5f;
    public Transform playerUI;
    public Transform statsBar;
    public Transform fireUI;
    public float energy = 100;
    public float difficulty = 1;
    public Transform trainSound;
    public Transform fadeOut;

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
    private AudioSource train;
    private TMP_Text lifeGainText;
    private Animator lifeGainAnim;

    private const float DifficultyPickupRaise = 0.05f;
    private const float DifficultyMinuteRaiseInv = 240; // 0.25 per Minute
    private const float TrainBasePitch = .45f;
    
    // Start is called before the first frame update
    void Start()
    {
        foodBar = playerUI.GetComponent<FoodBar>();
        moneyCtrl = statsBar.GetComponent<MoneyControl>();
        fireLight = fireUI.GetComponentInChildren<Light>();
        fireOrigColor = fireLight.color;
        fireOrigIntensity = fireLight.intensity;
        train = trainSound.GetComponent<AudioSource>();
        var floatingText = playerUI.transform.Find("HealthGain");
        lifeGainText = floatingText.GetComponentInChildren<TMP_Text>();
        lifeGainAnim = floatingText.GetComponent<Animator>();
        foreach (var child in transform)
        {
            floors.Enqueue(child as Transform);
        }
    }

    public void AddHealth(float amt)
    {
        if (amt > 0)
        {
            ShowFloatingText($"{amt}");
            difficulty += DifficultyPickupRaise;
        }
        foodBar.currentHealthPercent = Mathf.Min(100, foodBar.currentHealthPercent + amt);
        if (foodBar.currentHealthPercent < 0)
        {
            Lose();
        }
    }

    void ShowFloatingText(string txt)
    {
        lifeGainText.text = txt;
        lifeGainAnim.Play("FloatText");
        StartCoroutine(ResetText());
    }

    IEnumerator ResetText()
    {
        yield return new WaitForSeconds(.5f);
        lifeGainText.text = "";
        lifeGainAnim.Rebind();
    }

    public void AddEnergy(float amt)
    {
        energy += amt;
        var scale = fireUI.localScale;
        scale.y = energy / 200;
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
        var anim = fadeOut.GetComponent<Animator>();
        anim.Play("FadeOut");
        StartCoroutine(GoToLoseScene());
    }

    IEnumerator GoToLoseScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Evil");
    }
    
    // Update is called once per frame
    void Update()
    {
        AddHealth(-Time.deltaTime);
        AddEnergy(-Time.deltaTime);
        difficulty += Time.deltaTime / DifficultyMinuteRaiseInv;
        train.pitch = TrainBasePitch * difficulty;
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
