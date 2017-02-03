open System;
let getRandomIntArr count : int[] =
    let rnd = System.Random();
    let mutable arr = Array.zeroCreate count;
    for i = 0 to count - 1 do
        arr.[i] <- rnd.Next(777);
    arr;

let split (arr : _ []) =
    let n = arr.Length;
    arr.[0..n/2-1], arr.[n/2..n-1]; // divide array into two parts
 
let rec merge (l : 'a []) (r : 'a []) =
    let n = l.Length + r.Length
    let res = Array.zeroCreate<'a> n // create an array with n Length
    let mutable i, j = 0, 0
    for k = 0 to n-1 do
        if i >= l.Length 
            then res.[k] <- r.[j]; j <- j + 1;
        elif j >= r.Length 
             then res.[k] <- l.[i]; i <- i + 1;
        elif l.[i] < r.[j]
             then res.[k] <- l.[i]; i <- i + 1;
        else
            res.[k] <- r.[j]; j <- j + 1;

    res

let rec mergeSort = function
    | [||]  ->  [||] // empty
    | [|a|] ->  [|a|] // 1 element
    | arr   ->  let (x, y) = split arr;
                merge (mergeSort x) (mergeSort y); //the algorithm for merge sort is based on the idea
                // that it’s easier to merge two already sorted lists than it is to deal with a single unsorted list
let main() = 
    printfn "%A" (mergeSort (getRandomIntArr 50))

main();