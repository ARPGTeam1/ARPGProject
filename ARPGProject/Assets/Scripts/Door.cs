using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private bool isLocked;
    [SerializeField] [Range(0.75f, 0.999f)] private float openSpeed = 0.95f;
    
    private enum State { Opening, Open, Closing, Closed }
    private State _state;
    private NavMeshObstacle _navMeshObstacle;
    private Vector3 _closedPos;
    private Vector3 _openPos;
    private float _elapsed;
    
    private void Start()
    {
        _navMeshObstacle = door.GetComponent<NavMeshObstacle>();
        _closedPos = transform.position;
        _openPos = _closedPos + new Vector3(0, _closedPos.y + (door.transform.localScale.y / 2), 0);
        _navMeshObstacle.enabled = isLocked;
        ChangeState(State.Closed);
    }

    private void FixedUpdate()
    {
        if (isLocked) return;
        
        switch (_state)
        {
            case State.Opening:
                _elapsed += 1f - openSpeed;
                door.transform.position = Vector3.Slerp(_closedPos, _openPos, _elapsed);
                if (door.transform.position.y >= _openPos.y)
                {
                    door.transform.position = _openPos;
                    ChangeState(State.Open);
                }
                
                break;
            case State.Open:
                    
                break;
            case State.Closing:
                _elapsed += 1f - openSpeed;
                door.transform.position = Vector3.Slerp(_openPos, _closedPos, _elapsed);
                if (door.transform.position.y <= _closedPos.y)
                {
                    door.transform.position = _closedPos;
                    ChangeState(State.Closed);
                }
                
                break;
            case State.Closed:
                    
                break;
        }
    }

    public void Unlock()
    {
        //_navMeshObstacle.enabled = false;
    }

    public void Lock()
    {
        
    }

    private void ChangeState(State newState)
    {
        _elapsed = 0f;
        _state = newState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeState(State.Opening);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeState(State.Closing);
        }
    }
}
