(*
--- Day 1: Inverse Captcha ---

The night before Christmas, one of Santa's Elves calls you in a panic. 
"The printer's broken! We can't print the Naughty or Nice List!" By the time you make it to sub-basement 17, 
there are only a few minutes until midnight. "We have a big problem," she says; "there must be almost 
fifty bugs in this system, but nothing else can print The List. Stand in this square, quick! There's 
no time to explain; if you can convince them to pay you in stars, you'll be able to--" 
She pulls a lever and the world goes blurry.

When your eyes can focus again, everything seems a lot more pixelated than before. 
She must have sent you inside the computer! You check the system clock: 25 milliseconds until midnight. 
With that much time, you should be able to collect all fifty stars by December 25th.

Collect stars by solving puzzles. Two puzzles will be made available on each day millisecond 
in the advent calendar; the second puzzle is unlocked when you complete the first. 
Each puzzle grants one star. Good luck!

You're standing in a room with "digitization quarantine" written in LEDs along one wall. 
The only door is locked, but it includes a small interface. "Restricted Area - Strictly No Digitized Users Allowed."

It goes on to explain that you may only leave by solving a captcha to prove you're not a human. 
Apparently, you only get one millisecond to solve the captcha: too fast for a normal human, but it feels like hours to you.

The captcha requires you to review a sequence of digits (your puzzle input) 
and find the sum of all digits that match the next digit in the list. 
The list is circular, so the digit after the last digit is the first digit in the list.

For example:

1122 produces a sum of 3 (1 + 2) because the first digit (1) matches the second digit and the third digit (2) matches the fourth digit.
1111 produces 4 because each digit (all 1) matches the next.
1234 produces 0 because no digit matches the next.
91212129 produces 9 because the only digit that matches the next one is the last digit, 9.
What is the solution to your captcha?

--- Part Two ---

You notice a progress bar that jumps to 50% completion. Apparently, the door isn't yet satisfied, 
but it did emit a star as encouragement. The instructions change:

Now, instead of considering the next digit, it wants you to consider the digit halfway 
around the circular list. That is, if your list contains 10 items, only include a 
digit in your sum if the digit 10/2 = 5 steps forward matches it. 
Fortunately, your list has an even number of elements.

For example:

1212 produces 6: the list contains 4 items, and all four digits match the digit 2 items ahead.
1221 produces 0, because every comparison is between a 1 and a 2.
123425 produces 4, because both 2s match each other, but no other digit has a match.
123123 produces 12.
12131415 produces 4.
What is the solution to your new captcha?
*)

let input = [|8; 7; 8; 9; 3; 8; 2; 3; 2; 1; 5; 7; 3; 4; 2; 7; 5; 6; 7; 5; 4; 2; 5; 4; 7; 1; 6; 5; 8; 
              6; 9; 7; 5; 1; 2; 5; 3; 9; 4; 8; 6; 5; 2; 9; 7; 3; 4; 9; 3; 2; 1; 2; 3; 6; 5; 8; 6; 5; 
              7; 4; 6; 6; 2; 9; 9; 4; 4; 2; 9; 8; 9; 4; 2; 5; 9; 8; 2; 8; 5; 3; 6; 8; 4; 2; 7; 8; 1; 
              1; 9; 9; 2; 5; 2; 1; 6; 9; 1; 8; 2; 7; 4; 3; 4; 4; 9; 4; 3; 5; 2; 3; 1; 1; 9; 4; 4; 3; 
              6; 3; 6; 8; 2; 1; 8; 5; 9; 9; 4; 6; 3; 3; 9; 1; 5; 4; 4; 4; 6; 1; 7; 4; 5; 4; 7; 2; 9; 
              2; 2; 9; 1; 6; 5; 6; 2; 4; 1; 4; 8; 5; 4; 2; 7; 5; 4; 4; 9; 9; 8; 3; 4; 4; 2; 8; 2; 8; 
              3; 4; 4; 4; 6; 3; 8; 9; 3; 6; 1; 8; 2; 8; 2; 4; 2; 5; 2; 4; 2; 6; 4; 3; 3; 2; 2; 8; 2; 
              2; 9; 1; 6; 8; 5; 7; 9; 3; 5; 2; 4; 2; 1; 4; 1; 6; 3; 6; 1; 8; 7; 8; 5; 9; 9; 1; 9; 6; 
              2; 6; 8; 8; 5; 7; 9; 1; 5; 7; 2; 2; 6; 8; 2; 7; 2; 4; 4; 2; 7; 1; 1; 9; 8; 8; 3; 6; 7; 
              7; 6; 2; 8; 6; 5; 7; 4; 1; 3; 4; 1; 4; 6; 7; 2; 7; 4; 7; 1; 8; 1; 4; 9; 2; 5; 5; 1; 7; 
              3; 6; 8; 6; 8; 3; 9; 2; 6; 5; 8; 7; 4; 1; 8; 4; 1; 7; 6; 9; 8; 5; 5; 6; 1; 9; 9; 6; 4; 
              5; 4; 2; 5; 3; 1; 6; 5; 7; 8; 4; 1; 9; 2; 9; 2; 9; 4; 5; 3; 6; 7; 8; 3; 2; 6; 9; 3; 7; 
              7; 2; 8; 5; 7; 1; 7; 8; 1; 2; 1; 2; 1; 5; 5; 3; 4; 6; 5; 9; 2; 4; 3; 2; 8; 7; 4; 2; 4; 
              4; 7; 4; 1; 8; 1; 6; 1; 6; 6; 3; 2; 8; 6; 9; 3; 9; 5; 8; 5; 2; 9; 9; 3; 8; 3; 6; 7; 5; 
              7; 5; 6; 6; 9; 6; 6; 3; 2; 2; 8; 3; 3; 5; 5; 6; 6; 4; 3; 5; 2; 7; 3; 4; 8; 4; 3; 3; 1; 
              4; 5; 2; 8; 8; 3; 1; 7; 5; 9; 8; 1; 9; 5; 5; 6; 7; 9; 3; 3; 5; 3; 2; 7; 2; 3; 1; 9; 9; 
              5; 4; 5; 2; 2; 3; 1; 1; 1; 8; 9; 3; 6; 3; 9; 3; 1; 9; 2; 5; 8; 3; 3; 3; 8; 2; 2; 2; 5; 
              9; 5; 9; 8; 2; 5; 2; 2; 8; 3; 3; 4; 6; 8; 5; 3; 3; 2; 6; 2; 2; 2; 4; 8; 7; 4; 6; 3; 7; 
              4; 4; 9; 6; 2; 4; 6; 4; 4; 3; 1; 8; 4; 1; 8; 7; 4; 8; 6; 1; 7; 9; 4; 9; 4; 1; 7; 9; 3; 
              9; 2; 2; 8; 9; 8; 8; 2; 9; 3; 3; 9; 1; 9; 4; 1; 4; 5; 7; 7; 2; 2; 6; 4; 1; 9; 3; 6; 4; 
              1; 7; 4; 5; 6; 2; 4; 3; 8; 9; 4; 1; 8; 2; 6; 6; 8; 1; 9; 7; 1; 7; 4; 2; 5; 5; 7; 8; 6; 
              4; 4; 5; 9; 9; 4; 5; 6; 7; 4; 7; 7; 5; 8; 2; 7; 1; 5; 6; 9; 2; 3; 3; 6; 2; 4; 9; 2; 4; 
              3; 2; 5; 4; 7; 1; 1; 6; 5; 3; 5; 2; 9; 8; 7; 1; 3; 3; 6; 1; 2; 9; 8; 2; 5; 7; 3; 5; 2; 
              4; 9; 6; 6; 7; 4; 2; 5; 2; 3; 8; 5; 7; 3; 9; 5; 2; 3; 3; 9; 9; 2; 2; 9; 4; 8; 2; 1; 4; 
              2; 1; 8; 8; 7; 2; 4; 1; 7; 8; 5; 8; 5; 2; 5; 1; 9; 9; 6; 4; 2; 1; 9; 4; 5; 8; 8; 4; 4; 
              8; 5; 4; 3; 5; 6; 5; 4; 7; 4; 8; 4; 7; 2; 7; 2; 9; 8; 4; 2; 3; 2; 6; 3; 7; 4; 6; 6; 6; 
              6; 4; 6; 9; 5; 2; 1; 7; 1; 7; 6; 3; 5; 8; 2; 8; 3; 7; 8; 8; 7; 8; 1; 8; 4; 3; 1; 7; 1; 
              6; 3; 6; 8; 4; 1; 2; 1; 5; 6; 7; 5; 8; 5; 1; 7; 7; 8; 9; 8; 4; 6; 1; 9; 3; 7; 7; 5; 7; 
              5; 6; 9; 6; 4; 4; 7; 3; 6; 6; 8; 4; 4; 8; 5; 4; 2; 8; 9; 5; 3; 4; 2; 1; 5; 2; 8; 6; 9; 
              5; 9; 7; 2; 7; 6; 8; 8; 4; 1; 9; 7; 3; 1; 9; 7; 6; 6; 3; 1; 3; 2; 3; 8; 3; 3; 8; 9; 2; 
              2; 4; 7; 4; 3; 8; 1; 4; 9; 8; 2; 9; 9; 7; 5; 8; 5; 6; 1; 6; 1; 7; 5; 5; 1; 2; 2; 8; 5; 
              7; 6; 4; 3; 7; 3; 1; 9; 4; 5; 9; 1; 3; 3; 3; 5; 5; 5; 6; 2; 8; 8; 8; 1; 7; 1; 1; 2; 9; 
              9; 3; 9; 1; 1; 6; 9; 4; 9; 7; 2; 6; 6; 7; 6; 5; 6; 9; 1; 4; 2; 3; 8; 9; 9; 9; 2; 9; 1; 
              8; 3; 1; 9; 9; 7; 1; 6; 3; 4; 1; 2; 5; 4; 8; 9; 7; 7; 6; 4; 9; 4; 9; 1; 2; 2; 7; 2; 1; 
              9; 4; 7; 7; 7; 9; 6; 1; 2; 4; 1; 3; 4; 9; 5; 8; 5; 2; 7; 8; 4; 3; 2; 1; 3; 8; 2; 4; 7; 
              9; 2; 6; 8; 5; 1; 1; 7; 6; 9; 6; 6; 3; 1; 5; 1; 2; 1; 4; 1; 2; 4; 1; 4; 9; 6; 4; 5; 1; 
              8; 4; 5; 7; 5; 8; 6; 5; 5; 2; 7; 6; 1; 8; 6; 5; 9; 7; 7; 2; 4; 7; 4; 8; 4; 3; 2; 9; 9; 
              6; 2; 7; 6; 4; 9; 8; 5; 2; 7; 9; 1; 1; 2; 9; 2; 5; 3; 1; 1; 8; 5; 2; 9; 2; 1; 4; 9; 9; 
              4; 8; 1; 3; 9; 7; 2; 4; 3; 4; 5; 8; 4; 1; 5; 8; 4; 7; 8; 2; 3; 5; 2; 2; 1; 4; 9; 2; 1; 
              6; 3; 4; 8; 5; 8; 7; 3; 4; 6; 7; 1; 1; 1; 8; 4; 9; 5; 4; 2; 4; 1; 4; 3; 4; 3; 7; 2; 8; 
              2; 9; 7; 9; 2; 4; 3; 3; 4; 7; 8; 3; 1; 2; 5; 8; 2; 8; 5; 8; 5; 1; 2; 5; 9; 5; 7; 9; 1; 
              3; 3; 4; 3; 3; 1; 8; 2; 3; 8; 7; 4; 4; 4; 6; 5; 6; 3; 8; 6; 6; 7; 9; 8; 3; 1; 5; 8; 4; 
              9; 3; 3; 3; 9; 7; 9; 1; 5; 1; 3; 2; 7; 8; 5; 4; 1; 1; 6; 8; 6; 6; 8; 8; 4; 4; 7; 7; 3; 
              1; 6; 9; 6; 7; 7; 6; 4; 5; 9; 6; 2; 1; 9; 2; 4; 8; 2; 1; 6; 6; 7; 1; 1; 2; 7; 5; 1; 7; 
              8; 9; 8; 8; 4; 9; 8; 7; 8; 8; 3; 9; 9; 1; 8; 4; 5; 8; 1; 8; 5; 1; 3; 2; 4; 9; 9; 9; 4; 
              7; 6; 7; 5; 4; 3; 5; 2; 6; 1; 6; 9; 4; 6; 3; 7; 6; 6; 9; 7; 5; 7; 9; 1; 4; 6; 4; 7; 5; 
              6; 5; 2; 6; 9; 1; 1; 5; 8; 7; 3; 9; 9; 7; 6; 4; 7; 3; 6; 5; 5; 7; 9; 5; 9; 4; 6; 4; 9; 
              2; 3; 3; 5; 3; 8; 9; 6; 9; 2; 1; 3; 4; 2; 9; 4; 4; 8; 2; 1; 8; 3; 3; 9; 9; 1; 4; 5; 7; 
              1; 2; 5; 2; 5; 6; 3; 2; 9; 5; 6; 4; 4; 8; 9; 6; 3; 1; 3; 5; 2; 2; 6; 8; 7; 2; 2; 4; 5; 
              7; 6; 2; 8; 5; 1; 4; 5; 6; 4; 1; 2; 8; 2; 3; 1; 4; 8; 7; 3; 8; 2; 1; 1; 1; 6; 8; 2; 9; 
              7; 6; 8; 8; 6; 8; 3; 8; 1; 9; 2; 4; 1; 2; 9; 9; 6; 9; 3; 2; 9; 2; 4; 3; 7; 3; 3; 3; 7; 
              5; 2; 4; 2; 6; 2; 1; 3; 5; 3; 9; 9; 2; 5; 6; 6; 5; 8; 6; 3; 8; 4; 1; 8; 5; 1; 5; 2; 3; 
              9; 8; 7; 6; 7; 3; 2; 8; 6; 6; 5; 9; 6; 7; 3; 1; 8; 8; 8; 7; 7; 9; 5; 3; 2; 5; 7; 3; 2; 
              4; 3; 7; 1; 3; 1; 2; 8; 2; 3; 8; 4; 1; 9; 2; 3; 4; 9; 6; 3; 1; 9; 5; 5; 8; 9; 9; 8; 7; 
              5; 3; 9; 4; 6; 7; 2; 2; 1; 5; 1; 7; 5; 3; 5; 2; 7; 2; 3; 8; 4; 8; 9; 9; 5; 2; 4; 3; 8; 
              6; 2; 6; 7; 2; 6; 8; 9; 5; 9; 4; 8; 4; 8; 8; 1; 3; 7; 9; 9; 4; 4; 7; 9; 6; 3; 9; 2; 2; 
              5; 5; 4; 1; 9; 8; 3; 8; 7; 4; 3; 1; 6; 4; 7; 1; 4; 2; 7; 5; 4; 6; 3; 4; 5; 9; 3; 5; 1; 
              7; 4; 1; 2; 9; 6; 5; 8; 6; 4; 6; 5; 2; 1; 3; 6; 8; 9; 8; 5; 3; 7; 4; 3; 8; 5; 6; 5; 1; 
              8; 5; 8; 3; 4; 5; 1; 8; 4; 9; 6; 6; 1; 5; 9; 2; 8; 4; 4; 8; 7; 9; 2; 6; 4; 1; 9; 6; 7; 
              6; 1; 8; 6; 7; 4; 8; 1; 2; 5; 8; 7; 7; 8; 3; 9; 3; 6; 2; 3; 5; 8; 4; 8; 8; 4; 5; 3; 5; 
              2; 4; 6; 2; 3; 9; 7; 9; 4; 1; 7; 8; 9; 8; 1; 3; 8; 7; 6; 3; 2; 3; 1; 1; 2; 3; 8; 1; 1; 
              5; 3; 6; 2; 1; 7; 8; 5; 7; 6; 8; 9; 9; 1; 2; 1; 4; 2; 5; 4; 2; 8; 1; 1; 4; 6; 9; 6; 1; 
              5; 8; 6; 5; 2; 9; 7; 6; 2; 7; 7; 3; 9; 2; 2; 2; 4; 2; 2; 6; 2; 6; 8; 2; 4; 2; 3; 3; 2; 
              5; 8; 9; 5; 4; 6; 7; 5; 7; 4; 7; 7; 6; 8; 3; 3; 9; 8; 2; 6; 4; 2; 9; 4; 9; 2; 9; 4; 4; 
              2; 5; 9; 2; 1; 3; 1; 9; 4; 9; 3; 9; 8; 2; 6; 1; 8; 8; 4; 5; 4; 8; 4; 2; 7; 9; 5; 1; 4; 
              7; 2; 1; 2; 8; 8; 4; 1; 3; 2; 8; 3; 7; 6; 8; 1; 9; 2; 4; 1; 9; 5; 5; 1; 5; 3; 4; 2; 3; 
              4; 5; 2; 5; 3; 1; 5; 3; 8; 4; 1; 3; 4; 9; 2; 5; 7; 7; 2; 6; 2; 3; 4; 8; 3; 6; 9; 5; 8; 
              1; 3; 9; 9; 9; 2; 5; 6; 4; 7; 6; 2; 4; 6; 2; 3; 8; 6; 8; 2; 9; 9; 4; 6; 8; 4; 3; 6; 8; 
              5; 9; 6; 6; 7; 1; 5; 2; 4; 6; 3; 9; 7; 4; 9; 4; 9; 4; 3; 6; 3; 5; 9; 5; 8; 9; 9; 3; 1; 
              1; 3; 6; 2; 3; 6; 2; 4; 7; 9; 2; 9; 5; 5; 4; 8; 9; 9; 6; 7; 9; 1; 3; 9; 7; 4; 6; 1; 6; 
              2; 5; 5; 4; 1; 8; 3; 8; 5; 5; 2; 7; 8; 7; 1; 3; 5; 7; 4; 2; 4; 4; 2; 1; 1; 8; 5; 4; 2; 
              2; 7; 8; 2; 9; 9; 6; 9; 4; 4; 3; 1; 5; 1; 4; 7; 8; 9; 8; 6; 4; 1; 3; 3; 3; 3; 4; 2; 9; 
              1; 4; 4; 7; 9; 6; 6; 6; 4; 4; 2; 3; 7; 5; 4; 8; 1; 8; 2; 5; 6; 1; 7; 2; 8; 6; 2; 8; 1; 
              2; 8; 7; 7; 6; 8; 8; 6; 7; 5; 5; 1; 4; 1; 4; 2; 2; 6; 5; 2; 3; 9; 9; 9; 2; 5; 2; 9; 7; 
              7; 6; 2; 6; 2; 8; 4; 4; 3; 2; 9; 1; 8; 8; 2; 1; 8; 1; 8; 9; 2; 5; 4; 4; 9; 1; 2; 3; 8; 
              9; 5; 6; 4; 9; 7; 5; 6; 8|]
let testInput1 = [|1;2;1;2|]
let testInput2 = [|1;2;2;1|]
let testInput3 = [|1;2;3;4;2;5|]
let testInput4 = [|1;2;3;1;2;3|]
let testInput5 = [|1;2;1;3;1;4;1;5|]

let sum (input: int list) : int =
    let mutable acc = 0
    let sc = input.Length / 2
    let step (i: int) (v: int) : unit =
        let v' = 
            if i + sc >= input.Length
            then input.[(i + sc) - input.Length] 
            else input.[i + sc]
        if v = v' then acc <- acc + v
        ()

    List.iteri step input
    acc

printfn "Solution for %A = %d" testInput1 (sum (List.ofArray testInput1))
printfn "Solution for %A = %d" testInput2 (sum (List.ofArray testInput2))
printfn "Solution for %A = %d" testInput3 (sum (List.ofArray testInput3))
printfn "Solution for %A = %d" testInput4 (sum (List.ofArray testInput4))
printfn "Solution for %A = %d" testInput5 (sum (List.ofArray testInput5))

printfn "Solution for Captcha = %d" (sum (List.ofArray input))