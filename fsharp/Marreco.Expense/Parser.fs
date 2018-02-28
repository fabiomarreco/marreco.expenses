namespace Marreco.Expense
(*
module Parser =
    open Regex
    open OFXParser
    let loadFile = function
        | Regex ".+\.ofx^" [ file ] -> 
                let parser = new OFXParser.Parser()
                let settings = new OFXParser.ParserSettings()
                let ofx = OFXParser.Parser.GetExtract(file, settings)
                ofx.
        | _ -> ""
        
*)