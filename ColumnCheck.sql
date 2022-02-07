SELECT [name], TYPE_NAME([system_type_id]) FROM sys.columns 
          WHERE Name IN (N'Tagname', N'ScaleFactor')
          AND Object_ID = Object_ID(N'[dbo].[AgentTag]')
