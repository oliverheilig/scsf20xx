/***
*
*  Run with OSQL -S "(local)\SQLEXPRESS" -E -i GlobalBank.sql
*
***/
SET LANGUAGE English
USE [master]
GO
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'SCSF_GlobalBank')
BEGIN
	DROP DATABASE [SCSF_GlobalBank]
END
GO
CREATE DATABASE [SCSF_GlobalBank]
GO
USE [SCSF_GlobalBank]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ListQueue]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_ListQueue] 
AS

	select * from Queue' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_AssignQueueEntry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_AssignQueueEntry] 
@QueueId		int,
@AssignedTo		nvarchar(50)
AS

UPDATE Queue SET AssignedTo = @AssignedTo WHERE QueueId = @QueueId

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Customer](
	[CustomerId] [nvarchar](32) NOT NULL,
	[FirstName] [nvarchar](64) NOT NULL,
	[LastName] [nvarchar](64) NOT NULL,
	[MiddleInitial] [nchar](1) NULL,
	[SSNumber] [nchar](9) NOT NULL,
	[MotherMaidenName] [nvarchar](64) NULL,
	[CustomerLevel] [nvarchar](16) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Walkin]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Walkin](
	[WalkinId] [nvarchar](32) NOT NULL,
	[FirstName] [nvarchar](64) NOT NULL,
	[LastName] [nvarchar](64) NOT NULL,
	[MiddleInitial] [nchar](1) NULL,
	[SSNumber] [nchar](9) NOT NULL,
	[MotherMaidenName] [nvarchar](64) NULL,
	[CustomerLevel] [nvarchar](16) NOT NULL,
 CONSTRAINT [PK_Walkin] PRIMARY KEY CLUSTERED 
(
	[WalkinId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AccountType](
	[AccountTypeId] [int] NOT NULL,
	[AccountType] [nvarchar](64) NOT NULL,
	[Fees] [decimal](18, 2) NULL,
	[MinimumDeposit] [decimal](18, 2) NULL,
	[MaxMonthlyTransaction] [int] NULL,
	[InterestRate] [decimal](18, 2) NULL,
 CONSTRAINT [PK_AccountType] PRIMARY KEY CLUSTERED 
(
	[AccountTypeId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreditCardType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CreditCardType](
	[CreditCardTypeId] [int] NOT NULL,
	[CreditCardType] [nvarchar](64) NOT NULL,
	[MaxCreditLimit] [decimal](18, 2) NOT NULL,
	[Fees] [decimal](18, 2) NULL,
	[InterestRate] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_CreditCardType] PRIMARY KEY CLUSTERED 
(
	[CreditCardTypeId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AlertType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AlertType](
	[AlertTypeId] [int] NOT NULL,
	[AlertType] [nvarchar](64) NOT NULL,
	[Description] [nvarchar](256) NOT NULL,
	[StartDate] [smalldatetime] NOT NULL,
	[ExpirationDate] [smalldatetime] NULL,
 CONSTRAINT [PK_AlertType] PRIMARY KEY CLUSTERED 
(
	[AlertTypeId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Queue]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Queue](
	[QueueId] [int] NOT NULL,
	[VisitorName] [nvarchar](256) NOT NULL,
	[CustomerId] [nvarchar](32) NULL,
	[WalkinId] [nvarchar](32) NULL,
	[TimeIn] [datetime] NOT NULL,
	[Status] [nvarchar](12) NOT NULL,
	[ReasonCode] [nvarchar](64) NOT NULL,
	[Description] [nvarchar](256) NULL,
	[AssignedTo] [nvarchar](50) NULL,
 CONSTRAINT [PK_Queue] PRIMARY KEY CLUSTERED 
(
	[QueueId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Account]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Account](
	[AccountNumber] [nvarchar](32) NOT NULL,
	[CustomerId] [nvarchar](32) NOT NULL,
	[AccountTypeId] [int] NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[DateOpened] [smalldatetime] NOT NULL,
	[LastTransactionDate] [smalldatetime] NULL,
	[InterestRate] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountNumber] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreditCard]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CreditCard](
	[CreditCardNumber] [nvarchar](32) NOT NULL,
	[CustomerId] [nvarchar](32) NOT NULL,
	[CreditCardTypeId] [int] NOT NULL,
	[CreditLimit] [decimal](18, 2) NOT NULL,
	[AvailableBalance] [decimal](18, 2) NOT NULL,
	[PaymentDue] [decimal](18, 2) NULL,
	[DateOpened] [smalldatetime] NOT NULL,
	[LastPaymentDate] [smalldatetime] NULL,
 CONSTRAINT [PK_CreditCards] PRIMARY KEY CLUSTERED 
(
	[CreditCardNumber] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Alert]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Alert](
	[AlertId] [int] NOT NULL,
	[AlertTypeId] [int] NOT NULL,
	[CustomerId] [nvarchar](32) NOT NULL,
	[StartDate] [smalldatetime] NOT NULL,
	[ExpirationDate] [smalldatetime] NULL,
 CONSTRAINT [PK_Alert] PRIMARY KEY CLUSTERED 
(
	[AlertId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmailAddress]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmailAddress](
	[CustomerId] [nvarchar](32) NOT NULL,
	[Type] [nvarchar](16) NOT NULL,
	[EmailAddress] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_EmailAddress] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC,
	[Type] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PhoneNumber]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PhoneNumber](
	[CustomerId] [nvarchar](32) NOT NULL,
	[Type] [nvarchar](16) NOT NULL,
	[PhoneNumber] [nvarchar](24) NOT NULL,
 CONSTRAINT [PK_PhoneNumber] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC,
	[Type] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Comment](
	[CommentId] [int] NOT NULL,
	[CustomerId] [nvarchar](32) NOT NULL,
	[CommentDate] [datetime] NOT NULL,
	[AuthorName] [nvarchar](128) NOT NULL,
	[Comment] [text] NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Address](
	[CustomerId] [nvarchar](32) NOT NULL,
	[Type] [nvarchar](16) NOT NULL,
	[Address1] [nvarchar](128) NOT NULL,
	[Address2] [nvarchar](128) NULL,
	[City] [nvarchar](64) NOT NULL,
	[StateProvince] [nvarchar](64) NOT NULL,
	[Country] [nchar](10) NOT NULL,
	[PostalZipCode] [nvarchar](16) NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC,
	[Type] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WalkinAddress]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WalkinAddress](
	[WalkinId] [nvarchar](32) NOT NULL,
	[Type] [nvarchar](16) NOT NULL,
	[Address1] [nvarchar](128) NOT NULL,
	[Address2] [nvarchar](128) NULL,
	[City] [nvarchar](64) NOT NULL,
	[StateProvince] [nvarchar](64) NOT NULL,
	[Country] [nchar](10) NOT NULL,
	[PostalZipCode] [nvarchar](16) NOT NULL,
 CONSTRAINT [PK_WalkinAddress] PRIMARY KEY CLUSTERED 
(
	[WalkinId] ASC,
	[Type] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WalkinEmailAddress]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WalkinEmailAddress](
	[WalkinId] [nvarchar](32) NOT NULL,
	[Type] [nvarchar](16) NOT NULL,
	[EmailAddress] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_WalkinEmailAddress] PRIMARY KEY CLUSTERED 
(
	[WalkinId] ASC,
	[Type] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WalkinPhoneNumber]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WalkinPhoneNumber](
	[WalkinId] [nvarchar](32) NOT NULL,
	[Type] [nvarchar](16) NOT NULL,
	[PhoneNumber] [nvarchar](24) NOT NULL,
 CONSTRAINT [PK_WalkinPhoneNumber] PRIMARY KEY CLUSTERED 
(
	[WalkinId] ASC,
	[Type] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WalkinComment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WalkinComment](
	[WalkinCommentId] [int] NOT NULL,
	[WalkinId] [nvarchar](32) NOT NULL,
	[CommentDate] [datetime] NOT NULL,
	[AuthorName] [nvarchar](128) NOT NULL,
	[Comment] [text] NOT NULL,
 CONSTRAINT [PK_WalkinComment] PRIMARY KEY CLUSTERED 
(
	[WalkinCommentId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_FindCustomer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[sp_FindCustomer]
(
	@LastName			nvarchar(64) = null,
	@FirstName			nvarchar(64)= null,
	@MiddleInitial		nchar(1) = null,
	@SSNumber			nchar(9) = null,
	@Address1			nvarchar(128) = null,
	@Address2			nvarchar(128) = null,
	@City				nvarchar(64) = null,
	@StateProvince		nvarchar(64) = null,
	@PostalZipCode		nvarchar(16) = null,
	@PhoneNumber1		nvarchar(24) = null,
	@PhoneNumber2		nvarchar(24) = null,
	@PhoneNumber3		nvarchar(24) = null,
	@EmailAddress		nvarchar(128) = null
)
AS
	/* SET NOCOUNT ON */ 

	DECLARE @Customer TABLE 
	(
		CustomerID nvarchar(32)
	);

	INSERT INTO @Customer
	SELECT C.CustomerId
	FROM Customer C
	WHERE
		((@LastName is null) or (C.LastName like @LastName + ''%'')) and
		((@FirstName is null) or (C.FirstName like @FirstName + ''%'')) and
		((@MiddleInitial is null) or (C.MiddleInitial = @MiddleInitial)) and
		((@SSNumber is null) or (C.SSNumber = @SSNumber)) and 
		((@Address1 is null) or (exists(select 1 from Address where CustomerId = C.CustomerId and Address1 like @Address1 + ''%''))) and
		((@Address2 is null) or (exists(select 1 from Address where CustomerId = C.CustomerId and Address2 like @Address2 + ''%''))) and
		((@City is null) or (exists(select 1 from Address where CustomerId = C.CustomerId and City = @City ))) and
		((@StateProvince is null) or (exists(select 1 from Address where CustomerId = C.CustomerId and StateProvince = @StateProvince))) and
		((@PostalZipCode is null) or (exists(select 1 from Address where CustomerId = C.CustomerId and PostalZipCode = @PostalZipCode))) and
		((@PhoneNumber1 is null) or (exists(select 1 from PhoneNumber where CustomerId = C.CustomerId and PhoneNumber = @PhoneNumber1))) and
		((@PhoneNumber2 is null) or (exists(select 1 from PhoneNumber where CustomerId = C.CustomerId and PhoneNumber = @PhoneNumber2))) and
		((@PhoneNumber3 is null) or (exists(select 1 from PhoneNumber where CustomerId = C.CustomerId and PhoneNumber = @PhoneNumber3))) and
		((@EmailAddress is null) or (exists(select 1 from EmailAddress where CustomerId = C.CustomerId and EmailAddress = @EmailAddress)))


		SELECT C1.*
		FROM Customer C1 INNER JOIN @Customer C2 on C1.CustomerId = C2.CustomerId

		SELECT A.* FROM @Customer C RIGHT JOIN Address A ON C.CustomerID = A.CustomerId

		SELECT E.* FROM @Customer C RIGHT JOIN EmailAddress E ON C.CustomerId =E.CustomerId

		SELECT P.* FROM @Customer C RIGHT JOIN PhoneNumber P ON C.CustomerId = P.CustomerId

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_LookupCustomer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_LookupCustomer] 
@CustomerId		nvarchar(32)
AS

	SELECT * FROM Customer WHERE CustomerId = @CustomerId

	SELECT A.* FROM Address A WHERE @CustomerID = A.CustomerId

	SELECT E.* FROM EmailAddress E WHERE @CustomerId =E.CustomerId

	SELECT P.* FROM PhoneNumber P WHERE @CustomerId = P.CustomerId

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_InitializeGlobalBankForTesting]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[sp_InitializeGlobalBankForTesting]
AS
	SET NOCOUNT ON
	
	/*Clear GlobalBank Database*/
	DELETE Alert
	DELETE AlertType
	DELETE CreditCard
	DELETE CreditCardType
	DELETE Account
	DELETE AccountType
	DELETE Address
	DELETE EmailAddress
	DELETE PhoneNumber
	DELETE [Queue]
	DELETE Customer
	DELETE WalkinEmailAddress
	DELETE WalkinPhoneNumber
	DELETE WalkinComment
	DELETE WalkinAddress
	DELETE Walkin
	
	/*Records for Customer Table*/
	INSERT INTO [Customer]
			   ([CustomerId]
			   ,[FirstName]
			   ,[LastName]
			   ,[MiddleInitial]
			   ,[SSNumber]
			   ,[MotherMaidenName]
			   ,[CustomerLevel])
		 VALUES
			   (''1002003121''
			   ,''Leonids''
			   ,''Paturskis''
			   ,NULL
			   ,''111111111''
			   ,''Anne''
			   ,''1'')

	INSERT INTO [Customer]
			   ([CustomerId]
			   ,[FirstName]
			   ,[LastName]
			   ,[MiddleInitial]
			   ,[SSNumber]
			   ,[MotherMaidenName]
			   ,[CustomerLevel])
		 VALUES
			   (''1002003857''
			   ,''Kari''
			   ,''Hensien''
			   ,NULL
			   ,''222222222''
			   ,''Nancy''
			   ,''3'')

	INSERT INTO [Customer]
			   ([CustomerId]
			   ,[FirstName]
			   ,[LastName]
			   ,[MiddleInitial]
			   ,[SSNumber]
			   ,[MotherMaidenName]
			   ,[CustomerLevel])
		 VALUES
			   (''1002003487''
			   ,''Mary''
			   ,''Andersen''
			   ,''K''
			   ,''333333333''
			   ,''Sara''
			   ,''1'')

	/*Records for Address Table*/
	INSERT INTO [Address]
			   ([CustomerId]
			   ,[Type]
			   ,[Address1]
			   ,[Address2]
			   ,[City]
			   ,[StateProvince]
			   ,[Country]
			   ,[PostalZipCode])
		 VALUES
			   (''1002003121''
			   ,''Home''
			   ,''825 228th Ave. NE''
			   ,NULL
			   ,''Sammamish''
			   ,''WA''
			   ,''USA''
			   ,''98074'')

	INSERT INTO [Address]
			   ([CustomerId]
			   ,[Type]
			   ,[Address1]
			   ,[Address2]
			   ,[City]
			   ,[StateProvince]
			   ,[Country]
			   ,[PostalZipCode])
		 VALUES
			   (''1002003857''
			   ,''Home''
			   ,''14250 S.E. Newport Way''
			   ,NULL
			   ,''Bellevue''
			   ,''WA''
			   ,''USA''
			   ,''98006'')

	INSERT INTO [Address]
			   ([CustomerId]
			   ,[Type]
			   ,[Address1]
			   ,[Address2]
			   ,[City]
			   ,[StateProvince]
			   ,[Country]
			   ,[PostalZipCode])
		 VALUES
			   (''1002003857''
			   ,''Work''
			   ,''18138 73rd N.E.''
			   ,NULL
			   ,''Kenmore''
			   ,''WA''
			   ,''USA''
			   ,''98028'')

	INSERT INTO [Address]
			   ([CustomerId]
			   ,[Type]
			   ,[Address1]
			   ,[Address2]
			   ,[City]
			   ,[StateProvince]
			   ,[Country]
			   ,[PostalZipCode])
		 VALUES
			   (''1002003487''
			   ,''Home''
			   ,''19601 21st Ave N.W.''
			   ,NULL
			   ,''Shoreline''
			   ,''WA''
			   ,''USA''
			   ,''98177'')

	/*Records for EmailAddress Table*/
	INSERT INTO [EmailAddress]
			   ([CustomerId]
			   ,[Type]
			   ,[EmailAddress])
		 VALUES
			   (''1002003121''
			   ,''Personal''
			   ,''leonids@the-phonecompany.com'')

	INSERT INTO [EmailAddress]
			   ([CustomerId]
			   ,[Type]
			   ,[EmailAddress])
		 VALUES
			   (''1002003121''
			   ,''Official''
			   ,''lpat@proseware.com'')

	INSERT INTO [EmailAddress]
			   ([CustomerId]
			   ,[Type]
			   ,[EmailAddress])
		 VALUES
			   (''1002003857''
			   ,''Official''
			   ,''kari@contoso.com'')

	INSERT INTO [EmailAddress]
			   ([CustomerId]
			   ,[Type]
			   ,[EmailAddress])
		 VALUES
			   (''1002003487''
			   ,''Personal''
			   ,''mary_andersen@adventure-works.com'')
	           
	INSERT INTO [EmailAddress]
			   ([CustomerId]
			   ,[Type]
			   ,[EmailAddress])
		 VALUES
			   (''1002003487''
			   ,''Official''
			   ,''mandersenM@northwindtraders.com'')
	           
	/*Records for PhoneNumber Table*/
	INSERT INTO [PhoneNumber]
			   ([CustomerId]
			   ,[Type]
			   ,[PhoneNumber])
		 VALUES
			   (''1002003121''
			   ,''Home''
			   ,''1111111111'')

	INSERT INTO [PhoneNumber]
			   ([CustomerId]
			   ,[Type]
			   ,[PhoneNumber])
		 VALUES
			   (''1002003121''
			   ,''Work''
			   ,''2222222222'')

	INSERT INTO [PhoneNumber]
			   ([CustomerId]
			   ,[Type]
			   ,[PhoneNumber])
		 VALUES
			   (''1002003121''
			   ,''Mobile''
			   ,''3333333333'')

	INSERT INTO [PhoneNumber]
			   ([CustomerId]
			   ,[Type]
			   ,[PhoneNumber])
		 VALUES
			   (''1002003857''
			   ,''Home''
			   ,''4444444444'')

	INSERT INTO [PhoneNumber]
			   ([CustomerId]
			   ,[Type]
			   ,[PhoneNumber])
		 VALUES
			   (''1002003857''
			   ,''Mobile''
			   ,''5555555555'')
	           
	INSERT INTO [PhoneNumber]
			   ([CustomerId]
			   ,[Type]
			   ,[PhoneNumber])
		 VALUES
			   (''1002003487''
			   ,''Home''
			   ,''6666666666'')

	INSERT INTO [PhoneNumber]
			   ([CustomerId]
			   ,[Type]
			   ,[PhoneNumber])
		 VALUES
			   (''1002003487''
			   ,''Mobile''
			   ,''7777777777'')

	/*Records for AccountType Table*/
	INSERT INTO [AccountType]
			   ([AccountTypeId]
			   ,[AccountType]
			   ,[Fees]
			   ,[MinimumDeposit]
			   ,[MaxMonthlyTransaction]
			   ,[InterestRate])
		 VALUES
			   (1
			   ,''Savings''
			   ,NULL
			   ,100
			   ,6
			   ,3.3)

	INSERT INTO [AccountType]
			   ([AccountTypeId]
			   ,[AccountType]
			   ,[Fees]
			   ,[MinimumDeposit]
			   ,[MaxMonthlyTransaction]
			   ,[InterestRate])
		 VALUES
			   (2
			   ,''Checkings''
			   ,10
			   ,100
			   ,NULL
			   ,NULL)

	INSERT INTO [AccountType]
			   ([AccountTypeId]
			   ,[AccountType]
			   ,[Fees]
			   ,[MinimumDeposit]
			   ,[MaxMonthlyTransaction]
			   ,[InterestRate])
		 VALUES
			   (3
			   ,''CD''
			   ,0
			   ,0
			   ,NULL
			   ,NULL)
			   
	/*Records for Account Table*/
	INSERT INTO [Account]
			   ([AccountNumber]
			   ,[CustomerId]
			   ,[AccountTypeId]
			   ,[Balance]
			   ,[DateOpened]
			   ,[LastTransactionDate])
		 VALUES
			   (''34523232''
			   ,''1002003121''
			   ,1
			   ,300000
			   ,''02/04/2002''
			   ,''3/15/2006'')

	INSERT INTO [Account]
			   ([AccountNumber]
			   ,[CustomerId]
			   ,[AccountTypeId]
			   ,[Balance]
			   ,[DateOpened]
			   ,[LastTransactionDate])
		 VALUES
			   (''12333232''
			   ,''1002003121''
			   ,2
			   ,6000
			   ,''02/04/2002''
			   ,''04/20/2006'')

	INSERT INTO [Account]
			   ([AccountNumber]
			   ,[CustomerId]
			   ,[AccountTypeId]
			   ,[Balance]
			   ,[DateOpened]
			   ,[LastTransactionDate])
		 VALUES
			   (''34522603''
			   ,''1002003857''
			   ,1
			   ,15000
			   ,''06/21/2005''
			   ,''03/31/2006'')

	INSERT INTO [Account]
			   ([AccountNumber]
			   ,[CustomerId]
			   ,[AccountTypeId]
			   ,[Balance]
			   ,[DateOpened]
			   ,[LastTransactionDate])
		 VALUES
			   (''12332603''
			   ,''1002003857''
			   ,2
			   ,5500.34
			   ,''08/21/2005''
			   ,''4/2/2006'')

	INSERT INTO [Account]
			   ([AccountNumber]
			   ,[CustomerId]
			   ,[AccountTypeId]
			   ,[Balance]
			   ,[DateOpened]
			   ,[LastTransactionDate])
		 VALUES
			   (''34521211''
			   ,''1002003487''
			   ,1
			   ,6000
			   ,''2/1/2003''
			   ,''3/4/2006'')

	/*Records for CreditCardType Table*/
	INSERT INTO [CreditCardType]
			   ([CreditCardTypeId]
			   ,[CreditCardType]
			   ,[MaxCreditLimit]
			   ,[Fees]
			   ,[InterestRate])
		 VALUES
			   (1
			   ,''Gold''
			   ,10000
			   ,30
			   ,10)

	INSERT INTO [CreditCardType]
			   ([CreditCardTypeId]
			   ,[CreditCardType]
			   ,[MaxCreditLimit]
			   ,[Fees]
			   ,[InterestRate])
		 VALUES
			   (2
			   ,''Platinum''
			   ,50000
			   ,80
			   ,9.8)

	/*Records for CreditCard Table*/
	INSERT INTO [CreditCard]
			   ([CreditCardNumber]
			   ,[CustomerId]
			   ,[CreditCardTypeId]
			   ,[CreditLimit]
			   ,[AvailableBalance]
			   ,[PaymentDue]
			   ,[DateOpened]
			   ,[LastPaymentDate])
		 VALUES
			   (''2345435412341212''
			   ,''1002003121''
			   ,1
			   ,6000
			   ,5300
			   ,2300
			   ,''05/22/2003''
			   ,''04/18/2006'')

	INSERT INTO [CreditCard]
			   ([CreditCardNumber]
			   ,[CustomerId]
			   ,[CreditCardTypeId]
			   ,[CreditLimit]
			   ,[AvailableBalance]
			   ,[PaymentDue]
			   ,[DateOpened]
			   ,[LastPaymentDate])
		 VALUES
			   (''2345435434542341''
			   ,''1002003487''
			   ,2
			   ,35000
			   ,23000
			   ,25346
			   ,''02/22/2006''
			   ,''04/29/2006'')

	/*Records for AlertType Table*/
	INSERT INTO [AlertType]
			   ([AlertTypeId]
			   ,[AlertType]
			   ,[Description]
			   ,[StartDate]
			   ,[ExpirationDate])
		 VALUES
			   (1
			   ,''Cash Loan''
			   ,''Pre-approved Cash Loan''
			   ,''02/01/2006''
			   ,''06/30/2006'')

	/*Records for Alert Table*/
	INSERT INTO [Alert]
			   ([AlertId]
			   ,[AlertTypeId]
			   ,[CustomerId]
			   ,[StartDate]
			   ,[ExpirationDate])
		 VALUES
			   (1
			   ,1
			   ,''1002003121''
			   ,''03/01/2006''
			   ,''05/30/2006'')  
	RETURN


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CreateCustomer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_CreateCustomer]
@CustomerId			nvarchar(32),
@FirstName			nvarchar(64),
@LastName			nvarchar(64),
@MiddleInitial		nchar(1) = null,
@SSNumber			nchar(9),
@MotherMaidenName	nvarchar(64)	= null,
@CustomerLevel		nvarchar(16)
AS

insert into Customer(CustomerId, FirstName, LastName, MiddleInitial, SSNumber, MotherMaidenName, CustomerLevel)
values(@CustomerId, @FirstName, @LastName, @MiddleInitial, @SSNumber, @MotherMaidenName, @CustomerLevel)
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_LookupCustomerInQueue]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create proc [dbo].[sp_LookupCustomerInQueue]
@CustomerId		nvarchar(32)
as
	select C.* from Queue Q inner join Customer C on Q.CustomerId = C.CustomerId where Q.CustomerId = @CustomerId
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CreateWalkin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_CreateWalkin]
@WalkinId			nvarchar(32),
@FirstName			nvarchar(64),
@LastName			nvarchar(64),
@MiddleInitial		nchar(1)		= null,
@SSNumber			nchar(9),
@MotherMaidenName	nvarchar(64)	= null,
@CustomerLevel		nvarchar(16)
AS

insert into Walkin(WalkinId, FirstName, LastName, MiddleInitial, SSNumber, MotherMaidenName, CustomerLevel)
values(@WalkinId, @FirstName, @LastName, @MiddleInitial, @SSNumber, @MotherMaidenName, @CustomerLevel)
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_LookupWalkin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_LookupWalkin] 
@WalkinId		nvarchar(32)
AS

	SELECT * FROM Walkin WHERE WalkinId = @WalkinId

	SELECT A.* FROM WalkinAddress A WHERE @WalkinId = A.WalkinId

	SELECT E.* FROM WalkinEmailAddress E WHERE @WalkinId = E.WalkinId

	SELECT P.* FROM WalkinPhoneNumber P WHERE @WalkinId = P.WalkinId

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CustomerAccounts]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_CustomerAccounts]
@CustomerId			nvarchar(32)
AS

SELECT * FROM Account WHERE CustomerId = @CustomerId
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetAccountBalance]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetAccountBalance]
@AccountNumber		nvarchar(32),
@Balance			decimal = null output
AS

	select @balance = balance from Account where AccountNumber = @AccountNumber
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_UpdateAccountBalance]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_UpdateAccountBalance]
@AccountNumber		nvarchar(32),
@Amount				decimal
AS	

	update Account set Balance = Balance + @Amount where AccountNumber = @AccountNumber
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CreateAccount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_CreateAccount]
@AccountNumber		nvarchar(32),
@CustomerId			nvarchar(32),
@AccountTypeId		int,
@Balance			decimal = 0,
@DateOpened			datetime,
@InterestRate		decimal = null
AS

	insert into Account(AccountNumber, CustomerId, AccountTypeId, Balance, DateOpened, LastTransactionDate, InterestRate)
	values(@AccountNumber, @CustomerId, @AccountTypeId, @Balance, @DateOpened, GETDATE(), @InterestRate)

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CustomerCreditCards]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_CustomerCreditCards]
@CustomerId			nvarchar(32)
AS

SELECT * FROM CreditCard WHERE CustomerId = @CustomerId
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CustomerAlerts]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[sp_CustomerAlerts]
@CustomerId			nvarchar(32)
AS

SELECT * FROM Alert WHERE CustomerId = @CustomerId


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CreateQueueEntry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_CreateQueueEntry]
@VisitorName		nvarchar(256),
@CustomerId			nvarchar(32)	=	null,
@WalkinId			nvarchar(32)	=	null,
@TimeIn				datetime		=	null,
@Status				nvarchar(12),
@ReasonCode			nvarchar(64),
@Description		nvarchar(256)	= null
AS

declare @QueueId int

begin transaction

select @QueueId = coalesce(max(QueueId) + 1, 0) from Queue

select @TimeIn = coalesce(@TimeIn, GETDATE())

insert into Queue(QueueId, VisitorName, CustomerId, WalkinId, TimeIn, Status, ReasonCode, Description)
values (@QueueId, @VisitorName, @CustomerId, @WalkinId, @TimeIn, @Status, @ReasonCode, @Description)

commit

select * from Queue where QueueId = @QueueId' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_LookupQueueEntry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_LookupQueueEntry] 
@QueueId		int
AS
	select * from Queue where QueueId = @QueueId
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_RemoveQueueEntry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_RemoveQueueEntry] 
@QueueId		int
AS

delete Queue where QueueId = @QueueId' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CreateWalkinAddress]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_CreateWalkinAddress] 
@WalkinId			nvarchar(32),
@Type				nvarchar(16),
@Address1			nvarchar(128),
@Address2			nvarchar(128) = null,
@StateProvince		nvarchar(64),
@City				nvarchar(64),
@Country			nvarchar(10),
@PostalZipCode		nvarchar(16)
AS

INSERT WalkinAddress(WalkinId, Type, Address1, Address2, City, StateProvince, Country, PostalZipCode)
VALUES(@WalkinId, @Type, @Address1, @Address2, @City, @StateProvince, @Country, @PostalZipCode)
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CreateWalkinEmailAddress]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_CreateWalkinEmailAddress]
@WalkinId			nvarchar(32),
@Type				nvarchar(16),
@EmailAddress		nvarchar(128)
AS

INSERT WalkinEmailAddress(WalkinId, Type, EmailAddress)
VALUES(@WalkinId, @Type, @EmailAddress)' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CreateWalkinPhoneNumber]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_CreateWalkinPhoneNumber]
@WalkinId			nvarchar(32),
@Type				nvarchar(16),
@PhoneNumber		nvarchar(24)
AS

INSERT WalkinPhoneNumber(WalkinId, Type, PhoneNumber)
VALUES(@WalkinId, @Type, @PhoneNumber)' 
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Queue_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Queue]'))
ALTER TABLE [dbo].[Queue]  WITH CHECK ADD  CONSTRAINT [FK_Queue_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Queue_Walkin]') AND parent_object_id = OBJECT_ID(N'[dbo].[Queue]'))
ALTER TABLE [dbo].[Queue]  WITH CHECK ADD  CONSTRAINT [FK_Queue_Walkin] FOREIGN KEY([WalkinId])
REFERENCES [dbo].[Walkin] ([WalkinId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Account_AccountType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Account]'))
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_AccountType] FOREIGN KEY([AccountTypeId])
REFERENCES [dbo].[AccountType] ([AccountTypeId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Account_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Account]'))
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CreditCards_CreditCardType]') AND parent_object_id = OBJECT_ID(N'[dbo].[CreditCard]'))
ALTER TABLE [dbo].[CreditCard]  WITH CHECK ADD  CONSTRAINT [FK_CreditCards_CreditCardType] FOREIGN KEY([CreditCardTypeId])
REFERENCES [dbo].[CreditCardType] ([CreditCardTypeId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CreditCards_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[CreditCard]'))
ALTER TABLE [dbo].[CreditCard]  WITH CHECK ADD  CONSTRAINT [FK_CreditCards_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Alert_AlertType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Alert]'))
ALTER TABLE [dbo].[Alert]  WITH CHECK ADD  CONSTRAINT [FK_Alert_AlertType] FOREIGN KEY([AlertId])
REFERENCES [dbo].[AlertType] ([AlertTypeId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Alert_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Alert]'))
ALTER TABLE [dbo].[Alert]  WITH CHECK ADD  CONSTRAINT [FK_Alert_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EmailAddress_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[EmailAddress]'))
ALTER TABLE [dbo].[EmailAddress]  WITH CHECK ADD  CONSTRAINT [FK_EmailAddress_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PhoneNumber_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[PhoneNumber]'))
ALTER TABLE [dbo].[PhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK_PhoneNumber_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comment_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comment]'))
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Address_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Address]'))
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WalkinAddress_Walkin]') AND parent_object_id = OBJECT_ID(N'[dbo].[WalkinAddress]'))
ALTER TABLE [dbo].[WalkinAddress]  WITH CHECK ADD  CONSTRAINT [FK_WalkinAddress_Walkin] FOREIGN KEY([WalkinId])
REFERENCES [dbo].[Walkin] ([WalkinId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WalkinEmailAddress_Walkin]') AND parent_object_id = OBJECT_ID(N'[dbo].[WalkinEmailAddress]'))
ALTER TABLE [dbo].[WalkinEmailAddress]  WITH CHECK ADD  CONSTRAINT [FK_WalkinEmailAddress_Walkin] FOREIGN KEY([WalkinId])
REFERENCES [dbo].[Walkin] ([WalkinId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WalkinPhoneNumber_Walkin]') AND parent_object_id = OBJECT_ID(N'[dbo].[WalkinPhoneNumber]'))
ALTER TABLE [dbo].[WalkinPhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK_WalkinPhoneNumber_Walkin] FOREIGN KEY([WalkinId])
REFERENCES [dbo].[Walkin] ([WalkinId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WalkinComment_Walkin]') AND parent_object_id = OBJECT_ID(N'[dbo].[WalkinComment]'))
ALTER TABLE [dbo].[WalkinComment]  WITH CHECK ADD  CONSTRAINT [FK_WalkinComment_Walkin] FOREIGN KEY([WalkinId])
REFERENCES [dbo].[Walkin] ([WalkinId])
GO
exec sp_InitializeGlobalBankForTesting
GO