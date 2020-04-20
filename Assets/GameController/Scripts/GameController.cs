using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public delegate void GameStartHandler();
    public event GameStartHandler OnGameStart;

    [Header("Player")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Vector3 _playerSpawnPosition;
    [Space]
    [Header("Station")]
    [SerializeField] private GameObject _stationPrefab;
    [SerializeField] private Vector3 _stationSpawnPosition;
    [Space]
    [Header("UI")]
    [SerializeField] private Sprite _mouseSprite;
    [SerializeField] private GameObject _startCanvas;
    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private GameObject _loseCanvas;
    [SerializeField] private GameObject _winCanvas;
    
    private EnemySpawner _enemySpawner;
    private GameObject _playerObject;
    private GameObject _stationObject;

    private enum GameState
    {
        RUNNING,
        LOSE,
        WIN
    }
    private GameState _currentGameState;

    void Awake()
    {
        _currentGameState = GameState.RUNNING;
        _enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        
        Button startGameButton = _startCanvas.transform.Find("StartGameButton").GetComponent<Button>();
        startGameButton.onClick.AddListener(StartGameHandler);

        Button restartOnLose = _loseCanvas.transform.Find("RestartGameButton").GetComponent<Button>();
        restartOnLose.onClick.AddListener(StartGameHandler);

        Button restartOnWin = _winCanvas.transform.Find("RestartGameButton").GetComponent<Button>();
        restartOnWin.onClick.AddListener(StartGameHandler);
        
        _loseCanvas.SetActive(false);
        _winCanvas.SetActive(false);
        _gameCanvas.SetActive(false);
        _startCanvas.SetActive(true);
    }

    void StartGameHandler()
    {
        CleanLastPlay();
        Cursor.SetCursor(_mouseSprite.texture, Vector2.zero, CursorMode.Auto);

        _startCanvas.SetActive(false);
        
        _currentGameState = GameState.RUNNING;

        _playerObject = Instantiate(_playerPrefab, _playerSpawnPosition, Quaternion.identity);
        PlayerHealth playerHealth = _playerObject.GetComponent<PlayerHealth>();
        playerHealth.OnDeath += PlayerDeathHandler;

        _stationObject = Instantiate(_stationPrefab, _stationSpawnPosition, Quaternion.identity);
        StationRecharge stationRecharge = _stationObject.GetComponent<StationRecharge>();
        stationRecharge.OnFinishRecharge += StationRechargeFinishHandler;

        _enemySpawner.StartSpawn();
     
        _gameCanvas.SetActive(true);
        
        if(OnGameStart != null)
        {
            OnGameStart();
        }
    }

    void CleanLastPlay()
    {
        _loseCanvas.SetActive(false);
        _winCanvas.SetActive(false);

        if (_playerObject != null)
        {
            Destroy(_playerObject);
        }

        if (_stationObject != null)
        {
            Destroy(_stationObject);
        }

        GameObject[] missiles = GameObject.FindGameObjectsWithTag("MissilePickup");
        foreach (var item in missiles)
        {
            Destroy(item);
        }

        _enemySpawner.KillAllEnemies();
        DestroyAllProjectiles();
    }

    void PlayerDeathHandler()
    {
        if (_currentGameState != GameState.RUNNING) return;
        _currentGameState = GameState.LOSE;

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        _enemySpawner.StopSpawn();
        _gameCanvas.SetActive(false);
        _loseCanvas.SetActive(true);
    }

    void StationRechargeFinishHandler()
    {
        if (_currentGameState != GameState.RUNNING) return;
        _currentGameState = GameState.WIN;
        
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        _enemySpawner.StopSpawn();
        // _enemySpawner.KillAllEnemies();
        _gameCanvas.SetActive(false);
        _winCanvas.SetActive(true);
    }

    void DestroyAllProjectiles()
    {
        ProjectileMovement[] projectileMovements = GameObject.FindObjectsOfType<ProjectileMovement>();
        foreach (var movement in projectileMovements)
        {
            Destroy(movement.gameObject);
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_stationSpawnPosition, Vector3.one * 3f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_playerSpawnPosition, Vector3.one);
    }

}
