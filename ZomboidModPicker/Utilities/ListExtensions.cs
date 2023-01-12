using System.Collections.Generic;

namespace ZomboidModPicker.Utilities
{
    public static class ListExtensions
    {
        public static bool MoveUp<T>(this IList<T> collection, int index)
        {
            if (collection.Count < 2)
                return false;

            // If first, can't move up
            if (index == 0)
                return false;

            var targetItem = collection[index - 1];
            var current = collection[index];

            // Swap two items
            collection[index - 1] = current;
            collection[index] = targetItem;
            return true;
        }

        public static bool MoveDown<T>(this IList<T> collection, int index)
        {
            if (collection.Count < 2)
                return false;

            // If last, can't move down
            if (index >= collection.Count - 1)
                return false;

            var targetItem = collection[index + 1];
            var current = collection[index];

            // Swap two items
            collection[index + 1] = current;
            collection[index] = targetItem;
            return true;
        }
    }
}
