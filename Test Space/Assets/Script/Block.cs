using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [HideInInspector] public TapPosition onTap;
    private Color color;
    private Image image;
    private int col;
    private int row;

    private void Awake()
    {
        onTap = new TapPosition();
        image = GetComponent<Image>();
        color = Random.Range(0, 100) > 50 ? Color.Blue : Color.Red;
    }

    private void Start()
    {
        image.color = color == Color.Blue ? UnityEngine.Color.blue : UnityEngine.Color.red;
    }

    public void SetPosition(int col, int row)
    {
        this.col = col;
        this.row = row;
    }

    public void Clicked()
    {
        onTap.Invoke(col, row);
    }

    public enum Color
    {
        Red,
        Blue
    }

    public class TapPosition : UnityEvent<int, int> { };
}
