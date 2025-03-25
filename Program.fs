module CS220.Program

open System

let rec parseLoop (parser: ExprParser) =
  Console.Write ("> ")
  let line = Console.ReadLine ()
  if line.StartsWith "q" then ()
  elif String.IsNullOrEmpty line then parseLoop parser
  else
    match parser.Run line with
    | Ok (expr, dexpr) ->
      expr |> Expr.toString |> Console.WriteLine
      dexpr |> DeBruijnExpr.toString |> Console.WriteLine
      (Evaluate.nf dexpr) |> DeBruijnExpr.toString |> Console.WriteLine
    | Error (msg) -> Console.WriteLine ("Failed to parse: {0}", msg)
    parseLoop parser

/// Read the README.md before you proceed.
[<EntryPoint>]
let main _args =
  Console.OutputEncoding <- Text.Encoding.Unicode
  parseLoop (ExprParser ())
  0
