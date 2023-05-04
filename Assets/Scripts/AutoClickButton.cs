using UnityEngine;
using UnityEngine.EventSystems;

public class AutoClickButton : MonoBehaviour
{
    public GameObject button;

    public void ClickButton()
    {
        // Create a pointer event
        PointerEventData eventData = new PointerEventData(EventSystem.current);

        // Set the position of the pointer to the center of the button
        eventData.position = button.GetComponent<RectTransform>().position;

        // Simulate a pointer click on the button
        ExecuteEvents.Execute(button, eventData, ExecuteEvents.pointerClickHandler);
    }
}
