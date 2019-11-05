USE [master]
GO

/****** Object:  Database [AbnAmroNL]    Script Date: 2019-11-04 4:54:01 PM ******/
CREATE DATABASE [AbnAmroNL] 
GO

USE [AbnAmroNL]
GO

EXEC sp_changedbowner @loginame = 'sa'  
GO

CREATE SCHEMA [mt940] AUTHORIZATION DBO
GO

CREATE SCHEMA [seed] AUTHORIZATION DBO
GO


/****** Object:  Table [mt940].[Statement]    Script Date: 2019-11-04 4:55:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [mt940].[Statement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountIdentification] [varchar](50) NULL,
	[RelatedReference] [varchar](50) NULL,
	[SequenceNumber] [int] NULL,
	[StatementNumber] [int] NULL,
	[TransactionReferenceNumber] [varchar](50) NULL,
	[FileName] [nvarchar](150) NULL,
	[FileId] [int] NULL,
	[ChangedBy] [nvarchar](128) NOT NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
 CONSTRAINT [PK_Statement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [mt940].[StatementAudit] )
)
GO

/****** Object:  Table [mt940].[StatementBalance]    Script Date: 2019-11-04 4:55:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [mt940].[StatementBalance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StatementId] [int] NOT NULL,
	[Amount] [money] NULL,
	[Currency] [varchar](3) NULL,
	[Date] [datetime2](0) NULL,
	[Mark] [tinyint] NULL,
	[Type] [tinyint] NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
 CONSTRAINT [PK_StatementBalance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [mt940].[StatementBalanceAudit] )
)
GO

/****** Object:  Table [mt940].[BalanceType]    Script Date: 2019-11-04 4:55:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [mt940].[BalanceType](
	[Id] [tinyint] NOT NULL,
	[Name] [varchar](15) NULL,
 CONSTRAINT [PK_BalanceType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [mt940].[MarkType]    Script Date: 2019-11-04 4:55:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [mt940].[MarkType](
	[Id] [tinyint] NOT NULL,
	[Name] [varchar](15) NULL,
 CONSTRAINT [PK_MarkType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [mt940].[StatementInformation]    Script Date: 2019-11-04 4:55:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [mt940].[StatementInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StatementId] [int] NOT NULL,
	[TransactionCode] [int] NULL,
	[PostingText] [varchar](50) NULL,
	[JournalNumber] [varchar](50) NULL,
	[BankCodeOfPayer] [varchar](50) NULL,
	[AccountNumberOfPayer] [varchar](50) NULL,
	[NameOfPayer] [varchar](50) NULL,
	[TextKeyAddition] [int] NULL,
	[EndToEndReference] [varchar](50) NULL,
	[CustomerReference] [varchar](50) NULL,
	[MandateReference] [varchar](50) NULL,
	[CreditorReference] [varchar](50) NULL,
	[OriginatorsIdentificationCode] [varchar](50) NULL,
	[CompensationAmount] [varchar](50) NULL,
	[OriginalAmount] [varchar](50) NULL,
	[SepaRemittanceInformation] [varchar](50) NULL,
	[PayersReferenceParty] [varchar](50) NULL,
	[CreditorsReferenceParty] [varchar](50) NULL,
	[UnstructuredRemittanceInformation] [varchar](50) NULL,
	[IsUnstructuredData] [bit] NULL,
	[UnstructuredData] [nvarchar](max) NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
 CONSTRAINT [PK_StatementInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [mt940].[StatementInformationAudit] )
)
GO

/****** Object:  Table [mt940].[StatementLine]    Script Date: 2019-11-04 4:55:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [mt940].[StatementLine](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StatementId] [int] NOT NULL,
	[ValueDate] [datetime2](0) NULL,
	[EntryDate] [datetime2](0) NULL,
	[Mark] [tinyint] NULL,
	[FundsCode] [varchar](1) NULL,
	[Amount] [money] NULL,
	[TransactionTypeIdCode] [varchar](50) NULL,
	[CustomerReference] [varchar](50) NULL,
	[SupplementaryDetails] [varchar](50) NULL,
	[BankReference] [varchar](50) NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
 CONSTRAINT [PK_StatementLine] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [mt940].[StatementLineAudit] )
)
GO

/****** Object:  Table [mt940].[StatementLineInformation]    Script Date: 2019-11-04 4:55:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [mt940].[StatementLineInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StatementLineId] [int] NOT NULL,
	[TransactionCode] [int] NULL,
	[PostingText] [varchar](50) NULL,
	[JournalNumber] [varchar](50) NULL,
	[BankCodeOfPayer] [varchar](50) NULL,
	[AccountNumberOfPayer] [varchar](50) NULL,
	[NameOfPayer] [varchar](50) NULL,
	[TextKeyAddition] [int] NULL,
	[EndToEndReference] [varchar](50) NULL,
	[CustomerReference] [varchar](50) NULL,
	[MandateReference] [varchar](50) NULL,
	[CreditorReference] [varchar](50) NULL,
	[OriginatorsIdentificationCode] [varchar](50) NULL,
	[CompensationAmount] [varchar](50) NULL,
	[OriginalAmount] [varchar](50) NULL,
	[SepaRemittanceInformation] [varchar](50) NULL,
	[PayersReferenceParty] [varchar](50) NULL,
	[CreditorsReferenceParty] [varchar](50) NULL,
	[UnstructuredRemittanceInformation] [varchar](50) NULL,
	[IsUnstructuredData] [bit] NULL,
	[UnstructuredData] [nvarchar](max) NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
 CONSTRAINT [PK_StatementLineInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [mt940].[StatementLineInformationAudit] )
)
GO

ALTER TABLE [mt940].[Statement] ADD  CONSTRAINT [DF__ChangedBy]  DEFAULT (suser_sname()) FOR [ChangedBy]
GO

ALTER TABLE [mt940].[StatementBalance]  WITH CHECK ADD  CONSTRAINT [FK_StatementBalance_BalanceType] FOREIGN KEY([Type])
REFERENCES [mt940].[BalanceType] ([Id])
GO

ALTER TABLE [mt940].[StatementBalance] CHECK CONSTRAINT [FK_StatementBalance_BalanceType]
GO

ALTER TABLE [mt940].[StatementBalance]  WITH CHECK ADD  CONSTRAINT [FK_StatementBalance_MarkType] FOREIGN KEY([Mark])
REFERENCES [mt940].[MarkType] ([Id])
GO

ALTER TABLE [mt940].[StatementBalance] CHECK CONSTRAINT [FK_StatementBalance_MarkType]
GO

ALTER TABLE [mt940].[StatementBalance]  WITH CHECK ADD  CONSTRAINT [FK_StatementBalance_Statement] FOREIGN KEY([StatementId])
REFERENCES [mt940].[Statement] ([Id])
GO

ALTER TABLE [mt940].[StatementBalance] CHECK CONSTRAINT [FK_StatementBalance_Statement]
GO

ALTER TABLE [mt940].[StatementInformation]  WITH CHECK ADD  CONSTRAINT [FK_StatementBalance_StatementInformation] FOREIGN KEY([StatementId])
REFERENCES [mt940].[Statement] ([Id])
GO

ALTER TABLE [mt940].[StatementInformation] CHECK CONSTRAINT [FK_StatementBalance_StatementInformation]
GO

ALTER TABLE [mt940].[StatementLine]  WITH CHECK ADD  CONSTRAINT [FK_StatementLine_Statement] FOREIGN KEY([StatementId])
REFERENCES [mt940].[Statement] ([Id])
GO

ALTER TABLE [mt940].[StatementLine] CHECK CONSTRAINT [FK_StatementLine_Statement]
GO

ALTER TABLE [mt940].[StatementLineInformation]  WITH CHECK ADD  CONSTRAINT [FK_StatementBalance_StatementLineInformation] FOREIGN KEY([StatementLineId])
REFERENCES [mt940].[StatementLine] ([Id])
GO

ALTER TABLE [mt940].[StatementLineInformation] CHECK CONSTRAINT [FK_StatementBalance_StatementLineInformation]
GO


-- Stored Procs
/****** Object:  StoredProcedure [mt940].[DeleteStatement]    Script Date: 2019-11-04 4:56:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mt940].[DeleteStatement]
	@Id int
AS
BEGIN
	DELETE a 
		FROM [mt940].StatementLineInformation a
		INNER JOIN [mt940].StatementLine b ON b.Id = a.StatementLineId
		WHERE b.StatementId = @Id

	DELETE FROM [mt940].StatementLine
		WHERE StatementId = @Id

	DELETE FROM [mt940].StatementBalance
		WHERE StatementId = @Id

	DELETE FROM [mt940].StatementInformation
		WHERE StatementId = @Id

	DELETE from [mt940].[Statement]
		WHERE Id = @Id
END
GO

/****** Object:  StoredProcedure [seed].[BalanceType]    Script Date: 2019-11-04 4:56:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [seed].[BalanceType]
AS
BEGIN
	MERGE INTO [mt940].[BalanceType] AS target 
	USING (VALUES
			( 0, 'None'),
			( 1, 'Opening'),
			( 2, 'Intermediate'),
			( 3, 'Closing')) 
	AS source ([Id], [Name]) 
	ON [target].[Id] = [source].[Id]
	WHEN MATCHED THEN
		UPDATE SET 
			[Name] = [source].[Name]
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT ([Id], [Name])
		VALUES ([Id], [Name])
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;
	RETURN 0
END
GO

/****** Object:  StoredProcedure [seed].[MarkType]    Script Date: 2019-11-04 4:56:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [seed].[MarkType]
AS
BEGIN
	MERGE INTO [mt940].[MarkType] AS target 
	USING (VALUES
			( 0, 'Credit'),
			( 1, 'Debit'),
			( 2, 'ReverseCredit'),
			( 3, 'ReverseDebit')) 
	AS source ([Id], [Name]) 
	ON [target].[Id] = [source].[Id]
	WHEN MATCHED THEN
		UPDATE SET 
			[Name] = [source].[Name]
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT ([Id], [Name])
		VALUES ([Id], [Name])
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;
	RETURN 0
END
GO

EXECUTE [seed].[BalanceType]
GO

EXECUTE [seed].[MarkType]
GO