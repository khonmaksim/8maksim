module CS220.Arith

/// Please use the builder to construct expressions.
open CS220.DeBruijnBuilder
open CS220.DeBruijnExpr
open CS220.Evaluate
/// True (λ λ 2).
let t: DeBruijnExpr =
  abs (abs (ref 2))

/// False (λ λ 1).
let f: DeBruijnExpr =
  abs (abs (ref 1))

/// If-then-else. (λ λ λ ((3 2) 1))
let ite: DeBruijnExpr =
  abs (abs (abs (
    (ref 3 $ ref 2) $ ref 1
  )) )

/// Pair. (λ λ λ ((1 3) 2))
let pair: DeBruijnExpr =
  abs (abs (abs (
    (ref 1 $ ref 3) $ ref 2
  )) )

/// Car. (λ (1 λ λ 2))
let fst: DeBruijnExpr =
  abs (
    ref 1 $ abs (abs (ref 2))
  )

/// Cdr. (λ (1 λ λ 1))
let snd: DeBruijnExpr =
  abs (
    ref 1 $ abs (abs (ref 1))
  )

/// Zero (λ λ 1).
let zero: DeBruijnExpr =
  abs (abs (ref 1))

/// Successor (λ λ λ (2 ((3 2) 1))).
let succ: DeBruijnExpr =
  abs (abs (abs (
    ref 2 $ ((ref 3 $ ref 2) $ ref 1)
  )))
let isZero: DeBruijnExpr =
  abs (
    (ref 1 $ (abs f)) $ t
  )

/// Church numeral: One.
let one: DeBruijnExpr =
  abs (abs (ref 2 $ ref 1))

/// Church numeral: Two.
let two: DeBruijnExpr =
  abs (abs (ref 2 $ (ref 2 $ ref 1)))

/// Church numeral: Three.
let three: DeBruijnExpr =
  abs (abs (ref 2 $ (ref 2 $ (ref 2 $ ref 1))))

/// Church numeral: Four.
let four: DeBruijnExpr =
  abs (abs (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ ref 1)))))

/// Church numeral: Five.
let five: DeBruijnExpr =
  abs (abs (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ ref 1))))))

/// Church numeral: Six.
let six: DeBruijnExpr =
  abs (abs (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ ref 1)))))))

/// Church numeral: Seven.
let seven: DeBruijnExpr =
  abs (abs (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ ref 1))))))))

/// Church numeral: Eight.
let eight: DeBruijnExpr =
  abs (abs (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ ref 1)))))))))

/// Church numeral: Nine.
let nine: DeBruijnExpr =
  abs (abs (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ (ref 2 $ ref 1))))))))))


/// Addition. Church add = λm.λn.λf.λx. m f (n f x)
let add : DeBruijnExpr =
  abs (    // m
  abs (    // n
  abs (    // f
  abs (    // x
    // m f (n f x)
    (ref 4 $ ref 2) $ ((ref 3 $ ref 2) $ ref 1)
  ))))

/// Predecessor. λn.λf.λx. n (λg.λh. h (g f)) (λu. x) (λu. u)
let pred: DeBruijnExpr =
  abs (abs (abs (
    (ref 3 $
      abs (abs (
        ref 1 $ (ref 2 $ ref 4)
      ))
    ) $
    abs (ref 2) $
    abs (ref 1)
  )))

/// Subtraction. sub = λm.λn. n pred m
let sub: DeBruijnExpr =
  abs (abs (
    (ref 1 $ pred) $ ref 2
  ))

/// Convert a Church number to a natural number.
/// val toNatural: DeBruijnExpr -> int
let toNatural (dexpr: DeBruijnExpr) =
    // normalize to ensure it's in standard form
    let normal = nf dexpr
    match normal with
    | Abs (Abs body) ->
        let rec go expr acc =
            match expr with
            | App (Ref 2, tail) -> go tail (acc + 1)
            | Ref 1 -> acc
            | _ -> failwith "toNatural: not a Church numeral"
        go body 0
    | _ -> failwith "toNatural: not a Church numeral"

/// Multiplication.
let mul: DeBruijnExpr =
  abs (abs (abs (abs (
    (ref 4 $ (ref 3 $ ref 2)) $ ref 1
  ))))

/// Factorial using the Y combinator:
/// Y = λh. (λx. h (x x)) (λx. h (x x))
/// F = λf.λn. ite (isZero n) one (mul n (f (pred n)))
let factorial: DeBruijnExpr =
  // Y combinator
  (abs (
    (abs (
      ref 2 $ (ref 1 $ ref 1)
    ))
    $ abs (
        ref 2 $ (ref 1 $ ref 1)
    )
  ))
  $
  // F helper function
  (abs (abs (
    // apply ite to cond, then branch, else branch
    ((ite $ (isZero $ ref 1)) $ one) $ (mul $ ref 1 $ (ref 2 $ pred $ ref 1))
  )))
