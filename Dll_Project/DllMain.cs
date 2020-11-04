using System;

namespace Dll_Project
{
    public static class DllMain
    {
        public static void Main()
        {
            UnityEngine.Debug.Log("Dll Run Main !");

            var DllManagerObject = DllManager.Instance.transform.GetComponent<DllManager>();

            if (DllManagerObject != null && DllManagerObject.ExtralDatas.Length > 0)
            {
                foreach (var item in DllManagerObject.ExtralDatas) {
                    item.Target.gameObject.SetActive(true);
                }
            }
        } 
    }
}
