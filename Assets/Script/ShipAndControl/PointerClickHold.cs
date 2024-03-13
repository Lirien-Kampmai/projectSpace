using UnityEngine;
using UnityEngine.EventSystems;

namespace SpaceShooter
{
    /// <summary>
    /// Script checks if the button is pressed
    /// </summary>
    public class PointerClickHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool hold;
        public bool IsHold => hold;

        public void OnPointerDown(PointerEventData eventData) { hold = true; }
        public void OnPointerUp  (PointerEventData eventData) { hold = false; }
    }
}