## 说明文档 ##

### 一. 用途
>工程为VSVR SDK拓展工程，用于生成VSVR交互SDK所需的bytes字节码。

>编写VSVR交互逻辑时将此工程check到本地，并在此工程编写VSVR C# 交互代码，建议使用VisualStudio编写。

### 二. 使用步骤
#### 2.1 入口函数
代码 从```Dll_Project.DllMain``` 类的 ```Main()``` 函数开始执行, 对应的c#文件为 ```/Dll_Project/DllMain.cs```，入口函数中将2.4.2中配置的脚本进行初始化操作（将脚本所在gameobject启用），如示例中操作：
```
var DllManagerObject = DllManager.Instance.transform.GetComponent<DllManager>();

if (DllManagerObject != null && DllManagerObject.ExtralDatas.Length > 0)
{
    foreach (var item in DllManagerObject.ExtralDatas) {
        item.Target.gameObject.SetActive(true);
    }
}
```
#### 2.2 交互逻辑代码编写
###### 2.2.1 创建脚本文件
在```/Dll_Project/DllMain.cs```文件同目录下创建*.cs文件，文件命名于class命名相同，如工程示例中的```TestMessageDispatcher```

###### 2.2.2 继承类
代码中不能直接继承MonoBehaviour类必须继承DllGenerateBase包装类作为替代

#### 2.3 生成byte字节码
###### 2.3.1 设置生成事件
在VisualStudio  项目->'Dll_Project'属性 -> 生成事件-> 生成后事件命令行 填写 
```
copy "$(TargetDir)$(ProjectName).dll" "$(ProjectDir)unitybytes\$(ProjectName)dll.bytes"
copy "$(TargetDir)$(ProjectName).pdb" "$(ProjectDir)unitybytes\$(ProjectName)pdb.bytes"
```

或将 ```$(ProjectDir)unitybytes``` 改为 Unity工程目录中的场景所在路径 如：
```
copy "$(TargetDir)$(ProjectName).dll" "D:\vsvrskd\Assets\Scenes\Example\$(ProjectName)dll.bytes"
copy "$(TargetDir)$(ProjectName).pdb" "D:\vsvrskd\Assets\Scenes\Example\$(ProjectName)pdb.bytes"
```
配置为 Unity工程目录中的场景所在路径 后生成的 字节码文件则自动会出现在 Unity场景所在路径内,若保持默认的  ```$(ProjectDir)unitybytes``` 则需要在生成后手动将 ```unitybytes``` 中的两个字节码文件拷贝到Unity工程内。

###### 2.3.1 生成字节码
解决方案配置 设置为 ```Release``` 

点击 生成->生成解决方案后 会自动创建 两个 ```.bytes``` 文件 分别为：
```Dll_Projectdll.bytes```  和 ```Dll_Projectpdb.bytes``` ,两个文件为unity场景所需要的交互逻辑字节码文件。


#### 2.4 Unity工程配置
###### 2.4.1 配置加载字节码
将```DllManager.cs```组件拖入SDK场景任意物体中(**一个场景中只能存在一个```DllManager.cs```组件**),然后将```Dll_Projectdll.bytes```,```Dll_Projectpdb.bytes```分别拖入```DllAsset```,```PdbAsset```中
###### 2.4.2 配置交互脚本
将```GeneralDllBehavior```组件托入SDK场景任意物体中,```ScriptClassName```中填写包装类名(如果有命名空间,填写时也必须包含命名空间名称如```Dll_Project.TestMessageDispatcher```,如果不包含命名空间则直接填写类名，如```TestMessageDispatcher```),这个类就可以像平时写unity的MonoBehaviour类一样了。

将```GeneralDllBehavior```组件所在物体拖入2.4.1 中创建的 ```DllManager``` 所在 Inspector 中的 ExtralDatas 中，如果有多个```GeneralDllBehavior```，则在DllManager中设置多个 ExtralDatas，并依次拖入。
###### 2.4.3 配置交互脚本初始化
**手动将```GeneralDllBehavior```组件所在物体在Inspector中设置为不活动**，在入口函数中，在初始化时会将组件自动启用（```SetActive(true)```）即完成初始化。

#### 2.5 基本使用
###### 2.5.1 脚本内获取场景物体
使用 ExtralData 类,它在其他脚本中也存在,主要是用来方便获取一些场景中物体的引用而不需要写一些Find代码
如在```TestMessageDispatcher```中需要操作设置某个物体可见则 可以将物体拖入```TestMessageDispatcher```所在的```GeneralDllBehavior```组件中的 ExtralDatas 中 并使用```BaseMono.ExtralDatas``` 来获取到 ```ExtralData[]``` 如代码中所示：
```
public override void OnEnable()
{
    base.OnEnable();
    if (BaseMono.ExtralDatas != null && BaseMono.ExtralDatas.Length > 0)
    {
        BaseMono.ExtralDatas[0].Target.gameObject.SetActive(true);
    }
}
```
###### 2.5.2 发送接收VR指令消息
加入引用 ```using com.ootii.Messages;```

创建接收信息方法
```
void RecieveCChangeObj(IMessage msg)
{
    WsCChangeInfo rinfo = msg.Data as WsCChangeInfo;
    ShowbStr = rinfo.b;
    Debug.LogWarning(ShowbStr);
}
```

绑定事件：
```
public override void OnEnable()
{
    base.OnEnable();
    MessageDispatcher.AddListener(WsMessageType.RecieveCChangeObj.ToString(), RecieveCChangeObj);
}

public override void OnDisable()
{
    base.OnDisable();
    MessageDispatcher.RemoveListener(WsMessageType.RecieveCChangeObj.ToString(), RecieveCChangeObj);
}
```