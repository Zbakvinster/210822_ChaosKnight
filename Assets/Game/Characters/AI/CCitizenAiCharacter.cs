using UnityEngine;

namespace Game.Characters.AI
{
    public class CCitizenAiCharacter : CBaseAiCharacter
    {
        [SerializeField] private CCitizenCharacterConfig _citizenCharacterConfig;
        
        public override void OnUpdate(float deltaTime)
        {
            _navMeshAgent.SetDestination(Vector3.zero);
        }
    }
}