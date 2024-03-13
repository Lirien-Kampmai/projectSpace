using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script responsible for motion control.
    /// </summary>
    public class MovementController : MonoBehaviour
    {
        // list of input method
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }

        // target control
        [SerializeField] private SpaseShip targetShip;
        public void SetTargetShip(SpaseShip ship) => targetShip = ship;
           
        // target Jjoistick
        [SerializeField] private VirtualJoistick mobileJoistick;
        // link to control method
        [SerializeField] private ControlMode controllMode;

        // buttom fire primary and srcondary
        [SerializeField] private PointerClickHold mobileFirePrimary;
        [SerializeField] private PointerClickHold mobileFireSecondary;

        // control method start setting
        private void Start()
        {
            if (controllMode == ControlMode.Keyboard)
            {
                mobileJoistick.gameObject.     SetActive(false);
                mobileFirePrimary.gameObject.  SetActive(false);
                mobileFireSecondary.gameObject.SetActive(false);
            }
            else
            {
                mobileJoistick.gameObject.     SetActive(true);
                mobileFirePrimary.gameObject.  SetActive(true);
                mobileFireSecondary.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            // null check
            if (targetShip == null) return;

            // call controll ship
            if (controllMode == ControlMode.Keyboard) ControllKeyboard();
            if (controllMode == ControlMode.Mobile  ) ControllMobile();
        }

        // joistick
        private void ControllMobile()
        {
            // direction
            var dir = mobileJoistick.Value;

            // set point value 
            targetShip.ThrustControl =  dir.y;
            targetShip.TorqueControl = -dir.x;

            if (mobileFirePrimary.IsHold   == true) targetShip.Fire(TurretMode.Primary);
            if (mobileFireSecondary.IsHold == true) targetShip.Fire(TurretMode.Secondary);
        }

        // keyboard
        private void ControllKeyboard()
        {
            // local variables
            float thrust = 0;
            float torque = 0;

            // set value variables
            if (Input.GetKey(KeyCode.W)) thrust =  1.0f;
            if (Input.GetKey(KeyCode.S)) thrust = -1.0f;
            if (Input.GetKey(KeyCode.A)) torque =  1.0f;
            if (Input.GetKey(KeyCode.D)) torque = -1.0f;

            // set buttom fire
            if (Input.GetKeyUp(KeyCode.Space)) targetShip.Fire(TurretMode.Primary);
            if (Input.GetKeyUp(KeyCode.F    )) targetShip.Fire(TurretMode.Secondary);

            // set value global variables
            targetShip.ThrustControl = thrust;
            targetShip.TorqueControl = torque;
        }
    }
}