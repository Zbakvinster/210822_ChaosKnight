using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters.AI
{
    public abstract class CBaseAiCharacter : CBaseCharacter
    {
        [SerializeField] protected NavMeshAgent _navMeshAgent;
    }
}