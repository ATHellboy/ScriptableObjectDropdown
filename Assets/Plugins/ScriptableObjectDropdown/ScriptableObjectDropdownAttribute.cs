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

    #region Example
    /// <example>
    /// <para>Usage Examples</para>
    /// <code language="csharp"><![CDATA[
    /// using UnityEngine;
    /// using ScriptableObjectDropdown;
    /// 
    /// [CreateAssetMenu(menuName = "Create Block")]
    /// public class Block : ScriptableObject
    /// {
    ///     // Some fields and functions
    /// }
    /// 
    /// public interface IBlock
    /// {
    ///     // Some properties and functions signature
    /// }
    /// 
    /// public abstract class AbstarctBlock : ScriptableObject
    /// {
    ///     // Some fields and functions
    /// }
    /// 
    /// [CreateAssetMenu(menuName = "Blocks/Water")]
    /// public class WaterBlock : AbstarctBlock
    /// {
    ///     // Some fields and functions
    /// }
    /// 
    /// [CreateAssetMenu(menuName = "Blocks/Snow")]
    /// public class SnowBlock : ScriptableObject, IBlock
    /// {
    ///     // Some fields and functions
    /// }
    /// 
    /// [CreateAssetMenu(menuName = "Blocks/Sand")]
    /// public class SandBlock : Block
    /// {
    ///     // Some fields and functions
    /// }
    /// 
    /// [CreateAssetMenu(menuName = "Blocks/Dirt")]
    /// public class DirtBlock : ScriptableObject, IBlock
    /// {
    ///     // Some fields and functions
    /// }
    /// 
    /// public class BlockManager : MonoBehaviour
    /// {
    ///     // Without grouping (default is None)
    ///     [ScriptableObjectDropdown(typeof(Block))] public ScriptableObjectReference targetBlock;
    ///     
    ///     // By grouping
    ///     [ScriptableObjectDropdown(typeof(Block), grouping = ScriptableObjectGrouping.ByFolder)]
    ///     public ScriptableObjectReference targetBlockByGrouping;
    ///     
    ///     // Derived class
    ///     [ScriptableObjectDropdown(typeof(SandBlock))] public ScriptableObjectReference derivedClassTargetBlock;
    ///     
    ///     // Derived abstract class
    ///     [ScriptableObjectDropdown(typeof(AbstarctBlock))] public ScriptableObjectReference derivedAbstractClassTargetBlock;
    ///     
    ///     // Interface
    ///     [ScriptableObjectDropdown(typeof(IBlock))] public ScriptableObjectReference interfaceTargetBlock;
    /// }
    /// 
    /// [CreateAssetMenu(menuName = "Create Block Manager Settings")]
    /// public class BlockManagerSetting : ScriptableObject
    /// {
    ///     // Without grouping (default is None)
    ///     [ScriptableObjectDropdown(typeof(Block))] public ScriptableObjectReference targetBlock;
    ///     
    ///     // By grouping
    ///     [ScriptableObjectDropdown(typeof(Block), grouping = ScriptableObjectGrouping.ByFolder)]
    ///     public ScriptableObjectReference targetBlockByGrouping;
    ///     
    ///     // Derived class
    ///     [ScriptableObjectDropdown(typeof(SandBlock))] public ScriptableObjectReference derivedClassTargetBlock;
    ///     
    ///     // Derived abstract class
    ///     [ScriptableObjectDropdown(typeof(AbstarctBlock))] public ScriptableObjectReference derivedAbstractClassTargetBlock;
    ///     
    ///     // Interface
    ///     [ScriptableObjectDropdown(typeof(IBlock))] public ScriptableObjectReference interfaceTargetBlock;
    /// }
    /// ]]></code>
    /// </example>
    #endregion

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ScriptableObjectDropdownAttribute : PropertyAttribute
    {
        public ScriptableObjectGrouping grouping = ScriptableObjectGrouping.None;

        private Type _baseType;
        public Type BaseType
        {
            get { return _baseType; }
            private set { _baseType = value; }
        }

        public ScriptableObjectDropdownAttribute(Type baseType)
        {
            _baseType = baseType;
        }
    }
}