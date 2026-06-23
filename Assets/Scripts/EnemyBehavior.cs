using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyBehavior : MonoBehaviour
{
    // =========================
    // Game Manager Reference
    // =========================
    private GameBehavior _gameManager;

    // =========================
    // Enemy HP System
    // =========================
    private int _enemyLives = 3;

    public int EnemyLives
    {
        get { return _enemyLives; }
        private set
        {
            _enemyLives = value;

            if (_enemyLives <= 0)
            {
                Destroy(gameObject);
                Debug.Log("Enemy down!");
            }
        }
    }

    // =========================
    // NavMesh Patrol System
    // =========================
    private NavMeshAgent _agent;

    public Transform PatrolRoute;

    private List<Transform> _locations = new List<Transform>();
    private int _locationIndex = 0;

    // Player reference
    private Transform _player;

    void Start()
    {
        // Find Game Manager
        _gameManager = GameObject.Find("Game Manager")
                                 .GetComponent<GameBehavior>();

        // Setup NavMesh Agent
        _agent = GetComponent<NavMeshAgent>();

        // Find Player
        GameObject playerObj = GameObject.Find("Player");

        if (playerObj != null)
        {
            _player = playerObj.transform;
        }

        // Setup patrol points
        if (PatrolRoute != null)
        {
            foreach (Transform child in PatrolRoute)
            {
                _locations.Add(child);
            }
        }

        // Start patrol
        if (_locations.Count > 0)
        {
            MoveToNextPatrolLocation();
        }
    }

    void Update()
    {
        // Move to next waypoint when reaching current waypoint
        if (_locations.Count > 0 &&
            _agent != null &&
            !_agent.pathPending &&
            _agent.remainingDistance < 0.5f)
        {
            MoveToNextPatrolLocation();
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (_locations.Count == 0) return;

        _agent.destination = _locations[_locationIndex].position;

        _locationIndex = (_locationIndex + 1) % _locations.Count;
    }

    void OnTriggerEnter(Collider other)
    {
        // =========================
        // Player enters detection range
        // =========================
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Player detected - attack!");

            // Damage player
            if (_gameManager != null)
            {
                _gameManager.HP -= 2;
                Debug.Log("Player HP: " + _gameManager.HP);
            }

            // Chase player
            if (_agent != null && _player != null)
            {
                _agent.destination = _player.position;
            }
        }

        // =========================
        // Bullet hits enemy
        // =========================
        if (other.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;

            Debug.Log("Critical hit! Enemy lives: " + EnemyLives);

            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");

            // Resume patrol
            if (_locations.Count > 0)
            {
                MoveToNextPatrolLocation();
            }
        }
    }
}