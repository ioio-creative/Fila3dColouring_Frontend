// ----------------------------------------------------------------------------
// Game Architecture with Scriptable Objects
// 
// Authors: Christopher Wong, Hugo Yeung
// Date:   05/10/20
// ----------------------------------------------------------------------------

using System;
using UnityEngine;

namespace SOVariables
{
    public abstract class Variable<T> : ScriptableObject, ISerializationCallbackReceiver
        where T : IEquatable<T>
    {
        public event Action OnValueChanged;


        [SerializeField]
        private T initValue;

        [SerializeField]
        private T runtimeValue;
        public T Value { get { return runtimeValue; } }


        public static implicit operator T(Variable<T> variable)
        {
            return variable.runtimeValue;
        }

        public void InitializeValue(T _v)
        {
            initValue = _v;
            Reset();
        }

        public void SetValue(T aValue)
        {
            if (!runtimeValue.Equals(aValue))
            {
                runtimeValue = aValue;
                OnValueChanged?.Invoke();
            }
        }

        public void SetValue(Variable<T> aValue)
        {
            SetValue(aValue.Value);
        }

        public void Reset()
        {
            runtimeValue = initValue;
        }

        public override string ToString()
        {
            return Value.ToString();
        }


        #region ISerializationCallbackReceiver

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {
            Reset();
        }

        #endregion
    }


    public abstract class EnumVariable<T> : ScriptableObject, ISerializationCallbackReceiver
        where T : Enum
    {
        public event Action OnValueChanged;


        [SerializeField]
        private T initValue;

        [SerializeField]
        private T runtimeValue;
        public T Value { get { return runtimeValue; } }


        public static implicit operator T(EnumVariable<T> variable)
        {
            return variable.runtimeValue;
        }

        public void InitializeValue(T _v)
        {
            initValue = _v;
            Reset();
        }

        public void SetValue(T aValue)
        {
            if (!runtimeValue.Equals(aValue))
            {
                runtimeValue = aValue;
                OnValueChanged?.Invoke();
            }
        }

        public void SetValue(EnumVariable<T> aValue)
        {
            SetValue(aValue.Value);
        }

        public void Reset()
        {
            runtimeValue = initValue;
        }

        public override string ToString()
        {
            return Value.ToString();
        }


        #region ISerializationCallbackReceiver

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {
            Reset();
        }

        #endregion
    }
}