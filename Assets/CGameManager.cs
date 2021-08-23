using Game.Characters.Player;
using UnityEngine;

public class CGameManager : MonoBehaviour
{
    [SerializeField] private CPlayerCharacterController _playerCharacterController;

    private void Update()
    {
        _playerCharacterController.MyUpdate(Time.deltaTime);
    }
}
