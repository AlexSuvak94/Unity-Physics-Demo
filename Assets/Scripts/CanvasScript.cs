using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Image joystickBG;
    public Image joystickHandle;

    private Vector2 inputVector;
    private float radius;

    private void Start()
    {
        radius = joystickBG.rectTransform.sizeDelta.x / 2;
        joystickBG.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        joystickBG.gameObject.SetActive(true);
        joystickBG.rectTransform.position = eventData.position;
        joystickHandle.rectTransform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - (Vector2)joystickBG.rectTransform.position;
        direction = Vector2.ClampMagnitude(direction, radius);

        joystickHandle.rectTransform.position = joystickBG.rectTransform.position + (Vector3)direction;

        inputVector = direction / radius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickBG.gameObject.SetActive(false);
        inputVector = Vector2.zero;
    }

    public float Horizontal()
    {
        return inputVector.x;
    }

    public float Vertical()
    {
        return inputVector.y;
    }
}