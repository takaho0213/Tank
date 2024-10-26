using UnityEngine;

[System.Serializable]
public class ActiveObject
{
    [SerializeField] private GameObject ActiveObj;
    [SerializeField] private GameObject InActiveObj;

    public bool IsActive
    {
        set
        {
            ActiveObj?.SetActive(value);
            InActiveObj?.SetActive(!value);
        }
    }
}

[System.Serializable]
public class ActiveObjectArray
{
    [SerializeField] private GameObject[] ActiveObjs;
    [SerializeField] private GameObject[] InActiveObjs;

    public bool IsActive
    {
        set
        {
            foreach (var obj in ActiveObjs) obj.SetActive(value);
            foreach (var obj in InActiveObjs) obj.SetActive(!value);
        }
    }
}
