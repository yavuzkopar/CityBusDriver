using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMoneySpawner : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] floatingMoneyPool;
    [SerializeField]
    private TextMeshProUGUI moneyText;
    private int poolIndex;

    private void Start()
    {
        Passenger.OnMoneyUpdated += Passenger_OnMoneyUpdated;
        moneyText.text = "$" + BusManager.Instance.Money.ToString();
    }

    private void Passenger_OnMoneyUpdated(int moneyAmont)
    {
        if (poolIndex >= floatingMoneyPool.Length)
            poolIndex = 0;
        ActivatePoolObject(moneyAmont);
        AddMoney(moneyAmont);
        poolIndex++;
    }

    private void ActivatePoolObject(int moneyAmont)
    {
        TextMeshProUGUI textObj = floatingMoneyPool[poolIndex];
        textObj.text = moneyAmont.ToString();
        textObj.gameObject.SetActive(true);
        float parameterOfTextColor = (float)moneyAmont;
        textObj.color = TextColor(parameterOfTextColor);
    }

    private void AddMoney(int moneyAmont)
    {
        BusManager.Instance.AddMoney(moneyAmont);
        moneyText.text ="$"+ BusManager.Instance.Money.ToString();
    }

    private Color TextColor(float money)
    {
        float t = (money + 10) / 20f; // from (-10,+10)   to (0,1)
        Color a = Color.Lerp(Color.red, Color.yellow, t);
        Color b = Color.Lerp(Color.yellow, Color.green, t);
        return Color.Lerp(a, b, t);
    }
}
