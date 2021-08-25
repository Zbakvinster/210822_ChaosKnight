using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class CIngameMenu : MonoBehaviour
    {
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}