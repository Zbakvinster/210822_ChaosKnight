using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class CIngameMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _ingameMenu;
        
        public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        public void QuitGame() => Application.Quit();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                _ingameMenu.SetActive(!_ingameMenu.activeSelf);
        }
    }
}