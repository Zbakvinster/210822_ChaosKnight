using System;
using UnityEngine;

namespace Game.Characters.AI
{
    public class CCitizenCharacterConfig : CCharacterConfig
    {
        [SerializeField] private SFleeRanges _fleeRanges;

        public SFleeRanges FleeRanges => _fleeRanges;
        
        [Serializable]
        public struct SFleeRanges
        {
            [SerializeField] private float _min;
            [SerializeField] private float _max;

            public float Min => _min;
            public float Max => _max;
        }
    }
}