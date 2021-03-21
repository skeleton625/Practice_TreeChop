using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 0f;
    [SerializeField] private float BoostSpeed = 0f;
    [SerializeField] private float TurnSpeedX = 0f;
    [SerializeField] private float TurnSpeedY = 0f;
    [SerializeField] private CinemachineImpulseSource treeShake;
    private Transform CameraTransform = null;
    private Rigidbody PlayerRigidbody = null;

    private float preMoveSpeed = 0f;
    private Vector3 rotateAngle = Vector3.zero;
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 turnDirection = Vector2.zero;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        CameraTransform = Camera.main.transform;
        PlayerRigidbody = GetComponent<Rigidbody>();

        preMoveSpeed = MoveSpeed;
        rotateAngle = CameraTransform.rotation.eulerAngles;
    }

    private void Update()
    {
        TurnPlayer();
        MovePlayer();
    }

    private void TurnPlayer()
    {
        rotateAngle.x = Mathf.Clamp(rotateAngle.x - turnDirection.y, -30, 30);
        rotateAngle.y = Mathf.Repeat(rotateAngle.y + turnDirection.x, 360);
        transform.rotation = Quaternion.Euler(0, rotateAngle.y * TurnSpeedY, 0);
        CameraTransform.localRotation = Quaternion.Euler(rotateAngle.x * TurnSpeedX, 0, 0);
    }

    private void MovePlayer()
    {
        Vector3 velocity = CameraTransform.right * moveDirection.x;
        velocity += CameraTransform.forward * moveDirection.y;
        velocity.y = 0;
        PlayerRigidbody.velocity = velocity * preMoveSpeed * Time.deltaTime;
    }

    public void UpdateMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    public void UpdateTurnDirection(Vector2 direction)
    {
        turnDirection = direction * Time.deltaTime;
    }

    public void UpdateBoostState(bool isBoost)
    {
        if (isBoost)
            preMoveSpeed = BoostSpeed;
        else
            preMoveSpeed = MoveSpeed;
    }
}
