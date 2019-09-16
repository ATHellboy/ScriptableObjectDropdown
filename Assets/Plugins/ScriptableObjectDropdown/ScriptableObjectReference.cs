// Copyright (c) ATHellboy (Alireza Tarahomi) Limited. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using UnityEngine;

namespace ScriptableObjectDropdown
{
    /// <summary>
    /// Because PropertyDrawer OnGUI is not called for interfaces,
    /// I had to create parent class as an abstract layer.
    /// </summary>
    [Serializable]
    public class ScriptableObjectReference
    {
        public ScriptableObject value;
    }
}