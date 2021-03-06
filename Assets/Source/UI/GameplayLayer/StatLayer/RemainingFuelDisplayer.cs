﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace Zetta.UI.Controllers.ValueDisplayers
{
    public class RemainingFuelDisplayer : MonoBehaviour
    {
        public Text displayText;

        public void UpdateDisplayValue(TimeSpan remainingFuel)
        {
            if (remainingFuel.TotalDays > 3)
            {
                displayText.text = $"{Mathf.FloorToInt((float)remainingFuel.TotalDays)} Days";
            }
            else if (remainingFuel.TotalHours > 3)
            {
                displayText.text = $"{Mathf.FloorToInt((float)remainingFuel.TotalHours)} Hours";
            }
            else if (remainingFuel.TotalMinutes > 3)
            {
                displayText.text = $"{Mathf.FloorToInt((float)remainingFuel.TotalMinutes)} Minutes";
            }
            else
            {
                displayText.text = $"{Mathf.FloorToInt((float)remainingFuel.TotalSeconds)} Seconds";
            }
        }
    }
}