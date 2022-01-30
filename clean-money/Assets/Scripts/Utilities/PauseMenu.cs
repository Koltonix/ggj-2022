using UnityEngine;

namespace cm.utilities
{
    public class PauseMenu : MonoBehaviour
    {
        private bool isPaused = false;

        [SerializeField]
        private GameObject pauseMenu = null;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
                SetPause(!isPaused);
        }

        public void SetPause(bool value)
        {
            isPaused = value;
            pauseMenu.SetActive(isPaused);

            Time.timeScale = isPaused ? 0.0f : 1.0f;
        }

        private void OnDestroy()
        {
            Time.timeScale = 1.0f;
        }
    }
}