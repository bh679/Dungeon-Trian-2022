using System;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public bool smooth;
        public float smoothTime = 5f;


        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;


        public void Init(Transform character, Transform camera)
        {
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;
        }

	    Vector2 lastTouchPos;
        public void LookRotation(Transform character, Transform camera)
	    {
	    	
		    float yRot = 0;//CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
		    float xRot = 0;
		    
		    if (Input.touchCount > 0) 
		    {
			    Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero
   				Debug.Log(Input.touchCount);
   				
			    if (touch.phase == TouchPhase.Moved) 
			    {
			    	yRot = lastTouchPos.x-touch.position.x * XSensitivity;
			    	xRot = lastTouchPos.y-touch.position.y * YSensitivity;
			    	
				    // get the touch position from the screen touch to world point
				    Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
				    
				    // lerp and set the position of the current object to that of the touch, but smoothly over time.
				    //transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime);
			    }
				    
			    lastTouchPos = touch.position;
		    }
        	
		    //CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;

            m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
            m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

            if(clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

            if(smooth)
            {
                character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = m_CharacterTargetRot;
                camera.localRotation = m_CameraTargetRot;
            }
        }


        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
}
