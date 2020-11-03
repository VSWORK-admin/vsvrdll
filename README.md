# **说明文档** #

**1.工程为SDK拓展工程**

**2.代码从Dll_Project.DllMain类的Main函数开始执行**

**3.代码中不能直接继承MonoBehaviour类必须继承DllGenerateBase包装类作为替代**

**4.项目生成的文件在工程目录下的otherDir文件夹下**

**5.将otherDir文件夹下的Dll_Projectdll.bytes,Dll_Projectpdb.bytes文件复制到SDK工程下的Resources目录**

**6.使用SDK拓展工程时,将DllManager.cs组件拖入SDK场景任意物体中(一个场景中只能存在一个DllManager.cs组件),然后将Dll_Projectdll.bytes,Dll_Projectpdb.bytes分别拖入DllAsset,PdbAsset中**

**7.将GeneralDllBehavior组件托入SDK场景任意物体中,ScriptClassName中填写第3条中创建的包装类(如果有命名空间,填写时也必须包含命名空间如Dll_Project.DllMain)这个类就可以像平时写unity的MonoBehaviour类一样了**

**8.SDK中DllManager文件夹下的Extral文件夹里面存放的是一些工具和拓展类,可以在SDK拓展工程中直接使用,用来实现获取私有变量获取包装类等功能**

**9.ExtralData是个好用的类,他在其他脚本中也存在,主要是用来方便获取一些场景中物体的引用而不需要写一些Find代码**

**10.注意：Dll_Project工程中的依赖项的Assembly-CSharp改为你自己的Assembly-CSharp库路径,还有一些其他的我们公开的依赖库放在工程目录下的UnityDlls文件夹内**