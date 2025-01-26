using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    [SerializeField] private GameObject effectsGameOver;
    [SerializeField] private GameObject panelWin;

    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public SpawnEnemyState newSpawnEnemyState;
    [HideInInspector] public GamePlayState gamePlayState;
    [HideInInspector] public EnemyDieState enemyDieState;

    [HideInInspector] public GroundManager groundManager;
    [HideInInspector] public SpawnEnemy enemyManager;
    [HideInInspector] public UIManager uiManager;
    public Interstitial interstitial;

    public bool isGame;
    public void Initialize()
    {
        instance = this;
        interstitial = FindObjectOfType<Interstitial>();
        enemyManager = FindObjectOfType<SpawnEnemy>();
        groundManager = FindObjectOfType<GroundManager>();
        uiManager = FindObjectOfType<UIManager>();

        stateMachine = new StateMachine();

        newSpawnEnemyState = new SpawnEnemyState(this, interstitial);
        gamePlayState = new GamePlayState(this);
        enemyDieState = new EnemyDieState(this);

        stateMachine.InitializeState(newSpawnEnemyState);
        enemyManager.OnEndAllEnemy += GameOver;
    }
    private void OnEnable()
    {
        Enemy.OnRun += StartGame;
        EnemyHealth.OnDeath += EnemyDie;
        enemyDieState.OnEndDie += SpawnNewEnemy;
    }
    private void OnDisable()
    {
        Enemy.OnRun -= StartGame;
        EnemyHealth.OnDeath -= EnemyDie;
        enemyDieState.OnEndDie -= SpawnNewEnemy;
    }
    public void StartGame() // invoke when enemy lets strat run
    {
        stateMachine.SwitchState(gamePlayState);
    }
    public void EnemyDie() // invoke when enemy die
    {
        stateMachine.SwitchState(enemyDieState);
    }
    private void SpawnNewEnemy() 
    {
        stateMachine.SwitchState(newSpawnEnemyState);
    }
    private void GameOver()
    {
        panelWin.SetActive(true);
        Instantiate(effectsGameOver);
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
