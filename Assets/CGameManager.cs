using System;
using Game.Characters.AI;
using Game.Characters.Player;
using UnityEngine;

public class CGameManager : MonoBehaviour
{
    [SerializeField] private CPlayerCharacterController _playerCharacterController;
    [SerializeField] private CKingAiCharacterController _kingCharacterController;

    private void Start()
    {
        _kingCharacterController.Init(_playerCharacterController);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        
        _playerCharacterController.OnUpdate(deltaTime);
        _kingCharacterController.OnUpdate(deltaTime);
    }
}
