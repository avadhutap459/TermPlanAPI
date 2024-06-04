IF NOT EXISTS (select * from [dbo].[mstProduct] WHERE ProductName = 'SaralJeevanBima' )
BEGIN
        INSERT INTO [dbo].[mstProduct]
                   ([ProductName]
                   ,[CreatedBy]
                   ,[CreatedAt]
                   ,[LastModifiedBy]
                   ,[LastModifiedAt]
                   ,[Productcode])
             VALUES
                   ('SaralJeevanBima',
		           'Admin',
		           Getdate(),
		           'Admin',
		           getdate(),
		           '1029')
END
GO
IF NOT EXISTS (select * from [dbo].[mstProduct] WHERE ProductName = 'ProtectShield' )
BEGIN
    INSERT INTO [dbo].[mstProduct]
               ([ProductName]
               ,[CreatedBy]
               ,[CreatedAt]
               ,[LastModifiedBy]
               ,[LastModifiedAt]
               ,[Productcode])
         VALUES
               ('ProtectShield',
		       'Admin',
		       Getdate(),
		       'Admin',
		       getdate(),
		       '1034')
END
GO
IF NOT EXISTS (select * from [dbo].[mstProduct] WHERE ProductName = 'ProtectShieldPlus' )
Begin 
    INSERT INTO [dbo].[mstProduct]
               ([ProductName]
               ,[CreatedBy]
               ,[CreatedAt]
               ,[LastModifiedBy]
               ,[LastModifiedAt]
               ,[Productcode])
         VALUES
               ('ProtectShieldPlus',
		       'Admin',
		       Getdate(),
		       'Admin',
		       getdate(),
		       '1038')
END
GO
IF NOT EXISTS (select * from [dbo].[mstSource] WHERE SourceName = 'BOI_NewTerm' )
Begin 
    INSERT INTO [dbo].[mstSource]
               ([SourceName]
               ,[IsActive]
               ,[CreatedBy]
               ,[CreatedAt]
               ,[LastModifiedBy]
               ,[LastModifiedAt])
         VALUES
               ('BOI_NewTerm'
               ,1
               ,'Admin'
               ,getdate()
               ,'Admin'
               ,getdate())
End
GO