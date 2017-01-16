ALTER TABLE [DB_MoneyTrace].[dbo].[TB_Trace]
	ADD TransferredUserID UniqueIdentifier NULL


ALTER TABLE [dbo].[TB_Trace]  WITH CHECK ADD  CONSTRAINT [FK_TB_Trace_TB_User1] FOREIGN KEY([TransferredUserID])
REFERENCES [dbo].[TB_User] ([ID])

ALTER TABLE [dbo].[TB_Trace] CHECK CONSTRAINT [FK_TB_Trace_TB_User1]

-------------------------------------------------------------------------------------------------------------------------------------

