using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Kataru
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class NamedAttribute : System.Attribute
    {
        public string Name { get; set; }
        public NamedAttribute(string name) => Name = name;
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class CommandHandler : NamedAttribute
    {
        public CommandHandler(string name = "") : base(name) { }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class CharacterHandler : NamedAttribute
    {
        public CharacterHandler(string name = "") : base(name) { }
    }

    /// <summary>
    /// A MonoBehavior that can has KataruAttributes on its methods.
    /// Inherit from this class to be able to iterate over all methods containing named KataruAttributes.
    /// </summary>
    public class Attributed : MonoBehaviour
    {
        protected struct NamedAction<T>
        {
            public string name;
            public UnityAction<T> action;
        }

        protected IEnumerable<NamedAction<T>> GetActionsForAttribute<T, A>() where A : NamedAttribute
        {
            var type = this.GetType();
            // Debug.Log($"Type name: {type.Name}");
            foreach (var methodInfo in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                // Debug.Log($"Method name: {methodInfo.Name}");
                foreach (var attribute in methodInfo.GetCustomAttributes<A>(false))
                {
                    yield return new NamedAction<T>
                    {
                        name = attribute.Name.Length > 0 ? attribute.Name : methodInfo.Name,
                        action = (T arg) => methodInfo.Invoke(this, new object[] { arg })
                    };
                }
            }
        }
    }
}
