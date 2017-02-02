# Algorithms
// Bubble sort is the most basic way to sort a collection, and the most slowlest. It works by sequentially going through your array and comparing two values at a time, swapping them if necessary. It then repeats the process until no swaps are required.

// Selection sort is slow too.  It has O(n2) time complexity, making it inefficient on large lists, and generally performs worse than the similar insertion sort. We start by assuming that the first number in the array is the minimum. We then find out the smallest number in the array and if there is any number smaller than the first number, we exchange the numbers after one pass through the array. After this, the number in first position is smallest and array is sorted till position 1.
Next, we assume that the number at second position is the smallest in the remainder of the array. We pass through the array comparing each number with this number. After the pass is complete, we exchange this number with the smallest number in the remainder of the array. At the end of this, we will have first and second number in the array in sorted order. We repeat this step for rest of the numbers until all the numbers are sorted.

// Quick sort is a divide and conquer algorithm in the style of merge sort. The basic idea is to find a “pivot” item in the array to compare all other items against, then shift items such that all of the items before the pivot are less than the pivot value and all the items after the pivot are greater than the pivot value. After that, recursively perform the same operation on the items before and after the pivot.

// Merge sort is arguably the first useful sorting algorithm you learn in computer science. Merge sort has a complexity of O(n log n), making it one of the more efficient sorting algorithms available. Additionally, merge sort is a stable sort (just like insertion sort) so that the relative order of equivalent items remains the same before and after the sort.
The algorithm for merge sort is based on the idea that it’s easier to merge two already sorted lists than it is to deal with a single unsorted list. To that end, merge sort starts by creating n number of one item lists where n is the total number of items in the original list to sort. Then, the algorithm proceeds to combine these one item lists back into a single sorted list.

// Insertion sort works by separating an array into two sections, a sorted section and an unsorted section. Initially, of course, the entire array is unsorted. The sorted section is then considered to be empty. The first step is to add a value to the sorted section, so the first item in the array is used (a list of one item is always sorted). Then at each item in the unsorted section:

•	If the item value goes after the last item in the sorted section, then do nothing.

•	If the item value goes before the last item in the sorted section, remove the item value from the array and shift the last sorted item into the now-vacant spot.

Compare the item value to the previous value (second to last) in the sorted section.
If the item value goes after the previous value and before the last value, then place the item into the open spot between them, otherwise, continue this process until the start of the array is reached.
