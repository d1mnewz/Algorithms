

let mutable a = 15;
let mutable b = 10;
let swap (left : 'a byref) (right : 'a byref) =
    left <- left + right; // a = 15 ; b = 10;
    right <- left - right; // a == 15; b = 5 ;
    left <-  left - right; // a == 10; b == 5;

let main() =
    printfn "a = %d; b = %d" a b;
    swap (&a) (&b);
    printfn "a = %d; b = %d" a b;

main();
