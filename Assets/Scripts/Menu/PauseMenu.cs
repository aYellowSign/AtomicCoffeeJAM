using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject m_pause;
        public bool m_isPaused;
        // Start is called before the first frame update
        void Start()
        {
            m_pause.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (m_isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
        public void PauseGame()
        {
            m_pause.SetActive (true);
            Time.timeScale = 0f;
            m_isPaused = true;
        }
        public void ResumeGame()
        {
            m_pause.SetActive(false );
            Time.timeScale = 1f;
            m_isPaused= false;
        }
        public void GoToMainMenu(int menu)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(menu);
        }
        public void Quit()
        {
            Application.Quit();
        }
    }
}
