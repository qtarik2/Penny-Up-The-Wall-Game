using DG.Tweening;
using UnityEngine;

public class Effetcts : MonoBehaviour
{

    public ScaleData[] data;
    private int index;
   
    private void Start()
    {
        ScaleTheObj();
       
    }
    public void ScaleTheObj()
    {
        if(index>=data.Length)
        {
            index = 0;
        }
        transform.DOScale(data[index].size, data[index].time).OnComplete(() => ScaleTheObj());
        index++;
    }


  

[System.Serializable]
    public struct ScaleData
    {
        public float size;
        public float time;
    }
}
