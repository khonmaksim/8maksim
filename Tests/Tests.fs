namespace CS220

open CS220.DeBruijnBuilder

open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestClass () =
  let parser = ExprParser ()

  [<TestMethod; Timeout 1000; TestCategory "1">]
  member _.``Basic Expr Parsing Test`` () =
    match "(\x. (\y. (x y)))" |> parser.Run with
    | Ok (expr, dexpr) ->
      let expectedExpr = Lambda ("x", Lambda ("y", Apply (Var "x", Var "y")))
      let expectedDExpr: DeBruijnExpr = abs (abs (ref 2 $ ref 1))
      Assert.AreEqual<Expr> (expectedExpr, expr)
      Assert.AreEqual<DeBruijnExpr> (expectedDExpr, dexpr)
    | Error msg -> Assert.Fail msg

  [<TestMethod; Timeout 1000; TestCategory "1">]
  member _.``Value Test (true)`` () =
    let e = Arith.t
    let nf: DeBruijnExpr = e |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (e, nf)

  [<TestMethod; Timeout 1000; TestCategory "1">]
  member _.``Value Test (false)`` () =
    let e = Arith.f
    let nf: DeBruijnExpr = e |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (e, nf)

  [<TestMethod; Timeout 1000; TestCategory "2">]
  member _.``Value Test (ite)`` () =
    let e = Arith.ite
    let nf: DeBruijnExpr = e |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (e, nf)

  [<TestMethod; Timeout 1000; TestCategory "2">]
  member _.``Value Test (ite 2)`` () =
    let res: DeBruijnExpr =
      Arith.ite $ Arith.t $ Arith.f $ Arith.t |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (Arith.f, res)
    let res: DeBruijnExpr =
      Arith.ite $ Arith.f $ Arith.f $ Arith.t |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (Arith.t, res)

  [<TestMethod; Timeout 1000; TestCategory "3">]
  member _.``Value Test (pair 1)`` () =
    let e = Arith.pair
    let nf: DeBruijnExpr = e |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (e, nf)
    let e = Arith.fst
    let nf: DeBruijnExpr = e |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (e, nf)
    let e = Arith.snd
    let nf: DeBruijnExpr = e |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (e, nf)

  [<TestMethod; Timeout 1000; TestCategory "3">]
  member _.``Value Test (pair 2)`` () =
    let x = Arith.pair $ Arith.t $ Arith.f
    let res1: DeBruijnExpr = Arith.fst $ x |> Evaluate.nf
    let res2: DeBruijnExpr = Arith.snd $ x |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (Arith.t, res1)
    Assert.AreEqual<DeBruijnExpr> (Arith.f, res2)

  [<TestMethod; Timeout 1000; TestCategory "4">]
  member _.``Value Test (basic arith)`` () =
    let e = Arith.zero
    let nf: DeBruijnExpr = e |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (e, nf)
    let e = Arith.succ
    let nf: DeBruijnExpr = e |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (e, nf)
    let e = Arith.isZero
    let nf: DeBruijnExpr = e |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (e, nf)

  [<TestMethod; Timeout 1000; TestCategory "4">]
  member _.``Value Test (isZero)`` () =
    let e: DeBruijnExpr = Arith.isZero $ Arith.zero |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (Arith.t, e)
    let e: DeBruijnExpr =
      Arith.isZero $ (Arith.succ $ Arith.zero) |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (Arith.f, e)

  [<TestMethod; Timeout 1000; TestCategory "5">]
  member _.``Value Test (Church numerals 1)`` () =
    let e: DeBruijnExpr = Arith.succ $ Arith.zero |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (Arith.one, e)
    let e: DeBruijnExpr = Arith.succ $ Arith.one |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (Arith.two, e)
    let e: DeBruijnExpr = Arith.succ $ Arith.two |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (Arith.three, e)
    let e: DeBruijnExpr = Arith.succ $ Arith.three |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (Arith.four, e)
    let e: DeBruijnExpr = Arith.succ $ Arith.four |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (Arith.five, e)

  [<TestMethod; Timeout 1000; TestCategory "5">]
  member _.``Value Test (Church numerals 2)`` () =
    let e: DeBruijnExpr =
      Arith.ite $ Arith.t $ Arith.four $ Arith.two |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (Arith.four, e)
    let e: DeBruijnExpr =
      Arith.ite $ Arith.f $ Arith.four $ Arith.two |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (Arith.two, e)

  [<TestMethod; Timeout 1000; TestCategory "6">]
  member _.``Value Test (Church numerals addition)`` () =
    let e = Arith.add
    let nf: DeBruijnExpr = e |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (e, nf)
    let e: DeBruijnExpr = Arith.add $ Arith.two $ Arith.three |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (Arith.five, e)
    let e: DeBruijnExpr = Arith.add $ Arith.two $ Arith.zero |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (Arith.two, e)

  [<TestMethod; Timeout 1000; TestCategory "6">]
  member _.``Value Test (Church numerals pred)`` () =
    let e = Arith.pred
    let nf: DeBruijnExpr = e |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (e, nf)
    let e: DeBruijnExpr = Arith.pred $ Arith.four |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (Arith.three, e)
    let e1: DeBruijnExpr = Arith.pred $ Arith.six |> Evaluate.nf
    let e2: DeBruijnExpr = Arith.succ $ Arith.four |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (e1, e2)

  [<TestMethod; Timeout 1000; TestCategory "6">]
  member _.``Value Test (Church numerals subtraction)`` () =
    let e = Arith.sub
    let nf: DeBruijnExpr = e |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (e, nf)
    let e: DeBruijnExpr = Arith.sub $ Arith.eight $ Arith.three |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (Arith.five, e)
    let e: DeBruijnExpr = Arith.sub $ Arith.seven $ Arith.seven |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (Arith.zero, e)

  [<TestMethod; Timeout 1000; TestCategory "7">]
  member _.``Value Test (Church numerals to natural number)`` () =
    let n: int = Arith.toNatural Arith.three
    Assert.AreEqual<int> (3, n)
    let n: int = Arith.toNatural Arith.zero
    Assert.AreEqual<int> (0, n)
    let n = Arith.add $ Arith.nine $ Arith.eight |> Evaluate.nf
    let n: int = Arith.toNatural n
    Assert.AreEqual<int> (17, n)

  [<TestMethod; Timeout 1000; TestCategory "8">]
  member _.``Value Test (Church numerals multiplication)`` () =
    let e = Arith.mul
    let nf: DeBruijnExpr = e |> Evaluate.nf
    Assert.AreEqual<DeBruijnExpr> (e, nf)
    let e = Arith.mul $ Arith.six $ Arith.seven |> Evaluate.nf
    let n: int = Arith.toNatural e
    Assert.AreEqual<int> (42, n)
    let e = Arith.mul $ Arith.zero $ Arith.eight |> Evaluate.nf
    let n: int = Arith.toNatural e
    Assert.AreEqual<int> (0, n)
    let e = Arith.mul $ Arith.eight $ Arith.zero |> Evaluate.nf
    let n: int = Arith.toNatural e
    Assert.AreEqual<int> (0, n)

  [<TestMethod; Timeout 1000; TestCategory "9">]
  member _.``Value Test (factorial)`` () =
    let v: int = Arith.factorial $ Arith.five |> Evaluate.nf |> Arith.toNatural
    Assert.AreEqual<int> (120, v)