using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

namespace PlaceHolders.ListView
{
    public abstract class ListView<ModelType> : MonoBehaviour
    {
        public ObservableCollection<ModelType> Items { get; set; } = new ObservableCollection<ModelType>();
        
        [SerializeField] ViewPrefab<ModelType> ViewPrefab;

        readonly List<ViewPrefab<ModelType>> views = new List<ViewPrefab<ModelType>>();

        void OnEnable()
        {
            Items.CollectionChanged += OnCollectionChanged;
            RecreateList();
        }

        void OnDisable()
        {
            Items.CollectionChanged -= OnCollectionChanged;
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RecreateList();
        }

        void RecreateList()
        {
            foreach (Transform view in transform)
            {
                Destroy(view.gameObject);
            }
            views.Clear();

            foreach (ModelType item in Items)
            {
                ViewPrefab<ModelType> view = Instantiate(ViewPrefab, transform);
                view.Init(item);
                views.Add(view);
            }
        }

        public void Refresh()
        {
            if (views.Count == 0)
            {
                return;
            }

            if (views.Count != Items.Count)
            {
                RecreateList();
                Debug.LogWarning("All view items were recreated. This should not happened.");
                return;
            }

            for (int i = 0; i < views.Count; i++)
            {
                views[i].SetData(Items[i]);
            }
        }
    }

}