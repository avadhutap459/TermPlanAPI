
--Databaseaname_[BanKIntegration]__


IF (Not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'mstProduct'))
BEGIN
CREATE TABLE [dbo].[mstProduct](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[productName] [int] NULL,
	[createdBy] [varchar](50) NULL,
	[CreatedAt] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[LastModifiedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

End
GO


IF (Not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'mstSecurityMech'))
BEGIN
CREATE TABLE [dbo].[mstSecurityMech](
	[SecurityID] [int] IDENTITY(1,1) NOT NULL,
	[SecurityName] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[createdBy] [varchar](50) NULL,
	[CreatedAt] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[LastModifiedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[SecurityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

End
GO


IF (Not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'mstSource'))
BEGIN
CREATE TABLE [dbo].[mstSource](
	[SourceId] [int] IDENTITY(1,1) NOT NULL,
	[SourceName] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[createdBy] [varchar](50) NULL,
	[CreatedAt] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[LastModifiedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[SourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END
GO

IF (Not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'tnxErrorLog'))
BEGIN
CREATE TABLE [dbo].[tnxErrorLog](
	[ErrorLogId] [int] IDENTITY(1,1) NOT NULL,
	[LogId] [int] NULL,
	[StackMsg] [nvarchar](max) NULL,
	[createdBy] [varchar](50) NULL,
	[CreatedAt] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[LastModifiedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ErrorLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

End
GO


IF (Not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'txnSeviceLog'))
BEGIN
CREATE TABLE [dbo].[txnSeviceLog](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[SourceId] [int] NULL,
	[ProductId] [int] NULL,
	[PlainReq] [nvarchar](max) NULL,
	[PlainRes] [nvarchar](max) NULL,
	[EncrytedReq] [nvarchar](max) NULL,
	[EncrytedRes] [nvarchar](max) NULL,
	[Header] [nvarchar](max) NULL,
	[createdBy] [varchar](50) NULL,
	[CreatedAt] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[LastModifiedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO


IF (Not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'txnSourceMapwithSecurity'))
BEGIN
CREATE TABLE [dbo].[txnSourceMapwithSecurity](
	[ssmapId] [int] IDENTITY(1,1) NOT NULL,
	[SourceId] [int] NULL,
	[SecurityID] [int] NULL,
	[createdBy] [varchar](50) NULL,
	[CreatedAt] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[LastModifiedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ssmapId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END
GO
ALTER TABLE [dbo].[tnxErrorLog]  WITH CHECK ADD FOREIGN KEY([LogId])
REFERENCES [dbo].[txnSeviceLog] ([LogId])
GO
ALTER TABLE [dbo].[txnSeviceLog]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[mstProduct] ([ProductId])
GO
ALTER TABLE [dbo].[txnSeviceLog]  WITH CHECK ADD FOREIGN KEY([SourceId])
REFERENCES [dbo].[mstSource] ([SourceId])
GO
ALTER TABLE [dbo].[txnSourceMapwithSecurity]  WITH CHECK ADD FOREIGN KEY([SecurityID])
REFERENCES [dbo].[mstSecurityMech] ([SecurityID])
GO
ALTER TABLE [dbo].[txnSourceMapwithSecurity]  WITH CHECK ADD FOREIGN KEY([SourceId])
REFERENCES [dbo].[mstSource] ([SourceId])
GO
USE [master]
GO
ALTER DATABASE [BanKIntegration] SET  READ_WRITE 
GO
