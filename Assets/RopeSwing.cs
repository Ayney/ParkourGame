using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class RopeSwing : MonoBehaviour
    {
        private PlayerController PlayerControllerCS => GetComponent<PlayerController>();
        [SerializeField] float swingSpeed = 10f;
        [SerializeField] float afterSwingJumpForce = 150f;
        Rigidbody rb => GetComponent<Rigidbody>();
        DetectTrigger _ropeTrigger;
        public DetectTrigger RopeTrigger => _ropeTrigger ? _ropeTrigger : GameObject.Find("Rope Trigger").GetComponent<DetectTrigger>();
        Rigidbody RopeAttachedRB;
        ConfigurableJoint PlayerCJ => GetComponent<ConfigurableJoint>();

        private void OnEnable()
        {
            StartCoroutine(SubscribeToEventsDelayed(0.1f));
        }
        private void OnDisable()
        {
            PlayerControllerCS.input.Player.HoldRope.performed -= OnHoldRopeCalled;
            PlayerControllerCS.input.Player.HoldRope.canceled -= OnHoldRopeCancelled;
        }
        public void OnHoldRopeCalled(InputAction.CallbackContext value)
        {
            Debug.Log("HOLD ROPE COALD");
            if (PlayerControllerCS.IsHoldingOnRope) DetachFromRope();
            else CheckForRope();
        }
        public void OnHoldRopeCancelled(InputAction.CallbackContext value)
        {

        }

        private void Update()
        {
            if(PlayerControllerCS.IsHoldingOnRope)Swing();
        }
        void CheckForRope()
        {
            if (RopeTrigger.ObjectWithinRange)
            {
                AttachToRope();
            }
            else return;
        }
        void AttachToRope()
        {
            RopeAttachedRB = RopeTrigger.NearestGameObject().GetComponent<Rigidbody>();
            PlayerCJ.connectedBody = RopeAttachedRB;
            PlayerCJ.xMotion = ConfigurableJointMotion.Locked;
            PlayerCJ.yMotion = ConfigurableJointMotion.Locked;
            PlayerCJ.zMotion = ConfigurableJointMotion.Locked;
            PlayerControllerCS.IsHoldingOnRope = true;

        }
        void Swing()
        {
            if (!RopeTrigger.ObjectWithinRange) DetachFromRope();
            RopeAttachedRB.AddRelativeForce(RopeAttachedRB.transform.right * PlayerControllerCS.moveVector.z * swingSpeed);

        }
        void DetachFromRope()
        {
            RopeAttachedRB = null;
            PlayerCJ.connectedBody = null;
            PlayerCJ.xMotion = ConfigurableJointMotion.Free;
            PlayerCJ.yMotion = ConfigurableJointMotion.Free;
            PlayerCJ.zMotion = ConfigurableJointMotion.Free;
            rb.AddForce(transform.up * afterSwingJumpForce, ForceMode.Force);
            PlayerControllerCS.IsHoldingOnRope = false;
        }
        IEnumerator SubscribeToEventsDelayed(float delay)
        {
            yield return new WaitForSeconds(delay);
            PlayerControllerCS.input.Player.HoldRope.performed += OnHoldRopeCalled;
            PlayerControllerCS.input.Player.HoldRope.canceled += OnHoldRopeCancelled;
        }
    }
}

