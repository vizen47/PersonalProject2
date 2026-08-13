using UnityEngine;

namespace Agents
{
    public class HealthVisual : MonoBehaviour
    {
        [SerializeField] private Transform pivot;
        private HealthModule healthModule;
        private const float PivotTrmY = 0.25f;
        private Vector3 offsetVec;
        
        private void Update()
        {
            offsetVec = transform.root.eulerAngles;
            transform.localRotation = Quaternion.Euler(0, 0, -offsetVec.z);
        }
        
        public void SetVisualHealthBar(float amount)
        {
            amount = Mathf.Clamp(amount, 0, 100);
            amount *= 1.75f * 1 / 100;
            
            pivot.localScale = new Vector3(amount, PivotTrmY, 1);
        }
    }
}