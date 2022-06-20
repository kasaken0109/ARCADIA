﻿#if UNITY_EDITOR
using UnityEditor;

namespace Unity.Animations.SpringBones
{
    namespace Inspector
    {
        public class FloatSlider
        {
        

                    public FloatSlider(string newLabel, float newLeftValue, float newRightValue)
                    {
                        label = newLabel;
                        leftValue = newLeftValue;
                        rightValue = newRightValue;
                    }

                    public bool Show(ref float value)
                    {
                        var newValue = EditorGUILayout.Slider(label, value, leftValue, rightValue);
                        var valueChanged = false;
                        if (newValue != value)
                        {
                            value = newValue;
                            valueChanged = true;
                        }
                        return valueChanged;
                    }

                    public bool Show(SerializedProperty floatProperty)
                    {
                        var value = floatProperty.floatValue;
                        var valueChanged = Show(ref value);
                        if (valueChanged)
                        {
                            floatProperty.floatValue = value;
                        }
                        return valueChanged;
                    }    //エディタ拡張のコードを書く。

            private string label;
            private float leftValue;
            private float rightValue;
        }
    }
}
#endif