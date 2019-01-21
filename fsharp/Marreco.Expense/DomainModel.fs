namespace Marreco.Expense
open WrappedString;
open GeoCoordinate
open System

module Account = 
    type AccountId = Guid
    type AccountType = 
        | CreditCard
        | Checking
        | Cash

    type Account = {
        Id : AccountId
        Name : String50    
        Type : AccountType
    }


module Category = 
    type CategoryId = Guid
    type MasterCategory = MasterCategory of String50

    type Category =  {
        Id : CategoryId
        Name : String50
        MasterCategory : MasterCategory
    }

module Transaction =
    open Account
    open Category
    
    type Payee = Payee of String50 // yagni on beeing entity
    type Payer = Payer of String50

    type UnreconciledEntry = {
        Account : Account
        Identification : string option    
        Counterparty : string option
        Description : string option
        Amount : decimal
        When : DateTimeOffset
        Where : GeoCoordinate option
    }

    type Expense = {
        Account : Account
        Payee : Payee
        Description : String100
        Category : Category option
        Amount : MoneyAmount
        When : DateTimeOffset
        Where : GeoCoordinate
    }

    type Income = {
        Account : Account
        Payer : Payer
        Description : string option
        Amount : MoneyAmount
        When : DateTimeOffset
    }

    type Transfer = {
        InAccount : Account
        OutAccount : Account
        Amount :  MoneyAmount
        When : DateTimeOffset
        Description : string option
    }

    type TransactionEntry = 
        | Expense of Expense
        | Income of Income
        | Transfer of Transfer


    type EntryConciliationError = 
        | InvalidAccount


    type ReconcileEntry = UnreconciledEntry -> Result<TransactionEntry, EntryConciliationError>


(*
    Actions: 
        ReconcileEntry : UnreconciledEntry -> TransactionEntry 
            Accepting raw entries from Console, SMS, Web, etc., and turning into entries

                



*)

