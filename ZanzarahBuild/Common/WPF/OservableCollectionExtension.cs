using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Common.Wpf
{
    public static class OservableCollectionExtension
    {
        public static void AddRange<T>(this ObservableCollection<T> thisCollection, IEnumerable<T> collection)
        {
            foreach (T obj in collection) thisCollection.Add(obj);
        }
    }
}
