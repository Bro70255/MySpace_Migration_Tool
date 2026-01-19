CREATE TABLE [dbo].[Login_Dtls](
	[Login_id] [int] IDENTITY(1,1) NOT NULL,
	[Employee_Code] [int] NULL,
	[Name] [varchar](100) NULL,
	[User_type] [int] NULL,
	[Password] [varchar](50) NULL,
 CONSTRAINT [PK_Login_Dtls] PRIMARY KEY CLUSTERED 
(
	[Login_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mfBranchMaster]    Script Date: 19-01-2026 13:31:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO