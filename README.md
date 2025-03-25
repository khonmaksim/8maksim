CS220 - HW8
===

## Introduction

In this homework, you will learn the concept of lambda calculus and implement
some basic operations in lambda calculus. By implementing those lines annotated
with `failwith "IMPLEMENT"`, you should be able to pass all the tests.

The parsing part is already implemented for you using the `FParsec` library,
which is a parser combinator library for F#. You can consider it as an extension
of what you have learned in the lecture. Note that you don't need to fully
understand the parsing part, as it is not directly relevant to the homework.

I recommend you to read the Wiki page on lambda calculus before starting the
homework: https://en.wikipedia.org/wiki/Lambda_calculus.

#### Running the Project

`dotnet run` will launch a simple REPL for you, where you can type in a lambda
expression. After implementing all the functions, you should be able to see the
program outputs the following three lines for the given input string: (1)
pretty-printed line of lambda expression `Expr` constructed from the given input
string; (2) De Bruijn expression (https://en.wikipedia.org/wiki/De_Bruijn_index)
obtained by converting (1); and (3) the normal form of the given lambda
expression in `DebruijnExpr`. For example,

```
> (\x. x)
(λx.x)
λ 1
λ 1
```

The parser of a lambda expression is given. It always expects to get
fully-parenthesized expressions as input. Also, we use the backslash `\` to
represent the lambda symbol `λ`, and the dot `.` to distinguish the arguments
and body of a lambda term.

Note that you can always override the `main` function of the program to debug
your implementation. That is, you don't need to rely on the REPL to test your
implementation.

#### Lambda Calculus Basics

Lambda calculus is a formal system in mathematical logic for expressing
computation based on function abstraction and application.

The lambda symbol `λ` is an anonymous function in F#. For example, `λx.x`
denotes a function that takes an argument `x` and returns `x`. In F#, it will be
`fun x -> x`. In lambda calculus, we use such a simple notation to represent
values and computations.

The key point that you want to understand is that we can use lambda expressions
to represent numbers, arithmetic operations, conditional expressions, and so on.
And this homework is about implementing such operations using lambda
expressions.

#### How to Start?

You should start by understanding De Bruijn notation first. Read the Wikipedia
link and understand the concept. We have two separate types for regular lambda
expressions (`Expr`) and De Bruijn expressions (`DeBruijnExpr`). Note that `Var`
in `Expr` corresponds to `Ref` in DeBruijnExpr, `Lambda` corresponds to `Abs`,
and `Apply` corresponds to `App`.

Once you understand the concept of De Bruijn notation, you should start
implementing the conversion logic in `Translate.fs`. This will allow you to
translate `Expr` to `DeBruijnExpr` and vice versa.

Next, you can start implementing the normalization routine in `Evaluate.fs`. We
provide useful references as follows:

- https://en.wikipedia.org/wiki/Beta_normal_form
- Constructive Computation Theory (http://gallium.inria.fr/~huet/PUBLIC/CCT.pdf)

After that, you can define [Church
numerals](https://en.wikipedia.org/wiki/Church_encoding) and their arithmetic
operations with lambda expressions in `Arith.fs`.

#### FAQs

##### Q: Do I need to consider negative integers?

A: No. Just encode positive integers and zero using Church numerals.

##### Q: Do I need to modify the parser?

A: No. The current parser is to showcase that a simple REPL-like program can be
easily implemented using FParsec. You don't need to modify/improve the parser to
finish this homework. But, of course, you can modify it for your own practice.

## Build?

You can compile the project by typing `dotnet build`.

## Test?

You can test your implementation by typing `dotnet test`. If you can pass
all the tests, you should be good to commit and push your code.

## Structure

- `Program.fs`: The main entry point of the project.
- `Tests/Tests.fs`: The test suite for the project.
- `HomeworkX.fsproj`: The F# project file for the project, where `X` is the
  homework number.

## Problems

You should read the fsproj file and read the relevant source files to understand
the problems. Typically, we will provide an empty (or partial) implementation
for you to fill in where you can find some comments describing the problem. Of
course, you can also read the relevant test cases to understand the problem.

## Submission

You are given a GitHub classroom link to start the assignment, which will
automatically create a private repository for you. If your repository URL
contains your GitHub username, then you are in the right place. If not, please
get your own private repository from the GitHub classroom link.

You should make modification to your own repository to finish your homework.
Again, your own repository is only visible to you and the course staff. Plus,
your repository URL should contain your GitHub username. You should commit and
push your code to your own repository in order to get a grade.

Whenever you push your code to your repository, the GitHub classroom workflow
will automatically run the test cases and give you a grade based on the test
results. You can check the test results by clicking the "Actions" tab in your
repository page.

If you see a red cross in the "Actions" tab, it means that some of the test
cases are failing. You should check the test results to understand why the test
cases are failing. If you see a green check, it means that all the test cases
are passing, and you should be good to go.

You can always make another commit to fix problems in your implementation, and
push the changes to the repository. The GitHub classroom workflow will
re-evaluate your implementation and update your grade.

## Failure from the Workflow?

Don't panic if you see a red cross in the "Actions" tab. You can always make
another commit to fix problems in your implementation, and push the changes to
the repository. The GitHub classroom workflow will re-evaluate your
implementation and update your grade automatically.

## Auto-Grading?

Auto-grading is performed via the GitHub classroom workflow, which is defined in
`.github/workflows/classroom.yml`. The workflow basically runs `dotnet test` to
test your implementation and then gives you a grade based on the test results.
If you can pass all the tests in your local environment, you should be able to
get a full score.

## Cheating Policy

One may exploit the automatic grading system by hardcoding the expected result
in the test cases --- that is, one can simply add if-then-else statements in the
program to pass all the tests. But, we consider this attempt as cheating.

If we detect any cheating attempt (including but not limited to the one above),
we will immediately give you an F grade for the course, and report the case to
the department.

## Solutions?

There is no single correct solution to the problems. You can always come up with
your own solution as long as it can pass the test cases. If you have any
questions about the problems, feel free to write an issue in the
[main](https://github.com/KAIST-CS220/CS220-Main) repository. But please do
search the existing issues to see if your question has already been answered
before writing a new one.
