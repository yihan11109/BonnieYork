IF object_id(N'[dbo].[FK_dbo.CustomerReserve_dbo.StaffBusinessItems_StaffBusinessItemsId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerReserve] DROP CONSTRAINT [FK_dbo.CustomerReserve_dbo.StaffBusinessItems_StaffBusinessItemsId]
IF object_id(N'[dbo].[FK_dbo.CustomerReserve_dbo.StoreDetails_StoreId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerReserve] DROP CONSTRAINT [FK_dbo.CustomerReserve_dbo.StoreDetails_StoreId]
IF object_id(N'[dbo].[FK_dbo.StaffBusinessItems_dbo.StoreDetails_StoreId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffBusinessItems] DROP CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.StoreDetails_StoreId]
IF object_id(N'[dbo].[FK_dbo.StaffBusinessItems_dbo.BusinessItems_BusinessItemsId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffBusinessItems] DROP CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.BusinessItems_BusinessItemsId]
IF object_id(N'[dbo].[FK_dbo.StaffBusinessItems_dbo.StaffDetails_StaffId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffBusinessItems] DROP CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.StaffDetails_StaffId]
IF object_id(N'[dbo].[FK_dbo.CustomerReserve_dbo.CustomerDetails_CustomerId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerReserve] DROP CONSTRAINT [FK_dbo.CustomerReserve_dbo.CustomerDetails_CustomerId]
IF object_id(N'[dbo].[FK_dbo.CustomerReserve_dbo.StaffDetails_StaffId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerReserve] DROP CONSTRAINT [FK_dbo.CustomerReserve_dbo.StaffDetails_StaffId]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_BusinessItemsId' AND object_id = object_id(N'[dbo].[StaffBusinessItems]', N'U'))
    DROP INDEX [IX_BusinessItemsId] ON [dbo].[StaffBusinessItems]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_StoreId' AND object_id = object_id(N'[dbo].[StaffBusinessItems]', N'U'))
    DROP INDEX [IX_StoreId] ON [dbo].[StaffBusinessItems]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_StaffId' AND object_id = object_id(N'[dbo].[StaffBusinessItems]', N'U'))
    DROP INDEX [IX_StaffId] ON [dbo].[StaffBusinessItems]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_StoreId' AND object_id = object_id(N'[dbo].[CustomerReserve]', N'U'))
    DROP INDEX [IX_StoreId] ON [dbo].[CustomerReserve]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_StaffId' AND object_id = object_id(N'[dbo].[CustomerReserve]', N'U'))
    DROP INDEX [IX_StaffId] ON [dbo].[CustomerReserve]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_CustomerId' AND object_id = object_id(N'[dbo].[CustomerReserve]', N'U'))
    DROP INDEX [IX_CustomerId] ON [dbo].[CustomerReserve]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_StaffBusinessItemsId' AND object_id = object_id(N'[dbo].[CustomerReserve]', N'U'))
    DROP INDEX [IX_StaffBusinessItemsId] ON [dbo].[CustomerReserve]
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
WHERE parent_object_id = object_id(N'dbo.CustomerReserve')
AND col_name(parent_object_id, parent_column_id) = 'StaffId';
IF @var2 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CustomerReserve] DROP CONSTRAINT [' + @var2 + ']')
ALTER TABLE [dbo].[CustomerReserve] ALTER COLUMN [StaffId] [int] NOT NULL
DECLARE @var3 nvarchar(128)
SELECT @var3 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.CustomerReserve')
AND col_name(parent_object_id, parent_column_id) = 'CustomerId';
IF @var3 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CustomerReserve] DROP CONSTRAINT [' + @var3 + ']')
ALTER TABLE [dbo].[CustomerReserve] ALTER COLUMN [CustomerId] [int] NOT NULL
CREATE INDEX [IX_BusinessItemsId] ON [dbo].[StaffBusinessItems]([BusinessItemsId])
CREATE INDEX [IX_StaffId] ON [dbo].[StaffBusinessItems]([StaffId])
CREATE INDEX [IX_StaffId] ON [dbo].[CustomerReserve]([StaffId])
CREATE INDEX [IX_CustomerId] ON [dbo].[CustomerReserve]([CustomerId])
ALTER TABLE [dbo].[StaffBusinessItems] ADD CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.BusinessItems_BusinessItemsId] FOREIGN KEY ([BusinessItemsId]) REFERENCES [dbo].[BusinessItems] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[StaffBusinessItems] ADD CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.StaffDetails_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [dbo].[StaffDetails] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[CustomerReserve] ADD CONSTRAINT [FK_dbo.CustomerReserve_dbo.CustomerDetails_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[CustomerDetails] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[CustomerReserve] ADD CONSTRAINT [FK_dbo.CustomerReserve_dbo.StaffDetails_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [dbo].[StaffDetails] ([Id]) ON DELETE CASCADE
DECLARE @var4 nvarchar(128)
SELECT @var4 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.StaffBusinessItems')
AND col_name(parent_object_id, parent_column_id) = 'StoreId';
IF @var4 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[StaffBusinessItems] DROP CONSTRAINT [' + @var4 + ']')
ALTER TABLE [dbo].[StaffBusinessItems] DROP COLUMN [StoreId]
DECLARE @var5 nvarchar(128)
SELECT @var5 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.CustomerReserve')
AND col_name(parent_object_id, parent_column_id) = 'StoreId';
IF @var5 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CustomerReserve] DROP CONSTRAINT [' + @var5 + ']')
ALTER TABLE [dbo].[CustomerReserve] DROP COLUMN [StoreId]
DECLARE @var6 nvarchar(128)
SELECT @var6 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.CustomerReserve')
AND col_name(parent_object_id, parent_column_id) = 'StaffBusinessItemsId';
IF @var6 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CustomerReserve] DROP CONSTRAINT [' + @var6 + ']')
ALTER TABLE [dbo].[CustomerReserve] DROP COLUMN [StaffBusinessItemsId]
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202211281835450_createAllTable', N'BonnieYork.Migrations.Configuration',  0x1F8B0800000000000400ED5D5B6FE3BA117E2FD0FF20F8A92DF6C4490E166803E71C649D4D9B7677B388B3A7ED53404BB42344175792D304457F591FFA93FA174A5937DE6FBA58CE06FB9235C96FC8E170381C5233FFFBCF7F673F3F8781F30493D48FA3F3C9C9D1F1C481911B7B7EB43E9F6CB3D50FBF9FFCFCD3AF7F35FBE885CFCE2F55BD1FF37AA865949E4F1EB26C73369DA6EE030C417A14FA6E12A7F12A3B72E3700ABC787A7A7CFC87E9C9C914228809C2729CD9ED36CAFC10EEFE83FE3B8F23176EB22D083EC71E0CD2F27754B2D8A13A5F4008D30D70E1F9E4431C453EFC7B9C3C1E159527CE45E003D491050C56130744519C810C75F3EC5B0A17591247EBC506FD0082BB970D44F556204861D9FDB3A6BAEE488E4FF3914C9B861594BB4DB33834043CF9B164CD946E6EC5E049CD3AC4BC8F88C9D94B3EEA1D0311EFB6A91FC134BD8E567112965468BA67F320C9DB70587DC40178E734D5DED512820429FFF7CE996F836C9BC0F3086EB30404EF9CAFDB65E0BB7F812F77F1238CCEA36D10E09D46DD4665C40FE8A7AF49BC8149F6720B57E550AEBD893325DB4DE9867533AC4D31B2EB28FBF174E27C41C4C13280B54C605C58647102FF082398800C7A5F4196C124CA31E08EAB0C758AD6AE394350DEE60E2D095411264F20A81A2201464B71E27C06CF9F60B4CE1ECE27EFD1DABBF29FA157FD5076FE5BE4A3858BDA64C916AA68FD15C2470FBC2C32906439DD81E87D8CBC01A97D4820781C82DE9FE2C01F929B25BD81B859521B8C9B858A28A94A889D5A11FB029EFCF56E8DF396EC25CC808F56DF2D0C7675D2077F536C2D4758F93D57935E2571781B072414AFEAFD1D48D63043438BF5EA2FE26DE25243994D1BFD2ED5FAC4B84CB43DD6F04DCBF369451EE267F262A8E82F5C3746169044B44F8EF5649B199B62658134FD679C78C353DE313AFFB3BDF230A43CDFCDE3C0442F7D2416BE2B9BE27E085F785E8294C7F05C8641B0798823F8651B2E612213B0F7BD487697B455A20C56AB3B3F0B64B26CB737D152045337F137C5EE222485FEEC80D607A415618234E043EFA4AED0416E19C78F9FFCE8B17762D7519A817502C241A82122B01F4242B3856B8AF46BBE54E688AEF952993BC643CA6098720743D4B8272C9C6618C24A8CFD25AEC9B3BC645DAF2C026EAFAB42518779E54C5FB9954CBBB9D361525BB72E177556508563DBF2EBB5B2692909B1F261E44DDFEC5AC116C7F35E989A7C1BA83C9F9E74725E449656EF448ABD7829A3D3CD16720B4390F4BF537D45D62912ED7E367CB9DE51AB77B6DA3DD588D24392BA7C85246B60AE4CE58E83969B15BDDBAAB7354B37013B3566DE02BAFD9B72E5D22278D45AC9E65C370669617975BA34594352732DF763E7B00B4064EE30DD945B3D4CF5960E3D6C50C64BF4CDA1D7B7E1F35DBAF7906429DC7B9A2E1143CA7F8E9783F8627AB597AC9D699D0C0D4979127B5B77103FD39BF3A74B7B7ABED3FE30B985294C9E20778FA3EADC13DB47B3C149AA31BB9BACAE952B42D3EEE862A7E6DA1CEA8DDD6838F5DDA568206505D15C88EAF0CD0C5EC5AECF302D1C42728677747A611682895D44357EB38DB8B4EC8E1A0285D516A79CAA4B3492013C323B5A0A57D6B8BD3FCA1D44B2F6C92AF7CC5263F7107E4DE13622A8DEB553BB838D90D6653A9B662B7D6673CC23DBBE69332EADEFF09056C9C57E9E61689F6974359C61073EF849F670297D5066F77AAD13E3BCB56E156926852AB67740D536AEB107AA6CF9A698B8B4F86696461B24DBE9CD6A35F46D8EEAD0607BD4E11E1B6467222B516EEEEB4DC4B86AF526C27C5A257B86D968ACCFB456CF3168B194BED9D011C98B348D5D7FD737B6DFDC973AE4F03F469E63F86CA79810EA51109A1A24AEFE060928EAE5F9E4F8E8E884E1B53EB1DAD66F88715F1D91447FC75044520F935CEC403047B387D6911F65EC12F123D7DF80C08C13148CE65ACBA7B22648975CC2FCD503EAAF199B747A52DF4DB0DDA9A9524A41C5BCD914933E95502AEEFDC482A27B09888B25EBE053098A0145996CF2889DD0F333BB892E610033E85CB8C5A75E7390BAC06395135AF45E2702AD37A441245A6F3E75BAC25C85EF45B41536B848CC740DF246C8E8437D8F52A6DD4DCE5A600E21BDE8683DF60D20D07A0CD2E908EEDCDCAB24F33C5C2AF990BABB5A08871E258E18120786B129648DB10C28BA92B9D3B32CCA23E7FE2C0BF1CD9E7497D7B8E61B5EA0B4FB28127A43CBC7DEA250B26F287B42C9A30311639EB3432A1C52CF072516B5C7CDD014965E246B2D0FEE59D05EEC241D1A4AE0247C3F1051E35CC6EBA9218E2F434B08947226FB1844CBE5B0FF6D5D318EC1D5213357E37714885FBB8B8447E3E9BBE6415D26A21A5F831D88902A473280982AE76CFC82CA75FF8AA447EE0B6E04A7B94F30F2A6CABFFCD392CB8EEC42D94007902B1923B4C86311190690AAC2A58FDAA07E4630A954607D2974B9CCCBE073C6B95FFA96C2F28A292DAF3A68E9C8B11730937DE8DBDC29C89CEC8CDC91C88454318844A90289D2CEE2DE15E5CA7EB187214EF7D84A3AB892F162A50A24C61BC2A03135341185DDA32BE88CB53E39F0075B172BB01AE5C6E034451406B670B802C7FF161D6BC54AA1FA52CCB1B916AB474B2D08468D985F8261D03AEB734A324D8BA1AAAFB2781C35B9D1B1BBD32178AA5AA77697383CDE72B5803957558F73589E9A5C25D85C26608395EB019B2B000EB8486BD9F392FBF852CC48A527DBD897AD3F4A63E7352DED02156DB9BC251F3F0816B7A653D5C6ADAA37541B67A891CAB0E525F75192808B4A9F9E99578F1E9F60F33573E30D2479D8AEA81239C191CAC82FD55AC8D8B394DE266FC128C947FE2CAB347D24865E12DD9DD0D02DD21BCBF8EFAF586EA90FEAFA47756C30229355FF60DE9A33D59BB0FA005997CDA645E8E3F287D954102379F6196C367EB4C6622697BF388B2260F2FC87857928E1B0C098BAC456411F776B4A68F0600DA952441AF5F4CA4FD2EC12646009F2B77B732F64AAF18ECB82834845517C2266A7B23AA9546DF3BFE973BA24C031C7ED50225DA14187B9EB6237F90A1B9F4171F2E8D6200009E7A1E63C0EB6612476A8885BD72E351C42E86713E390B1887130B2441F918D388CA3B2A5C6C875345C0E6E5D668C8A45BDE5E062A5FAC86CB4601C992D3546E672822E33461570822DD547A622FDE2B054118B399B52CB90F1F6314B9FF1C6935A454BE7C8F720135D2341D2D031D2D6FDE816DCB74AA0487CAE62B4FA032C1CAAFED14086EAEFA908F1A97F35D49DC59B75467B163FEB63155167719839E7E1BC0CA109218BA334BF1A70BA8A094B70BAFAD1604CF4275CC4F0E842831914617EB5C3DB9D01CAA020E44436BF1BCC031E8495980ABC401F0F0FB48AC3E1BFEBA391E134703CB2C46495A778CC0C72A11345FA984D640C1CAEF97534DA5DE525B1B2257958265624BFFDB8ED472CF82381D4FC6CA01F8AF88E8466287E325DC54BC85BC24B23A4EA0B7D1CA7FACD604C783821626478C168D685960BD1CCF851016AD9406A907E9609F3E102A1C9555F358871EB7765CCBEC5C7D9A7387464040B917405605823B82B053946F3175431DC18E933357F9BA86C3854F36BFF8A726823960CA2465A4D78C99B69B7279DA5BE8B35D15B0A340DDDA544E84B7F99ED30C275847DEB442C21C937506234229C1369596105C678AC014A14F465F4ED59C23BD998E56006F23DECF63CBE6D958CBAC35B2AC6BEA59EB6B026420E61CDD6BF8E46D0897BF30EEC4F0194AE012A6C3E6E0D4E068D61C0EA92D14C7B7D61DB6ECA45301AD32D6EDAAFC79DD51D64C9C053C45C5BD3556AEAF5F535754D3D2BAF8CD5F97E993BE4A2CAC441AC7AF2BDFCFE78F192A293F6515EE168F18F601EF83057F65585CF20F25730CD8A983B93D3E393532A67F078F2F74ED3D40B3857EEBCD437C2883003C410F273062BA304D9E408A46830AFE691E0C3E7F3C9BF766DCE9CEBBFDD97CDDE3937099AED33E7D8F9374E5A27EA262FF36EF40412F701244CC822336851A2DD6EE1A94CB0DD8233895F3B821765CDED16BE1FD68872E27604CF4D81CBC53E55605BA5897D1DFA84CDCEAAAB529A966DB40A1562943B7DBB509F2D238876068CDD746B4BB3710ED6AE30E914AB5DE15219543B63013FFE297FEEDE7791FF5403DA2EDDA9951EE24C209BDDB442FE4D089E7F6B8AC766306D05C74B54D10A909B8CA215229D70C200CC32D3E2EBD818FA3134ED92220A94B7B1C180E740EC08934E79D84A56C9C0F6ADA038A978FA907C652CA243157F41023ADD6540356FB71CC860C7FA2B71D7CC80B45572B5D731DD7BD47607680103328F9986AD6395A6AC231BAA9D2A6C67A59AF7969768ECCD466BBF53C923961EAEDE1A686FE02C03264B912EF5A665AB0E70D21BB5B4BE981446FBB1E66C13E0BC0E913EB8FD90973266208F08234C56595EACFCB17689505E8788F6A375B51D4D5412933E340BFF12F760278C976CC36E890E9AA1A2A33414DC703AAA70587DE79C1834009FF44B6596EC7E63EE99C51C128529D853E6065970A2FD0471567D40D1B9D4697DB6C1527D45791EC693D0A18E5A248B363740EA863D04BA7FBDD918883837A2A084DF8998C9BF0D1A858C8D290EB84DE68431E44718C13E3AB0A459EEA2231036E3FC066662A6CC3987C74B1346317D3522227E973E4AE130CA48607C10EC3F458B7D22844338309A0AF07E0F8AE65904C623529667D5C3142B9B6DECE0A2FE7711E2DF423E3B0EE93F70BED3C389D7CFC629A427B1B348FCC5D72BE7136F192329285CB19C8A3A31ECB563F6F3686215D4B40CA3FA4BC7D86FE07FFE50FB4C1020A4A8CB5C8B14023C9A54257DBADA8906645475076B948D40C8DAB2869A9C56C2021E99B2D4E7F07154090DACB21648D4D7EB4951C0D51E86B191C56AA8BFF0E47BCB39D036B900A5BF0E398D0047FD1B05211F9625FB490E601F9D7DE0053544BCFFB62CA17644657280F105F26FB7235930718C21FA3BDB98CDF6ACF185DE378FB36FC1BA6EE2EAB38108D0F1728B5A87C51B1874C24DFD750331439811748983655D27B798AA332ED5A3AA0AF57AE7331A9A874E9D1749E6AF809BA16217CDB31FAD27CE2F20D8A22A1FC325F4AEA39B6DB6D96668C8305C060433F373B28CFE2E7900D9E7D9CDEEBBBBB48B21A06E221D95C19BE8C3D60FBCBADF579C17460288FC005E3E89CAE732CB9F46AD5F6AA42F71A40954B2AFF61BDCC1701320B0F4265A807CB735EFDBB7147E826BE0BE54F124C420EA8920D93EBBF4778FCDD312A3698FFE8B64D80B9F7FFA3F9F30ADBEA5BB0000 , N'6.2.0-61023')
