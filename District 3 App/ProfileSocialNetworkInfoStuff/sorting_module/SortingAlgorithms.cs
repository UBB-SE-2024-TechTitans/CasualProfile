using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace District_3_App.ProfileSocialNetworkInfoStuff.Sorting_module
{
    public class SortingAlgorithms<T> : ISortingAlgorithms<T>
        where T : IComparable<T>
    {
        public SortingAlgorithms()
        {
        }

        public void BubbleSortAscending(List<T> list, Func<T, T, int> compare)
        {
            bool swapped = false;
            int numberOfElements = list.Count;
            while (swapped != true)
            {
                swapped = true;
                for (int index = 0; index < numberOfElements - 1; index++)
                {
                    if (list[index].CompareTo(list[index + 1]) > 0)
                    {
                        T temporary = list[index];
                        list[index] = list[index + 1];
                        list[index + 1] = temporary;
                        swapped = false;
                    }
                }
            }
        }
        public void BubbleSortDescending(List<T> list, Func<T, T, int> compare)
        {
            bool swapped = false;
            int numberOfElements = list.Count;
            while (swapped != true)
            {
                swapped = true;
                for (int index = 0; index < numberOfElements - 1; index++)
                {
                    if (list[index].CompareTo(list[index + 1]) < 0)
                    {
                        T temporary = list[index];
                        list[index] = list[index + 1];
                        list[index + 1] = temporary;
                        swapped = false;
                    }
                }
            }
        }

        private void MergeLists(List<T> domainList, int left, int middle, int right, Func<T, T, int> compare, bool sortingTypes)
        {
            var leftListLength = middle - left + 1;
            var rightListLength = right - middle;

            var leftTemporaryList = new List<T>();
            var rightTemporaryList = new List<T>();
            int i, j;

            for (i = 0; i < leftListLength; i++)
            {
                leftTemporaryList.Add(domainList[left + i]);
            }

            for (j = 0; j < rightListLength; j++)
            {
                rightTemporaryList.Add(domainList[middle + 1 + j]);
            }

            i = 0;
            j = 0;
            int k = left;

            while (i < leftListLength && j < rightListLength)
            {
                if (sortingTypes)
                {
                    if (compare(leftTemporaryList[i], rightTemporaryList[j]) <= 0)
                    {
                        domainList[k++] = leftTemporaryList[i++];
                    }
                    else
                    {
                        domainList[k++] = rightTemporaryList[j++];
                    }
                }
                else
                    if (compare(leftTemporaryList[i], rightTemporaryList[j]) >= 0)
                {
                    domainList[k++] = leftTemporaryList[i++];
                }
                else
                {
                    domainList[k++] = rightTemporaryList[j++];
                }
            }

            while (i < leftListLength)
            {
                domainList[k++] = leftTemporaryList[i++];
            }
            while (j < rightListLength)
            {
                domainList[k++] = rightTemporaryList[j++];
            }
        }

        private void RecursiveMergeSort(List<T> domainList, int left, int right, Func<T, T, int> compare, bool sortingType)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                RecursiveMergeSort(domainList, left, middle, compare, sortingType);
                RecursiveMergeSort(domainList, middle + 1, right, compare, sortingType);

                MergeLists(domainList, left, middle, right, compare, sortingType);
            }
        }

        public void MergeSortAscending(List<T> domainList, Func<T, T, int> compare)
        {
            // List<DomainObject> temporaryList = new List<DomainObject>(domainList.Count);
            RecursiveMergeSort(domainList, 0, domainList.Count - 1, compare, true);
        }
        public void MergeSortDescending(List<T> domainList, Func<T, T, int> compare)
        {
            RecursiveMergeSort(domainList, 0, domainList.Count - 1, compare, false);
        }

        public void GnomeSortAscending(List<T> domainList, Func<T, T, int> compare)
        {
            int index = 0;

            while (index < domainList.Count)
            {
                if (index == 0 || compare(domainList[index], domainList[index - 1]) >= 0)
                {
                    index++;
                }
                else
                {
                    T temporaryObject = domainList[index];
                    domainList[index] = domainList[index - 1];
                    domainList[index - 1] = temporaryObject;
                    index--;
                }
            }
        }
        public void GnomeSortDescending(List<T> domainList, Func<T, T, int> compare)
        {
            int index = 0;

            while (index < domainList.Count)
            {
                if (index == 0 || compare(domainList[index], domainList[index - 1]) <= 0)
                {
                    index++;
                }
                else
                {
                    T temporaryObject = domainList[index];
                    domainList[index] = domainList[index - 1];
                    domainList[index - 1] = temporaryObject;
                    index--;
                }
            }
        }

        public void QuickSortAscending(List<T> domainList, Func<T, T, int> compare)
        {
            domainList = QuickSort(domainList, 0, domainList.Count - 1, compare, true); // ascending
        }
        public void QuickSortDescending(List<T> domainList, Func<T, T, int> compare)
        {
            domainList = QuickSort(domainList, 0, domainList.Count - 1, compare, false); // ascending
        }

        private List<T> QuickSort(List<T> domainList, int leftIndex, int rightIndex, Func<T, T, int> compare, bool sortingType)
        {
            int index_1 = leftIndex;
            int index_2 = rightIndex;
            T pivot = domainList[leftIndex];
            while (index_1 <= index_2)
            {
                if (sortingType)
                {
                    while (compare(domainList[index_1], pivot) < 0)
                    {
                        index_1++;
                    }

                    while (compare(domainList[index_2], pivot) > 0)
                    {
                        index_2--;
                    }
                }
                else
                {
                    while (compare(domainList[index_1], pivot) > 0)
                    {
                        index_1++;
                    }

                    while (compare(domainList[index_2], pivot) < 0)
                    {
                        index_2--;
                    }
                }

                if (index_1 <= index_2)
                {
                    T temp = domainList[index_1];
                    domainList[index_1] = domainList[index_2];
                    domainList[index_2] = temp;
                    index_1++;
                    index_2--;
                }
            }

            if (leftIndex < index_2)
            {
                QuickSort(domainList, leftIndex, index_2, compare, sortingType);
            }

            if (index_1 < rightIndex)
            {
                QuickSort(domainList, index_1, rightIndex, compare, sortingType);
            }

            return domainList;
        }
    }
}
