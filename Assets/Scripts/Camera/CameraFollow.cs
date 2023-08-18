using System;
using UnityEngine;

namespace Platformer
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] 
        private float yOffset;
        [SerializeField] 
        private float zOffset;
        [SerializeField] 
        private Transform target;
        [SerializeField] 
        private float cameraFollowSpeed;
        
        private void FixedUpdate()
        {
            transform.position = FollowPosition();
        }

        /// <summary>
        /// Camera Follows player based on the Vector Position of the target
        /// We set the Y offset from the target
        /// We use a zOffset so we can control distance of camera from from Target
        /// using slerp to get smooth camera transition on movement when following
        /// </summary>
        /// <returns></returns>
        private Vector3 FollowPosition()
        {
            var newPosition = new Vector3(target.position.x, target.position.y + yOffset,zOffset);
            return Vector3.Slerp(transform.position, newPosition, cameraFollowSpeed * Time.deltaTime);
        }
        
        
    }
}
