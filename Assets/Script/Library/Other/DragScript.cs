using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    /// <summary>開始時</summary>
    public UnityAction<PointerEventData> OnBeginDragAction { get; set; }

    /// <summary>ドラック中</summary>
    public UnityAction<PointerEventData> OnDragAction { get; set; }

    /// <summary>終了時</summary>
    public UnityAction<PointerEventData> OnEndDragAction { get; set; }

    /// <summary>ドラッグ状態のものが範囲内に放された時</summary>
    public UnityAction<PointerEventData> OnDropAction { get; set; }

    public void OnBeginDrag(PointerEventData data) => OnBeginDragAction?.Invoke(data);
    public void OnDrag(PointerEventData data) => OnDragAction?.Invoke(data);
    public void OnEndDrag(PointerEventData data) => OnEndDragAction?.Invoke(data);
    public void OnDrop(PointerEventData data) => OnDropAction?.Invoke(data);
}