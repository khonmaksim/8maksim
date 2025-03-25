namespace CS220

open FParsec

type ExprParser () =

  let isAllowedChar c = isAsciiLetter c || isDigit c

  let ws = spaces

  let paren p = between (pchar '(' >>. ws) (ws .>> pchar ')') p

  let pidentifier = many1Satisfy2L isAsciiLetter isAllowedChar "identifier"

  let pdot = pchar '.'

  let pexpr, pexprref = createParserForwardedToRef ()

  let plambda =
    let p = pchar '\\' >>. ws >>. pidentifier .>> ws .>> pdot .>> ws .>>. pexpr
    paren p
    |>> Lambda

  let papply =
    let p = pexpr .>> ws .>>. pexpr
    paren p
    |>> Apply

  let pvar =
    let p = pidentifier
    attempt (paren p) <|> p
    |>> Var

  (* Tying the knot *)
  do pexprref := choice [ attempt plambda; attempt papply; attempt pvar ]

  member __.Run line =
    match runParserOnString pexpr () "" line with
    | Success (expr, _, _) ->
      try
        let dexpr = Translate.toDebruijn expr
        if DeBruijnExpr.isValid dexpr then Result.Ok (expr, dexpr)
        else Result.Error "Invalid lambda expression is given."
      with Translate.UnknownIdentifierException id ->
        Result.Error ("Unknown identifier (" + id + ")")
    | Failure (err, _, _) -> Result.Error (err)
