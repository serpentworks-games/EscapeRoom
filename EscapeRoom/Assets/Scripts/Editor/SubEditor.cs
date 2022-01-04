using System;
using UnityEditor;

public abstract class SubEditor<T>
{
    Editor editor;
    Action defer;

    public abstract void OnInspectorGUI(T instance);

    public void Init(Editor editor)
    {
        this.editor = editor;
    }

    public void Update()
    {
        if(defer != null) defer();

        defer = null;
    }

    protected void Defer(Action action)
    {
        defer += action;
    }

    protected void Repaint()
    {
        editor.Repaint();
    }
}