USE [Maverick.Example]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 04/04/2013 15:57:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SocialStatus]    Script Date: 04/04/2013 15:57:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SocialStatus](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_SocialStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person_SocialStatus]    Script Date: 04/04/2013 15:57:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person_SocialStatus](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Person_ID] [bigint] NOT NULL,
	[SocialStatus_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_Person_SocialStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Person_SocialStatus_Person]    Script Date: 04/04/2013 15:57:02 ******/
ALTER TABLE [dbo].[Person_SocialStatus]  WITH CHECK ADD  CONSTRAINT [FK_Person_SocialStatus_Person] FOREIGN KEY([Person_ID])
REFERENCES [dbo].[Person] ([ID])
GO
ALTER TABLE [dbo].[Person_SocialStatus] CHECK CONSTRAINT [FK_Person_SocialStatus_Person]
GO
/****** Object:  ForeignKey [FK_Person_SocialStatus_SocialStatus]    Script Date: 04/04/2013 15:57:02 ******/
ALTER TABLE [dbo].[Person_SocialStatus]  WITH CHECK ADD  CONSTRAINT [FK_Person_SocialStatus_SocialStatus] FOREIGN KEY([SocialStatus_ID])
REFERENCES [dbo].[SocialStatus] ([ID])
GO
ALTER TABLE [dbo].[Person_SocialStatus] CHECK CONSTRAINT [FK_Person_SocialStatus_SocialStatus]
GO
