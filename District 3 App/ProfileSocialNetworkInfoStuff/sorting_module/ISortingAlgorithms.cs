using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace District_3_App.ProfileSocialNetworkInfoStuff.Sorting_module
{
    public interface ISortingAlgorithms<T>
    {
        void BubbleSortAscending(List<T> list, Func<T, T, int> compare);
        void BubbleSortDescending(List<T> list, Func<T, T, int> compare);

        void MergeSortAscending(List<T> domainList, Func<T, T, int> compare);
        void MergeSortDescending(List<T> domainList, Func<T, T, int> compare);

        void GnomeSortAscending(List<T> domainList, Func<T, T, int> compare);
        void GnomeSortDescending(List<T> domainList, Func<T, T, int> compare);

        void QuickSortAscending(List<T> domainList, Func<T, T, int> compare);
        void QuickSortDescending(List<T> domainList, Func<T, T, int> compare);
    }
}
