module CS220.Arith

/// Please use the builder to construct expressions.
open DeBruijnBuilder

/// True (λ λ 2).
let t: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// False (λ λ 1).
let f: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// If-then-else. (λ λ λ ((3 2) 1))
let ite: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Pair. (λ λ λ ((1 3) 2))
let pair: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Car. (λ (1 λ λ 2))
let fst: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Cdr. (λ (1 λ λ 1))
let snd: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Zero (ル ル 1).
let zero = f (* Zero = False *)

/// Successor (λ λ λ (2 ((3 2) 1))).
let succ: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// IsZero?
let isZero: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Church numeral: One.
let one: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Church numeral: Two.
let two: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Church numeral: Three.
let three: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Church numeral: Four.
let four: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Church numeral: Five.
let five: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Church numeral: Six.
let six: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Church numeral: Seven.
let seven: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Church numeral: Eight.
let eight: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Church numeral: Nine.
let nine: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Addition.
let add: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Predecessor.
let pred: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Subtraction.
let sub: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Convert a Church number to a natural number.
/// val toNatural: DeBruijnExpr -> int
let toNatural (dexpr: DeBruijnExpr) =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Multiplication.
let mul: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code

/// Factorial.
let factorial: DeBruijnExpr =
  failwith "IMPLEMENT" // REMOVE this line when you implement your own code
