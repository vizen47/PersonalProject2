using CoreLib;
using UnityEngine;

namespace Players
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Vector2 rangeSize;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private float moveSpeed = 2.5f;
        
        [Header("Gravity")]
        [field: SerializeField] public float extraGravity;
        [field: SerializeField] public float gravityDelay;
        
        private float _timeAir;
        private float _xMoveInput;

        public NotifyValue<bool> IsGrounded = new NotifyValue<bool>();
        public NotifyValue<Vector2> Velocity = new NotifyValue<Vector2>();

        [field: SerializeField] public Rigidbody2D Rb { get; private set; }
        [SerializeField] private FuelSystem fuelSystem;
        public bool CanMove { get; private set; } = true;

        public void SetMovementInput(float input)
        {
            _xMoveInput = CanMove ? input : 0f;
        }

        public void TrueCanMove(bool canMove)
        {
            if (canMove)
                CanMove = true;
        }

        public void FalseCanMove() => CanMove = false;
        
        private void FixedUpdate()
        {
            IsGrounded.Value = CheckGround();
            AddGravityToRb();
            ApplyVelocity();
        }

        private void AddGravityToRb()
        {
            _timeAir = IsGrounded.Value ? 10 : _timeAir += Time.fixedDeltaTime;
        }
        
        private void ApplyVelocity()
        {
            Vector2 tangent = transform.right; // 표면을 따라가는 이동 방향
            Rb.linearVelocity = tangent * (_xMoveInput * moveSpeed) + (Rb.linearVelocity - tangent * Vector2.Dot(Rb.linearVelocity, tangent));
    
            if (_timeAir > gravityDelay)
            {
                Vector2 gravityDir = -transform.up; // 표면 normal의 반대 방향
                AddForceToRb(gravityDir * (extraGravity * Time.fixedDeltaTime));
            }
            Velocity.Value = Rb.linearVelocity;
        }

        private void AddForceToRb(Vector2 force, ForceMode2D forceMode =  ForceMode2D.Force)
        {
            Rb.AddForce(force, forceMode);
        }

        private bool CheckGround()
        {
            Collider2D collider = Physics2D.OverlapBox(transform.position -transform.up * 0.1f, rangeSize,transform.eulerAngles.z, whatIsGround);
            return collider != null;
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
        Gizmos.color = Color.red;

        Vector3 center = transform.position - transform.up * 0.1f;
        Quaternion rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);

        Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);

        Gizmos.DrawWireCube(Vector3.zero, rangeSize);

        Gizmos.matrix = Matrix4x4.identity;
        }
        #endif
    }
}