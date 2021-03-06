﻿#pragma warning disable CS0649

using UnityEngine;
using UnityEngine.UI;

namespace Zetta.UI.Controllers.ValueDisplayers
{
    public class PowerDrawBar : MonoBehaviour, IStatLayerReportable
    {
        [SerializeField] private RectTransform barRect;
        [SerializeField] private RectTransform drawRect;
        [SerializeField] private RectTransform capacityRect;
        [SerializeField] private Text drawText;
        [SerializeField] private Text capacityText;

        public float value { get; set; } = 1f;
        public float displayValue { get; set; } = 1f;
        public float max { get; set; } = 1f;
        public float displayMax { get; set; } = 1f;
        public float multiplier { get; set; } = 0.02f;
        public float step { get; set; } = 0.01f;

        public void RefreshDisplayFields()
        {
            displayValue = value;
            displayMax = max;
        }

        public void UpdateBar(float displayValue, float displayMax)
        {
            barRect.localScale = new Vector3(Mathf.Clamp01(displayValue / displayMax), 1, 1);
            drawRect.anchoredPosition = new Vector2(-(capacityRect.sizeDelta.x + 10), 0);
            capacityText.text = $"/{Mathf.Round(displayMax)} KW";
            drawText.text = Mathf.Round(displayValue).ToString();
        }

        public void Update()
        {
            displayValue = Zetta.Math.Interpolationf.MixedInterpolate(
                displayValue,
                value,
                multiplier,
                step);

            displayMax = Zetta.Math.Interpolationf.MixedInterpolate(
                displayMax,
                max,
                multiplier,
                step);

            UpdateBar(displayValue, displayMax);
        }
    }
}