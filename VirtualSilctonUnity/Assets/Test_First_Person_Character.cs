using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.VR;



namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class Test_First_Person_Character : MonoBehaviour
    {

        private Camera m_Camera;


        // protected OmniMovementComponent MovementComponent;
        private CharacterController m_CharacterController;



        // Use this for initialization
        private void Start()
        {
            // MovementComponent = GetComponent<OmniMovementComponent>();
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;



            //I am testing out new script
            // m_Camera.transform.Rotate(90f,0f,0f,Space.World);
        }


        // Update is called once per frame
        private void Update()
        {



            //Trying something new:
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Vector3 position = m_Camera.transform.localPosition;
                position.x--;
                m_Camera.transform.localPosition = position;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Vector3 position = m_Camera.transform.localPosition;
                position.x++;
                m_Camera.transform.localPosition = position;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Vector3 position = m_Camera.transform.localPosition;
                position.z++;
                m_Camera.transform.localPosition = position;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Vector3 position = m_Camera.transform.localPosition;
                position.z--;
                m_Camera.transform.localPosition = position;
            }
        }
//
//         // void UseOmniInputToMovePlayer()
//         // {
//         //     if (MovementComponent.omniFound)
//         //         MovementComponent.GetOmniInputForCharacterMovement();
//         //    else if (MovementComponent.developerMode)
//         //         MovementComponent.DeveloperModeUpdate();
//         //
//         //
//         //     if (MovementComponent.GetForwardMovement() != Vector3.zero)
//         //         m_CharacterController.Move(MovementComponent.GetForwardMovement());
//         //     if (MovementComponent.GetStrafeMovement() != Vector3.zero)
//         //         m_CharacterController.Move(MovementComponent.GetStrafeMovement());
//         // }
//
//
//         private void PlayLandingSound()
//         {
//             m_AudioSource.clip = m_LandSound;
//             m_AudioSource.Play();
//             m_NextStep = m_StepCycle + .5f;
//         }
//
//
//         private void FixedUpdate()
//         {
//             float speed;
//             GetInput(out speed);
//             // always move along the camera forward as it is the direction that it being aimed at
//             Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;
//
//             // get a normal for the surface that is being touched to move along it
//             RaycastHit hitInfo;
//             Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
//                                m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
//             desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;
//
//             m_MoveDir.x = desiredMove.x*speed;
//             m_MoveDir.z = desiredMove.z*speed;
//
//
//             if (m_CharacterController.isGrounded)
//             {
//                 m_MoveDir.y = -m_StickToGroundForce;
//
//                 if (m_Jump)
//                 {
//                     m_MoveDir.y = m_JumpSpeed;
//                     PlayJumpSound();
//                     m_Jump = false;
//                     m_Jumping = true;
//                 }
//             }
//             else
//             {
//                 m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
//             }
//             m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);
//
//             ProgressStepCycle(speed);
//             UpdateCameraPosition(speed);
//
//             m_MouseLook.UpdateCursorLock();
//         }
//
//
//         private void PlayJumpSound()
//         {
//             m_AudioSource.clip = m_JumpSound;
//             m_AudioSource.Play();
//         }
//
//
//         private void ProgressStepCycle(float speed)
//         {
//             if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
//             {
//                 m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
//                              Time.fixedDeltaTime;
//             }
//
//             if (!(m_StepCycle > m_NextStep))
//             {
//                 return;
//             }
//
//             m_NextStep = m_StepCycle + m_StepInterval;
//
//             PlayFootStepAudio();
//         }
//
//
//         private void PlayFootStepAudio()
//         {
//             if (!m_CharacterController.isGrounded)
//             {
//                 return;
//             }
//             // pick & play a random footstep sound from the array,
//             // excluding sound at index 0
//             int n = Random.Range(1, m_FootstepSounds.Length);
//             m_AudioSource.clip = m_FootstepSounds[n];
//             m_AudioSource.PlayOneShot(m_AudioSource.clip);
//             // move picked sound to index 0 so it's not picked next time
//             m_FootstepSounds[n] = m_FootstepSounds[0];
//             m_FootstepSounds[0] = m_AudioSource.clip;
//         }
//
//
//         private void UpdateCameraPosition(float speed)
//         {
//             Vector3 newCameraPosition;
//             if (!m_UseHeadBob)
//             {
//                 return;
//             }
//             if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
//             {
//                 m_Camera.transform.localPosition =
//                     m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
//                                       (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
//                 newCameraPosition = m_Camera.transform.localPosition;
//                 newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
//             }
//             else
//             {
//                 newCameraPosition = m_Camera.transform.localPosition;
//                 newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
//             }
//             m_Camera.transform.localPosition = newCameraPosition;
//         }
//
//
//         private void GetInput(out float speed)
//         {
//             // Read input
//             float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
//             float vertical = CrossPlatformInputManager.GetAxis("Vertical");
//
//             bool waswalking = m_IsWalking;
//
// #if !MOBILE_INPUT
//             // On standalone builds, walk/run speed is modified by a key press.
//             // keep track of whether or not the character is walking or running
//             m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
// #endif
//             // set the desired speed to be walking or running
//             speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
//             m_Input = new Vector2(horizontal, vertical);
//
//             // normalize input if it exceeds 1 in combined length:
//             if (m_Input.sqrMagnitude > 1)
//             {
//                 m_Input.Normalize();
//             }
//
//             // handle speed change to give an fov kick
//             // only if the player is going to a run, is running and the fovkick is to be used
//             if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
//             {
//                 StopAllCoroutines();
//                 StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
//             }
//         }
//
//
//         private void RotateView()
//         {
//             m_MouseLook.LookRotation (transform, m_Camera.transform);
//         }
//
//
//         private void OnControllerColliderHit(ControllerColliderHit hit)
//         {
//             Rigidbody body = hit.collider.attachedRigidbody;
//             //dont move the rigidbody if the character is on top of it
//             if (m_CollisionFlags == CollisionFlags.Below)
//             {
//                 return;
//             }
//
//             if (body == null || body.isKinematic)
//             {
//                 return;
//             }
//             body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
//         }
    }
}
