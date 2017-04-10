
// If it is the first element, it is already sorted. return 1;
// Pick next element
// Compare with all elements in the sorted sub-list
// Shift all the elements in the sorted sub-list 
//          that is greater than the value to be sorted
// Insert the value
// Repeat until list is sorted
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
