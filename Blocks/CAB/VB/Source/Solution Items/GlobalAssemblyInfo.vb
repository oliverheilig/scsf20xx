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

Imports System
Imports Microsoft.VisualBasic
Imports System.Reflection
Imports System.Runtime.InteropServices
#If DEBUG Then
<Assembly: AssemblyConfiguration("Debug")> 
#Else
<Assembly: AssemblyConfiguration("Release")>
#End If

<Assembly: AssemblyCompany("Microsoft")>
<Assembly: AssemblyCopyright("Copyright© Microsoft Corporation.  All rights reserved.")>
<Assembly: AssemblyTrademark("")>
<Assembly: AssemblyCulture("")>
<Assembly: AssemblyVersion("3.0.0.0")> 
<Assembly: AssemblyFileVersion("3.0.0.0")> 
<Assembly: ComVisible(False)>
<Assembly: CLSCompliant(True)>
