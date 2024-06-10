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
               ,[LastModifiedAt]
               ,[PrivateFilePath]
               ,[PrivateFilePassword]
               ,[PublicFilePath]
               ,[EncryptDecryptPassword])
         VALUES
               ('BOI_NewTerm'
               ,1
               ,'Admin'
               ,getdate()
               ,'Admin'
               ,getdate()
               ,'D:\Certificate\UBI\pvtkeytxn.pfx'
               ,'Sud@4321'
               ,'D:\Certificate\UBI\Public ServiceCertificateSUD.crt'
               ,'BF6886467F3E4984BE219BC8010F382D')
End
GO