// this function will work with all F# types
let rec quicksort (list) = // recursive
   match list with                    // switch analogue    
   | [] ->                            // if the list is empty then return empty list
        []                            
   | firstElem::otherElements ->      // divide array into two parts: one element firstElem and the rest of the list - otherElements
        let smallerElements =         // if the list is not empty     
            otherElements             // extract the smaller ones
            |> List.filter (fun e -> e < firstElem)  // take all the elements smaller than firstElem
            |> quicksort              // and sort them
        let largerElements =          // extract the large ones
            otherElements 
            |> List.filter (fun e -> e >= firstElem) // get from otherElements all the elements that are >= firstElem
            |> quicksort              // and sort them
        // combine the 3 parts into a new list and return it
        List.concat [smallerElements; [firstElem]; largerElements]; // but here i need to show firstElem as list so it can be applyed to List.Concat
                                                                    // so i put it in square brackets
let randomList =
    let rnd = System.Random();
    List.init (rnd.Next(50)) (fun _ -> rnd.Next (500)) // list of max 50 elements with max value of 500.
    

let main ()= 
    printfn "%A" (quicksort randomList)

main();