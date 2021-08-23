using System;
using UnityEngine;

namespace Game.Characters.AI
{
    [CreateAssetMenu(fileName = "CitizenCharacterConfig", menuName = "ScriptableObjects/CitizenCharacterConfig")]
    public class CCitizenCharacterConfig : CCharacterConfig
    {
        [SerializeField] private float _walkMovementSpeed;
        [SerializeField] private SRanges _walkRanges;
        [SerializeField] private SRanges _fleeRanges;

        public SRanges WalkRanges => _walkRanges;
        public SRanges FleeRanges => _fleeRanges;
        
        [Serializable]
        public struct SRanges
        {
            [SerializeField] private float _min;
            [SerializeField] private float _max;

            public float Min => _min;
            public float Max => _max;
        }
    }
}