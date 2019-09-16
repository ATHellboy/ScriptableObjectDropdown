using Example.Blocks;
using ScriptableObjectDropdown;
using UnityEngine;

namespace Example
{
    public class BlockManager : MonoBehaviour
    {
        [SerializeField] private bool _debug = true;
        [SerializeField] private BlockManagerSettings _blockSettings = default;
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

        void Start()
        {
            if (_debug)
            {
                PrintValue(targetBlock);
                PrintValue(targetBlockByGrouping);
                PrintValue(derivedClassTargetBlock);
                PrintValue(derivedAbstractClassTargetBlock);
                PrintValue(interfaceTargetBlock);

                PrintValue(_blockSettings.targetBlock);
                PrintValue(_blockSettings.targetBlockByGrouping);
                PrintValue(_blockSettings.derivedClassTargetBlock);
                PrintValue(_blockSettings.derivedAbstractClassTargetBlock);
                PrintValue(_blockSettings.interfaceTargetBlock);
            }
        }

        private void PrintValue(ScriptableObjectReference scriptableObjectReference)
        {
            print(scriptableObjectReference.value);
        }
    }
}