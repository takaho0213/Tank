using UnityEngine;
using System.Collections.Generic;
using static TankAutoMoveScript;
using static TankEnemyScript;
using Unity.VisualScripting;

public class TutorialGUIScript : MonoBehaviour//GUIWindowScript
{
    [SerializeField] GUIWindowScript guiWindow;

    [SerializeField] private Font font;

    [SerializeField] private int labelFontSize;
    [SerializeField] private int fieldFontSize;

    [SerializeField] private float labelSpace;
    [SerializeField] private float fieldSpace;

    [SerializeField] private GUISelectField<int> health;
    [SerializeField] private GUISelectField<MoveType> moveType;
    [SerializeField] private GUISelectField<float> moveSpeed;
    [SerializeField] private GUISelectField<bool> isMoveVectorNormalized;
    [SerializeField] private GUISelectField<float> turretLerp;
    [SerializeField] private GUISelectField<AttackType> attackType;
    [SerializeField] private GUISelectField<float> shootInterval;
    [SerializeField] private GUISelectField<float> bulletSpeed;
    [SerializeField] private GUISelectField<int> bulletReflectionCount;
    [SerializeField] private GUISelectField<Color> fillColor;

    [SerializeField] private GUISliderField slider;
    [SerializeField] private GUIIntSliderField intSlider;

    private List<BaseGUIField> fields;

    private Vector2 scroll;

    private GUIStyle labelStyle;
    private GUIStyle buttonStyle;
    private GUIStyle sliderStyle;

    private void Start()
    {
        fields = new List<BaseGUIField>
        {
            health,
            moveType,
            moveSpeed,
            isMoveVectorNormalized,
            turretLerp,
            attackType,
            shootInterval,
            bulletSpeed,
            bulletReflectionCount,
            fillColor,
            slider,
            intSlider,
        };

        foreach (var field in fields)
        {
            field.Init();
        }

        guiWindow.Window += Window;
    }

    private void Window(int id)
    {
        if (labelStyle == null)
        {
            labelStyle = new(GUI.skin.label)
            {
                font = font,
                fontSize = labelFontSize,
            };

            buttonStyle = new(GUI.skin.button)
            {
                font = font,
                fontSize = fieldFontSize,
            };

            sliderStyle = new(GUI.skin.horizontalSlider)
            {
                font = font,
                fontSize = fieldFontSize,
            };

            foreach (var f in fields)
            {
                var fieldStyle = f switch
                {
                    _ => buttonStyle,
                };

                f.SetStyle(labelStyle, fieldStyle, labelSpace);
            }
        }

        scroll = GUILayout.BeginScrollView(scroll);

        foreach (var field in fields)
        {
            field.Field();

            GUILayout.Space(fieldSpace);
        }

        GUILayout.EndScrollView();
    }
}
