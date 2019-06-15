using ScriptableObjectDropdown;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    // Without grouping (default is None)
    [ScriptableObjectDropdown] public Block firstTargetBlock;
    // By grouping
    [ScriptableObjectDropdown(grouping = ScriptableObjectGrouping.ByFolder)] public Block secondTargetBlock;
}