using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScriptGUI : MonoBehaviour
{
    [Tooltip("Longueur des boutons")]
    [SerializeField]
    private float m_ButtonLenght = 100f;
    [SerializeField]
    private Texture2D[] m_MyTexture;
    [SerializeField]
    private Texture2D m_BoxBackground;
    [Tooltip("Immage pour le bouton arrière")]
    [SerializeField]
    private Texture2D m_LeftArrow;
    [Tooltip("Immage pour le bouton avant")]
    [SerializeField]
    private Texture2D m_RightArrow;
    [SerializeField]
    private string[] m_MyStrings;


    private bool m_CaracterVisible = true;
    private int m_SelectionIndex;
    private int m_ScreenSelect = 0;
    private float m_HorisontalSliderValue = 0f;
    private int m_Scale = 2;
    private int m_Background = 0;
    private Vector2 m_ScrollPosition;
    private Rect m_BoxRect;


    private void OnGUI()
    {


        m_BoxRect = new Rect(m_ButtonLenght, 0f, Screen.width - m_ButtonLenght*2, Screen.height / 4);
        Rect GroupRect = new Rect(0f, 0f, Screen.width, Screen.height / 4);
        Rect scrollRect = GetCenterRect(m_BoxRect, m_BoxRect.width * 0.5f);
        scrollRect.width += 50f;
        Rect scrollViewRect = scrollRect;
        scrollViewRect.height += 100;

        GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
        if (m_MyTexture[m_Background] != null)
        {
            boxStyle.normal.background = m_MyTexture[m_Background];
        }
        GUI.BeginGroup(GroupRect);
        Rect leftButtonRect = new Rect(0f, 0f, m_ButtonLenght, Screen.height / 4);
        PreviousScreen(GUI.Button(leftButtonRect, m_LeftArrow));

        Rect RightButtonRect = new Rect(Screen.width - m_ButtonLenght, 0f, m_ButtonLenght, Screen.height / 4);
        NextScreen(GUI.Button(RightButtonRect, m_RightArrow));

        if (m_ScreenSelect == 0)
        {
           
            GUI.Box(m_BoxRect, "Cheats", boxStyle);
            m_BoxRect.height = 20f;

            SetNewLine();

            Rect sliderRect = GetCenterRect(m_BoxRect, m_BoxRect.width * 0.5f);


            m_HorisontalSliderValue = GUI.HorizontalSlider(sliderRect, m_HorisontalSliderValue, -1f, 1f);
            transform.position = new Vector3(m_HorisontalSliderValue * 3f, 0, 0);

            SetNewLine(30f);

            Rect ToggleRect = GetCenterRect(m_BoxRect, m_BoxRect.width * 0.5f);
            ToggleVisibility(GUI.Toggle(ToggleRect, m_CaracterVisible, " ShowSelf?"));

            SetNewLine(30f);
            Rect ToolbarRect = GetCenterRect(m_BoxRect, m_BoxRect.width * 0.5f);
            m_Scale = GUI.Toolbar(ToolbarRect, m_Scale, m_MyStrings);
            switch (m_Scale)
            {
                case 0:
                    transform.localScale = Vector3.zero;
                    break;
                case 1:
                    transform.localScale = Vector3.one * 0.5f;
                    break;
                case 2:
                    transform.localScale = Vector3.one;
                    break;
                case 3:
                    transform.localScale = Vector3.one * 1.5f;
                    break;
                case 4:
                    transform.localScale = Vector3.one * 2f;
                    break;
                default:
                    break;
            }
            
        }
        else if (m_ScreenSelect == 1)
        {
            GUI.Box(m_BoxRect, "Stats", boxStyle);
            m_BoxRect.height = 20f;

            SetNewLine();

            GUIStyle labelStyle = new GUIStyle(GUI.skin.GetStyle("Label"));
            GUIContent myContent = new GUIContent("Local Position : " + transform.position.ToString(), "Local Position");
            Vector2 labelSize = labelStyle.CalcSize(myContent);

            Rect LabelRectPos = GetCenterRect(m_BoxRect, labelSize.x);
            GUI.Box(LabelRectPos, m_BoxBackground);
            GUI.Label(LabelRectPos, myContent, labelStyle);

            SetNewLine();

            myContent = new GUIContent("Local Rotation : " + transform.rotation.ToString(), "Local Rotation");
            labelSize = labelStyle.CalcSize(myContent);
            Rect LabelRectRot = GetCenterRect(m_BoxRect, labelSize.x);
            GUI.Box(LabelRectRot,m_BoxBackground);
            GUI.Label(LabelRectRot, myContent, labelStyle);

            SetNewLine();
            myContent = new GUIContent("Local Scale : " + transform.localScale.ToString(), "Local Scale");
            labelSize = labelStyle.CalcSize(myContent);
            Rect LabelRectScale = GetCenterRect(m_BoxRect, labelSize.x);
            GUI.Box(LabelRectScale, m_BoxBackground);
            GUI.Label(LabelRectScale, myContent, labelStyle);


        }
        else if (m_ScreenSelect == 2)
        {
            
            GUI.Box(m_BoxRect, "GUI", boxStyle);
            scrollRect.y += 50f;
            scrollRect.width += 50f;
            m_ScrollPosition = GUI.BeginScrollView(scrollRect, m_ScrollPosition, scrollViewRect);
            m_BoxRect.height = 20f;
            SetNewLine();
            m_BoxRect.height = 300f;
            //m_BoxRect.width -= m_BoxRect.width/3;
            Rect selectionGrid = GetCenterRect(m_BoxRect, m_BoxRect.width * 0.5f);
            m_Background = GUI.SelectionGrid(selectionGrid, m_Background, m_MyTexture,1);
            GUI.EndScrollView();
        }
        GUI.EndGroup();
    }

    private Rect GetCenterRect(Rect i_GroupRect, float i_Width = -1f)
    {
        Rect rect = i_GroupRect;

        //Operateur Ternaire
        //              Condition ? If True           : If False
        rect.width = i_Width < 0f ? i_GroupRect.width : i_Width;


        rect.x += i_GroupRect.width * 0.5f - rect.width * 0.5f;
        return rect;
    }

    private void SetNewLine(float i_Height = 20f, float i_Spacing = 5f)
    {
        m_BoxRect.y += m_BoxRect.height + i_Spacing;
        m_BoxRect.height = i_Height;

    }

    private void ToggleVisibility(bool i_Toggle)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = i_Toggle;
        m_CaracterVisible = i_Toggle;
    }

    private void NextScreen(bool i_Click)
    {
        if (i_Click)
        {
            if (m_ScreenSelect < 2)
            {
                m_ScreenSelect++;
            }
            else
            {
                m_ScreenSelect = 0;
            }
        }
        
    }

    private void PreviousScreen(bool i_Click)
    {
        if (i_Click)
        {
            if (m_ScreenSelect > 0)
            {
                m_ScreenSelect--;
            }
            else
            {
                m_ScreenSelect = 2;
            }
        }    
    }
}
