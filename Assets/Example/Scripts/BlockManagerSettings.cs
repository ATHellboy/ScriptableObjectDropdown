using UnityEngine;
using ScriptableObjectDropdown;

[CreateAssetMenu(menuName ="Create Block Manager Settings")]
public class BlockManagerSettings : ScriptableObject
{
    // Without grouping (default is None)
    [ScriptableObjectDropdown] public Block firstTargetBlock;
    // By grouping
    [ScriptableObjectDropdown(grouping = ScriptableObjectGrouping.ByFolderFlat)] public Block secondTargetBlock;
}