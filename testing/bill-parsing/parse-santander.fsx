#r @"./packages/Deedle/lib/net40/Deedle.dll"
#r @"./packages/FSharp.Data/lib/net45/FSharp.Data.dll"
#load @"./packages/Deedle/Deedle.fsx"
open System
open System.IO
open System.Text
open System.Text.RegularExpressions

open System.Globalization

open FSharp.Data
open Deedle

type Fatura = HtmlProvider<"C:/Users/fabio.marreco/Desktop/fatura/Santander _ IBPF.html">
let fatura = Fatura.Load("C:/Users/fabio.marreco/Desktop/fatura/Santander _ IBPF.html")
let ``pt-BR`` = CultureInfo.GetCultureInfo("pt-BR")

let (|FormatedDate|_|) culture str = 
    match (DateTime.TryParse(str, culture, DateTimeStyles.AssumeLocal)) with
    | true, dt -> Some (dt)
    | _ -> None

let (|FormatedDecimal|_|) culture str = 
    match Decimal.TryParse
                (str, NumberStyles.AllowThousands ||| NumberStyles.AllowDecimalPoint ||| NumberStyles.AllowLeadingSign, culture) with
    | true, v -> Some(v)
    | _ -> None


type Money = | BRL of decimal | USD of decimal
type Expense =  { Date : DateTime; Description : string; Value : Money; Reais : decimal }
 


let expenses = 
    let cotacao = match fatura.Tables.Table3.Rows.[2].Column2 with | FormatedDecimal ``pt-BR`` value -> value | _ -> failwith "Invalid Exchange Rate"
    fatura.Tables.Table1.Rows 
    |> Seq.choose (fun r-> match (r.Data, r.``Valor (R$)``, r.``Valor (US$)``) with 
                           | (FormatedDate ``pt-BR`` dt,  FormatedDecimal ``pt-BR`` value, _) ->
                                 Some( { Date = dt; Description = r.Descrição; Value = BRL value; Reais = value }) 
                           | (FormatedDate ``pt-BR`` dt,  _, FormatedDecimal ``pt-BR`` value) ->
                                 Some( { Date = dt; Description = r.Descrição; Value = USD value; Reais =  cotacao * value }) 
                           | _ -> None)
    |> Frame.ofRecords



fatura.Tables.Table7


for row in fatura.Tables.Table1.Rows do
    printfn "%O" row.Data

    