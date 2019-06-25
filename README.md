# ScriptableObjectDropdown
ScriptableObjectDropdown is an attribute for the Unity Inspector.
It is used for showing ScriptableObjects which are created in your project, in dropdown menu and select between them in Inspector.

![](Images/ScriptableObjectDropdown.gif)

# Usage Example
1. Clone this repository or download the latest [release package available](https://github.com/ATHellboy/ScriptableObjectDropdown/releases) (There isn't an example folder in `.unitypackage`).

2. Create `ScriptableObject` class which you  want to create specified objects by that.

```cs
using UnityEngine;

[CreateAssetMenu(menuName = "Create Block")]
public class Block : ScriptableObject
{
    // Some fields
}
```

3. Create ScriptableObjects in the project.

![](Images/Resources.PNG)

4. Use `ScriptableObjectDropdown` attribute by setting optional grouping (Default grouping is None) like this in `MonoBeahviour` or `ScriptableObject` derived classes.

**MonoBehavior**

```cs
using ScriptableObjectDropdown;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    // Without grouping (default is None)
    [ScriptableObjectDropdown] public Block firstTargetBlock;
    // By grouping
    [ScriptableObjectDropdown(grouping = ScriptableObjectGrouping.ByFolder)] public Block secondTargetBlock;
}
```

![](Images/MonoBehaviourDefaultGrouping.png)

![](Images/MonoBehaviourByFolderGrouping.png)

**ScriptableObject**
```cs
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
```

![](Images/ScriptableObjectDefaultGrouping.png)

![](Images/ScriptableObjectByFolderFlatGrouping.png)

# License
MIT License

Copyright (c) 2019 Alireza Tarahomi

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
