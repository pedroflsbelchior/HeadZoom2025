using Obvious.Soap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public interface CSVData
{
    public string GetColumnNames(string name);
    public string GetColumnValues(string name);
}

[System.Serializable]
public class CSVSoapColumn
{
    [SerializeField]
    private string columnName = "";
    [Tooltip("If true, only the last value will be stored. If false, all values will be stored.")]
    public bool singleValue = false;
    [Tooltip("If true, the last value will be kept when the variable is released. If false, the last value will be lost.")]
    public bool keepLastValue = false;
    [Tooltip("Should changes to the value of this column trigger a write to the CSV.")]
    public bool TriggerCSVUpdate = false;
    public List<string> IgnoreUpdateValues = new List<string>();
    public ScriptableVariableBase variable = null;

    [HideInInspector]
    public UnityEvent onValueChanged;

    private bool isNull => variable == null;
    private string _separator = ",";
    private Stack<string> values = new Stack<string>();

    private PropertyInfo valueProperty;
    private EventInfo onValueChangedEvent;
    private Type cachedType;
    private EventHandler handler = null;
    private Delegate cachedDelegate = null;
    private static CultureInfo culture = CultureInfo.InvariantCulture;

    public bool isEmpty() => values.Count == 0;

    public string GetValue()
    {
        if (keepLastValue && !isEmpty())
            return values.Peek();
        if (values.TryPop(out string result))
            return result;
        return "";
    }


    public string GetPeek()
    {
        if (values.TryPeek(out string result))
            return result;
        return "";
    }

    public string GetHeader(string separator = ",")
    {
        if (isNull) return "";

        object obj = valueProperty.GetValue(variable);
        if (obj is Vector3 v3)
            return $"{columnName}_x{separator}{columnName}_y{separator}{columnName}_z";
        if (obj is Vector2 v2)
            return $"{columnName}_x{separator}{columnName}_y";
        if (obj is Vector2Int v2int)
            return $"{columnName}_x{separator}{columnName}_y";
        if (obj is Quaternion q)
            return $"{columnName}_x{separator}{columnName}_y{separator}{columnName}_z{separator}{columnName}_w";
        return columnName;
    }

    private static string JoinFloats(string separator = ",", CultureInfo culture = null, params float[] values)
        => (values == null || values.Length == 0) ? string.Empty :
         string.Join(separator, values.Select(value => value.ToString(culture ??= CultureInfo.InvariantCulture)));


    private static string JoinDoubles(string separator = ",", CultureInfo culture = null, params double[] values)
        => (values == null || values.Length == 0) ? string.Empty :
         string.Join(separator, values.Select(value => value.ToString(culture ??= CultureInfo.InvariantCulture)));

    private static string JoinDecimals(string separator = ",", CultureInfo culture = null, params decimal[] values)
        => (values == null || values.Length == 0) ? string.Empty :
         string.Join(separator, values.Select(value => value.ToString(culture ??= CultureInfo.InvariantCulture)));


    private string ValueToString(string separator = ",")
    {
        if (isNull) return "";

        object obj = valueProperty.CanRead ? valueProperty.GetValue(variable) : null;
        if (obj is Vector3 v3)
            return JoinFloats(separator, culture, v3.x, v3.y, v3.z);
        if (obj is Vector2 v2)
            return JoinFloats(separator, culture, v2.x, v2.y);
        if (obj is Vector2Int v2int)
            return $"{v2int.x}{separator}{v2int.y}";
        if (obj is Quaternion q)
            return JoinFloats(separator, culture, q.x, q.y, q.z, q.w);
        if (obj is Matrix4x4 m4x4)
            return JoinFloats(separator, culture, m4x4.m00, m4x4.m01, m4x4.m02, m4x4.m03, m4x4.m10, m4x4.m11, m4x4.m12, m4x4.m13, m4x4.m20, m4x4.m21, m4x4.m22, m4x4.m23, m4x4.m30, m4x4.m31, m4x4.m32, m4x4.m33);
        if (obj is float f)
            return $"{f.ToString(culture)}";
        if (obj is double d)
            return $"{d.ToString(culture)}";
        if (obj is decimal dec)
            return $"{dec.ToString(culture)}";

        #region Unity.Mathematics
        //if (obj is double2 d2)
        //    return JoinDoubles(separator, culture, d2.x, d2.y);
        //if (obj is double3 d3)
        //    return JoinDoubles(separator, culture, d3.x, d3.y, d3.z);
        //if (obj is double4 d4)
        //    return JoinDoubles(separator, culture, d4.x, d4.y, d4.z, d4.w);
        //if (obj is double2x2 d2x2)
        //    return JoinDoubles(separator, culture, d2x2.c0.x, d2x2.c0.y, d2x2.c1.x, d2x2.c1.y);
        //if (obj is double2x3 d2x3)
        //    return JoinDoubles(separator, culture, d2x3.c0.x, d2x3.c0.y, d2x3.c1.x, d2x3.c1.y, d2x3.c2.x, d2x3.c2.y);
        //if (obj is double2x4 d2x4)
        //    return JoinDoubles(separator, culture, d2x4.c0.x, d2x4.c0.y, d2x4.c1.x, d2x4.c1.y, d2x4.c2.x, d2x4.c2.y, d2x4.c3.x, d2x4.c3.y);
        //if (obj is double3x2 d3x2)
        //    return JoinDoubles(separator, culture, d3x2.c0.x, d3x2.c0.y, d3x2.c0.z, d3x2.c1.x, d3x2.c1.y, d3x2.c1.z);
        //if (obj is double3x3 d3x3)
        //    return JoinDoubles(separator, culture, d3x3.c0.x, d3x3.c0.y, d3x3.c0.z, d3x3.c1.x, d3x3.c1.y, d3x3.c1.z, d3x3.c2.x, d3x3.c2.y, d3x3.c2.z);
        //if (obj is double3x4 d3x4)
        //    return JoinDoubles(separator, culture, d3x4.c0.x, d3x4.c0.y, d3x4.c0.z, d3x4.c1.x, d3x4.c1.y, d3x4.c1.z, d3x4.c2.x, d3x4.c2.y, d3x4.c2.z, d3x4.c3.x, d3x4.c3.y, d3x4.c3.z);
        //if (obj is double4x2 d4x2)
        //    return JoinDoubles(separator, culture, d4x2.c0.x, d4x2.c0.y, d4x2.c0.z, d4x2.c0.w, d4x2.c1.x, d4x2.c1.y, d4x2.c1.z, d4x2.c1.w);
        //if (obj is double4x3 d4x3)
        //    return JoinDoubles(separator, culture, d4x3.c0.x, d4x3.c0.y, d4x3.c0.z, d4x3.c0.w, d4x3.c1.x, d4x3.c1.y, d4x3.c1.z, d4x3.c1.w, d4x3.c2.x, d4x3.c2.y, d4x3.c2.z, d4x3.c2.w);
        //if (obj is double4x4 d4x4)
        //    return JoinDoubles(separator, culture, d4x4.c0.x, d4x4.c0.y, d4x4.c0.z, d4x4.c0.w, d4x4.c1.x, d4x4.c1.y, d4x4.c1.z, d4x4.c1.w, d4x4.c2.x, d4x4.c2.y, d4x4.c2.z, d4x4.c2.w, d4x4.c3.x, d4x4.c3.y, d4x4.c3.z, d4x4.c3.w);
        //if (obj is float2 f2)
        //    return JoinFloats(separator, culture, f2.x, f2.y);
        //if (obj is float3 f3)
        //    return JoinFloats(separator, culture, f3.x, f3.y, f3.z);
        //if (obj is float4 f4)
        //    return JoinFloats(separator, culture, f4.x, f4.y, f4.z, f4.w);
        //if (obj is float2x2 f2x2)
        //    return JoinFloats(separator, culture, f2x2.c0.x, f2x2.c0.y, f2x2.c1.x, f2x2.c1.y);
        //if (obj is float2x3 f2x3)
        //    return JoinFloats(separator, culture, f2x3.c0.x, f2x3.c0.y, f2x3.c1.x, f2x3.c1.y, f2x3.c2.x, f2x3.c2.y);
        //if (obj is float2x4 f2x4)
        //    return JoinFloats(separator, culture, f2x4.c0.x, f2x4.c0.y, f2x4.c1.x, f2x4.c1.y, f2x4.c2.x, f2x4.c2.y, f2x4.c3.x, f2x4.c3.y);
        //if (obj is float3x2 f3x2)
        //    return JoinFloats(separator, culture, f3x2.c0.x, f3x2.c0.y, f3x2.c0.z, f3x2.c1.x, f3x2.c1.y, f3x2.c1.z);
        //if (obj is float3x3 f3x3)
        //    return JoinFloats(separator, culture, f3x3.c0.x, f3x3.c0.y, f3x3.c0.z, f3x3.c1.x, f3x3.c1.y, f3x3.c1.z, f3x3.c2.x, f3x3.c2.y, f3x3.c2.z);
        //if (obj is float3x4 f3x4)
        //    return JoinFloats(separator, culture, f3x4.c0.x, f3x4.c0.y, f3x4.c0.z, f3x4.c1.x, f3x4.c1.y, f3x4.c1.z, f3x4.c2.x, f3x4.c2.y, f3x4.c2.z, f3x4.c3.x, f3x4.c3.y, f3x4.c3.z);
        //if (obj is float4x2 f4x2)
        //    return JoinFloats(separator, culture, f4x2.c0.x, f4x2.c0.y, f4x2.c0.z, f4x2.c0.w, f4x2.c1.x, f4x2.c1.y, f4x2.c1.z, f4x2.c1.w);
        //if (obj is float4x3 f4x3)
        //    return JoinFloats(separator, culture, f4x3.c0.x, f4x3.c0.y, f4x3.c0.z, f4x3.c0.w, f4x3.c1.x, f4x3.c1.y, f4x3.c1.z, f4x3.c1.w, f4x3.c2.x, f4x3.c2.y, f4x3.c2.z, f4x3.c2.w);
        //if (obj is float4x4 f4x4)
        //    return JoinFloats(separator, culture, f4x4.c0.x, f4x4.c0.y, f4x4.c0.z, f4x4.c0.w, f4x4.c1.x, f4x4.c1.y, f4x4.c1.z, f4x4.c1.w, f4x4.c2.x, f4x4.c2.y, f4x4.c2.z, f4x4.c2.w, f4x4.c3.x, f4x4.c3.y, f4x4.c3.z, f4x4.c3.w);
        #endregion

        return obj.ToString();
    }

    public Delegate CreateAction(Type type)
    {
        var methodInfo = typeof(CSVSoapColumn).GetMethod("OnValueChanged", BindingFlags.Public | BindingFlags.Instance).MakeGenericMethod(type);
        return Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(type), this, methodInfo);
    }

    // Generic listener for the OnValueChanged events from the ScriptableVariable<T> class.
    public void OnValueChanged<T>(T t)
    {
        if (isNull)
            return;
        StoreValue();
        if (TriggerCSVUpdate)
        {
            if (values.TryPeek(out string lastValue) && !IgnoreUpdateValues.Contains(lastValue.Trim()))
                onValueChanged.Invoke();
        }
    }

    private void StoreValue()
    {
        //Debug.Log(variable.name);

        if (isNull)
            return;
        if (singleValue)
            values.Clear();
        values.Push(ValueToString(_separator));
    }

    public void Initialize(string separator = ",")
    {
        _separator = separator;

        if (isNull) return;

        if (handler == null)
            handler = (sender, e) => StoreValue();

        var variableType = variable.GetType();
        valueProperty = variableType.BaseType.GetProperty("Value"); // the getter is defined in the ScriptableVariable<T> class, not in the (for example) IntVariable class itself.
        onValueChangedEvent = variableType.GetEvent("OnValueChanged");

        if (valueProperty != null && onValueChangedEvent != null)
        {
            cachedDelegate = CreateAction(valueProperty.PropertyType);
            onValueChangedEvent.AddEventHandler(variable, cachedDelegate);
            StoreValue();
        }
    }

    public void Release()
    {
        if (isNull) return;
        if (onValueChangedEvent == null || cachedDelegate == null) return;
        onValueChangedEvent.RemoveEventHandler(variable, cachedDelegate);
        cachedDelegate = null;
    }
}

public enum CSVSoapSeparator
{
    Comma,
    Semicolon,
    Tab,
    Pipe
}

public class CSVSoap : MonoBehaviour
{
    public bool LogEveryFrame = false;
    public CSVSoapSeparator columnSeparator = CSVSoapSeparator.Comma;
    public List<CSVSoapColumn> variables = new List<CSVSoapColumn>();


    [Header("Events")]
    public UnityEvent<List<string>> onOutputRows;

    private string _separator = ",";

    private bool valuesChanged = false;

    public void SetSeparator(CSVSoapSeparator separator)
    {
        switch (separator)
        {
            case CSVSoapSeparator.Comma:
                _separator = ",";
                break;
            case CSVSoapSeparator.Semicolon:
                _separator = ";";
                break;
            case CSVSoapSeparator.Tab:
                _separator = "\t";
                break;
            case CSVSoapSeparator.Pipe:
                _separator = "|";
                break;
            default:
                _separator = ",";
                break;
        }
    }

    public string GetHeaders()
    {
        List<string> _headers = new List<string>();
        foreach (CSVSoapColumn column in variables)
            _headers.Add(column.GetHeader(_separator));
        return string.Join(_separator, _headers);
    }

    public string GetValues()
    {
        List<string> _values = new List<string>();
        foreach (CSVSoapColumn column in variables)
        {
            _values.Add(column.GetValue());
        }
        return string.Join(_separator, _values);
    }

    public void Initialize()
    {
        foreach (CSVSoapColumn column in variables)
            column.Initialize(_separator);

        if (!LogEveryFrame)
            foreach (CSVSoapColumn column in variables)
                column.onValueChanged.AddListener(valueChanged);
    }

    private void valueChanged()
    {
        valuesChanged = true;
    }

    private bool going = false;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        SetSeparator(columnSeparator);

        Initialize();

        going = true;
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(UpdateCSV());
        onOutputRows.Invoke(new List<string>() { GetHeaders() });

    }

    private void OnDisable()
    {
        going = false;
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator UpdateCSV()
    {
        while (going)
        {
            yield return new WaitForEndOfFrame();

            if (LogEveryFrame || valuesChanged)
            {
                valuesChanged = false;
                onOutputRows.Invoke(new List<string>() { GetValues() });
            }
        }
    }
}
