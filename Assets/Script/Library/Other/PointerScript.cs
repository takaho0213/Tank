using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    /// <summary>�J�n��</summary>
    public UnityAction<PointerEventData> OnPointerEnterAction { get; set; }

    /// <summary>�I����</summary>
    public UnityAction<PointerEventData> OnPointerExitAction { get; set; }

    /// <summary>��������</summary>
    public UnityAction<PointerEventData> OnPointerDownAction { get; set; }

    /// <summary>��������</summary>
    public UnityAction<PointerEventData> OnPointerUpAction { get; set; }

    /// <summary>�͈͓��ŗ�������</summary>
    public UnityAction<PointerEventData> OnPointerClickAction { get; set; }

    public void OnPointerEnter(PointerEventData data) => OnPointerEnterAction?.Invoke(data);
    public void OnPointerExit(PointerEventData data) => OnPointerExitAction?.Invoke(data);
    public void OnPointerDown(PointerEventData data) => OnPointerDownAction?.Invoke(data);
    public void OnPointerUp(PointerEventData data) => OnPointerUpAction?.Invoke(data);
    public void OnPointerClick(PointerEventData data) => OnPointerClickAction?.Invoke(data);
}
