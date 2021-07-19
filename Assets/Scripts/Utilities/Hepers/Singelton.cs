﻿using UnityEngine;

namespace NinjaSlicer.Utilities.Helpers
{
    public abstract class Singelton<T> : MonoBehaviour where T : Component
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if(instance == null)
                    {
                        var gameObject = new GameObject
                        {
                            hideFlags = HideFlags.HideAndDontSave
                        };

                        instance = gameObject.AddComponent<T>();
                    }
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }
    }
}