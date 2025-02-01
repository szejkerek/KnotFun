using UnityEngine;

namespace PlaceHolders.ListView
{
    public abstract class ViewPrefab<ModelType> : MonoBehaviour
    {
        public void Init(ModelType model)
        {
            SetData(model);
        }
        public abstract void SetData(ModelType data);
    }
}