using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] private GameObject Obj;

    [SerializeField] private StageEyeCatchScript EyeCatch;

    /// <summary>�v���C���[�^���N</summary>
    [SerializeField, LightColor] private TankPlayerScript Player;

    /// <summary>�e�̃v�[�����X�g</summary>
    [SerializeField] private PoolList<BulletScript> BulletList;

    [SerializeField] private Button QuitButton;

    public void Active() => Obj.SetActive(true);

    private void Awake()
    {
        Player.Init(BulletList.GetObject, null);

        QuitButton.onClick.AddListener(() => Obj.SetActive(false));
    }
}
