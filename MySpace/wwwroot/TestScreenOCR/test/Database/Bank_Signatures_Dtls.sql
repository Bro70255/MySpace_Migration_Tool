ALTER TABLE [dbo].[Bank_Signatures_Dtls] ADD  CONSTRAINT [DF_Bank_Signatures_Dtls_Status]  DEFAULT ((0)) FOR [Status]
GO
/****** Object:  StoredProcedure [dbo].[Add_New_Bank]    Script Date: 19-01-2026 13:31:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO