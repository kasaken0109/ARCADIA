using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UISelectInput : MonoBehaviour
{
    [SerializeField]
    private Button _back;

    [SerializeField]
    private Button _goqQuest;

    PlayerInput _playerInput;

    [SerializeField]
    private bool IsActiveMode = false;

    ButtonSelectController _buttonSelectController;

    void Awake()
    {
        TryGetComponent(out _playerInput);
        _buttonSelectController = FindObjectOfType<ButtonSelectController>();
    }
    private void OnEnable()
    {
        _playerInput.actions["Back"].started += OnBackScene;
        _playerInput.actions["GoQuest"].started += OnGoQuest;
    }

    private void OnDisable()
    {
        _playerInput.actions["Back"].started -= OnBackScene;
        _playerInput.actions["GoQuest"].started -= OnGoQuest;
    }

    public void OnSelectButton(InputAction.CallbackContext obj)
    {
        _buttonSelectController.ButtonSelect(obj.ReadValue<Vector2>());
        Debug.Log("Connection");
    }

    private void OnBackScene(InputAction.CallbackContext obj)
    {
        _back.onClick?.Invoke();
    }

    private void OnGoQuest(InputAction.CallbackContext obj)
    {
        _goqQuest.onClick?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        var gamepad = Gamepad.current;
        //_buttonSelectController.enabled = gamepad != null;
    }
}
