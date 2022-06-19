using UnityEngine;

public class PlayerInteration : Player
{
    [SerializeField] private float _takeDistance;
    [SerializeField] private float _holdDistance;
    [SerializeField] private float _forceThrow;
    [SerializeField] private GameObject _hand;
    [SerializeField] private Gun _gun;

    private bool _isPickUping = false;

    private GameObject _currentObject; 

    protected override void Awake()
    {
        base.Awake();

        _playerInput.Player.PickUp.performed += ctx => PickUp();
        _playerInput.Player.Throw.performed += ctx => Throw();
    }

    private void Update()
    {
        if(_currentObject != null)
            _currentObject.transform.position = _hand.transform.position;
    }

    private void PickUp()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hitInfo, _takeDistance) && hitInfo.collider.gameObject.isStatic == false && _isPickUping == false)
        {
            _currentObject = hitInfo.collider.gameObject;

            _currentObject.transform.position = default;
            _currentObject.transform.SetParent(transform, worldPositionStays: false);
            //_currentObject.transform.localPosition += new Vector3(0, 2, _holdDistance);
            

            _currentObject.GetComponent<Rigidbody>().isKinematic = true;

            _gun.gameObject.SetActive(false);
            _isPickUping = true;
        }
        else if (_isPickUping == true)
        {
            Throw(true);
        }
    }

    private void Throw(bool drop = false)
    {
        if (_isPickUping)
        {
            _currentObject.transform.parent = null;

            var ridigbody = _currentObject.GetComponent<Rigidbody>();
            ridigbody.isKinematic = false;

            if (drop == false)
                ridigbody.AddForce(transform.forward * _forceThrow, ForceMode.Impulse);
            _gun.gameObject.SetActive(true);
            _isPickUping = false;
            _currentObject = null;
        }
    }
}
