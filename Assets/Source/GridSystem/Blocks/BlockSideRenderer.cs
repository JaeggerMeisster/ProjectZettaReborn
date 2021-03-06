﻿using UnityEngine;

namespace Zetta.GridSystem.Blocks
{
    [System.Serializable]
    public struct BlockSideRenderer
    {
        [SerializeField]
        public GameObject frontSide;

        [SerializeField]
        public GameObject rightSide;

        [SerializeField]
        public GameObject backSide;

        [SerializeField]
        public GameObject leftSide;

        public void Render(bool[] sidesToRender)
        {
            frontSide.SetActive(sidesToRender[0]);
            rightSide.SetActive(sidesToRender[1]);
            backSide.SetActive(sidesToRender[2]);
            leftSide.SetActive(sidesToRender[3]);
        }
    }
}