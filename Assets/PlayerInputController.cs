using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    PlayerInput _playerInput;
    PlayerControll _playerControll;
    BulletFire _bulletFire;
    BulletSelectController _bulletSelectController;
    CameraController _cameraController;

    Vector3 dir;
    private void Awake()
    {
        TryGetComponent(out _playerInput);
        TryGetComponent(out _playerControll);
        _bulletFire = FindObjectOfType<BulletFire>();
        _bulletSelectController = FindObjectOfType<BulletSelectController>();
        _cameraController = FindObjectOfType<CameraController>();
    }

    private void OnEnable()
    {
        _playerInput.actions["Jump"].started += OnJump;
        _playerInput.actions["Fire"].started += OnFire;
        _playerInput.actions["Dodge"].started += OnDodge;
        _playerInput.actions["OpenUI"].started += OnSelect;
        _playerInput.actions["BulletSelect"].started += OnBulletSelect;
        _playerInput.actions["Run"].performed += OnRun;
        _playerInput.actions["Run"].canceled += OnRunCanceled;
        _playerInput.actions["Attack"].started += OnAttack;
        _playerInput.actions["SpecialAttack"].started += OnSpecialAttack;


        _playerInput.actions["Option"].started += OnMenu;
        _playerInput.actions["LockOn"].performed += OnLockOn;
       // NewTest a = new NewTest();
        //a.Player.Move.performed += contexet => _playerControll.Move(Vector3.zero); 
    }

    private void OnDisable()
    {
        _playerInput.actions["Jump"].started -= OnJump;
        _playerInput.actions["Fire"].started -= OnFire;
        _playerInput.actions["Dodge"].started -= OnDodge;
        _playerInput.actions["OpenUI"].started -= OnSelect;
        _playerInput.actions["BulletSelect"].started += OnBulletSelect;
        _playerInput.actions["Run"].performed -= OnRun;
        _playerInput.actions["Run"].canceled -= OnRunCanceled;
        _playerInput.actions["Attack"].started -= OnAttack;
        _playerInput.actions["SpecialAttack"].started -= OnSpecialAttack;
        _playerInput.actions["Option"].started -= OnMenu;
        _playerInput.actions["LockOn"].performed -= OnLockOn;
    }
    private void FixedUpdate()
    {
        var direction = _playerInput.actions["Move"].ReadValue<Vector2>();
        dir = new Vector3(direction.x, 0, direction.y);
        _playerControll.Move(dir);
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        var direction = obj.ReadValue<Vector2>();
        var dir = new Vector3(direction.x, 0, direction.y);
        _playerControll.Move(dir);
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        _playerControll.Move(Vector3.zero);
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        _playerControll.Jump();
    }

    private void OnFire(InputAction.CallbackContext obj)
    {
        _bulletFire.ShootBullet();
    }

    private void OnDodge(InputAction.CallbackContext obj)
    {
        _playerControll.Dodge(dir);
    }

    private void OnSelect(InputAction.CallbackContext obj)
    {
        _bulletSelectController.OpenBulletMenu();
    }

    private void OnBulletSelect(InputAction.CallbackContext obj)
    {
        _bulletSelectController.SelectBullet(obj.ReadValue<float>());
    }

    private void OnRun(InputAction.CallbackContext obj)
    {
        _playerControll.SetRunning(true);
    }

    private void OnRunCanceled(InputAction.CallbackContext obj)
    {
        _playerControll.SetRunning(false);
    }
    private void OnAttack(InputAction.CallbackContext obj)
    {
        _playerControll.BasicAttackActive();
    }

    private void OnSpecialAttack(InputAction.CallbackContext obj)
    {
        _playerControll.SpecialAttackActive();
    }


    private void OnMenu(InputAction.CallbackContext obj)
    {
        GameManager.Instance.SetMenu();
    }

    private void OnLockOn(InputAction.CallbackContext obj)
    {
        _cameraController.LockON();
    }
}
