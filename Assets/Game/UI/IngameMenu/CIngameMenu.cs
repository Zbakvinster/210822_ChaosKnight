using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.UI.IngameMenu
{
    public class CIngameMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _ingameMenu;
        [SerializeField] private Slider _cameraSensitivitySlider;

        public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        public void QuitGame() => Application.Quit();

        public void SetCameraSensitivity(float sensitivity)
            => CGameManager.Instance.Player.CameraSensitivity = sensitivity; 

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                _ingameMenu.SetActive(!_ingameMenu.activeSelf);
        }

        private void Start()
        {
            _cameraSensitivitySlider.value = CGameManager.Instance.Player.CameraSensitivity;
        }
    }
}