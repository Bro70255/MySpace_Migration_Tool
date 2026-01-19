CREATE TABLE [dbo].[Approval_Flow_Table](
	[Approval_id] [int] IDENTITY(1,1) NOT NULL,
	[ID] [int] NULL,
	[Emp_code] [int] NULL,
	[Created_datetime] [datetime] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Approval_Flow_Table] PRIMARY KEY CLUSTERED 
(
	[Approval_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bank_Correction_dtls]    Script Date: 19-01-2026 13:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO