using UnityEngine;

public class CheckWall : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Vector2 angle;
    [SerializeField] private float distance = 0.25f;

    public bool IsHit {get; private set;}
    public Vector2 HitNormal {get; private set;}
    public Vector2 HitPoint {get; private set;}
    
    private void Update()
    {
        CheckPoint();
    }
    
    private void CheckPoint()
    {
        Vector2 worldDirection = transform.TransformDirection(angle);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, worldDirection, distance, whatIsGround);

        IsHit =  hit.collider != null;
        if (IsHit)
        {
            HitNormal = hit.normal;
            HitPoint = hit.point;
        }
        
        Debug.DrawRay(transform.position, worldDirection * distance, Color.red);
    }
}
