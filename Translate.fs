module CS220.Translate

open CS220
open CS220.DeBruijnExpr

exception UnknownIdentifierException of string

/// Convert an Expr to a DeBruijnExpr
let toDebruijn (expr: Expr) : DeBruijnExpr =
    let rec build env = function
        | Expr.Var v ->
            match List.tryFindIndex ((=) v) env with
            | Some idx -> Ref (idx + 1)
            | None -> raise (UnknownIdentifierException v)
        | Expr.Lambda (v, body) ->
            Abs (build (v :: env) body)
        | Expr.Apply (f, e) ->
            App (build env f, build env e)
    build [] expr

/// Convert a DeBruijnExpr to an Expr
let toExpr (dexpr: DeBruijnExpr) : Expr =
    // Generate fresh variable names: v1, v2, v3, ...
    let rec build env nextName = function
        | Ref n ->
            if n <= List.length env then
                Expr.Var (List.item (n - 1) env), nextName
            else
                failwithf "Invalid DeBruijn index: %d" n
        | Abs body ->
            let name = sprintf "v%d" nextName
            let exprBody, nextName' = build (env @ [name]) (nextName + 1) body
            Expr.Lambda (name, exprBody), nextName'
        | App (f, e) ->
            let fExpr, nextName' = build env nextName f
            let eExpr, nextName'' = build env nextName' e
            Expr.Apply (fExpr, eExpr), nextName''
    fst (build [] 1 dexpr)
