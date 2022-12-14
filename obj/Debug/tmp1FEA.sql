IF object_id(N'[dbo].[FK_dbo.StaffBusinessItems_dbo.BusinessItems_BusinessItemsId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffBusinessItems] DROP CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.BusinessItems_BusinessItemsId]
IF object_id(N'[dbo].[FK_dbo.CustomerReserves_dbo.StaffBusinessItems_StaffBusinessItemsId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [FK_dbo.CustomerReserves_dbo.StaffBusinessItems_StaffBusinessItemsId]
IF object_id(N'[dbo].[FK_dbo.StaffBusinessItems_dbo.StaffDetails_StaffId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffBusinessItems] DROP CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.StaffDetails_StaffId]
IF object_id(N'[dbo].[FK_dbo.StaffBusinessItems_dbo.StoreDetails_StoreId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffBusinessItems] DROP CONSTRAINT [FK_dbo.StaffBusinessItems_dbo.StoreDetails_StoreId]
IF object_id(N'[dbo].[FK_dbo.CustomerReserves_dbo.StoreDetails_StoreId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [FK_dbo.CustomerReserves_dbo.StoreDetails_StoreId]
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
WHERE parent_object_id = object_id(N'dbo.CustomerReserves')
AND col_name(parent_object_id, parent_column_id) = 'StoreId';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[CustomerReserves] ALTER COLUMN [StoreId] [int] NOT NULL
DECLARE @var1 nvarchar(128)
SELECT @var1 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.CustomerReserves')
AND col_name(parent_object_id, parent_column_id) = 'StaffId';
IF @var1 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [' + @var1 + ']')
ALTER TABLE [dbo].[CustomerReserves] ALTER COLUMN [StaffId] [int] NOT NULL
DECLARE @var2 nvarchar(128)
SELECT @var2 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.CustomerReserves')
AND col_name(parent_object_id, parent_column_id) = 'CustomerId';
IF @var2 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [' + @var2 + ']')
ALTER TABLE [dbo].[CustomerReserves] ALTER COLUMN [CustomerId] [int] NOT NULL
CREATE INDEX [IX_StoreId] ON [dbo].[CustomerReserves]([StoreId])
CREATE INDEX [IX_StaffId] ON [dbo].[CustomerReserves]([StaffId])
CREATE INDEX [IX_CustomerId] ON [dbo].[CustomerReserves]([CustomerId])
ALTER TABLE [dbo].[CustomerReserves] ADD CONSTRAINT [FK_dbo.CustomerReserves_dbo.StoreDetails_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[StoreDetails] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[CustomerReserves] ADD CONSTRAINT [FK_dbo.CustomerReserves_dbo.CustomerDetails_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[CustomerDetails] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[CustomerReserves] ADD CONSTRAINT [FK_dbo.CustomerReserves_dbo.StaffDetails_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [dbo].[StaffDetails] ([Id]) ON DELETE CASCADE
DECLARE @var3 nvarchar(128)
SELECT @var3 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.CustomerReserves')
AND col_name(parent_object_id, parent_column_id) = 'StaffBusinessItemsId';
IF @var3 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [' + @var3 + ']')
ALTER TABLE [dbo].[CustomerReserves] DROP COLUMN [StaffBusinessItemsId]
DROP TABLE [dbo].[StaffBusinessItems]
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202211281821382_createAllTable', N'BonnieYork.Migrations.Configuration',  0x1F8B0800000000000400ED5D5B6F1BBB117E2FD0FFB0D0535BE458B68300A7867C0E6C396EDD26B16139A7ED9341ED52F2C27B517757A98D83FEB23EF427F52F947BE7FDB237AD1C212F8E487E430E87C3992177F8BFFFFC77F6F38BEF59DF6014BB61703E39393A9E5830B043C70DD6E7936DB2FAE1C7C9CF3FFDF637B38F8EFF62FD52D67B9FD6432D83F87CF294249BB3E934B69FA00FE223DFB5A3300E57C9911DFA53E084D3D3E3E33F4E4F4EA610414C109665CDEEB741E2FA30FB0FFAEF3C0C6CB849B6C0FB1C3AD08B8BDF51C92243B5BE001FC61B60C3F3C96518042EFC47183D1FE59527D685E702D49105F456130B044198800475F3EC6B0C17491406EBC506FD00BC87D70D44F556C08B61D1FDB3BABAEE488E4FD3914CEB862594BD8D93D037043C795FB0664A376FC4E049C53AC4BC8F88C9C96B3AEA8C818877DBD80D601CDF04AB30F20B2A34DDB3B917A56D38AC3EE200BCB3EA6AEF2A09418294FE7B67CDB75EB28DE07900B74904BC77D6DD76E9B9F65FE1EB43F80C83F360EB7978A751B75119F103FAE92E0A37304A5EEFE1AA18CA8D33B1A664BB29DDB06A86B5C947761324EF4F27D617441C2C3D58C904C685451246F04F30801148A07307920446418A0133AE32D4295A597386A0BCCD035A12A8228CBE01AF6C8804182DC589F519BC7C82C13A793A9F7C406BEFDA7D814EF943D1F9AF818B162E6A93445BA8A2F537089F1DF0BA484094A47407A2F7317006A4761941F03C04BD3F879E3B24370B7A0371B3A03618377315515095103B6D44EC0BF8E6AEB335CE5BB25730012E5A7DF7D0CBEAC44FEE26DF5A8EB0F247AE26BD8E42FF3EF448285ED5C70710AD61828616EAD55F84DBC8A686329BD6FA5DAAF5897199687BACE141CBF369050EE267F46AA8E82F6C3B44169044B44F8EF5649B199B62658138FE571839C353CE189DFED95E7918529E67F33830D12B1789856BCBA6B81FC2178E1321E5313C97A1E76D9EC2007ED9FA4B18C904EC432F92DD256D952883D5EAC14D3C992C37DB9B682982B11DB99B7C771192427F7640EB12694518210DF8D43BA96BE4C82DC3F0F9931B3CF74EEC268813B08E803F08354404F6434868B6704D917ECD97D21CD1355F4A73C7784809F463EE60881A8F8485530F435889B1BFC435799697ACEBF3CCBE82D13D8C911F07B99DA7EA88BA2FA9C60C4056D77408A551C3ED7B5928EA34AF9CE92DB792693733352C35D7AB725167055538E639BF5E2BB39C12F2466198B4E9C134E7D2E207604CADD60D54BAD8279DB8BCC858EC9D486E4E2C6574BAD905EFA10FA2FE37DB3B646023D1EEC766691C2668B935D17BAB7A136BA47D987DCA44FF508D0F1AA8470D946E3D6D41CAF96A8B53CCF71562C7003A24A3A550BEE3D6574A2B51A246C82A8FCC7A65ED447E4DA1A928A8DEA719261984A49AD41C6BDF7DB936EFC056A735BA8E5DDF4AA73789F5926D0F1A9D4BEB3B8CDB9672B19BD0AD76505157411B76E0D28D92A72BE92154B313AF4EE207ADB706916652E8E186A750D83E61760A55353CA8252EAD4E0CCDEF50B76592A5506C9A717C43CA7F0997831C20F4EA213756D69D0C0D4979143A5B7B90C391C389C5D07B5C076E037B4AA176318CBC9EEA868AA8FF45854762EFA33ACFA9C377787815BBF6765AC4CC65DCEECCC321D96E6C45142D0F660497163FD4A4D10619C8F1ED6A35680C5623E8D074F5712559B64C1B89727DCA6622C665AB8308F36915EC19C65B6DAC661B1DA2D262293D69D511C98B380E6D37EB1BDB6FEE150172F81F03C732BC2F904F08751B014D0D125777830414F5F27C727C7474C2F05A9F58B579D6C4B8D71D48A27F602822A987512A76C09BA3D943EBC80D127689B881ED6E8067C6090A4673ADA5535911A44BAE607A5689FA6BC6269D9E54FE25DB9D8A2AA51454CC9B4D31E9930BA5F83C4D24211A876B1CD9C88FE15552A14385238112713FA1E760761B5C410F26D0BAB0F3EF48E620B681C32A20B4B09D0E8456399201C4553967E3175445584E2447BA31BA5A98E8387F8FF2A4DD4D8ECC336E5B2F1A578F7D0348B01E83743A821FD7EE449265CEB7785BD6F0C471858819E58308B0D609E3D0D2ABC1B5416C05256BF4D46FE140EE4E68793E9F5420A40E2025AE55E041DF48508778B49604D7246E2E70920E0D256D12BEEF89A871C2643AAA47618E4A84402967B29BAC7B628A2AC631B02EDC6B339477634565DC49AFAFB4D81BF528ED9BB06A8C6540BB533277E3175A6E444C2443F2F0582D3C7588D528C024FF84414B363BB20E65031D40B2648CD0228F7D1D3B8054E5514ED406F5338051193EAAE2E457CBB40CBE249C90FBD7181651F7B888FED2D291622F6022FBE8AA0EB3CAE28E8CDC91C88454318844A902898A6C897B97972BD0189DCFE0313534118583A52B283987994E1CCE61A53A4895B5CF87AA8A1558B5126270EA220A031370AE60F0BFDFC35A697EF247AF49F3887E355A4A7099E56E1EBFC7A075D6D194649A0643251F6AB0ACD48C421BC6A17963E4AD46C3C0B3DEAC346099EAE621CB3793A06893B0283654B9BA6812CCE4808B949B392FA5375C786B593326671C9523A445A8248D6369FD338F7BC02FE09C323064161AA27926D80DCC62417A13D152D4147A4E2BB46114DC68295E3BD16FDCAF3DC4CA4DE96A1B3BDBFA8BC7D8BBEE8D79FC5B0D2CD7D4BE9EBEB7870D46644DE9FB76AD3953DEB4A87C90AA6C36CD3319163FCCA6829487B3CF60B37183359602B1F8C55AE4F90FE73F2CCC3303FA39C6D48E390902ABDE5694D0E0C11A52A58834EAE9B51BC5C91548C012A43762E68ECF54E3795C021BB9A42876AAD8A92C8DE8B26DFA37EDEA49F215723CD702E91A0DDA4FBDDF6CF215E6278362A5C92A810722CEF5A779E86DFD40EC938B5B5751191C4218AA11E390A9057130B2441F914D2088A3B2A5C6C855723B0E6E55668C8A25B1E3E062A5FAC86CF23F1C992D3546E672822E33461570822DD547A612F7E1B054118B399B52CB900918314B9F09EA925A454BE7C8F720135D2341D2D031D2D6FDE8163C3C47A048C27662B4EAFB211CAAFAD14086AACF8108F1A97E35D49DF94D50467BE63FEB63E549E4709839E73AAA0CA1CE0887A3D4BF1A70BA4CF14670BAFCD1604CF4073BC4F0E84283191461DE35C3C3F3AD911359FF6E300F784E35622AF0027D3C3C6F1A0E87FFAE8F467E6884E3912526AB9CF89A885CE844913E66FDCD100E57FF3A1AED4E06D53AB2257958265624BFFDB8ED472C11128154FF6CA01FF25C478466C87F325DC54BC85BC24B23A432F7078E53FE663026FC434B626478C168D6853A8068B23214681A6B438930F2D551DE4662B627331CFCE227B1194B2E848AD1886C3DA4786305C678AC16200AFA5A793B5E299DB8087230837532ACA3303ED39ECC4AC25B2AC6067E4FC6709D4184300EAB5F4723E8F2831133475888A4E5084B5A8F7B1B18DF3AC1325C305B93E90AA97356E050F5AFFD1B4B43AF5D32C504E939E12507F76E973AAB8C2976A0B40450BA5A4BD8BC2FB5D58DD5497EC3CF805525A399F6EAA4AFDD948B6034A65BDCB4DF502DABCAC99281A78839EFA4AB54D4AB734FEA7C73569C35AADF7D630E1FF32A130BB1EA9BEBA4078F8BD73881FE515AE168F14F6FEEB930DD78CB0A9F41E0AE609CE4291026A7C727A7D4DB71E379C76D1AC78EC739ABE5E50F177EA03F404A073765F04295B4A1C95B31140DE6C62E127CF8723EF9356B7366DDFCFDB168F6CEBA8DD06C9F59C7D6BF71D23AC990782FB005DF40643F8188C92061062D7A70AD5B78EA45B06EC19907C03A8217BD9ED62D7C3FAC11BD8DD6113CF729342EF6A902BBD173616F439FB0AF74E9AA94BA651BAD42A556E44E5F96E2B065E6C4CE80B123526D69367E8BAB2B4CFAA9ADAE70A997B43A63013F4D227FEE3E74F10E960674B367AF1AE921CE04B2AF5C95C8BFF3C1CBEF4DF1D897AC5AC1F1723FB602E4E6776C8548E77034006BF85CCDDBD818FA31349BBD2C2350DEC60603FE904C4798F4BB31AD64957C6BA1151427BB6D1F922FCF807290FDB66F9AE893CE9AB522CDBE84A24BBD6ED9AA039C27545A2E27E69994DD2CCFA6AF54BC8DD5B477263EEF5D87814C5C46981A3DC5D0C8C16EF45AC1DB90D01DEAFBBD5B1CCCC3001A9E4EA3BCFF1D7950ED0CA1763EAA796F7999FB0F1E5A473A8B7B06B9C74AAB0F4B513BDA412536EF63D2F827897B3B61BC04DCCDCC8A41B35677949A9A9BA762D779A807CDDE2BFDCE92252BBC6435D2CCD20D44A6A76CA654BA10715A9F37255E824F2FC626589D64821E4FCAE72AF3C08E933BEF209BDE7791AF79C78999772F66F21BCBA390B11124BC35CEAD6C265DCA673FF0343BC26C6C6F413E14775147291C46D990C762443590D2FD34A04C05781CE6934906E3F108D518B6B34185ABD976B677D986BB482DDC404A3B4E253CF0D353FB9327984D6E454F62671980F39BEBE71367192229C823609C8AEA9CBC84D830B489521E4DAC829A96613661E918FB4A38CC234A55D2A7AB9D9658465597BB06B98BF9335955D0A4A59BDD5848ADA8A126A795009947A62875397336AA04C98DB2204BD4C73EA73CEE8C2DB89651E747DEA754C66D731653FAF08D64276E942776072CE939E7700B66F0D4BA3241F1F89209B7D31F0D9838D634C16D5931ECE2E839F9AF79A6DF06ECEB26B32FFB452BF255B6A8B59F9F6323772976D735C40C6106D026BC94AA4EBAFD970E13D5A3B20A7502FF190DCD412ECC4594B82B6027A8D846DB66F604F62FC0DBA22A1FFD25746E82DB6DB2D92668C8D05F7A043353A74B463F4B5F4CF679769B7DC011773104D44DA4AB12781B5C6E5DCFA9FA7DCDB925208048BDB9E25A433A97497ABD61FD5A217D09034DA0827D9513FA00FD8D87C0E2DB6001D29563DEB7AF31FC04D7C07E2D3F4C1683A8278264FBECCACDEE2DC50546DD1EFD17C9B0E3BFFCF47F3D1C7FF3F6AB0000 , N'6.2.0-61023')

