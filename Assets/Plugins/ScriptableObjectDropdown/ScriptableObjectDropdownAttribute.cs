// Copyright (c) ATHellboy (Alireza Tarahomi) Limited. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using UnityEngine;

namespace ScriptableObjectDropdown
{
    /// <summary>
    /// Indicates how selectable scriptableObjects should be collated in drop-down menu.
    /// </summary>
    public enum ScriptableObjectGrouping
    {
        /// <summary>
        /// No grouping, just show type names in a list; for instance, "MainFolder > NestedFolder > SpecialScriptableObject".
        /// </summary>
        None,
        /// <summary>
        /// Group classes by namespace and show foldout menus for nested namespaces; for
        /// instance, "MainFolder >> NestedFolder >> SpecialScriptableObject".
        /// </summary>
        ByFolder,
        /// <summary>
        /// Group scriptableObjects by folder; for instance, "MainFolder > NestedFolder >> SpecialScriptableObject".
        /// </summary>
        ByFolderFlat
    }

    /// <example>
    /// <para>Usage Examples</para>
    /// <code language="csharp"><![CDATA[
    /// using UnityEngine;
    /// using ScriptableObjectDropdown;
    /// 
    /// [CreateAssetMenu(menuName = "Create Block")]
    /// public class Block : ScriptableObject
    /// {
    ///     // Some fields
    /// }
    /// 
    /// public class BlockManager : MonoBehaviour
    /// {
    ///     [ScriptableObjectDropdown] public Block targetBlock;
    ///     
    ///     // or
    ///     
    ///     [ScriptableObjectDropdown(grouping = ScriptableObjectGrouping.ByFolder)] public Block targetBlock;
    /// }
    /// 
    /// // or
    /// 
    /// [CreateAssetMenu(menuName = "Create Block Manager Settings")]
    /// public class BlockManagerSetting : ScriptableObject
    /// {
    ///     [ScriptableObjectDropdown] public Block targetBlock;
    ///     
    ///     // or
    ///     
    ///     [ScriptableObjectDropdown(grouping = ScriptableObjectGrouping.ByFolder)] public Block targetBlock;
    /// }
    /// ]]></code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ScriptableObjectDropdownAttribute : PropertyAttribute
    {
        public ScriptableObjectGrouping grouping = ScriptableObjectGrouping.None;

        public ScriptableObjectDropdownAttribute() { }
    }
}