using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SuspicionMeter : MonoBehaviour
{
    public float Amount;
    public Image FillImage;
    public Gradient ColorGradient;
    public TextMeshProUGUI AmountText;

    public void Start()
    {
        SuspicionMeterSingleton.Instance.RegisterMeter(this);
    }

    public void UpdateSuspicion()
    {
        FillImage.fillAmount = Amount;
        FillImage.color = ColorGradient.Evaluate(Amount);
        AmountText.text = $"{Amount:P0}";
    } 
}

public class SuspicionMeterSingleton : Singleton<SuspicionMeterSingleton>
{
    public SuspicionMeter Meter;

    public void RegisterMeter(SuspicionMeter meter)
    {
        Meter = meter;
    }

    public void AddSuspicion(float amount)
    {
        if(Meter.Amount + amount <= 1f && Meter.Amount + amount >= 0f)
        {
            Meter.Amount += amount;
        }
        if(Meter.Amount + amount >= 1f)
        {
            Meter.Amount = 1f;
        }

        Meter.UpdateSuspicion();
    }
}
