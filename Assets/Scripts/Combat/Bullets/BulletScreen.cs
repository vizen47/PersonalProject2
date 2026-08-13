using UnityEngine;

namespace Combat.Bullets
{
    public class BulletScreen : MonoBehaviour
    {
        [SerializeField] private float offscreenMargin = 0.1f;
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera =  Camera.main;
        }

        public bool IsOffscreen()
        {
            Vector3 viewportPos = _mainCamera.WorldToViewportPoint(transform.position);
            return viewportPos.x < -offscreenMargin || viewportPos.x > 1 + offscreenMargin ||
                   viewportPos.y < -offscreenMargin || viewportPos.y > 1 + offscreenMargin;
        }
    }
}