using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

    private int _x;
    private int _y;

    private SpriteRenderer _renderer;

    public void SetPos(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public void SetRenderer(SpriteRenderer renderer)
    {
        _renderer = renderer;
    }

    public void SetColor(Color color)
    {
        _renderer.color = color;
    }
}
