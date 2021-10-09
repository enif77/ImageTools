using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ImageManipulator.Avalonia
{
    public static class Extensions
    {
        // https://stackoverflow.com/questions/19112922/sort-observablecollectionstring-through-c-sharp
        
        /// <summary>
        /// Inserts an item into a list in the correct place, based on the provided key and key comparer. Use like OrderBy(o => o.PropertyWithKey).
        /// </summary>
        public static int InsertInPlace<TItem, TKey>(this ObservableCollection<TItem> collection, TItem itemToAdd, Func<TItem, TKey> keyGetter)
        {
            var index = collection.ToList().BinarySearch(keyGetter(itemToAdd), Comparer<TKey>.Default, keyGetter);
            
            collection.Insert(index, itemToAdd);
            
            return index; 
        }
        
        /// <summary>
        /// Binary search.
        /// </summary>
        /// <returns>Index of item in collection.</returns> 
        /// <notes>This version tops out at approximately 25% faster than the equivalent recursive version. This 25% speedup is for list
        /// lengths more of than 1000 items, with less performance advantage for smaller lists.</notes>
        public static int BinarySearch<TItem, TKey>(this IList<TItem> collection, TKey keyToFind, IComparer<TKey> comparer, Func<TItem, TKey> keyGetter)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            var lower = 0;
            var upper = collection.Count - 1;

            while (lower <= upper)
            {
                var middle = lower + (upper - lower) / 2;
                var comparisonResult = comparer.Compare(keyToFind, keyGetter.Invoke(collection[middle]));
                switch (comparisonResult)
                {
                    case 0:
                        return middle;
                    
                    case < 0:
                        upper = middle - 1;
                        break;
                    
                    default:
                        lower = middle + 1;
                        break;
                }
            }

            // If we cannot find the item, return the item below it, so the new item will be inserted next.
            return lower;
        }
    }
}