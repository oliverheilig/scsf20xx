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
Imports System.Net
Imports System.Reflection
Imports System.Security.Permissions
' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.
<Assembly: AssemblyTitle("Microsoft.Practices.CompositeUI")> 
<Assembly: AssemblyDescription("Microsoft Composite UI Application Block")> 
<Assembly: _
	DataProtectionPermission(SecurityAction.RequestMinimum, _
			Flags:=DataProtectionPermissionFlags.ProtectData Or DataProtectionPermissionFlags.UnprotectData)> 
<Assembly: FileIOPermission(SecurityAction.RequestMinimum, Unrestricted:=True)> 
<Assembly: ReflectionPermission(SecurityAction.RequestMinimum, Flags:=ReflectionPermissionFlag.MemberAccess)> 
<Assembly: SecurityPermission(SecurityAction.RequestMinimum, Flags:=SecurityPermissionFlag.ControlPrincipal)> 
<Assembly: WebPermission(SecurityAction.RequestMinimum, Unrestricted:=True)> 
