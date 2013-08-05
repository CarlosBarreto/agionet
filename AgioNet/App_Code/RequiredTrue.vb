Imports System
Imports System.Globalization
Imports System.ComponentModel.DataAnnotations

<AttributeUsage(AttributeTargets.[Property] Or _
        AttributeTargets.Field, AllowMultiple:=False)> _
Public NotInheritable Class RequiredTrue
    Inherits ValidationAttribute


End Class