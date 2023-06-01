using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[DefaultExecutionOrder(-1)]
public class UIManager : MonoBehaviour
{
    [Header("Button")]
    public Button playButton;
    public Button returnToMenuButton;
    public Button quitButton;

   

    

    // Start is called before the first frame update
    void Start()
    {
        
        if (playButton)
        {
            playButton.onClick.AddListener(StartGame);

        }


        if (returnToMenuButton)
            returnToMenuButton.onClick.AddListener(BackToMainMenu);

        if (quitButton)
            quitButton.onClick.AddListener(GameQuit);



    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }




    }

    

    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
