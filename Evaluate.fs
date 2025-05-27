module CS220.Evaluate

open CS220.DeBruijnExpr

/// Shift all free indices in expr by 'd', adjusting those > cutoff 'c'.
let rec shift d c expr =
    match expr with
    | Ref k when k > c -> Ref (k + d)
    | Ref k -> Ref k
    | Abs body -> Abs (shift d (c + 1) body)
    | App (f, x) -> App (shift d c f, shift d c x)

/// Substitute all occurrences of index 'j' with substitution 's' in expr.
/// Assumes 's' has been shifted appropriately for top-level substitution.
let rec subst j s expr =
    match expr with
    | Ref k when k = j -> s
    | Ref k when k > j -> Ref (k - 1)
    | Ref k -> Ref k
    | Abs body ->
        // under a binder, increase j and shift s
        Abs (subst (j + 1) (shift 1 0 s) body)
    | App (f, x) -> App (subst j s f, subst j s x)

/// Perform a single beta-reduction step if possible
let rec oneStep expr =
    match expr with
    | App (Abs body, arg) ->
        // beta-redex: substitute arg for index 1 in body
        let argShifted = shift 1 0 arg
        let substed = subst 1 argShifted body
        // shift back after substitution
        Some (shift -1 0 substed)
    | App (f, x) ->
        match oneStep f with
        | Some f' -> Some (App (f', x))
        | None ->
            match oneStep x with
            | Some x' -> Some (App (f, x'))
            | None -> None
    | Abs body ->
        match oneStep body with
        | Some b' -> Some (Abs b')
        | None -> None
    | Ref _ -> None

/// Return the normal form of the given DeBruijnExpr.
let rec nf e =
    match oneStep e with
    | Some e' -> nf e'
    | None -> e
