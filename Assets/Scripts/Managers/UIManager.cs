using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;


[DefaultExecutionOrder(-1)]
public class UIManager : MonoBehaviour
{
    [Header("Button")]
    public Button playButton;
    public Button returnToMenuButton;
    public Button quitButton;
    public Button continueButton;

    [Header("Pause Elements")]
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    private bool gamePaused;
    private PlayerStateMachine cRef;
    private EnemyStateMachine eRef;




    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 )
        {
            cRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>();

        
            //Invoke("GetPlayerCOmponents", 1f);
            eRef = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStateMachine>();

        }

        if (playButton)
        {
            playButton.onClick.AddListener(StartGame);

        }


        if (returnToMenuButton)
            returnToMenuButton.onClick.AddListener(BackToMainMenu);

        if (quitButton)
            quitButton.onClick.AddListener(GameQuit);

        if (continueButton)
            quitButton.onClick.AddListener(ContinueFromSave);



    }
    private void GetPlayerCOmponents()
    {
       //cRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>();
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

        if (Input.GetKeyDown(KeyCode.P))
            PauseGame();

        if (Input.GetKeyDown(KeyCode.M))
            LoadGame();


    }

    

    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(1);
    }

    public void ContinueFromSave()
    {

        SceneManager.LoadScene(1);
        
        Invoke("LoadGame", 1.5f);
       

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

    public void PauseGame()
    {
        gamePaused = !gamePaused;
        pausePanel.SetActive(gamePaused);

        if (gamePaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void SaveGame()
    {
        cRef.SaveGamePrepare();
        eRef.SaveGamePrepare();

        GameManager.Instance.SaveGame();
    }

    public void LoadGame()
    {
        cRef.LoadGameComplete();
        eRef.LoadGameComplete();

        GameManager.Instance.LoadGame();
    }
}
