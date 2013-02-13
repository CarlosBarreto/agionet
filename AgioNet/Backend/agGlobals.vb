Public Class agGlobals
    '// Fields
    Public Const __DATABASE__ As String = "AgioNet v1.3"
    Public Const __ENPASSWD__ As String = "00Ag10t3ch00"
    Public Const __PASS__ As String = "Agiotech01"
    Public Const __SERVER__ As String = "localhost"
    Public Const __USER__ As String = "aguser"

    '// Nested Types
    Public Structure AllOrderInfo
        public string OrderID as string
        public string OrderDateas string
        public string CustomerType as string
        public string RFC as string
        public string Email as string
        public string Address as string
        public string ExternalNumber as string
        public string InternalNumber as string
        public string Address2 as string
        public string City as string
        public string State as string
        public string Country as string
        public string ZipCode as string
        public string Telephone as string
        public string Telephone2 as string
        public string Telephone3 as string
        public string Delivery as string
        public string DeliveryTime as string
        public string ProductClass as string
        public string ProductType as string
        public string ProductTrademark as string
        public string ProductModel as string
        public string ProductDescription as string
        public string PartNumber as string
        public string SerialNumber as string
        public string Revision as string
        public string ServiceType as string
        public string FailureType as string
        public string Comment as string
    End Structure

    Public Structure FailureView
        public string FAILUREID as string
        public string TESTID as string
        public string DESCRIPTION as string
        public string POSSIBLESOLUTION as string
        public string FOUNDBY as string
        public string FOUNDDATE as string
        public string RESOLVED as string
    End Structure
    
    Public Structure PrintData
        public string ReportedFailure as string
        public string OrderId as string
        public string SerialNo as string
        public string PartNo as string
        public string Model as string
        public string TrackNo as string
        public string ScanDate as string
        public string ScanBy as string
        public string PackType as string
        public string PackDamage as string
        public string NonDoucumentDamage as string
        public string CorrectPack as string
        public string Accesories as string
        public string Cosmetic as string
        public string Warranty as string
        public string ReRepair as string
        public string Comment as string
    End Structure
    
    Public Structure TestView
        public string TESTID as string
        public string ORDERID as string
        public string TESTNAME as string
        public string TESTDESCRIPTION as string
        public string TESTRESULT as string
        public string TESTSTART as string
        public string TESTEND as string
        public string TEXTLOG as string
        public string CREATEBY as string
    End Structure

End Class
