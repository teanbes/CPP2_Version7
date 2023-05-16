using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : Singleton<GameManager>
{
    [HideInInspector]
    public PlayerInput playerActions;

    
    protected override void Awake()
    {
        base.Awake();
        playerActions = new PlayerInput();


    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }



    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Vars for MousePos()
    public GameObject playerPrefab;
    [HideInInspector] public GameObject playerInstance = null;
    [HideInInspector] public Transform currentSpawnPoint;

    public Ray MousePos()
    {
        Vector3 screenSpacePos = playerActions.Player.Look.ReadValue<Vector2>();
        return Camera.main.ScreenPointToRay(screenSpacePos);
        
    }

    public void SpawnPlayer(Transform spawnPoint)
    {
        playerInstance = Instantiate(playerPrefab, spawnPoint.transform, false);
        currentSpawnPoint = spawnPoint;
    }

    public void Respawn()
    {

        if (playerInstance)
            playerInstance.transform.position = currentSpawnPoint.position;

    }

} 
