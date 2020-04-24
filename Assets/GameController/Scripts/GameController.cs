using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private Sprite _crosshairSprite;
    [SerializeField] private GameObject _startCanvas;
    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private GameObject _loseCanvas;
    [SerializeField] private GameObject _winCanvas;
    
    private StartScreenController _startScreenController;
    private EnemySpawner _enemySpawner;

    private GameObject _playerObject;
    private StationRecharge _stationRecharge;

    private AudioSource _musicSource;

    private enum GameState
    {
        RUNNING,
        LOSE,
        WIN
    }
    private GameState _currentGameState;
    private bool _isFirstPlay;

    void Awake()
    {
        _musicSource = GetComponent<AudioSource>();

        _currentGameState = GameState.RUNNING;
        _enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        _startScreenController = GameObject.FindObjectOfType<StartScreenController>();
        _startScreenController.OnEndStartGameAnimation += HandleEndStartGameAnimation;
        _startScreenController.OnEndFirstAnimation += HandleEndFirstAnimation;
        
        _loseCanvas.SetActive(false);
        _winCanvas.SetActive(false);
        _gameCanvas.SetActive(false);
        _startCanvas.SetActive(true);

        _isFirstPlay = true;
    }

    void Start()
    {
        _startScreenController.Idle();
    }

    public void StartGame()
    {
        _startCanvas.GetComponent<UICanvas>().Hide();
        _startScreenController.StartGame();
    }
    
    void HandleEndStartGameAnimation()
    {
        RestartGame();
    }

    void HandleEndFirstAnimation()
    {
        CreateStation();
     
        _gameCanvas.SetActive(true);
        _gameCanvas.GetComponent<UICanvas>().Show(.5f, 0f, true);
    }

    void RestartGame()
    {
        if (!_isFirstPlay)
        {
            CleanLastPlay();
        }
        
#if UNITY_WEBGL
        float xspot = _crosshairSprite.texture.width / 2;
        float yspot = _crosshairSprite.texture.height / 2;
        Vector2 hotSpot = new Vector2(xspot, yspot);
        Cursor.SetCursor(_crosshairSprite.texture, hotSpot, CursorMode.ForceSoftware);
#else
        Cursor.SetCursor(_crosshairSprite.texture, Vector2.zero, CursorMode.Auto);
#endif

        _startCanvas.SetActive(false);
        
        _currentGameState = GameState.RUNNING;

        _playerObject = Instantiate(_playerPrefab, _playerSpawnPosition, Quaternion.identity);
        PlayerHealth playerHealth = _playerObject.GetComponent<PlayerHealth>();
        playerHealth.OnDeath += PlayerDeathHandler;

        if (!_isFirstPlay)
        {
            CreateStation();
        }
        _stationRecharge.StartRecharging();

        _enemySpawner.StartSpawn();
     
        if (!_isFirstPlay)
        {
            _gameCanvas.SetActive(true);
            _gameCanvas.GetComponent<UICanvas>().Show(.5f, 0f, true);
        }
        
        if(OnGameStart != null)
        {
            OnGameStart();
        }
        
        _musicSource.Play();
        _isFirstPlay = false;
    }

    void CleanLastPlay()
    {
        _loseCanvas.GetComponent<UICanvas>().Hide();
        _winCanvas.GetComponent<UICanvas>().Hide();

        if (_playerObject != null)
        {
            Destroy(_playerObject);
        }

        if (_stationRecharge.gameObject != null)
        {
            Destroy(_stationRecharge.gameObject);
        }

        _enemySpawner.KillAllEnemies();
        
        GameObject[] missiles = GameObject.FindGameObjectsWithTag("Pickup");
        foreach (var item in missiles)
        {
            Destroy(item);
        }

        DestroyAllProjectiles();
    }

    void CreateStation()
    {
        GameObject stationObject = Instantiate(_stationPrefab, _stationSpawnPosition, Quaternion.identity);
        _stationRecharge = stationObject.GetComponent<StationRecharge>();
        _stationRecharge.OnFinishRecharge += StationRechargeFinishHandler;
    }

    void DestroyAllProjectiles()
    {
        ProjectileMovement[] projectileMovements = GameObject.FindObjectsOfType<ProjectileMovement>();
        foreach (var movement in projectileMovements)
        {
            Destroy(movement.gameObject);
        }
    }

    void PlayerDeathHandler()
    {
        if (_currentGameState != GameState.RUNNING) return;
        _currentGameState = GameState.LOSE;
        
        StartCoroutine(FinishCoroutine(_loseCanvas));
    }

    void StationRechargeFinishHandler()
    {
        if (_currentGameState != GameState.RUNNING) return;
        _currentGameState = GameState.WIN;
        
        StartCoroutine(FinishCoroutine(_winCanvas));
    }

    IEnumerator FinishCoroutine(GameObject canvas)
    {
        _musicSource.Stop();
        _enemySpawner.StopSpawn();
        
        yield return new WaitForSeconds(1.5f);

        canvas.SetActive(true);
        yield return StartCoroutine(canvas.GetComponent<UICanvas>().ShowCoroutine(1f, 0f, true));

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        _gameCanvas.SetActive(false);
    }

    IEnumerator ShowGroupCoroutine(GameObject canvas)
    {
        float alpha = 0f;

        CanvasGroup group = canvas.GetComponent<CanvasGroup>();
        group.alpha = alpha;
        while (alpha < 1f)
        {
            alpha += 2f * Time.deltaTime;
            group.alpha = alpha;
            yield return null;
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
