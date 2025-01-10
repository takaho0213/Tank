# if UNITY_EDITOR

using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DebugScript : MonoBehaviour
{
    private PointerEventData pointData;

    private readonly List<RaycastResult> RayResult = new();

    private void Start()
    {
        pointData = new(EventSystem.current);

        if (EventSystem.current is null) Debug.LogError("EventSystem is null");
    }

    private IEnumerable<RaycastResult> RayAll()
    {
        pointData.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointData, RayResult);

        return RayResult;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var ray = RayAll().FirstOrDefault();

            string text = ray.gameObject != null ? ray.gameObject.name : string.Empty;

            Debug.Log("�ŏ��Ƀq�b�g�����I�u�W�F�N�g => " + text);
        }
        if (Input.GetMouseButtonDown(2)) Debug.Log($"�q�b�g�����S�ẴI�u�W�F�N�g => {string.Join(", ", RayAll().Select((v) => v.gameObject.name))}");
    }
}

# endif