using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    private Camera _mainCamera;
    private PlayerInputActions _playerInputActions;
    private Draggable _activeDraggable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _mainCamera = Camera.main;
        
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
        
        _playerInputActions.Controls.Press.started += OnSelect;
        _playerInputActions.Controls.Press.canceled += OnDrop;
    }

    private void OnSelect(InputAction.CallbackContext context)
    {
        Ray ray = _mainCamera.ScreenPointToRay(_playerInputActions.Controls.Position.ReadValue<Vector2>());
        
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        _activeDraggable = hit.collider.GetComponent<Draggable>();
        if(_activeDraggable) _activeDraggable.Pickup();
        
    }
    
    private void OnDrop(InputAction.CallbackContext context){
        if(_activeDraggable) _activeDraggable.Drop();
        _activeDraggable = null;
    }

    // Update is called once per frame
    private void Update()
    {
        if(!_activeDraggable) return;
        _activeDraggable.Drag(_playerInputActions.Controls.Delta.ReadValue<Vector2>());
    }
}
