using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    public static GameManager Instance
    {
        get => _instance;
    }

    public PlayerStateMachine playerPrefab;
    [HideInInspector] public PlayerStateMachine playerInstance = null;
    [HideInInspector] public Transform currentSpawnPoint;

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


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    
      
    }



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

   

} 
