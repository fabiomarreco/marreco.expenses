namespace Marreco.Expense
open WrappedString;
open GeoCoordinate
open System


type Account = Undefined
type ReferenceId = string
type Payee = Undefined
type Money = Undefined

type MasterCategory = String50
type Category =  {
    Name : String50
    MasterCategory : MasterCategory
}

type UnreconciledEntry = {
    Account : Account
    Identification : string option    
    Payee : string option
    Description : string option
    Amount : decimal
    When : DateTimeOffset
    Where : GeoCoordinate option
}

type Expense = {
    Account : Account
    Identification : ReferenceId option
    Payee : Payee
    Description : String100
    Amount : Money
    When : DateTimeOffset
    Where : GeoCoordinate

}

