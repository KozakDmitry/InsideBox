using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        public float rotationAngleX;
        public int distance;
        public float offsetY;

        [SerializeField]
        private Transform following;

        

        private void LateUpdate()
        {
            if (following == null) return;

            Quaternion rotation = Quaternion.Euler(rotationAngleX, 0, 0);

            Vector3 position = rotation * new Vector3(0, 0, -distance) + FollowingPointPosition();

            transform.position = position; 
            transform.rotation = rotation;
        }

        public void Follow(GameObject following) => this.following = following.transform;

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = following.position;
            followingPosition.y += offsetY;
            return followingPosition;
        }
    }
}