using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
   
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public LedgeDetector LedgeDetector { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get;  set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeLength { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }
    [field: SerializeField] public GameObject aura;
    [field: SerializeField] public GameObject DeathParticles { get; private set; }
    [field: SerializeField] public GameObject gameOverPanel;
    [field: SerializeField] public GameObject Sword;

    [field: SerializeField] public UIManager uiManager;

    [HideInInspector] public bool isSpell;
    [HideInInspector] public bool isWeapon;
    [HideInInspector] public bool isCombo;
    [HideInInspector] public bool isSaved;

    [HideInInspector] public bool RandomEnemiesArea1;
    [HideInInspector] public bool RandomEnemiesArea2;



    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;
    public Transform MainCameraTransform { get; private set; }

    [field: SerializeField] public Health Health;
    public int playerhealth;

    private void Start()
    {
        // Initial weapon state
        isWeapon = false;
        // Initial spell state
        isSpell = false;
        // Initial combo state
        isCombo = false;
        // Initial player state
        isSaved = GameManager.StateManager.gameState.player.isSaved;

        if (isSaved) 
        {
            Debug.Log("el archivo esta salvado");
            LoadGameComplete();
            
            GameManager.Instance.LoadGame();
            Health.HealthBarHandler();


        }

        if (isWeapon) 
        {
            Sword.SetActive(true);
        }
        

        MainCameraTransform = Camera.main.transform;

       
        SwitchState(new PlayerFreeLookState(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnIncreaseHealth += HandleHealthIncrease;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnIncreaseHealth -= HandleHealthIncrease;
        Health.OnDie -= HandleDie;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }

    private void HandleHealthIncrease() 
    {
        SwitchState(new PlayerGetsHealth(this)); 
    }


    private void HandleDie()
    {
        SwitchState(new PlayerDeadState(this));
    }

    // Function called when saving game
    public void SaveGamePrepare()
    {
        // Get Player Data Object
        LoadSaveManager.GameStateData.DataPlayer data = GameManager.StateManager.gameState.player;

        // Fill in player data for save game
        data.isSaved = true;
        data.collectedSpell = isSpell;
        data.collectedSword = isWeapon;
        data.collectedCombo = isCombo;
        data.health = Health.health;

        data.posRotScale.posX = transform.position.x;
        data.posRotScale.posY = transform.position.y;
        data.posRotScale.posZ = transform.position.z;

        data.posRotScale.rotX = transform.localEulerAngles.x;
        data.posRotScale.rotY = transform.localEulerAngles.y;
        data.posRotScale.rotZ = transform.localEulerAngles.z;

        data.posRotScale.scaleX = transform.localScale.x;
        data.posRotScale.scaleY = transform.localScale.y;
        data.posRotScale.scaleZ = transform.localScale.z;



    }

    // Function called when loading is complete
    public void LoadGameComplete()
    {
        // Get Player Data Object
        LoadSaveManager.GameStateData.DataPlayer data =
            GameManager.StateManager.gameState.player;

        // Load data back to Player
        isSpell = data.collectedSpell;
        isWeapon = data.collectedSword;
        isCombo =  data.collectedCombo;
        Health.health = data.health;
        Health.HealthBarHandler();


        // Set position

        Controller.enabled = false;
        transform.position = new Vector3(data.posRotScale.posX,
            data.posRotScale.posY, data.posRotScale.posZ);
        Controller.enabled = true;

        
        // Set rotation
        transform.rotation = Quaternion.Euler(data.posRotScale.rotX,
            data.posRotScale.rotY, data.posRotScale.rotZ);

        // Set scale
        transform.localScale = new Vector3(data.posRotScale.scaleX,
            data.posRotScale.scaleY, data.posRotScale.scaleZ);

       
    }

    
    public void DestroyObjectOnDeath()
    {
        Destroy(this);
    }
}
