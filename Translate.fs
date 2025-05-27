module CS220.Translate

open CS220.Expr
open CS220.DeBruijnExpr

exception UnknownIdentifierException of string

/// Convert a named-variable Expr to a De Bruijn indexed expression (1-based indices)
let toDebruijn (expr: Expr) : DeBruijnExpr =
    let rec walk env e =
        match e with
        | Var name ->
            match List.tryFindIndex ((=) name) env with
            | Some idx -> Ref (idx + 1)
            | None -> raise (UnknownIdentifierException name)
        | Lambda (name, body) ->
            // push the binder name at head
            let newEnv = name :: env
            Abs (walk newEnv body)
        | Apply (f, x) ->
            App (walk env f, walk env x)
    walk [] expr

/// Convert a De Bruijn indexed expression back to a named-variable Expr
let toExpr (dexpr: DeBruijnExpr) : Expr =
    let rec walk env depth d =
        match d with
        | Ref idx when idx >= 1 && idx <= List.length env ->
            // 1-based index: 1 refers to head of env
            Var (List.item (idx - 1) env)
        | Ref idx ->
            // invalid index
            raise (UnknownIdentifierException (sprintf "%d" idx))
        | Abs body ->
            // generate a fresh variable name
            let varName = sprintf "v%d" depth
            let newEnv = varName :: env
            Lambda (varName, walk newEnv (depth + 1) body)
        | App (f, x) ->
            Apply (walk env depth f, walk env depth x)
    walk [] 0 dexpr
