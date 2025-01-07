using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(GraphicRaycaster))]
public class DragAndDrop : MonoBehaviour
{
    private Camera _mainCamera;
    private PlayerInputActions _playerInputActions;
    private DraggableSegment _activeDraggableSegment;
    private GraphicRaycaster _raycaster;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _mainCamera = Camera.main;
        _raycaster = GetComponent<GraphicRaycaster>();

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();

        _playerInputActions.Controls.Press.started += OnSelect;
        _playerInputActions.Controls.Press.canceled += OnDrop;
    }

    private void OnSelect(InputAction.CallbackContext context)
    {
        Vector2 pointPosition = _playerInputActions.Controls.Position.ReadValue<Vector2>();
        // Ray ray = _mainCamera.ScreenPointToRay(_playerInputActions.Controls.Position.ReadValue<Vector2>());
        //
        // Debug.Log(_playerInputActions.Controls.Position.ReadValue<Vector2>());
        //
        // RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        //
        // RaycastHit2D hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(pointPosition), Vector2.zero);
        //
        // List<RaycastResult> results = new List<RaycastResult>();
        // _raycaster.Raycast(pointPosition, results);
        //
        // if (!hit.collider) return;
        // Debug.Log(hit.collider.gameObject.name);

        // _activeDraggableSegment = hit.collider.GetComponent<DraggableSegment>();
        // if(_activeDraggableSegment) _activeDraggableSegment.Pickup();
    }

    private void OnDrop(InputAction.CallbackContext context)
    {
        // if(_activeDraggableSegment) _activeDraggableSegment.Drop();
        // _activeDraggableSegment = null;
    }

    // Update is called once per frame
    private void Update()
    {
        // if(!_activeDraggableSegment) return;
        // _activeDraggableSegment.Drag(_playerInputActions.Controls.Delta.ReadValue<Vector2>());
    }
}