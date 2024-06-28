SET IDENTITY_INSERT [dbo].[mstProduct] ON
IF NOT EXISTS (select * from [dbo].[mstProduct] WHERE ProductName = 'SaralJeevanBima' )
BEGIN
        INSERT INTO [dbo].[mstProduct]
                   ([ProductId]
                   ,[ProductName]
                   ,[CreatedBy]
                   ,[CreatedAt]
                   ,[LastModifiedBy]
                   ,[LastModifiedAt]
                   ,[Productcode])
             VALUES
                   (1,
                   'SaralJeevanBima',
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
               ([ProductId]
               ,[ProductName]
               ,[CreatedBy]
               ,[CreatedAt]
               ,[LastModifiedBy]
               ,[LastModifiedAt]
               ,[Productcode])
         VALUES
               (2,
               'ProtectShield',
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
               ([ProductId]
               ,[ProductName]
               ,[CreatedBy]
               ,[CreatedAt]
               ,[LastModifiedBy]
               ,[LastModifiedAt]
               ,[Productcode])
         VALUES
               (3,
               'ProtectShieldPlus',
		       'Admin',
		       Getdate(),
		       'Admin',
		       getdate(),
		       '1038')
END
Go
IF NOT EXISTS (select * from [dbo].[mstProduct] WHERE ProductName = 'New Term' )
Begin 
    INSERT INTO [dbo].[mstProduct]
               ([ProductId]
               ,[ProductName]
               ,[CreatedBy]
               ,[CreatedAt]
               ,[LastModifiedBy]
               ,[LastModifiedAt]
               ,[Productcode])
         VALUES
               (4,
               'New Term',
		       'Admin',
		       Getdate(),
		       'Admin',
		       getdate(),
		       '1038')
END
SET IDENTITY_INSERT [dbo].[mstProduct] OFF
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
               ,[EncryptDecryptPassword]
               ,[secretkeyfortoken]
               ,[secretkeyforchecksum])
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
               ,'BF6886467F3E4984BE219BC8010F382D'
               ,'2CEF55EB764E420DAADFD047BE9DAEE1'
               ,'DAB2D4A679EA48F39CF006475CF48859')
End
GO



SET IDENTITY_INSERT [dbo].[mstSecurityMech] ON
IF NOT EXISTS (select * from [dbo].[mstSecurityMech] WHERE SecurityName = 'DecryptToken' )
Begin 
    Insert into [dbo].[mstSecurityMech](SecurityID,SecurityName,Type,IsActive,CreatedAt,CreatedBy,LastModifiedAt,LastModifiedBy)
    Values(1,'Decrypt Token','Request',1,GETDATE(),'Admin',GETDATE(),'Admin')
END
IF NOT EXISTS (select * from [dbo].[mstSecurityMech] WHERE SecurityName = 'ValidateToken' )
Begin 
    Insert into [dbo].[mstSecurityMech](SecurityID,SecurityName,Type,IsActive,CreatedAt,CreatedBy,LastModifiedAt,LastModifiedBy)
    Values(2,'Validate Token','Request',1,GETDATE(),'Admin',GETDATE(),'Admin')
END
IF NOT EXISTS (select * from [dbo].[mstSecurityMech] WHERE SecurityName = 'ValidateDigitalSign' )
Begin 
    Insert into [dbo].[mstSecurityMech](SecurityID,SecurityName,Type,IsActive,CreatedAt,CreatedBy,LastModifiedAt,LastModifiedBy)
    Values(3,'Validate Digital Sign','Request',1,GETDATE(),'Admin',GETDATE(),'Admin')
END
IF NOT EXISTS (select * from [dbo].[mstSecurityMech] WHERE SecurityName = 'DecryptRequest' )
Begin 
    Insert into [dbo].[mstSecurityMech](SecurityID,SecurityName,Type,IsActive,CreatedAt,CreatedBy,LastModifiedAt,LastModifiedBy)
    Values(4,'Decrypt Request','Request',1,GETDATE(),'Admin',GETDATE(),'Admin')
END
IF NOT EXISTS (select * from [dbo].[mstSecurityMech] WHERE SecurityName = 'ValidateCheckSum' )
Begin 
    Insert into [dbo].[mstSecurityMech](SecurityID,SecurityName,Type,IsActive,CreatedAt,CreatedBy,LastModifiedAt,LastModifiedBy)
    Values(5,'Validate Checksum','Request',1,GETDATE(),'Admin',GETDATE(),'Admin')
END
IF NOT EXISTS (select * from [dbo].[mstSecurityMech] WHERE SecurityName = 'EncryptResponse' )
Begin 
    Insert into [dbo].[mstSecurityMech](SecurityID,SecurityName,Type,IsActive,CreatedAt,CreatedBy,LastModifiedAt,LastModifiedBy)
    Values(6,'Encrypt Response','Response',1,GETDATE(),'Admin',GETDATE(),'Admin')
END
IF NOT EXISTS (select * from [dbo].[mstSecurityMech] WHERE SecurityName = 'DigitalSign' )
Begin 
    Insert into [dbo].[mstSecurityMech](SecurityID,SecurityName,Type,IsActive,CreatedAt,CreatedBy,LastModifiedAt,LastModifiedBy)
    Values(7,'Digital Sign','Response',1,GETDATE(),'Admin',GETDATE(),'Admin')
END
IF NOT EXISTS (select * from [dbo].[mstSecurityMech] WHERE SecurityName = 'ChecksumGeneration' )
Begin 
    Insert into [dbo].[mstSecurityMech](SecurityID,SecurityName,Type,IsActive,CreatedAt,CreatedBy,LastModifiedAt,LastModifiedBy)
    Values(8,'Checksum Generation','Response',1,GETDATE(),'Admin',GETDATE(),'Admin')
END
SET IDENTITY_INSERT [dbo].[mstSecurityMech] OFF



Insert into [dbo].[txnSourceMapwithSecurity](SourceId,SecurityID,orderId,CreatedBy,CreatedAt,LastModifiedBy,LastModifiedAt)
Values(1,1,1,'Admin',GETDATE(),'Admin',GETDATE()),
(1,2,2,'Admin',GETDATE(),'Admin',GETDATE()),
(1,3,3,'Admin',GETDATE(),'Admin',GETDATE()),
(1,4,4,'Admin',GETDATE(),'Admin',GETDATE()),
(1,5,5,'Admin',GETDATE(),'Admin',GETDATE()),
(1,6,1,'Admin',GETDATE(),'Admin',GETDATE()),
(1,7,2,'Admin',GETDATE(),'Admin',GETDATE()),
(1,8,3,'Admin',GETDATE(),'Admin',GETDATE())


