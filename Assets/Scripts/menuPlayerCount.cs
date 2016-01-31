using UnityEngine;
using System.Collections;

public class menuPlayerCount : MonoBehaviour
{
    public MenuBase _menuBase;
    public int level;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int menuState = _menuBase._menuState;
        bool isActive;

        if (level <= menuState)
        {
            isActive = true;

        }
        else {
            isActive = false;
        }

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (!isActive)
        {
            updateColor(ConvertColor(68, 9, 9, 0));
        }
        else if (isActive && level == 1)
        {
            updateColor(ConvertColor(255, 178, 0, 150));
        }
        else if (isActive && level == 2)
        {
            updateColor(ConvertColor(114, 198, 255, 150));
        }
        else if (isActive && level == 3)
        {
            updateColor(ConvertColor(128, 255, 98, 150));
        }
        else if (isActive && level == 4)
        {
            updateColor(ConvertColor(255, 57, 141, 150));
        }
    }

    private void updateColor(Color inColor)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = Color.Lerp(inColor, renderer.color, 0.7f);
    }

    private Color ConvertColor(int r, int g, int b)
    {
        return new Color(r / 255.0f, g / 255.0f, b / 255.0f);
    }

    private Color ConvertColor(int r, int g, int b, int a)
    {
        return new Color(r / 255.0f, g / 255.0f, b / 255.0f, a / 255.0f);
    }
}

