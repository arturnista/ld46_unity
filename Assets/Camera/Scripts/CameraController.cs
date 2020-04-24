using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] private float _minDistance = 7f;
    [SerializeField] private float _maxDistance = 7f;

    private Camera _camera;
    private PlayerMovement _playerMovement;

    void Awake()
    {
        _camera = GetComponent<Camera>();
        GameController gameController = GameObject.FindObjectOfType<GameController>();
        gameController.OnGameStart += HandleGameStart;
    }

    void HandleGameStart()
    {
        _playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (_playerMovement != null)
        {
            float distance = Vector2.Distance((Vector2)transform.position, (Vector2)_playerMovement.transform.position) + 1f;
            // _camera.orthographicSize = Mathf.Max(_minDistance, Mathf.Min(_maxDistance, distance));
            if (distance > _minDistance)
            {
                _camera.orthographicSize = Mathf.MoveTowards(_camera.orthographicSize, _maxDistance, 3f * Time.deltaTime);
            }
            else
            {
                _camera.orthographicSize = Mathf.MoveTowards(_camera.orthographicSize, _minDistance, 3f * Time.deltaTime);
            }
        }
    }

}
