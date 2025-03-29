using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVWriter : MonoBehaviour
{
    [Tooltip("The number of frames between each write to the file.")]
    public int frameInterval = 100;

    [Tooltip("The suffix to append to the filename.")]
    public string suffix = "";

    private int frameCount = 0;
    private string _filename;
    private Queue<string> rows = new Queue<string>();

    public void SetFilename(string filename)
    {
        if (string.IsNullOrWhiteSpace(filename))
            return;

        string extension = Path.GetExtension(filename);
        string file = Path.GetFileNameWithoutExtension(filename);
        //if (string.Equals(".csv",extension.ToLower()))
        //    _filename = $"{file}_{suffix}.csv";
        //else
        //    _filename = filename + ".csv";

        _filename = $"{file}_{suffix}.csv";

    }
    private void AddRow(string row)
    {
        rows.Enqueue(row);
    }
    public void AddRows(IEnumerable<string> rows)
    {
        foreach (var item in rows)
        {
            //Debug.Log($"Adding row");
            AddRow(item);
        }
    }
    public void Clear() 
        => rows.Clear();
    /// <summary>
    /// Writes all rows to the file
    /// </summary>
    /// <param name="lines"></param>
    private void Write(IEnumerable<string> lines)
    {
#if !UNITY_EDITOR
        var path = System.IO.Path.Combine(Application.persistentDataPath, _filename);
#else
        var path = System.IO.Path.Combine(".", _filename);
#endif
        System.IO.File.AppendAllLines(path, lines);
    }

    /// <summary>
    /// Writes all rows to the file and clears the queue.
    /// </summary>
    public void Flush() {
        var lines = rows.ToArray();
        Clear();
        if (lines.Length > 0)
            Write(lines);
    }

    public void LateUpdate()
    {
        frameCount++;
        if (frameCount == frameInterval)
        {
            Flush();
            frameCount = 0;
        }
    }
    private void OnDestroy() 
        => Flush();
    private void OnApplicationPause(bool pause)
    {
        if (pause) 
            Flush();
    }

    private void OnDisable()
    {
        Flush();
    }
}
