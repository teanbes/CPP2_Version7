using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Target Target { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }

    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public int AttackKnockback { get; private set; }
    [field: SerializeField] public GameObject DeathParticles { get; private set; }

    // Enemy Type: Follow or Patrol
    [field: SerializeField] public bool IsPatrolling { get; private set; }


    // Vars for anemy path
    [field: SerializeField] public Transform PathTarget;
    [field: SerializeField] public GameObject[] Path { get; private set; }
    [field: SerializeField] public int PathIndex;
    [field: SerializeField] public float DistThreshhold { get; private set; }

    // PowerUps
    [field: SerializeField] public GameObject[] collectibles;
    Vector3 currentEulerAngles;
    Quaternion currentRotation;
    public bool enemyDead = false;

    // Save Data vars
    public Health player;
    public int enemyID;
    public int health;



    private void Start()
    {
        // Store the InstanceID
        enemyID = GetInstanceID();

        // Get Components
        // player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        Invoke("GetPlayerComponents", 2f);
        

        // looks for empty gameobjects to set the path
        if (Path.Length <= 0) Path = GameObject.FindGameObjectsWithTag("Patrol");

        // Aproximation for path, almost never reaches 0 because of float calculations
        if (DistThreshhold <= 0) DistThreshhold = 0.5f;

        Agent.updatePosition = false;
        Agent.updateRotation = false;

        if (!IsPatrolling)
            SwitchState(new EnemyIdleState(this));
        if (IsPatrolling)
            SwitchState(new EnemyPatrollingState(this));
        
    }
   
    private void GetPlayerComponents()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }
   

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new EnemyImpactState(this));
    }

    private void HandleDie()
    {
        SwitchState(new EnemyDeadState(this));
        enemyDead = true;
        StartCoroutine(SpawnCollectible());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
    }

    private IEnumerator SpawnCollectible()
    {
        yield return new WaitForSeconds(2f); // wait for 1 second
        SpawnPowerUps(collectibles);
        

    }


    public void SpawnPowerUps(GameObject[] collectibles)
    {
        if (collectibles.Length > 0)
        {
            currentEulerAngles = new Vector3(-90, 0, 0);
            currentRotation.eulerAngles = currentEulerAngles;
            transform.rotation = currentRotation;
            
            int index = Random.Range(0, collectibles.Length);
            Vector3 newPos = transform.position;
            newPos.y += 2;


            Instantiate(collectibles[index], newPos, transform.rotation);
            

        }

    }

    public void SaveGamePrepare()
    {
        // Create enemy data for this enemy
        LoadSaveManager.GameStateData.DataEnemy data =
            new LoadSaveManager.GameStateData.DataEnemy();

        // Fill in data for current enemy
        data.enemyID = GetInstanceID();
        data.health = health;

        data.posRotScale.posX = transform.position.x;
        data.posRotScale.posY = transform.position.y;
        data.posRotScale.posZ = transform.position.z;

        data.posRotScale.rotX = transform.rotation.x;
        data.posRotScale.rotY = transform.rotation.y;
        data.posRotScale.rotZ = transform.rotation.z;

        data.posRotScale.scaleX = transform.localScale.x;
        data.posRotScale.scaleY = transform.localScale.y;
        data.posRotScale.scaleZ = transform.localScale.z;

        // Add enemy to Game State
        GameManager.StateManager.gameState.enemies.Add(data);
    }

    // Function called when loading is complete
    public void LoadGameComplete()
    {
        // Cycle through enemies and find matching ID
        List<LoadSaveManager.GameStateData.DataEnemy> enemies =
            GameManager.StateManager.gameState.enemies;

        // Reference to this enemy
        LoadSaveManager.GameStateData.DataEnemy data = null;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].enemyID == enemyID)
            {
                // Found enemy. Now break break from loop
                data = enemies[i];
                break;
            }
        }

        // If here and no enemy is found, then it was destroyed when saved. So destroy.
        if (data == null)
        {
            Destroy(gameObject);
            return;
        }

        // Else load enemy data
        enemyID = data.enemyID;
        health = data.health;

        // Set position
        transform.position = new Vector3(data.posRotScale.posX,
            data.posRotScale.posY, data.posRotScale.posZ);

        // Set rotation
        transform.localRotation = Quaternion.Euler(data.posRotScale.rotX,
            data.posRotScale.rotY, data.posRotScale.rotZ);

        // Set scale
        transform.localScale = new Vector3(data.posRotScale.scaleX,
            data.posRotScale.scaleY, data.posRotScale.scaleZ);

        // Clear existing enemy data
        enemies.Remove(data);
    }

}
