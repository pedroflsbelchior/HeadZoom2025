using Obvious.Soap;
using UnityEngine;
using UnityEngine.UI;

public class SetCurrentImageUserGrade : MonoBehaviour
{
    public IntVariable UserGradeVariable;
    public int UserGrade;
    public Toggle _Toggle;
    public Toggle Toggle
    {
        get
        {
            if (_Toggle == null)
                _Toggle = GetComponent<Toggle>();
            return _Toggle;
        }
    }
    public Image _Image;
    public Image Image
    {
        get
        {
            if (_Image == null)
                _Image = GetComponent<Image>();
            return _Image;
        }
    }
    private void OnEnable()
    {
        Toggle.SetIsOnWithoutNotify(false);
        SetNormalColors();
    }

    public void SetNormalColors()
    {
        Toggle.transition = Selectable.Transition.ColorTint;
        Toggle.targetGraphic = Image;
        Image.color = Toggle.colors.normalColor;
    }

    public void SetSelectedColors()
    {
        Toggle.transition = Selectable.Transition.None;
        Image.color = Toggle.colors.highlightedColor;
    }

    public void Grade(bool hasGraded)
    {
        

        if (Toggle == null)
        {
            Debug.LogError("Toggle not found");
            return;
        }

        if (hasGraded)
        {
            UserGradeVariable.Value = UserGrade;
            SetSelectedColors();
        }
        else
        {
            SetNormalColors();
        }
    }

}
