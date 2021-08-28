using System.Collections;
using Cinemachine;
using Game.Characters.AI.King;
using UnityEngine;

namespace Game.UI
{
    public class CGameOverController : MonoBehaviour
    {
        [SerializeField] private GameObject _fadeOut;
        [SerializeField] private float _fadeOutTime;
        [SerializeField] private GameObject _fadeIn;
        [SerializeField] private float _fadeInTime;
        [SerializeField] private Camera _camera;
        [SerializeField] private CKingAiCharacterController _king;
        [SerializeField] private float _cameraHorizontalOffset;
        [SerializeField] private float _cameraVerticalOffset;
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        [SerializeField] private CinemachineBrain _cinemachineBrain;
        [SerializeField] private GameObject _gameOverText;
        [SerializeField] private float _gameOverTextDelay;

        public void StartTransition() => StartCoroutine(Transition());

        private IEnumerator Transition()
        {
            _fadeOut.SetActive(true);

            yield return new WaitForSeconds(_fadeOutTime);

            _fadeOut.SetActive(false);

            // _cinemachineVirtualCamera.enabled = false;
            // _cinemachineBrain.enabled = false;
            _camera.transform.position = _king.transform.position
                                         + _king.transform.forward * _cameraHorizontalOffset
                                         + Vector3.up * _cameraVerticalOffset;
            _camera.transform.LookAt(_king.transform);
            _camera.gameObject.SetActive(true);

            _fadeIn.SetActive(true);

            yield return new WaitForSeconds(_fadeInTime);

            _fadeIn.SetActive(false);

            yield return new WaitForSeconds(_gameOverTextDelay);

            _gameOverText.SetActive(true);
        }
    }
}