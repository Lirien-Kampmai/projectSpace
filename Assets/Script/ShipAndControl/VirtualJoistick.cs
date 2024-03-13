using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpaceShooter
{
    /// <summary>
    /// Script responsible for implementing a virtual joistick on the screen.
    /// </summary>
    public class VirtualJoistick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        // image background and stick
        [SerializeField] private Image joistickBackground;
        [SerializeField] private Image stick;

        // specific stick value
        public Vector3 Value { get; private set; }

        // calculation for dragging stick in the JoistickBackground area
        public void OnDrag(PointerEventData eventData)
        {
            // vector set zero position
            Vector2 position = Vector2.zero;

            // global stick coordinates converted to Joistick Background coordinates 
            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (joistickBackground.rectTransform, eventData.position, eventData.pressEventCamera, out position);

            #region Normalization vector

            // we bring the coordinates to a value from -1 to 1.
            // divide the coordinates of the stick point by the width(x) and length(y)
            position.x = (position.x / joistickBackground.rectTransform.sizeDelta.x);
            position.y = (position.y / joistickBackground.rectTransform.sizeDelta.y);

            // we shift the coordinates to the center, making a positive and negative step
            position.x = position.x * 2 - 1;
            position.y = position.y * 2 - 1;

            // setting our position
            Value = new Vector3(position.x, position.y, 0);

            // normalization vector
            if (Value.magnitude > 1) Value = Value.normalized;

            #endregion

            // setting stick movement limits inside joistick limits
            float offsetX = joistickBackground.rectTransform.sizeDelta.x / 2 - stick.rectTransform.sizeDelta.x / 2;
            float offsetY = joistickBackground.rectTransform.sizeDelta.y / 2 - stick.rectTransform.sizeDelta.y / 2;

            // normalized vector * scalar = correct movement
            stick.rectTransform.anchoredPosition = new Vector2(Value.x * offsetX, Value.y * offsetY);
        }

        // stick movement on click
        public void OnPointerDown(PointerEventData eventData) { OnDrag(eventData); }

        // default stick position after click
        public void OnPointerUp(PointerEventData eventData)
        {
            Value = Vector3.zero;
            stick.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}