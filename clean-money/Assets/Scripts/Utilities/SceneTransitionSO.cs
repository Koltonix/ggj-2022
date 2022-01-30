using UnityEngine;

namespace cm.utilities
{
    [CreateAssetMenu(fileName = "SceneTransition", menuName = "ScriptableObjects/Tools/SceneTransition")]
    public class SceneTransitionSO : ScriptableObject
    {
        public void LoadScene(int value)
        {
            SceneTransition.LoadScene(value);
        }

        public void RestartScene()
        {
            SceneTransition.RestartScene();
        }

        public  void Quit()
        {
            SceneTransition.Quit();
        }
    }
}