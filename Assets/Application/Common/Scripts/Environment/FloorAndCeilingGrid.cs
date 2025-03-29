using UnityEngine;
//using Shapes;
using System.Collections.Generic;
public class FloorAndCeilingGrid : MonoBehaviour
{
    [Range(1, 100)]
    [Tooltip("Size of the grid")]
    public int Size = 30;
    [Range (0, 0.5f)]
    [Tooltip("Thickness of the grid")]
    public float Thickness = 0.02f;
    [Tooltip("Color of the grid")]
    public Color Color = new Color(0.1f, 0.1f, 0.1f, 1f);

    private int _size = 30;
    private float _thickness = 0.02f;
    private Color _color = new Color(0.1f, 0.1f, 0.1f, 1f);

    //private List<Line> lines = new List<Line>();

    // Creates a line between two points
    private GameObject CreateLine(Vector3 start, Vector3 end, Transform parent, Color color, float Thickness)
    {
        GameObject obj = new GameObject();
        //var line = obj.AddComponent<Line>();
        //line.Start = start;
        //line.End = end;
        //line.Color = new Color(0.1f, 0.1f, 0.1f, 1f);
        //line.Thickness = 0.02f;
        //line.BlendMode = ShapesBlendMode.Opaque;
        //line.RenderQueue = 1999;
        
        //lines.Add(line);

        obj.transform.SetParent(parent);
        obj.transform.localPosition = Vector3.zero;
        
        return obj;
    }

    // Creates a square grid of 2 x size + 1
    public void CreateGrid(int size, Color color, float thickness)
    {
        //lines.Clear();

        for (int i = 0; i < size * 2 + 1; i++)
        {
            CreateLine(new Vector3(i - size, 0, -size), new Vector3(i - size, 0, size), transform, color, thickness);
            CreateLine(new Vector3(-size, 0, i - size), new Vector3(size, 0, i - size), transform, color, thickness);
        }
    }

    public void Start()
    {
        _size = Size;
        _thickness = Thickness;
        _color = Color;

        CreateGrid(Size, Color, Thickness);
    }

    private void UpdateLines()
    {
        //foreach (var line in lines)
        //{
        //    line.Color = Color;
        //    line.Thickness = Thickness;
        //}
    }

    private void Update()
    {
        if (_size != Size)
        {
            _size = Size;
            CreateGrid(Size, Color, Thickness);
            return;
        }

        if (_thickness != Thickness || _color != Color)
        {
            UpdateLines();
        }
    }

}
