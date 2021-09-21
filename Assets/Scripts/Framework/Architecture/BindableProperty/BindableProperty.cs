using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Framework
{
    public class BindableProperty<T> where T : IEquatable<T>
    {
        private T mValue = default(T);
        public void Register(Action<T> valueChanged)
        {
            OnValueChanged += valueChanged;
        }
        public void Unregister(Action<T> valueChanged)
        {
            OnValueChanged -= valueChanged;
        }
        public T Value
        {
            get
            {
                return mValue;
            }
            set
            {
                if (!value.Equals(mValue))
                {
                    mValue = value;
                    OnValueChanged?.Invoke(value);
                }
            }
        }
        public Action<T> OnValueChanged;
    }

}