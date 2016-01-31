using UnityEngine;
using System.Collections;

public class MenuColorer : MonoBehaviour {
    public MenuBase _menuBase;
    public bool noLight;
    public bool fire;
    public bool credits;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        int _menuState = _menuBase._menuState;

        if (credits)
        {
            if (_menuState == -1)
            {
                updateColor(ConvertColor(220, 230, 230));
            }
            else {
                updateColor(ConvertColor(255, 255, 255));
            }
        }

        else if (_menuState <= 0 && fire)
        {
            updateColor(ConvertColor(223, 54, 26));
        }
        else if (_menuState <= 0 && !noLight)
        {
            updateColor(ConvertColor(68, 9, 9));
        } else if (_menuState <= 0 && noLight)
        {
            updateColor(ConvertColor(68, 9, 9, 0));
        }
        else if (_menuState == 1)
        {
            updateColor(ConvertColor(182, 127, 0));
        }
        else if (_menuState == 2)
        {
            updateColor(ConvertColor(63, 166, 235));
        }
        else if (_menuState == 3)
        {
            updateColor(ConvertColor(96, 248, 60));
        }
        else if (_menuState == 4 && fire)
        {
            updateColor(ConvertColor(255, 14, 14));
        }
        else if (_menuState == 4)
        {
            updateColor(ConvertColor(255, 57, 141));
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
