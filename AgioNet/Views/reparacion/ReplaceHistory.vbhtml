@ModelType AgioNet.ReplaceHistoryModel
    
@Code
    Dim Read() As AgioNet.ReplaceHistoryModel = TempData("ModelRep")

    Dim grid As WebGrid = New WebGrid(Read)
    'Dim item As Agionet_v1.TestListModel
    
End Code

<div class="Title">
    Replace History
</div>
<div class="Content">
    @grid.GetHtml(columns:=grid.Columns( _
        grid.Column("OrderID", "Order", canSort:=True, style:="FailureID"), _
        grid.Column("OldPartNo", "Old PartNo", canSort:=True, style:="FailureID"), _
        grid.Column("NewPartNo", "New PartNo", canSort:=True, style:="Failure"), _
        grid.Column("Failure", "Failure", canSort:=True, style:="PSol") _
    ))
</div>