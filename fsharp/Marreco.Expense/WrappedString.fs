namespace Marreco.Expense
open System


module WrappedString = 
    let inline trim (s:string) = s.Trim()

    type String50 = private String50 of string with
        member this.Value = let (String50 s) = this in s
    
    type String100 = private String100 of string with
        member this.Value = let (String100 s) = this in s


    let string50 (str:string) = 
        match str.Trim() with    
        | s when s.Length > 1 && s.Length <= 50 -> Some(String50 s)
        | _ -> None


    let string100 =  trim  >> function | s when s.Length >1 && s.Length<= 100 -> Some (String100 s) | _ -> None
