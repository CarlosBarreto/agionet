@Code
    ViewData("Title") = "_ftpFilePartial"
    Dim OrderID As String = Html.Encode(TempData("OrderID"))
    Dim PrintData As Object = TempData("PrintData")
    
    'Return Me.RedirectToAction("imprimir_hoja")
    Dim filename As String = Me.Server.MapPath("~/Content/temp/rec/") & OrderID & ".pdf"
    'Return File(filename, "application/pdf", Server.HtmlEncode(filename))
    
    Response.Clear()
    Response.ContentType = "application/pdf" '"application/octet-stream"
    Response.AddHeader("Content-Disposition", "attachment; filename=" + OrderID & ".pdf")
    Response.Flush()
    Response.WriteFile(filename)
    Response.End()

    'Response.Redirect(Url.Action("ingresar_orden")) '', new { id = Model.ID }));
End Code
