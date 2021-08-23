using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters.AI
{
    public abstract class CBaseAiCharacter : CBaseCharacter
    {
        [SerializeField] protected CCharacterConfig _characterConfig;
        [SerializeField] protected NavMeshAgent _navMeshAgent;
    }
}