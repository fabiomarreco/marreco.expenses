namespace Marreco.Expense
open System



module CardId = 
    open System.Text.RegularExpressions;
    type T = CardId of string
    let create = function
        | null -> None
        |  s when Regex("^\d{4}$").IsMatch(s) -> Some(CardId s)
        | _ -> None
namespace Marreco.Expense

open System

module Transaction = 
    type Value =  
    | BRL of decimal
    | USD of decimal

    type Transaction = { Date :DateTime; Description : string; Value : Value }

module Statement = 
    open Transaction
    type CardId =  CardId.T

    type CheckingAccount = 
        | Santander of string
        | Caixa of string

    type CreditCardStatement = 
        { Id : CardId; Maturity : DateTime; Transactions : Transaction list }
    type CheckingAccountStatement = 
        { Account : CheckingAccount; IssueDate : DateTime; Transactions : Transaction list }

    type Statement = 
        | Credit of CreditCardStatement
        | Checking of CheckingAccountStatement

