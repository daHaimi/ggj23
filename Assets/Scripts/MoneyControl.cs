using System.Linq;
using TMPro;
using UnityEngine;

public class MoneyControl : MonoBehaviour
{
    private TMP_Text txt;
    private int amount;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponentsInChildren<TMP_Text>().First(g => g.name.Equals("MoneyCount"));
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = $"{amount}";
    }

    public void AddCurrency(float value)
    {
        amount += Mathf.RoundToInt(value);
    }

    public float GetCurrent()
    {
        return amount;
    }
}
