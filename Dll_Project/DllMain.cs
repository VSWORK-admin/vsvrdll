using System;

namespace Dll_Project
{
    public static class DllMain
    {
        public static void Main()
        {
            UnityEngine.Debug.Log("Dll Run Main !");

            var extralData = DllManager.Instance.transform.GetComponent<HFExtralData>();

            if (extralData != null && extralData.ExtralDatas.Length > 0)
            {
                extralData.ExtralDatas[0].Target.gameObject.SetActive(true);
            }
        }
    }
}
