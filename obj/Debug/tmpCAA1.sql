IF object_id(N'[dbo].[FK_dbo.CustomerReserves_dbo.StaffBusinessItems_StaffBusinessItemsId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [FK_dbo.CustomerReserves_dbo.StaffBusinessItems_StaffBusinessItemsId]
IF object_id(N'[dbo].[FK_dbo.CustomerReserves_dbo.StoreDetails_StoreId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [FK_dbo.CustomerReserves_dbo.StoreDetails_StoreId]
IF object_id(N'[dbo].[FK_dbo.StaffBusinessItems_dbo.StoreDetails_StoreId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffBusinessItems] DROP CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.StoreDetails_StoreId]
IF object_id(N'[dbo].[FK_dbo.StaffBusinessItems_dbo.BusinessItems_BusinessItemsId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffBusinessItems] DROP CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.BusinessItems_BusinessItemsId]
IF object_id(N'[dbo].[FK_dbo.StaffBusinessItems_dbo.StaffDetails_StaffId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffBusinessItems] DROP CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.StaffDetails_StaffId]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_BusinessItemsId' AND object_id = object_id(N'[dbo].[StaffBusinessItems]', N'U'))
    DROP INDEX [IX_BusinessItemsId] ON [dbo].[StaffBusinessItems]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_StoreId' AND object_id = object_id(N'[dbo].[StaffBusinessItems]', N'U'))
    DROP INDEX [IX_StoreId] ON [dbo].[StaffBusinessItems]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_StaffId' AND object_id = object_id(N'[dbo].[StaffBusinessItems]', N'U'))
    DROP INDEX [IX_StaffId] ON [dbo].[StaffBusinessItems]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_StoreId' AND object_id = object_id(N'[dbo].[CustomerReserves]', N'U'))
    DROP INDEX [IX_StoreId] ON [dbo].[CustomerReserves]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_StaffBusinessItemsId' AND object_id = object_id(N'[dbo].[CustomerReserves]', N'U'))
    DROP INDEX [IX_StaffBusinessItemsId] ON [dbo].[CustomerReserves]
ALTER TABLE [dbo].[CustomerReserves] ADD [BusinessItemsId] [int]
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
CREATE INDEX [IX_BusinessItemsId] ON [dbo].[CustomerReserves]([BusinessItemsId])
CREATE INDEX [IX_BusinessItemsId] ON [dbo].[StaffBusinessItems]([BusinessItemsId])
CREATE INDEX [IX_StaffId] ON [dbo].[StaffBusinessItems]([StaffId])
ALTER TABLE [dbo].[CustomerReserves] ADD CONSTRAINT [FK_dbo.CustomerReserves_dbo.BusinessItems_BusinessItemsId] FOREIGN KEY ([BusinessItemsId]) REFERENCES [dbo].[BusinessItems] ([Id])
ALTER TABLE [dbo].[StaffBusinessItems] ADD CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.BusinessItems_BusinessItemsId] FOREIGN KEY ([BusinessItemsId]) REFERENCES [dbo].[BusinessItems] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[StaffBusinessItems] ADD CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.StaffDetails_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [dbo].[StaffDetails] ([Id]) ON DELETE CASCADE
DECLARE @var2 nvarchar(128)
SELECT @var2 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.StaffBusinessItems')
AND col_name(parent_object_id, parent_column_id) = 'StoreId';
IF @var2 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[StaffBusinessItems] DROP CONSTRAINT [' + @var2 + ']')
ALTER TABLE [dbo].[StaffBusinessItems] DROP COLUMN [StoreId]
DECLARE @var3 nvarchar(128)
SELECT @var3 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.CustomerReserves')
AND col_name(parent_object_id, parent_column_id) = 'StoreId';
IF @var3 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [' + @var3 + ']')
ALTER TABLE [dbo].[CustomerReserves] DROP COLUMN [StoreId]
DECLARE @var4 nvarchar(128)
SELECT @var4 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.CustomerReserves')
AND col_name(parent_object_id, parent_column_id) = 'StaffBusinessItemsId';
IF @var4 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [' + @var4 + ']')
ALTER TABLE [dbo].[CustomerReserves] DROP COLUMN [StaffBusinessItemsId]
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202211281712076_updateStaffBusinessItems', N'BonnieYork.Migrations.Configuration',  0x1F8B0800000000000400ED5DDD6EE3BA11BE2FD077107CD5167BE2248B05DAC039075967D3A6DDDD2CE2EC697B15C812ED0891255792D304459FAC177DA4BE42295B3FFCE790FAB19C13EC4D562467C8E1C7E17048CFFCEF3FFF9DFCF4BC0A9D2794A4411C9D8F4E8E8E470E8ABCD80FA2E5F968932D7EF8FDE8A71F7FFDABC9277FF5ECFC5CD67B9FD7C32DA3F47CF49065EBB3F138F51ED0CA4D8F568197C469BCC88EBC783576FD787C7A7CFC87F1C9C9186112234CCB7126B79B280B5668FB1FFCDF691C79689D6DDCF04BECA3302DBEE392D996AAF3D55DA174ED7AE87CF4318EA200FD3D4E1E8F769547CE4518B8B82333142E468E1B4571E666B89B67DF5334CB92385ACED6F8831BDEBDAC11AEB770C31415DD3FABAB4347727C9A8F645C372C49799B348B5786044FDE17A219B3CDAD043CAA448785F7090B397BC947BD152096DD260D2294A6D7D1224E56051796EFD9344CF23602511F0908BC73EA6AEF2A846020E5FFDE39D34D986D12741EA14D96B8E13BE7DB661E06DE5FD0CB5DFC88A2F368138664A771B77119F5017FFA96C46B94642FB768510CE5DA1F3963BADD986D583523DAEC46761D65EF4F47CE57CCDC9D87A8C2042185591627E88F2842899B21FF9B9B652889721A682B558E3BC36BDB9C63A86E73879704AE889227372C1B6200E3A53872BEB8CF9F51B4CC1ECE471FF0DABB0A9E915F7E283AFF3D0AF0C2C56DB2648374BCFE8AD0A3EFBECC3237C972BE3DF1FB14F93D72FB9820F7B10F7E7F8AC3A04F6916FC7A9266C1AD3769EE5444C155C1ECD48AD957F729586ED7B868C95EA2CC0DF0EABB45E1B64EFA10AC775BCB11517E2FD4A45749BCBA8D439A94A8EAFD9D9B2C51868716C3EACFE24DE23143998C6BFDAED4FAD4B84CB43DD1F04DCB8B79453E9667F262A8E82F3C2FC6169002DA27C7306C7363D3AC2C374DFF19277EFF9CB782CEFF6CAE3C0C394FB7F3D833D3CB00C322F05453DC0DE30BDF4FB0F2E85FCA280CD70F7184BE6E567394A800F6A11364B7C95B076577B1B80BB2508565BBBD8945114ABD2458EF7617292BFC670BBC3E62AD8812AC011F3A6775850F72F3387EFC1C448F9D33BB8ED2CC5D26EEAA176E9809EA8691D46C119A22DD9A2FA53902355F4A73C77848195AA5C2C15035EE290BA71E86B412677FC96B8A2C2F55D74B8B40D8EBB250D6615139D7576125D36E6E7598D2D6ADCA659D955411D8B6E27A8D6C5A0621563E8CBCE99B5D2BD9E244DE0B53936F8DB4E7D39356CE8BD8D2EA9CC96E2F9EABF8B4B385DCA2959B74BF537DC3D6298676371BBE54EF4CB70B1525B72845C91312EA1EA6CE3DB3D66B0DA4ACC8E921756D2B05AADFA7F86AF2E1E8EA8A35ABAA81F9A0D41E9086BB2E6B36E8F767ABBD81C398C9EEC0347EDB1F24FB03469EA1D3A394AC61330A24866D8B49BCC463EC416D6F7969F6BB616F110D0CF2969436AB22602ADE74F751A838BACA3DA74BF881886B4AB71F49F52E0D78C52014D594863CA4FBC60ADBC6474DB77D53D7425EBF407F73898BFDB89CC1CE50A8A236ECC0C720C91E2E959767763775AD18F68D75AB6C97D02832CBDB3342D19ADD9E550DDFD49290572B5E865FA06EDB224BA3D880F70F869CFF1CCF7BB9F8E8D43961ADAC5B191A467912FB1BAF974B9DB79B96BEF7B816EC6EFE76456FA39B0D81771C0106C57A6B886D51E9C322BB0FF06051D7053687A2EADD906C204505E5100475C49D17556CDBEDD6E0324685A5D61C6E225FA8B1A5F4762DA33798D4BE301B2BC29848031751ABBE7021B021CEF3561D2C6DE844C048445AC87EA5560AD2788D162DDF56A7C18202B4C107F5F466B1E8D1920181DB769F14025AB5A15A41B97E686102E3B2D51B84C5BC0AF1F4E335B33688ACDED1B0B0543EB68140F2224D632FD8F68DEFB7F089153DFC4F91EF18BEB7DA4D08F39A0B4F0D866BB0C600C5BD3C1F1D1F1D9D70B28633ABCCDC9A99F0B918CDF4771C478C7A94E4B073C3299E3DBC8E8228E397481079C1DA0DCD24C19001AEB57C2A2B866CC925CA9FABE0FE9A8909D293CACFC577A7E2CA28059DF02663027D6A50AA6FE66428015ED3D518E10EBB3A7C4079A9F0B8331B01F0B70223A85F3D40113417907E70E787BD0252E225D1A144E735E12169A526A1D7BB0D56402350AAC5D0232AD5028274847C19B21744AA9C76F26D14E0C123F76BC28836DCAF0157F47DA31030FA5EF668AD6860FB737170DB1FF8546E112530403E120686CAED53B551C35F2802F7EA13767E2637D1250A51869C0B6FF71BFAA99B7AAECF1F1EB051EEB70564C090FA4233603E0F67A7D7F8C80C6026F42F74086BA1DF1FA4CD070969C170F60368C13C1E88861679C3948052BAC61828552E5943D02AAFA9EC8D0F7BD8293AD417E014723F10A809AEFA20C6A1D0D90502811667AA9F79817C5203D18AF271F46CAD0AE66AF89E24F9CF3F64E001FC16046829AA200AF89DE78180543B921E60AA9DB3E10355783F20438FFAB2A0064E7DE164747C57FFA61784CB96CEECAA81F6802B952040EC89582B3DA06A77E783DBE07E4628295560756B7839CFCBD07326B880FC9EA2E20E322DEEC25874E4B4672853FD84BFBE7452DDC270B8A32953A8E22852A51A4A8C7696F76E57AEA1C679A9387A5C0D2045E960D90A5AC911E6924072442984924E7CA24A10BAD5C9414CB12AD6D0AA951B47A72E6268100B470838719409A2158F42FDADA963736F5A8D9659109C1A31BF25254843D6E798161A40A09A1FD6F1E234B8EF135E78A86FFC88F16A56A801719530854BC15E8CB2A7B9723942EE056C2EAA049204E012782D059F260B592ADF388BD637F076C5F87E855AD752856C7C9BD2BDF094AF202512045F11D85D12B0B2542E40BB5B810E97B7EE352648A652D79E8D77BA55798AFC7B30F0DBCA52F8F84F2245AD6BD4CC39CA0E4C62C3987943BB9596E871BE5A114A4FA646EEBD860A50742485D94A168252040FE145057435193A9BA00AC8D0BBD499C8C4EF1C7969E9FD1D708F07311899E50FF76F34964CF9F6B23A87576593F12E367CF161329604919F7C71D7EB205A1241E58B2FCE6C17517EFAC3CC3CD6FA6A4763EC519B2FEB35A838E1C1BB4BC49462D6B8A757419266976EE6CEDDFC8DECD45F71D5445E07C979AEE428772CF053591EF8CAB6F9DFACBB4311015EE0BD29285DE141AF720FD076F23547258E8A9387FF774337113C889EC6E16615C9FD52F2D695679224217557CAE9D0C1DA496274099C221F929DA4CA971A53AEC2850BE85665C65489B0E002BA44299C321F4E9DA4CC971A53164A822D33A62A91045F0AA7CC844227C932453CCDC99859869CD3945BFADCA506AD55403A47BD0799E81A0525808E51B6EE46B7902E6A8A8AC2752DA75645362049551F0D3054052AA0E0537D35D49DBBDF8670DA73F7194E6B17969B243315FC404545A18EB14D52A9BF1A48BA0C9A4D49BAFC683026369400353CB6D060066534BFD9D1DB9E018A400EF444D6DF0DE6818C524D4D055900A74746A226C991DFE1D4E81008243DBAC46495A7649C037AA15345709A753403925CFD7530DA5DE777B2B22545B44CAC4871FB61DB8F44745C8A52FDD9403FEC02E0529A61F7C97415CF916809CF8D2895D109493AE5378331912160A89191058359177AC7B6C9CAD05003AC0D2D85AE5647F17A8EDB56CCE8903F09A13651C54F45E4D4B8D7D0D4B6A27B2A2DA74BC517A5E14E1418D3E3B50255D0D54ADCF3CA69E5C8A02666B06EFA3D380CCFD4A7E3278A96A0B1C1DF91715CC73AA45675F5753040575F8F981D8CA59440076345EB611B4DC35B275B618A8EC4D56738AD3ABA1E49AAFEDABDF1D4F7DAA583E1D12729B2E4EDB8B74F9DD5E6994F4F10AAC1F672FAEBCA9234B59BF70987D2E5DC021024A4A01090361FF6E1860EFAC411AB4A0633EDD54570B3299791014CB7BC69B79E7C7E67A74B7A9E22EE3A9CAD5271AFAEC599EBEF497115AD4FB4CEDD4DEFAA8C1C2CAAA7C0CFEFA5672F295675477985A3D93FC26918A0DC0E2B2B7C71A36081D26C17336B747A7C72CA246B1F4EE2F4719AFAA1E02A5F94734C1AD1A9871860412EE0992ECA974D72568607F7A3060C7CF47C3EFAD7B6CD9973FDB7FBA2D93BE726C1B37DE61C3BFF265943A2F88A529E474F6EE23DB8091772CC8CB42CC379BBE49914DCED12E7326EB7445E96AEBC5DF2DD8846968CBC25F2C2DCE342DAA71ADA56F9B95F873EE1D36243554ADDB2895661720208A76F1B9BBF61C8FFD6081337E860341B27BF6E8B269BDBBA2DBA4CEAEAD644208EEF2F9EBB0F6D249E0690B6CB336DA5870413C8A7952E29FF66E53EFFD6941E9F3ABA113951D28246048589091A5164930F1810B34C71FB3A36866E0C4DBB6CB412E56D6C3090C9675BA2C9E69A6D84553A5960235282B42C5D205F1D82EF70B14F850787637FDBAC8939C467DE8432AF5B36E12F495B00ED04D3BC494F0409411BAE2D2EE9E77ED6AA6DAEC5D7B1B40ECEDE176527ECC9DEE5C0649550D0EAB46D9573EF7520748F86CFC12D0EE2EE1C7CECB1CA5ED7D271AA9955D4ECC06ADE5B51FEB9B7E35A4B3AEB359ED9FAB1DEF668420B58DB25547A1DF3BDBF730A7DFBDDDD52155F261FEC848992F6D81993BD66BA69299D8D30EA922E6A5AD7B96B7A8DD3A8FC2536CF76BFA119EDB2D130D11AA0414381293EAAE03EAAF0739D4046F710AE75D0E87F58C2B31C462CF956B2C6B49F1E667FD8D1FED46110E039F4DC2E2D2571D93F5CD4BF1918045606107ADD260F8BE5E6D46AB46955F4B0C699320E636B03BD2C1FEAEED624538A99A27AE5B0EB59CD59826E009ACE389F49ABFB211DD84F1AB5F8D54044FE43874182C3280389F189BE4B156486D2C33CF99B0278BF277EF3AC21C38194A5697798B0B2D9C60E2ECB471B293D2CF0D9720A8F9E13601F4E7E0E3EA0263B89AD65DED8FD1CEA7CE4CF638C829D4F5D501192C7019CA343C493A8A0E76598C54339C6AE127D88983295E07CC1E940545CA1D235C819229EC9AA0290974D561129E70EB38F48791635F4EC40094A446C8AD2408099412530B1CA52A2505FAF21258910DE86E1BB250AA8A3EC0E7B4B33D2349F483FE2E93B73885584FC3D88A4BF7C206D2C29F9D6034C777100093E1AE0676FE2E9386747539130C68236C1C7F0927134DBAC2D8438C4341BADD92C661A6878E933CC73655888AE9DDC187CD00F7CF2DEE0D6ABDD3B2F7CF84F83654D62826946C8A3CEDC559DDC982C8FFF4C8FCA2ACC0BB52F78683E3E905F2459B070BD0C177B789E836839727E76C30DAEF2693547FE7574B3C9D69B0C0F19ADE62125CCDC85A0E2BF4D0042F77972B3FD8D6BDAC6107037B18ECAD04DF47113847ED5EF2BC12B3A0989DC37513CFBCBE732CB9FFF2D5F2A4A5FE30848A8105FE552B943AB758889A537D1CCCD8D12F3BE7D4FD167B474BD9732768B9C887E2268B14F2E83ED6BEEB4A051B7C7FFC518F657CF3FFE1F9500575F8AC40000 , N'6.2.0-61023')

