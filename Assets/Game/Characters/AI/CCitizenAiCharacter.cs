using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Characters.AI
{
    public class CCitizenAiCharacter : CBaseAiCharacter
    {
        [SerializeField] private CCitizenCharacterConfig _citizenCharacterConfig;
        
        public override void OnUpdate(float deltaTime)
        {
        }

        private void Start()
        {
            Vector3 destination = new Vector3(
                Random.Range(_citizenCharacterConfig.FleeRanges.Min, _citizenCharacterConfig.FleeRanges.Max),
                50,
                Random.Range(_citizenCharacterConfig.FleeRanges.Min, _citizenCharacterConfig.FleeRanges.Max));
            _navMeshAgent.SetDestination(destination);
        }
    }
}