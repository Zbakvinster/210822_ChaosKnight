using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Game.UI
{
    public class CGameWinController : MonoBehaviour
    {
        [SerializeField] private GameObject _cityDestroyed;
        [SerializeField] private float _fadeOutDelay;
        [SerializeField] private GameObject _fadeOut;
        [SerializeField] private float _fadeOutTime;
        [SerializeField] private GameObject _fadeIn;
        [SerializeField] private float _fadeInTime;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _cameraHorizontalOffset;
        [SerializeField] private float _cameraVerticalOffset;
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        [SerializeField] private CinemachineBrain _cinemachineBrain;
        [SerializeField] private GameObject _gameWinText;
        [SerializeField] private float _gameWinTextDelay;

        public void StartTransition() => StartCoroutine(Transition());

        private IEnumerator Transition()
        {
            _cityDestroyed.SetActive(true);

            yield return new WaitForSeconds(_fadeOutDelay);
            
            _fadeOut.SetActive(true);

            yield return new WaitForSeconds(_fadeOutTime);

            _cityDestroyed.SetActive(false);
            _fadeOut.SetActive(false);

            _cinemachineVirtualCamera.enabled = false;
            _cinemachineBrain.enabled = false;
            _camera.transform.position = CGameManager.Instance.Player.transform.position
                                         + CGameManager.Instance.Player.transform.forward * _cameraHorizontalOffset
                                         + Vector3.up * _cameraVerticalOffset;
            _camera.transform.LookAt(CGameManager.Instance.Player.transform);
        
            _fadeIn.SetActive(true);
            CGameManager.Instance.Player.enabled = false;

            yield return new WaitForSeconds(_fadeInTime);
        
            _fadeIn.SetActive(false);

            yield return new WaitForSeconds(_gameWinTextDelay);

            _gameWinText.SetActive(true);
        }
    }
}
