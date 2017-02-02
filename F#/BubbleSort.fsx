//using bubble sort in a functional language isn`t very efficient,
// because the implementation has to reverse the list many times 
//(and this can`t be really implemented very efficiently for immutable lists).
let swap i j (arr : 'a []) =
    let tmp = arr.[i]
    arr.[i] <- arr.[j]
    arr.[j] <- tmp

let bubbleSort arr =
    let rec loop (arr : 'a []) =
        let mutable swaps = 0
        for i = 0 to arr.Length - 2 do
            if arr.[i] > arr.[i+1] then
                swap i (i+1) arr
                swaps <- swaps + 1
        if swaps > 0 then loop arr else arr

    loop arr

let main ()= 
    printfn "%A" (bubbleSort [|3;2;3;69;42;213|] )

main();