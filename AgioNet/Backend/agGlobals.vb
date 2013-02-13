Public Class agGlobals
    '// Fields
    Public Const __DATABASE__ As String = "AgioNet v1.3"
    Public Const __ENPASSWD__ As String = "00Ag10t3ch00"
    Public Const __PASS__ As String = "Agiotech01"
    Public Const __SERVER__ As String = "localhost"
    Public Const __USER__ As String = "aguser"

    '// Nested Types
    Public Structure AllOrderInfo
        Public OrderID As String
        Public OrderDate As String
        Public CustomerType As String
        Public RFC As String
        Public Email As String
        Public Address As String
        Public ExternalNumber As String
        Public InternalNumber As String
        Public Address2 As String
        Public City As String
        Public State As String
        Public Country As String
        Public ZipCode As String
        Public Telephone As String
        Public Telephone2 As String
        Public Telephone3 As String
        Public Delivery As String
        Public DeliveryTime As String
        Public ProductClass As String
        Public ProductType As String
        Public ProductTrademark As String
        Public ProductModel As String
        Public ProductDescription As String
        Public PartNumber As String
        Public SerialNumber As String
        Public Revision As String
        Public ServiceType As String
        Public FailureType As String
        Public Comment As String
    End Structure

    Public Structure FailureView
        Public FAILUREID As String
        Public TESTID As String
        Public DESCRIPTION As String
        Public POSSIBLESOLUTION As String
        Public FOUNDBY As String
        Public FOUNDDATE As String
        Public RESOLVED As String
    End Structure
    
    Public Structure PrintData
        Public ReportedFailure As String
        Public OrderId As String
        Public SerialNo As String
        Public PartNo As String
        Public Model As String
        Public TrackNo As String
        Public ScanDate As String
        Public ScanBy As String
        Public PackType As String
        Public PackDamage As String
        Public NonDoucumentDamage As String
        Public CorrectPack As String
        Public Accesories As String
        Public Cosmetic As String
        Public Warranty As String
        Public ReRepair As String
        Public Comment As String
    End Structure
    
    Public Structure TestView
        Public TESTID As String
        Public ORDERID As String
        Public TESTNAME As String
        Public TESTDESCRIPTION As String
        Public TESTRESULT As String
        Public TESTSTART As String
        Public TESTEND As String
        Public TEXTLOG As String
        Public CREATEBY As String
    End Structure

End Class
