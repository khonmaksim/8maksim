namespace CS220

/// This type represents expressions represented by "De Bruijn index":
/// https://en.wikipedia.org/wiki/De_Bruijn_index
type DeBruijnExpr =
  | Ref of int
  | Abs of DeBruijnExpr
  | App of DeBruijnExpr * DeBruijnExpr

module DeBruijnExpr =
  let rec toString = function
    | Ref n -> n.ToString ()
    | Abs e -> "\u03bb" + " " + toString e
    | App (f, e) -> "(" + toString f + " " + toString e + ")"

  /// Check if the given DeBruijnExpr is valid at depth n.
  let rec private isValidAtDepth n = function
    | Ref m -> m < n
    | Abs e -> isValidAtDepth (n + 1) e
    | App (f, e) -> isValidAtDepth n f && isValidAtDepth n e

  let isValid e = isValidAtDepth 1 e

/// It is a good practice to build expressions only through this builder. Do not
/// directly construct expressions, but use this module.
module DeBruijnBuilder =

  let ref n =
    failwith "IMPLEMENT" // REMOVE this line when you implement your own code

  let abs e =
    failwith "IMPLEMENT" // REMOVE this line when you implement your own code

  let app f e =
    failwith "IMPLEMENT" // REMOVE this line when you implement your own code

  let ($) f e = app f e

type Expr =
  | Var of string
  | Lambda of string * Expr
  | Apply of Expr * Expr

module Expr =
  let rec toString = function
    | Var n -> n.ToString ()
    | Lambda (v, e) -> "(\u03bb" + v.ToString () + "." + toString e + ")"
    | Apply (f, e) -> "(" + toString f + " " + toString e + ")"
