using UnityEngine;
using ScriptableObjectDropdown;
using Example.Blocks;

namespace Example
{
    [CreateAssetMenu(menuName = "Create Block Manager Settings")]
    public class BlockManagerSettings : ScriptableObject
    {
        // Without grouping (default is None)
        [ScriptableObjectDropdown(typeof(Block))] public ScriptableObjectReference targetBlock;
        // By grouping
        [ScriptableObjectDropdown(typeof(Block), grouping = ScriptableObjectGrouping.ByFolder)]
        public ScriptableObjectReference targetBlockByGrouping;
        // Derived class
        [ScriptableObjectDropdown(typeof(SandBlock))] public ScriptableObjectReference derivedClassTargetBlock;
        // Derived abstract class
        [ScriptableObjectDropdown(typeof(AbstarctBlock))] public ScriptableObjectReference derivedAbstractClassTargetBlock;
        // Interface
        [ScriptableObjectDropdown(typeof(IBlock))] public ScriptableObjectReference interfaceTargetBlock;
    }
}