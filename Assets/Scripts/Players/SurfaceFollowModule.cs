using UnityEngine;

public class SurfaceFollowModule : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 360f;
    public CheckWall[] WallCheckers {get; private set;}
    
    public void SetWallCheckers(CheckWall[] wallCheckers)
    {
        WallCheckers = wallCheckers;
    }
    
    private Vector2 GetCurrentSurfaceNormal()
    {
        foreach (var wallChecker in WallCheckers)
        {
            if (wallChecker.IsHit)
                return wallChecker.HitNormal;
        }
        return Vector2.up;
    }
        
    private void RotateToSurface(Vector2 targetNormal)
    {
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, targetNormal) * transform.rotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime);
    }
    
    private void FixedUpdate()
    {
        Vector2 surfaceNormal = GetCurrentSurfaceNormal();
        RotateToSurface(surfaceNormal);
    }
}
