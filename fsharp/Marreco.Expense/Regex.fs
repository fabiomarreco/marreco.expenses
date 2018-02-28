namespace Marreco.Expense
open System;

module Regex =
    let (|Regex|_|) pattern str  =
        let reg = Text.RegularExpressions.Regex(pattern)
        match reg.Match(str) with
        | m when not m.Success -> None
        | m -> [for g in m.Groups -> g.Value ] |> Some

