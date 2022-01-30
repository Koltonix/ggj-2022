using UnityEngine;
using UnityEngine.SceneManagement;

namespace cm.utilities
{
    public class SceneTransition : MonoBehaviour
    {
        public static void LoadScene(int value)
        {
            SceneManager.LoadScene(value);
        }

        public static void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public static void Quit()
        {
            Application.Quit();
        }
    }
}