@ModelType AgioNet.ReplaceCompHistoryModel
    
@Code
    Dim Read() As AgioNet.ReplaceCompHistoryModel = TempData("ModelRep")

    Dim grid As WebGrid = New WebGrid(Read)
    'Dim item As Agionet_v1.TestListModel
    
End Code

<div class="Title">
    Replace History
</div>
<div class="Content">
    @grid.GetHtml(columns:=grid.Columns( _
        grid.Column("OrderID", "Orden", canSort:=True, style:="FailureID"), _
        grid.Column("Component", "Componente", canSort:=True, style:="FailureID"), _
        grid.Column("Failure", "Falla", canSort:=True, style:="Failure"), _
        grid.Column("Process", "Proceso", canSort:=True, style:="PSol") _
    ))
</div>