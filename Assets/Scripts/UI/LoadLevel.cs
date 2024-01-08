using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class LoadLevel : MonoBehaviour
    {
        public void OnRestartButton()
        {
            LoadScene();
        }

        public void OnNextLevelButton()
        {
            LoadScene();
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
