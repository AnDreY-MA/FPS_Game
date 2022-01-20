using UnityEngine;

public class Player : MonoBehaviour
{
    protected PlayerInput _playerInput;

    private bool _haveWeapon = false;

    private Gun _weapon;

    protected virtual void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Player.WeaponPickUp.performed += ctx => PickUpWeapon();
        _weapon = FindObjectOfType<Gun>();
    }

    private void Start()
    {
        _weapon.gameObject.SetActive(false);
    }

    private void PickUpWeapon()
    {
        if (_haveWeapon == false)
        {
            _haveWeapon = true;
            _weapon.gameObject.SetActive(_haveWeapon);
        }
        else if (_haveWeapon == true)
        {
            _haveWeapon = false;
            _weapon.gameObject.SetActive(_haveWeapon);
        }
    }
}
