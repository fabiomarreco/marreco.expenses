#r "paket: nuget FSharp.Data \
nuget XPlot.GoogleCharts \
nuget Deedle"
#load "./.fake/tests.fsx/intellisense.fsx"

open System
open System.Text.RegularExpressions
open XPlot.GoogleCharts
open Deedle
open FSharp.Data

[<Literal>] 
let SampleCSV = "0;-22.90998403,-43.18215881;Santander;SANTANDER INFORMA;{SANTANDER}Santander Informa: Transferencia entre contas a debito efetuada em 11/06/18 as 09:36 no valor de R$ 466,00
1528733473662;-22.90943578,-43.18260439;Way;COMPRA;{SANTANDER}Santander Informa: Compra APROVADA Cartao VISA final 7591 de R$ 11,00 em 11/06/18 as 13:11 CACAU SHOW";
[<Literal>] 
let Path = "C:/Users/fabio.marreco/Dropbox/tasker/sms/sms.txt";

let (|Regex|_|) pattern input = 
    let m = Regex.Match(input, pattern, RegexOptions.IgnorePatternWhitespace)
    if (m.Success) then [for g in m.Groups -> g.Value] |> List.skip 1 |> Some
    else  None

let (|DecimalStr|_|) input = 
    let (success, value) = Decimal.TryParse (input, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture.NumberFormat)
    if success then Some value
    else None

type AppMsg = CsvProvider<Sample= SampleCSV, Separators=";", HasHeaders=false, Schema="when (int64), geo (string) , App, type, message">



type Geo = { Lat: decimal; Long : decimal }
type AppMessage = { When : DateTimeOffset option; Where : Geo option; App: string; Type : string; Message: string }

let msgs = AppMsg.Load(Path).Rows 
           |> Seq.map (fun r -> { When = match r.When with | 0L -> None  | v -> DateTimeOffset.FromUnixTimeMilliseconds v |> Some;
                                  Where = match r.Geo with 
                                          | Regex (@"([-\d\.]+),([-\d\.]+)") [ DecimalStr lat; DecimalStr long ] -> Some { Lat = lat; Long = long } 
                                          | _ -> None
                                  App = r.App;
                                  Type = r.Type;
                                  Message = r.Message })




msgs  |> Frame.ofRecords |> Chart.Table

