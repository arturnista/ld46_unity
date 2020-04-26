using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenController : MonoBehaviour
{

    
    public delegate void EndStartGameAnimationHandler();
    public event EndStartGameAnimationHandler OnEndFirstAnimation;
    public event EndStartGameAnimationHandler OnEndStartGameAnimation;
    
    [SerializeField] private Vector2 _teleportPosition = default;
    [SerializeField] private Vector2 _finalPosition = Vector2.zero;
    [Space]
    [SerializeField] private Vector2 _finalPlayerPosition = Vector2.zero;
    [Space]
    [SerializeField] private GameObject _cameraPrefab;
    [SerializeField] private GameObject _startScreenPlayerPrefab;
    [SerializeField] private Vector2 _startScreenPlayerPosition = default;

    private GameObject _startScreenPlayerCreated;
    private Camera _startScreenCamera;
    private Transform _spaceLightsObject;
    private float _time;

    private GameController _gameController;

    private bool _alreadyPlayed;
    private bool _startScreenCameraIsMoving;
    private bool _gameCameraIsMoving;

    void Awake()
    {
        _alreadyPlayed = false;
    }

    public void Idle()
    {
        _startScreenPlayerCreated = Instantiate(_startScreenPlayerPrefab, _startScreenPlayerPosition, Quaternion.identity);
        _startScreenCamera = _startScreenPlayerCreated.GetComponentInChildren<Camera>();
        _spaceLightsObject = _startScreenPlayerCreated.transform.Find("SpaceLights");
    }

    public void StartGame()
    {
        if (_alreadyPlayed)
        {
            if (OnEndStartGameAnimation != null)
            {
                OnEndStartGameAnimation();
            }
            return;
        }
        _alreadyPlayed = true;
        StartCoroutine(AnimationCoroutine());
    }

    void Update()
    {
        if (_startScreenCameraIsMoving)
        {
            _startScreenCamera.transform.Translate(Vector2.up * 5f * Time.deltaTime, Space.Self);
        }
    }

    IEnumerator AnimationCoroutine()
    {
        _startScreenCameraIsMoving = true;
        yield return new WaitForSeconds(1f);
        _startScreenCameraIsMoving = false;

        Vector2 _spaceLightsOffset = _spaceLightsObject.transform.localPosition - _startScreenCamera.transform.localPosition;
        _spaceLightsObject.transform.parent = null;
        
        ParticleSystem system = _spaceLightsObject.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule mainModule = system.main;
        // mainModule.gravityModifier = 1f;
        _spaceLightsObject.transform.position = _teleportPosition + _spaceLightsOffset;
        
        Destroy(_startScreenPlayerCreated);

        GameObject anotherScreenPlayer = Instantiate(_startScreenPlayerPrefab, new Vector3(0f, -35f, 0f), Quaternion.identity);
        Destroy(anotherScreenPlayer.GetComponentInChildren<Camera>().gameObject);
        Destroy(anotherScreenPlayer.transform.Find("SpaceLights").gameObject);
        anotherScreenPlayer.GetComponent<StartScreenPlayer>().Speed = 0f;

        if (OnEndFirstAnimation != null)
        {
            OnEndFirstAnimation();
        }
        
        GameObject camera = Instantiate(_cameraPrefab, _teleportPosition, Quaternion.identity);

        Vector3 cameraStartPos = _teleportPosition;
        Vector3 cameraFinalPos = _finalPosition;
        cameraStartPos.z = -10f;
        cameraFinalPos.z = -10f;

        float speed = 12f;
        float playerSpeed = 13.5f;

        while (camera.transform.position != cameraFinalPos)
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, cameraFinalPos, speed * Time.deltaTime);
            anotherScreenPlayer.transform.position = Vector3.MoveTowards(anotherScreenPlayer.transform.position, _finalPlayerPosition, playerSpeed * Time.deltaTime);
            yield return null;

            speed -= 2f * Time.deltaTime;
            playerSpeed -= 1.7f * Time.deltaTime;
        }

        yield return new WaitForSeconds(0.3f);

        Destroy(anotherScreenPlayer);
        if (OnEndStartGameAnimation != null)
        {
            OnEndStartGameAnimation();
        }
    }

}
