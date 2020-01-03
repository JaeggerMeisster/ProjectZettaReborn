﻿#pragma warning disable CS4014

using UnityEngine;
using Zetta.Controllers;
using Zetta.GridSystem;
using Zetta.GridSystem.Blueprints;
using Zetta.UI;

namespace Zetta
{
    public static partial class Debugger
    {
        public static void SpawnShipByName(string name)
        {
            var blueprint = BlueprintManager.loadedBlueprints.GetFirstWithName(name);
            if (blueprint != default)
            {
                PlayerController.Instance.Ship = Ship.InstantiateShip(blueprint);
            }
            else
            {
                NoticeManager.Instance.Prompt($"Blueprint \"{name}\" not found");
                Debug.LogWarning($"Blueprint \"{name}\" not found");
            }
        }
    }
}