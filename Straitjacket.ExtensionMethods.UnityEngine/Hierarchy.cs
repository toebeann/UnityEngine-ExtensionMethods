using System;
using UnityEngine;

namespace Straitjacket.ExtensionMethods.UnityEngine
{
    public static class Hierarchy
    {
        public static string GetPath(this Transform current)
        {
            if (current.parent == null)
                return "/" + current.name;
            return current.parent.GetPath() + "/" + current.name;
        }
        public static string GetPath(this GameObject gameObject) => gameObject.transform.GetPath();
        public static string GetPath(this Component component) => $"{component.transform.GetPath()}/{component.GetType()}";

        public static Transform GetRoot(this Transform current)
        {
            if (current.parent == null)
                return current;
            return current.parent.GetRoot();
        }
        public static GameObject GetRoot(this GameObject gameObject) => gameObject.transform.GetRoot().gameObject;

        public static Transform FindAncestor(this Transform current, string name)
        {
            if (current.parent == null || current.parent.name == name)
                return current.parent;
            return current.parent.FindAncestor(name);
        }
        public static GameObject FindAncestor(this GameObject gameObject, string name) => gameObject.transform.FindAncestor(name).gameObject;

        public static Transform FindAncestor(this Transform current, Func<Transform, bool> filter)
        {
            if (current.parent == null || filter(current.parent))
                return current.parent;
            return current.parent.FindAncestor(filter);
        }
        public static GameObject FindAncestor(this GameObject gameObject, Func<GameObject, bool> filter)
            => gameObject.transform.FindAncestor(x => filter(x.gameObject)).gameObject;
    }
}
