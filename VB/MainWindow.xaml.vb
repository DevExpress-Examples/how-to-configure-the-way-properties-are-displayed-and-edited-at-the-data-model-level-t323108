﻿Imports System.IO
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.Xpf.Core
Imports DevExpress.Mvvm.DataAnnotations

Namespace DXSample
    Partial Public Class MainWindow
        Inherits DXWindow

        Public Sub New()
            Dim contact = New Contact With {.FirstName = "Carolyn", .LastName = "Baker", .Email = "carolyn.baker@example.com", .Phone = "(555)349-3010", .Address = "1198 Theresa Cir", .City = "Whitinsville", .State = "MA", .Zip = "01582"}
            contact.Photo = GetPhoto(contact)
            DataContext = contact
            InitializeComponent()
        End Sub
        Private Function GetPhoto(ByVal contact As Contact) As Byte()
            Return GetPhoto(contact.FirstName & contact.LastName & ".jpg")
        End Function
        Private Function GetPhoto(ByVal name As String) As Byte()
            Return File.ReadAllBytes("Images\" & name)
        End Function
    End Class
    Public Class Contact
        <Display(GroupName := "General Info"), Required, MaxLength(25, ErrorMessage := "Value is too long")> _
        Public Property FirstName() As String

        <Display(GroupName := "General Info"), Required> _
        Public Property LastName() As String

        <Display(GroupName := "General Info", AutoGenerateField := False), DisplayFormat(NullDisplayText := "<empty>"), CreditCard> _
        Public Property CreditCardNumber() As String

        <Display(GroupName := "Contacts"), DisplayFormat(NullDisplayText := "<empty>"), DataType(DataType.EmailAddress)> _
        Public Property Email() As String

        <Display(GroupName := "Contacts"), DataType(DataType.PhoneNumber), DisplayFormat(NullDisplayText := "<empty>")> _
        Public Property Phone() As String

        <Display(GroupName := "Address"), DisplayFormat(NullDisplayText := "<empty>")> _
        Public Property Address() As String

        <Display(GroupName := "Address"), DisplayFormat(NullDisplayText := "<empty>"), RegExMaskAttribute(Mask := "\w{1,25}", UseAsDisplayFormat := True, ShowPlaceHolders := False)> _
        Public Property City() As String

        <Display(GroupName := "Address"), DisplayFormat(NullDisplayText := "<empty>"), CustomValidation(GetType(ContactValidator), "ValidateString")> _
        Public Property State() As String

        <Display(GroupName := "Address"), DisplayFormat(NullDisplayText := "<empty>")> _
        Public Property Zip() As String

        <Display(GroupName := "General Info"), PropertyGridEditor(TemplateKey := "ImageTemplate")> _
        Public Property Photo() As Byte()
    End Class
    Public Class ContactValidator
        Public Shared Function ValidateString(ByVal value As Object) As ValidationResult
            If value Is Nothing OrElse value.ToString().Length > 25 Then
                Return New ValidationResult("Value is too long")
            End If
            Return ValidationResult.Success
        End Function
    End Class
End Namespace