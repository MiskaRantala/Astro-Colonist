using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat {

    [SerializeField]
    private BarScript bar;

    [SerializeField]
    private float maxValue;

    [SerializeField]
    private float currentValue;

    public float CurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            bar.Value = Mathf.Clamp(value, 0, MaxValue);
            currentValue = value;
        }
    }

    public float MaxValue
    {
        get
        {
            return maxValue;
        }

        set
        {
            bar.MaxValue = value;
            maxValue = value;
        }
    }

    public void Initialize()
    {
        this.maxValue = maxValue;
        this.currentValue = currentValue;
    }
}
