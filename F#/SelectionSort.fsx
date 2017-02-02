let swap i j (arr : 'a []) =
    let tmp = arr.[i]
    arr.[i] <- arr.[j]
    arr.[j] <- tmp

let selectionSort (arr : 'a []) =
    let rec loop n (arr : 'a []) =
        if n >= arr.Length - 1 then arr 
        else
            let mutable x, mini = arr.[n], n
            for i = n+1 to arr.Length - 1 do
                if (arr.[i] < x) // find the smallest item 
                    then
                    x    <- arr.[i];
                    mini <- i;
            if (n <> mini)
                then 
                swap n (mini) arr // and swap it into position of first item
            loop (n+1) arr // repeat on the remaining n-1 items
    loop 0 arr;
let main () = 
    printfn "%A" (selectionSort [|1;3;1;94;2;321;22;0|])

main();