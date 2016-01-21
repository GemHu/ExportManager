using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections;

namespace Dothan.DzHelpers
{
    public static class ObservableCollectionExtension
    {
        public static void SortAdd<T>(this ObservableCollection<T> This, T child)
        {
            This.SortAdd<T>(child, null);
        }

        public static void SortAdd<T>(this ObservableCollection<T> This, T child, IComparer comparer)
        {
            if (child == null) return;

            // child 没法进行比较，直接执行插入操作；
            if (comparer == null && !(child is IComparable))
            {
                This.Add(child);
                return;
            }

            // 用二分法查找插入位置
            int left = 0, right = This.Count - 1, mid;
            while (left <= right)
            {
                mid = left + (right - left) / 2;

                T midItem = This[mid];
                if (midItem == null)
                {
                    right = mid - 1;
                }
                else
                {
                    if (comparer != null)
                    {
                        if (comparer.Compare(child, midItem) >= 0)
                            left = mid + 1;
                        else
                            right = mid - 1;
                    }
                    else if (child is IComparable)
                    {
                        if ((child as IComparable).CompareTo(midItem) >= 0)
                            left = mid + 1;
                        else
                            right = mid - 1;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            // left 就是插入点的序号
            This.Insert(left, child);
        }
    }
}
