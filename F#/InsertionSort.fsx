//https://cdn.programiz.com/sites/tutorial2program/files/Insertion-Sort-Algorithm-Programming.jpg
//https://www.tutorialspoint.com/data_structures_algorithms/insertion_sort_algorithm.htm
//Step 1 − If it is the first element, it is already sorted. return 1;
//Step 2 − Pick next element
//Step 3 − Compare with all elements in the sorted sub-list
//Step 4 − Shift all the elements in the sorted sub-list 
//          that is greater than the value to be sorted
//Step 5 − Insert the value
//Step 6 − Repeat until list is sorted
let mutable arr = [|1;9;4;2;8;3;6;4;3;2;4;5;6;7;7;7|];
 
let insert (arr: 'a[]) i item =  // where i = index
    let mutable i = i - 1   // avoid indexoutofrange exception
                            // item = what to paste into 
    while i >= 0 && item < arr.[i] do // comparing this and prev element
        arr.[i + 1] <- arr.[i] // and replacing it if this < prev elem
        i <- i - 1 // iterating
    arr.[i + 1] <- item 

let isort (arr: 'a []) =
    for i in 1 .. (arr.Length - 1) do
        insert arr i arr.[i]
    arr

let main() = 
    printfn "%A" (isort arr);

main();