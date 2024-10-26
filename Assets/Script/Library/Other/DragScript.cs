using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    /// <summary>�J�n��</summary>
    public UnityAction<PointerEventData> OnBeginDragAction { get; set; }

    /// <summary>�h���b�N��</summary>
    public UnityAction<PointerEventData> OnDragAction { get; set; }

    /// <summary>�I����</summary>
    public UnityAction<PointerEventData> OnEndDragAction { get; set; }

    /// <summary>�h���b�O��Ԃ̂��̂��͈͓��ɕ����ꂽ��</summary>
    public UnityAction<PointerEventData> OnDropAction { get; set; }

    public void OnBeginDrag(PointerEventData data) => OnBeginDragAction?.Invoke(data);
    public void OnDrag(PointerEventData data) => OnDragAction?.Invoke(data);
    public void OnEndDrag(PointerEventData data) => OnEndDragAction?.Invoke(data);
    public void OnDrop(PointerEventData data) => OnDropAction?.Invoke(data);
}