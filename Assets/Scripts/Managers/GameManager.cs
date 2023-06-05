using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    // Reference to single active instance of object - for singleton behaviour
    public static GameManager Instance
    {
        get => _instance;
    }

      
    public static LoadSaveManager StateManager
    {
        get
        {
            if(!statemanager)
                statemanager = Instance.GetComponent<LoadSaveManager>();

            return statemanager;
            // return null;

        }

    }

    // reference to SaveLoad game manager
    private static LoadSaveManager statemanager = null;

    // should load from save game on level load, or restart level from defaults
    private static bool bShouldLoad = false;


    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if(bShouldLoad) 
        {
            // load the file to read from
            StateManager.Load(Application.persistentDataPath + "/SaveGame.xml");
            
            // Reset load flag
            bShouldLoad = false;
        }

    }

    public PlayerStateMachine playerPrefab;
    [HideInInspector] public PlayerStateMachine playerInstance = null;
    [HideInInspector] public Transform currentSpawnPoint;


    public void SpawnPlayer(Transform spawnPoint)
    {
        playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        currentSpawnPoint = spawnPoint;

        
    }

    public void Respawn()
    {

        if (playerInstance)
            playerInstance.transform.position = currentSpawnPoint.position;

    }

    public void SaveGame()
    {
        StateManager.Save(Application.persistentDataPath + "/SaveGame.xml");
    }

    public void LoadGame()
    {
        bShouldLoad = true;

        StateManager.Load(Application.persistentDataPath + "/SaveGame.xml");

    }
   

} 
