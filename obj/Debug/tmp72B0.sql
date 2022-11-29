IF object_id(N'[dbo].[FK_dbo.CustomerReserves_dbo.StaffBusinessItems_StaffBusinessItemsId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [FK_dbo.CustomerReserves_dbo.StaffBusinessItems_StaffBusinessItemsId]
IF object_id(N'[dbo].[FK_dbo.StaffBusinessItems_dbo.StoreDetails_StoreId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffBusinessItems] DROP CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.StoreDetails_StoreId]
IF object_id(N'[dbo].[FK_dbo.CustomerReserves_dbo.StoreDetails_StoreId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [FK_dbo.CustomerReserves_dbo.StoreDetails_StoreId]
IF object_id(N'[dbo].[FK_dbo.StaffBusinessItems_dbo.BusinessItems_BusinessItemsId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffBusinessItems] DROP CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.BusinessItems_BusinessItemsId]
IF object_id(N'[dbo].[FK_dbo.StaffBusinessItems_dbo.StaffDetails_StaffId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffBusinessItems] DROP CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.StaffDetails_StaffId]
IF object_id(N'[dbo].[FK_dbo.CustomerReserves_dbo.CustomerDetails_CustomerId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [FK_dbo.CustomerReserves_dbo.CustomerDetails_CustomerId]
IF object_id(N'[dbo].[FK_dbo.CustomerReserves_dbo.StaffDetails_StaffId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [FK_dbo.CustomerReserves_dbo.StaffDetails_StaffId]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_BusinessItemsId' AND object_id = object_id(N'[dbo].[StaffBusinessItems]', N'U'))
    DROP INDEX [IX_BusinessItemsId] ON [dbo].[StaffBusinessItems]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_StoreId' AND object_id = object_id(N'[dbo].[StaffBusinessItems]', N'U'))
    DROP INDEX [IX_StoreId] ON [dbo].[StaffBusinessItems]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_StaffId' AND object_id = object_id(N'[dbo].[StaffBusinessItems]', N'U'))
    DROP INDEX [IX_StaffId] ON [dbo].[StaffBusinessItems]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_StoreId' AND object_id = object_id(N'[dbo].[CustomerReserves]', N'U'))
    DROP INDEX [IX_StoreId] ON [dbo].[CustomerReserves]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_StaffId' AND object_id = object_id(N'[dbo].[CustomerReserves]', N'U'))
    DROP INDEX [IX_StaffId] ON [dbo].[CustomerReserves]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_CustomerId' AND object_id = object_id(N'[dbo].[CustomerReserves]', N'U'))
    DROP INDEX [IX_CustomerId] ON [dbo].[CustomerReserves]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_StaffBusinessItemsId' AND object_id = object_id(N'[dbo].[CustomerReserves]', N'U'))
    DROP INDEX [IX_StaffBusinessItemsId] ON [dbo].[CustomerReserves]
DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.StaffBusinessItems')
AND col_name(parent_object_id, parent_column_id) = 'BusinessItemsId';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[StaffBusinessItems] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[StaffBusinessItems] ALTER COLUMN [BusinessItemsId] [int] NOT NULL
DECLARE @var1 nvarchar(128)
SELECT @var1 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.StaffBusinessItems')
AND col_name(parent_object_id, parent_column_id) = 'StaffId';
IF @var1 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[StaffBusinessItems] DROP CONSTRAINT [' + @var1 + ']')
ALTER TABLE [dbo].[StaffBusinessItems] ALTER COLUMN [StaffId] [int] NOT NULL
DECLARE @var2 nvarchar(128)
SELECT @var2 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.CustomerReserves')
AND col_name(parent_object_id, parent_column_id) = 'StoreId';
IF @var2 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [' + @var2 + ']')
ALTER TABLE [dbo].[CustomerReserves] ALTER COLUMN [StoreId] [int] NOT NULL
DECLARE @var3 nvarchar(128)
SELECT @var3 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.CustomerReserves')
AND col_name(parent_object_id, parent_column_id) = 'StaffId';
IF @var3 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [' + @var3 + ']')
ALTER TABLE [dbo].[CustomerReserves] ALTER COLUMN [StaffId] [int] NOT NULL
DECLARE @var4 nvarchar(128)
SELECT @var4 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.CustomerReserves')
AND col_name(parent_object_id, parent_column_id) = 'CustomerId';
IF @var4 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [' + @var4 + ']')
ALTER TABLE [dbo].[CustomerReserves] ALTER COLUMN [CustomerId] [int] NOT NULL
CREATE INDEX [IX_BusinessItemsId] ON [dbo].[StaffBusinessItems]([BusinessItemsId])
CREATE INDEX [IX_StaffId] ON [dbo].[StaffBusinessItems]([StaffId])
CREATE INDEX [IX_StoreId] ON [dbo].[CustomerReserves]([StoreId])
CREATE INDEX [IX_StaffId] ON [dbo].[CustomerReserves]([StaffId])
CREATE INDEX [IX_CustomerId] ON [dbo].[CustomerReserves]([CustomerId])
ALTER TABLE [dbo].[CustomerReserves] ADD CONSTRAINT [FK_dbo.CustomerReserves_dbo.StoreDetails_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[StoreDetails] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[StaffBusinessItems] ADD CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.BusinessItems_BusinessItemsId] FOREIGN KEY ([BusinessItemsId]) REFERENCES [dbo].[BusinessItems] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[StaffBusinessItems] ADD CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.StaffDetails_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [dbo].[StaffDetails] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[CustomerReserves] ADD CONSTRAINT [FK_dbo.CustomerReserves_dbo.CustomerDetails_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[CustomerDetails] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[CustomerReserves] ADD CONSTRAINT [FK_dbo.CustomerReserves_dbo.StaffDetails_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [dbo].[StaffDetails] ([Id]) ON DELETE CASCADE
DECLARE @var5 nvarchar(128)
SELECT @var5 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.StaffBusinessItems')
AND col_name(parent_object_id, parent_column_id) = 'StoreId';
IF @var5 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[StaffBusinessItems] DROP CONSTRAINT [' + @var5 + ']')
ALTER TABLE [dbo].[StaffBusinessItems] DROP COLUMN [StoreId]
DECLARE @var6 nvarchar(128)
SELECT @var6 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.CustomerReserves')
AND col_name(parent_object_id, parent_column_id) = 'StaffBusinessItemsId';
IF @var6 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [' + @var6 + ']')
ALTER TABLE [dbo].[CustomerReserves] DROP COLUMN [StaffBusinessItemsId]
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202211281815196_createAllTable', N'BonnieYork.Migrations.Configuration',  0x1F8B0800000000000400ED5D5B6F1BBB117E2FD0FFB0D0535BE458B60F02B4867C0E1C3969DD26716039A7ED9341ED52F2C27B517757A98DA2BFAC0FFD49FD0BE56A6FBCDFF6A29523E4C511C96FC8E170381C7267FEF79FFFCE7E7E0E03E71B4C523F8E2E276727A71307466EECF9D1FA72B2CD563FFC7EF2F34FBFFED5ECBD173E3BBF54F57ECCEBA196517A3979CCB2CDC5749ABA8F3004E949E8BB499CC6ABECC48DC329F0E2E9F9E9E91FA667675388202608CB716677DB28F343B8FB0FFAEF3C8E5CB8C9B620F8147B3048CBDF51C96287EA7C06214C37C0859793777114F9F0EF71F27452549E3857810F50471630584D1C1045710632D4CD8BAF295C64491CAD171BF40308EE5F3610D55B81208565F72F9AEABA23393DCF47326D1A5650EE36CDE2D010F0ECC7923553BAB915832735EB10F3DE2326672FF9A8770C44BCDBA67E04D3F4265AC5495852A1E95ECC83246FC361F50907E08DD3547B534B0812A4FCDF1B67BE0DB26D022F23B8CD1210BC71BE6C9781EFFE05BEDCC74F30BA8CB64180771A751B95113FA09FBE24F10626D9CB1D5C9543B9F126CE946C37A51BD6CDB036C5C86EA2ECC7F389F3191107CB00D63281716191C509FC238C600232E87D0159069328C7803BAE32D4295ABBE60C41799B7BB4245045987C0341D51009305A8A13E71378FE08A375F67839798BD6DE07FF197AD50F65E7BF463E5AB8A84D966CA18AD65F217CF2C0CB22034996D31D88DEFBC81B90DABB0482A721E8FD290EFC21B959D21B889B25B5C1B859A88892AA84D8B915B1CFE09BBFDEAD71DE92BD8619F0D1EABB83C1AE4EFAE86F8AADE5042B7FE06AD20F491CDEC50109C5ABFA700F9235CCD0D062BDFA8B789BB8D45066D346BF4BB53E312E136D8F353C6A793EADC843FC4C5E0C15FD95EBC6C8029288F6D9A99E6C336353AC2C90A6FF8C136F78CA3B46E77FB6571E8694E7BB791C98E8B58FC4C2776553DC0FE12BCF4B90F2189ECB3008368F71043F6FC3254C6402F6B617C9EE92B64A94C16A75EF67814C96EDF6265A8A60EA26FEA6D85D84A4D09F1DD07A87B4224C90067CEC9DD40774905BC6F1D3473F7AEA9DD84D9466609D8070106A8808EC8790D06CE19A22FD9A2F9539A26BBE54E68EF1903218A6DCC110351E080BA71986B012637F896BF22C2F59D7E73BFB0A26773045E738C8ED3C5547D47D49356600B2BAA643A88C1A6EDFAB4251A779E54C6FB9954CBBB953C35273BD2E1775565085639EF3EBB532CB2921B772C3E44D8FA6399716DF01636AB56EA0F2887DD6C99117198BBD1329CC89A58C4E37BBE01D0C41D2FF66FB0519D848B4FBB159E47A47BD43B1D51EA846941E92D4E52B24590373652AF77DB4DC6F698341BD335B7A3AD8A9317378D0ED8FCA954B8BE0516B259B73DD18A485F1D8E9D2646D61CDB5DC879D5398816C17307B946FFD281B4A6D2275EB960E4C8C03C6EBF9E8C0ECDB4AFA2EDD9948B214EE4C4D179021E53FC7CB417C4FBD1A57D6CEC34E8686A43C89BDAD3B885FEDE8ECEAD2F8B673AF60DB87CCBDD26C686AF70AE610B0F21049F7E8FDEDF25C6BC6D84630326DEA7B5ED1A8CB0AA27914D5E15B2CBC8A5D1F965A789E64FCEFEC98C42C22139B8A6A7CB4ABB8B4BAF13E591D8C041AB32D4E39DFD7881D03F88F76B4148EB771FBAA945B98448190551E98F5CA6E62FC9AC27D4C50BD6B177C073B31AD107576EDAEF47807F734EAEE77ACD36D8EC964DBA346E7D2FA0E0FB9955CECE7D98EF6995057411B76E09D9F648FD7D2078876AF1D3B39DCB4DE1A449A49B193D83BF06A3BDFD88357B63C2A262E2DBE95A8D106C9767ABB5A0D7D75A63A38D91EF7B84727D9B9D04A949BC71126625CB53A8A309F56C99E61361A6B7BD0EAED0B2D96D207323A227995A6B1EBEFFAC6F69BFBB28B1CFEFBC8730C9F791513423D22435383C4D5DF200145BDBC9C9C9E9C9C31BCD627561F551A62DC576A24D1DF311491D4C324173B10CCD1ECA175E44719BB44FCC8F5372030E30405A3B9D6F2A9AC09D225D7307F6282FA6BC6269D9ED43E08B63B35554A29A898379B62D2A7124AC525AB5850746F5C71B1642FFE5582624051269B3C6267F4FCCC6EA36B18C00C3A576EF169E01CA42EF058E58416BDD78940EB0D691089D69B4F9DAE30EF0EF622DA0A1B5C2466BA06792364F4A1BE4729D3EE26672D3087905E74B41EFB0610683D06E97404F7CDEE5592790E3A957C48BD752D84438F12470C8903C3D814B2C65806145DC9DCE95916E591732442DBD8A4FAA2C4B1D07B125ACEBB722D837A8C42CB8C652F42CBCCDD8198C3DAF7F7524BD5FC327F781D69DB65915A37B4EDED6D6653E60E65409B7270FC6A5CE8EC934A92D4F347094DED71363C0A4A1F9368AD25AE2FC45E28251D1A4AFE247C3F1051E33CC8D1D1590A4B4122044A39937D7976201682621C036BC783B40CC49FD6888447E33B1B4D47954C4435BE9E3D1021558E64003155CED9F805957BFD21921EF95D482338CD7D9AD16D82FC33632DB9ECC86A940D7400B99231428B3C16C16600A92AAEB4501BD4CF0826950AAC2F45AF9779197CCE38F7AB5F53585EB1A6E5551F2D1D39F60266B2C008CD9D9AEC9289913B1299902A069128552051DA59DCBBA25CD92FF6A8C4E91E5B490757325EAC5481C438561834A68626A2B07B74059DB1D62707FE60EB620556A3DC189CA688C2C0160E57E0F8B13BB056AC14AA2F851D9B6BE17AB4D48260D488F9253006ADB33EA724D3B418AAFA0494C751931B4DD6DCD7B9D32478AA5AA7662464BCE56A0173AEAA1EA7B13C35B94AB3B94CC3062BD7033657601C7091D6B2E725F7EDB49891CA9B1CE3BB1CFD511A5FDED0D22E50D15D300FD34F3ACC1318B8C6770A9D318FB572F5D4AFAD6ED4FFD44CA028ED7CDD1D78BBF584AA031FB591AEB69D07EE6B4801CB95CE5433772A3D3E81D563E63FED6DC90B3FBA93CBA7DE525739045BCADCA0CB5B12CA856595A673CAD03DA56B8218FAA37A6319FFE127CB2DB58744DF47820D467456D0F788B4E64CF518B53EB9D765B36911A3BFFC61361504F39F7D029B8D1FADB1E0FEE52FCEA288EC3FFF61611EF33E2C30A62E617ED37E869A121A3C5843AA1491463DFDE02769760D32B004F9A3E1B91732D5787E0AC109B0A2287645B053591D11ABB6F9DFB4834412899FE3EF29913EA04187B9CF6837F98AC31583E2E46918400012CE0BF1791C6CC348ECC912B7AE7D993884D0C129C62183E6E36064893E221B1A1F47654B8D91EBB0ED1CDCBACC18150BCFCEC1C54AF591D9B0F638325B6A8CCCE5045D668C2AE0045BAA8F4C85A4C761A922167336A59621E36665963E730D426A152D9D23DF834C748D044943C7485BF7A35B70A736812271768BD1EA2F3F71A8FA470319AA3FE424C4A7FED55077161FCB30DAB3F8591FAB088F8EC3CC395FECC8109A58E7384AF3AB01A7ABE0E504A7AB1F0DC6447F3B4A0C8F2E34984111E6173BBCDD19A08CE6444E64F3BBC13CE0D1C289A9C00BF4F1F088E0381CFEBB3E1A190709C7234B4C56798A073B22173A51A48FD98434C2E19A5F47A3DD559E672B5B9287656245F2DB8FDB7EC442FC1248CDCF06FAA188E24B6886E227D355BC84BC25BC3442AA229BE038D56F0663C2E3C01123C30B46B32E389EB8B6C68F0A50CB065283F4B34C982FA6084DAEFA9C4A8C5B3FE863F62D3ECE3EC5A123235888A42B00C31AC15D29C8319ABFA00ABEC9489FA9F9DB84D3C4A19A5FFB5794431BB164F44BD26AC24B8EA6DD9E7496FA12DC446F29D03474971261DCFACB74A712AE47EC634D62294A3EE214A311E1F4480B0D2B30C6630D59A2A02FE371CF2BA5930D5E0E66B04E86DDE6C7B73D9361C3784BC5D847D5D356D884F822ACE2FAD7D1083A71FFDE811D2B80D2356485CDFBDA09BAD1E064D42B06AC2E19CDB4D717BFEDA65C04A331DDE2A6FD7AEE59DD41960C3C45CCF5375DA5A65E5F8353D7DDB3F2EA599DE09EB98B2EAA4C1CC4AA6FBE97DF432F5E5274623FC92B9C2CFE11CC031FE6CABEAAF00944FE0AA65911346C727E7A763E71AE021FA4C53B853125AC9FA6A91770AEEE7989D28421AD060882E6E70C5EA8C29CD924C5A568309F3D20C187CF97937FEDDA5C38377F7B289BBD716E1334DB17CEA9F36F9CB44ED4635EAAF9E81B48DC47903031D7CCA04599E5BB85A7529F770BCE643AEF085E9426BE5BF87E58234A02DF113C37E73B17FB5C816D9517FD75E813361DB9AE4A695AB6D12A548C64EEF4ED6215B70C81DC19307663AE2DCDC649C7BBC2A4738A77854BA50CEF8C05FC00CEFCB97BDB45C26F0D68BBFCDE567A8833816C3AEF0AF9372178FEAD291E9BB2BB151C2F53512B406E36A2568874C6210330CBBCBCAF6363E8C7D0B44BA12B50DEC606039E31B7234C3A416E2B5925138BB482E2E462EB43F255B7D4072BFE8274A5BACB806ADE6E3990D1DAF557E2AE990169ABEC9AAF63BAF7A8ED0ED0020664224B0D5BC72A4F654736543B55D8CE4A35EF2D2FD3E4D1466BBF53C9432E1FF5D678B725CE0AC4EECFCDA8372D5B758093D9AEA5E1C764AFDB8F21699B3CEC75ACA683DB8A79E9B60672C630C2649521CBCA156C9744EA7588683F5A57DBC7452580EA43B3F0EF8F0F76C278898AEC96E8A0D97D3A4AE1C30DC5A40AA5D677BE9E4183374A3FB666C90A5F2B8E3B030F1593413798689F01C4C55D1A2C3CB8EA1B90CEA54EEBCB1396EA2BCA91339E643875B42359A4C201D2DEEC21DFC2EBCD644384EA1105B4FC4EC44CFE79D32864ECF0B2CE589860AF58C00635D9EC046CEFE65A971962C690F8650476DBC09ACDD26A1B41820CE35C2C6662A6CC0F8B871814465C7E352222FE046394C261943D652CBB9E85941EE66E672AC0FBDDE9CC339E8C47A42C7D2387295636DBD8C16528E9221D89857C769C7E64E0DCE487935B840DED494F626759438A0FB52E27DE32465250B8FE391575F26D68E717E1D1C42AA869196620918EB1DF2425FCA1F699CC44485197B916E94E7834A94AFA74B593A2C8A8EA0ED628738A90B5650D3539ADE42A3C3265A9CFE1E3A892AF58655891A8AFD7934E85AB3D0CC3898BD5507F11FDF7961FA56D22144A7F1D72CA138EFA378ADBBF6F96F490C8A4ADCE199625634C4F629F0162600D34444E91B62CA14C08650292F1250B69B79C2C9838C634209D5932669BFCF8D27B98E7F2B0605D37B93BD82025E83CBE45ADC3E291DAEC1AA6FEBA819821CC08BAC449BCAE939B98955380EA5155857A5EF7090DCD43C7F4AB24F357C0CD50B18BE6D98FD613E717106C5195F7E1127A37D1ED36DB6C333464182E038299B96341467F97A084ECF3EC76F74D6EDAC5105037918ECAE06DF46EEB075EDDEF0F9C27800288DC6351BE59CCE732CBDF2EAE5F6AA4CF71A40954B2AF76B4DCC3701320B0F4365A807C6F34EFDBD7147E846BE0BE54B166C420EA8920D93EBBF6771FA2A42546D31EFD17C9B0173EFFF47F3D73B653B2C60000 , N'6.2.0-61023')

