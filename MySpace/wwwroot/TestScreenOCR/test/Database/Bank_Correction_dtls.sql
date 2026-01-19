CREATE TABLE [dbo].[Bank_Correction_dtls](
	[Correction_Id] [int] NULL,
	[ACCOUNT_NUMBER] [numeric](18, 0) NULL,
	[SIGNATURE1] [varchar](200) NULL,
	[SIGNATURE2] [varchar](200) NULL,
	[SIGNATURE3] [varchar](200) NULL,
	[SIGNATURE4] [varchar](200) NULL,
	[Edited_by] [int] NULL,
	[Created_datetime] [datetime] NULL,
	[Status] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bank_Signatures_Dtls]    Script Date: 19-01-2026 13:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO