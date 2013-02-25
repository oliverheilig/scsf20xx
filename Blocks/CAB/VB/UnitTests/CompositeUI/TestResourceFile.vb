'===============================================================================
' Microsoft patterns & practices
' CompositeUI Application Block
'===============================================================================
' Copyright © Microsoft Corporation.  All rights reserved.
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
' OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
' LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
' FITNESS FOR A PARTICULAR PURPOSE.
'===============================================================================

Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Reflection

Friend Class TestResourceFile
	Implements IDisposable

	Private filename As String
	Private Shared buffer As Byte() = New Byte(7999) {}

	Public Sub New(ByVal resourceName As String)
		filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, resourceName)
		Directory.CreateDirectory(Path.GetDirectoryName(filename))
		resourceName = Me.GetType().Namespace & "." & resourceName.Replace("\"c, "."c)

		Using resStream As Stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)
			Using outStream As FileStream = File.Open(filename, FileMode.Create, FileAccess.Write)
				Do While True
					Dim read As Integer = resStream.Read(buffer, 0, buffer.Length)
					If read = 0 Then
						Exit Do
					End If
					outStream.Write(buffer, 0, read)
				Loop
			End Using
		End Using
	End Sub

	Public Sub Dispose() Implements IDisposable.Dispose
		Try
			File.Delete(filename)
		Catch
		End Try
	End Sub

End Class
