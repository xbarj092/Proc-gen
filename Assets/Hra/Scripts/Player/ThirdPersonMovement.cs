using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Main references")]
    [SerializeField] private PlayerAnimationController _animationController;

    // TODO - camera stabilizer
    // currently the camera is set to character root bone, but that bone is rotating like 1-2 degrees when animation is playing
    // we want to have static camera rotation that is not being changed by animation
    [SerializeField] private Transform _playerCamera;

    [Header("Values")]
    [SerializeField] private float _turnSmoothTime;
    [SerializeField] private float _triggerCooldownTime;

    private Vector3 _direction;

    private float _turnSmoothVelocity;
    private float _lastTriggerTime;

    private const float DIRECTION_MAGNITUDE_MOVEMENT_THRESHOLD = 0.1f;

    private const string AXIS_HORIZONTAL = "Horizontal";
    private const string AXIS_VERTICAL = "Vertical";

    private void Update()
    {
        float horizontal = Input.GetAxisRaw(AXIS_HORIZONTAL);
        float vertical = Input.GetAxisRaw(AXIS_VERTICAL);

        if (_animationController.CanMove)
        {
            _direction = new Vector3(horizontal, 0, vertical).normalized;
        }

        if (Input.anyKey && _direction.magnitude >= DIRECTION_MAGNITUDE_MOVEMENT_THRESHOLD)
        {
            DetermineMovementAction();

            if (_animationController.CanMove)
            {
                RotatePlayer(_direction);
            }
        }
        else
        {
            _animationController.PlayAnimation(PlayerAnimation.Idle);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animationController.PlayAnimation(PlayerAnimation.Roll);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            _animationController.PlayAnimation(PlayerAnimation.Jump);
        }
    }

    private void DetermineMovementAction()
    {
        if (Time.time - _lastTriggerTime > _triggerCooldownTime)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _animationController.PlayAnimation(PlayerAnimation.FastRun);
            }
            else if (Input.GetKey(KeyCode.LeftAlt))
            {
                _animationController.PlayAnimation(PlayerAnimation.Walk);
            }
            else
            {
                _animationController.PlayAnimation(PlayerAnimation.Run);
            }

            _lastTriggerTime = Time.time;
        }
    }

    private void RotatePlayer(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _playerCamera.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
