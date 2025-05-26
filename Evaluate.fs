module CS220.Evaluate

/// Shift De Bruijn indices by d positions above cutoff c
let rec shift d c = function
  | Ref k -> 
      if k >= c then Ref (k + d) else Ref k
  | Abs body -> 
      Abs (shift d (c + 1) body)
  | App (func, arg) -> 
      App (shift d c func, shift d c arg)

/// Substitute expression s for variable j in expression t
let rec subst j s t =
  match t with
  | Ref k ->
      if k = j then s
      elif k > j then Ref (k - 1)
      else Ref k
  | Abs body ->
      Abs (subst (j + 1) (shift 1 0 s) body)
  | App (func, arg) ->
      App (subst j s func, subst j s arg)

/// Perform one step of beta reduction
let rec betaReduce = function
  | App (Abs body, arg) ->
      // Beta reduction: substitute arg for variable 1 in body, then shift down
      let substituted = subst 1 (shift 1 0 arg) body
      Some (shift (-1) 0 substituted)
  | App (func, arg) ->
      // Try to reduce function first
      match betaReduce func with
      | Some func' -> Some (App (func', arg))
      | None ->
          // If function can't be reduced, try argument
          match betaReduce arg with
          | Some arg' -> Some (App (func, arg'))
          | None -> None
  | Abs body ->
      // Try to reduce body
      match betaReduce body with
      | Some body' -> Some (Abs body')
      | None -> None
  | Ref _ -> None

/// Return the normal form of the given DeBruijnExpr
let rec nf e =
  match betaReduce e with
  | Some e' -> nf e'  // Continue reducing
  | None -> e         // Already in normal form
