using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private SpriteRenderer _weaponSprite;
    [SerializeField] private Transform _weaponRotationPoint;
    [SerializeField] private Transform _weaponHolder; 
    [SerializeField] private Transform _leftHand;     
    [SerializeField] private Transform _rightHand;
    
    private Vector2 movement;

    public void Update()
    {
        movement.x = _joystick.Horizontal;
        movement.y = _joystick.Vertical;
        
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            _weaponRotationPoint.rotation = Quaternion.Slerp(_weaponRotationPoint.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            _weaponSprite.flipY = movement.x < 0;
            
            SyncHandsWithWeapon();
        }
    }
    
    private void SyncHandsWithWeapon()
    {
        Vector2 directionToWeaponLeft = (Vector2)_weaponHolder.position - (Vector2)_leftHand.position;
        float leftAngle = Mathf.Atan2(directionToWeaponLeft.y, directionToWeaponLeft.x) * Mathf.Rad2Deg;
        _leftHand.localEulerAngles = new Vector3(0, 0, leftAngle + 90);
        
        Vector2 directionToWeaponRight = (Vector2)_weaponHolder.position - (Vector2)_rightHand.position;
        float rightAngle = Mathf.Atan2(directionToWeaponRight.y, directionToWeaponRight.x) * Mathf.Rad2Deg;
        _rightHand.localEulerAngles = new Vector3(0, 0, rightAngle + 90);
    }
    
    private void OnDrawGizmosSelected()
    {
        if (_weaponHolder != null && _leftHand != null && _rightHand != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_leftHand.position, _weaponHolder.position);
            Gizmos.DrawLine(_rightHand.position, _weaponHolder.position);
        }
    }
}
