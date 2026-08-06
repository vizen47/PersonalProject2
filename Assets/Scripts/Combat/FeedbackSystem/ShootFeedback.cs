using System;
using UnityEngine;

namespace Combat.FeedbackSystem
{
    public class ShootFeedback : AbstractFeedback
    {
        [SerializeField] private Transform fireTransform;
        [SerializeField] private float fireRecovery = 12f;
        [SerializeField] private float kickDistance = 0.1f;
        [SerializeField]  private float kickAngle = 0.5f;
        private Vector3 _originalLocalPosition;
        private float _originalLocalAngleZ; // 초기 z축회전과 위치를 저장한다.

        private void Awake()
        {
            _originalLocalPosition = fireTransform.localPosition;
            _originalLocalAngleZ = fireTransform.localEulerAngles.z;
        }

        private void Update()
        {
            fireTransform.localPosition = Vector3.Lerp(
                fireTransform.localPosition, _originalLocalPosition, fireRecovery * Time.deltaTime);

            float currentZ = fireTransform.localEulerAngles.z;
            float targetZ = Mathf.LerpAngle(currentZ, _originalLocalAngleZ, fireRecovery * Time.deltaTime);
            fireTransform.localEulerAngles = new Vector3(0, 0, targetZ);
        }

        public override void CreateFeedback()
        {
            float zAngle = fireTransform.eulerAngles.z;
            
            // 여기서 추가 작업해야해.

            fireTransform.localPosition -= new Vector3(kickDistance, 0, 0); // 뒤쪽으로 백
            fireTransform.localEulerAngles += new Vector3(0, 0, kickAngle);
        }

        public override void FinishFeedback()
        {
            
        }
    }
}