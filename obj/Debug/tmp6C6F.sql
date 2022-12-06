DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.CustomerReserves')
AND col_name(parent_object_id, parent_column_id) = 'ReserveEnd';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[CustomerReserves] DROP COLUMN [ReserveEnd]
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202212061228580_editCustomerReserve4', N'BonnieYork.Migrations.Configuration',  0x1F8B0800000000000400ED5D5B6FE3BA117E2FD0FF20F8A92DF6C449160BB481730EB2CEA627EDEE2688B3E7B44F012DD18E10597225394D50F497F5A13FA97FA1D49D972145EA663B6BE4C511C96F7819CE0C47D4CCFFFEF3DFC94F2F2BCF7AC661E406FEF9E8E4E8786461DF0E1CD75F9E8F36F1E2873F8E7EFAF1B7BF997C72562FD62F45BDF7493DD2D28FCE478F71BC3E1B8F23FB11AF5074B472ED308882457C6407AB317282F1E9F1F19FC62727634C204604CBB226771B3F765738FD87FC3B0D7C1BAFE30DF2BE040EF6A2FC392999A5A8D657B4C2D11AD9F87CF431F07D17FF3D089F8EB2CA23EBC27311E9C80C7B8B91857C3F88514CBA79F62DC2B3380CFCE56C4D1E20EFFE758D49BD05F2229C77FFACAAAE3B92E3D36424E3AA6101656FA2385819029EBCCFA766CC376F34C1A372EAC8E47D22931CBF26A34E2790CCDD26727D1C45D7FE22085739159EEED9D40B9336C0541F0100EFACAADABB92430823257FEFACE9C68B37213EF7F1260E91F7CEBADDCC3DD7FE2B7EBD0F9EB07FEE6F3C8FEE34E93629631E9047B761B0C661FC7A8717F950AE9D913566DB8DF9866533AA4D36B26B3F7E7F3AB2BE12E268EEE19227A85998C54188FF8C7D1CA2183BB7288E71E82718389D55813A47EB9EB037A182C367E415540933926D35B2BEA097CFD85FC68FE7A30F641F5DB92FD8291EE41DF9E6BB6413923671B8C175B47EC5F8C941AFB31885714277207A9F7C67406A1F438C9ED2210E49908CB16F723F079E3BE4EAE5F4065ABD9CDA70AB47131C60F53271961355103B6D44EC2B7A7697A93CE2C8A6D2E912C7C825D2E50E7B699DE8D15D676AF0882A7F00A5FE5518ACEE028F8582AA3EDCA3708993650BF4EACF824D687343998C2B5DA4D450CCB84C3413D5F0A091605ABE43E6337C1568AA9B014B6C887061DB01B1F7149BE3E4586F7708B353B3375114FD33085532A027CAE952253FDB8B1F43CAD3941306267AE912C672ED0E64BB21E10BC709096F0E3FCBD8F3D68F818FBF6E56731CAA18EC432F9CDD25ED3A56468BC5BD1B7B2A5E6EA6DD782EC2911DBAEB4C3F4949919F1DD0FA48E42A0E890C7DEC9DD41539B6CE83E0E9B3EB3FF54EECDA8F62B40CD16A106A84081E84D0CF1839B3C74025603A229419729744C3764F4B6ACD81165ABF565D61A5E95A758515683CA418AF2270304C8D07C6F0AB8621AD2498A5F29A9041AAEA7A612881BD2E0A651D86CA85BE82954CBB990A66E511A02C977556520530F9E17AAD4C7D8E431AB9A192A607731FA49536AF2758A34EC804D798B127FD18D06B5CEB9FE887F22D316487A79AD93EF31E940E47E80EAF50D8BFC2BE25A701B2EBFA31B0D422F1572207E45A87ADF2C009214E344AEAC1F25156D95CAEAB5D3B2DF526AFF8EB356C43470EBB1466BE1CBAED41BE83B498756B2BE7D319EF04A4465BF46639F7BBE1456B59433A34DA3795F169B2638A5687BD02D3CAA767188F5C63C9DEE86CC1B3A6F200D25C9437F3C9970D0F8C2911991D18E9DFA57FBD5ED568FA240D29FF25980FE20CD5F73077428E705E18381B7B10E7EBC123BACB1E51F5E9AA7CD32D33B5F20A0F8CD6E0CC2CA00E7CA6822A767D9E6AE12703CD42853FADB9FA2DA7DD58FFE62D0F0A58AE480CDF6767AB8C5EA39BC562E8ED57C7C74D771FC8C9AA6DDA8895A729F3E2B08931C9B63DB03348EB3B34050BBED8CE6D0B6D2B4D571A1876E0A31BC68F97CA9B67DDDCAA1BDEDC2816F60E47387CC6A0CCE3EA3CF002A6927CEA9A82F55153BDD58B2F61604DC460DEF820079542C150B327FEBBB7EE4AEDC3039133237D6B22F99DBDBF6B86C5DC15D6076BE128E6B7BCD453ACAC285852EADAA6D72A7801D7A74CAC1B894484EAC8C48B280A6C37EDB9784A03AFB6B0F3F3C9772CC37B2ED936E26ED1900D4524A4BB263291F4F27CF4076125F429952AA4A204DED161291E1F1D9D084489A0C56122E99037256B4B44B7EBC7A254767DDB5D23CFAC7F1C8CA6784F96B224C8975CE2E46D3DE9AFD99AE8F44472FB58EC5CD9074E2BD54DE5644CF1621D8B2ADE24C9B946E7B512CD9CEC7B5313FED479630D302744E8845F9DC98D7F893D1C63EBC2CE3E6E9BA2C8468E2882C996773A61E7FAE10CC2CBF5EB67C4C8C5EBDAADB0B0FCEE808CAB342E1268B2948A7735AEF969C9EFEDB36DED4806E0D9DA35D3E94369186E8551C157A132EE51BF17AD18A77AB7AEA1836BE0CDF852E4FD46CCA51AE8007CA59A082DF2D41748DBD3E0908B54A95395FE524E73977E7A43BDAD7C23C2D18059AC4B4352DDA1A1B4AE62DEF504587E10DF1EAB016F95945CA07AC5A4C504B57CA6BAECBD271AB6661C4371A77CAD765FBBAA5D1D3206D2F47B546C24383AF59955CF6BB23FA719ADF10CC0BA5A6BA8D58FDC55BA13FCCBB9B874994AE6EFEA8583652F1C44627DEB77BD9E6D8119E1F5D0E908EDEF1F802333A7266943FAE9E3B0385C976F622EE749197E8981773BDF229CBFDE89720F3DCF4109F60CC7AA6F032BAFAACAD328F0268BCC6879019129AD41E284AFBC7759796DBF581718D035B6420D5E75EC1390AA229D3E29E68A2AD5412A8F093054595C83C54B0C018DAFA089578A3B2960598343A4360CC868F067AB542BCD2F5DF93D6EFE42A01C3BB71104F161FE068082D6D9976376D2B42654F54D03349BBA9E6B73DF35338FAA7D69EEAC86E611DCF1E633A8F8884A9C3E4DAFA9A1DF547774868E523DBE6E3065F0D709E26CD5BBEEF49D77D46064A25ADF55D7DBCCC86FDE49B662AD0BCACC09C56F4189FA30F33AF1A0DDCE1674DF563259754E1423378ADEA88CFC26BDB155CD850471BA0C8EF50D0EF6D4386BAC800627F91E457DDD7D88FA79541D2F9B1C303B9A49C98912403766C9E2DE4679C429CB26E32C246BFE603296C46E9D7C41EBB5EB2FA958AEF9136B9605729DFE30330F71BACA30C636C3CFFC81ACA444F6235A62AE9490263DBD72C328BE44319AA3E41ED1D45909D5A0039DC4802E28CACF6CE2BA16A675D136F9CD9F241581578183718E744506BD4A8ED9A93CAAB14605142B89BA8B3C1402F7F7A681B759F9F2C3BFBC351B0F95C6614BF411C5A8A734AA586A8C5C46C80470CB3263543A1226004C1737C34E835ECA90D3427D5C3136290D2C961A238373CC9719A3CAE618286E862DCCB150A88FCB4510A551B922117332E6B6BCE0FB12C48CE0DE662598967C53DB6226724D81A421CF94ADFB9163F43B6C0645F16E5B8E26B9DE46036BDE8093D328BF09A151CB87067C5A7EE2C1B068F9541F695645C7A4A1A8C7FA5859BC4B1A660ADC0F572154C12B6994EAA9C14C17D12899992E1E1A8C89FFAA84191E5F68B08232CCDB66787468487621ABE706EB40877F6496822E30D85B548847664351CFF5D1D86F96693CB6C44492301F26B3C28429D2C7AC3E3FA6E1AAA706DAAEFCE0875173E55363BD997D1C00E8CCAC6067745BDDA1B991D50E6199D8EB70FB7E345C79354190CD869AB28C1DC7F4A57C6AD0A32A181CD3A7EAB1810CCCC2BB31D22F7B642AA9E61812537323A4221A1B8D533C3318131D6C8D19195DB0333B8C7B4DD0D680548169D9906A807E3699701D1C34F96477C5E5B8E5A537411F37C0814CB4F2F1CEB053F962A01D23C960345848DEB4DF3308205D99929D5922F57B0EE3EDDEE2BCA868BDDBDA74174F71E6224286554555A2A1AAA7DB3F3BB10192D83D47971C4E14DD9D28B629AD0AA75B07E24A02A52BAFA4CDFB12581D5A1065941C01AC2CD99965AF7DCB67B2F06A308DA5AF03E867F1774FCBB0E15D1861CE946C5F4354A158185BBE7CFA062525FFF6BA9B3D234133D83452847E760D7DFB18E25073BF89D01FC96D7B39CA360F827DDBC24CB811D675421518E3012F0CD99281F79D700983AF5252CF9F94FF979730F20B10F55975851B1159959145A6EAD97592DB10B3D78870E15152E168F60F6FEAB938510A45852FC87717388AB3C042A3D3E393532E33EFEE64C91D4791E3011748A0EC24D2182403044A7293099ED58542320CB00365A6F59F51683FA250081C5661B74944DB2D3C97B9B45B703151690FF8545ED28ED0654964BB85EF67E6A529627BC0EF7EE6C104B020F6690D76A324A96F432689B9495332C2C753A41E7E391FFD2B6D76665DFFEDA16AF9CEBA09891E3AB38EAD7F9B2EA232C9A96E57409036BDE2A279824C9546D56C19ACB33360EA0687F61E33CE6ADA15269FB4B42B5C2E2769675300871A85D7EE4317194535A09B25106D241D810514F38516C8BF5BA197DF1B0B012127682B3828CA7D2B4030927D2B443E5A7D2B303E446C3B30310FA7015EC31C846F439796076A33ED953793E9AB46E902258AA57D36C0AE8099647F5D81F2B9FC5A6D04365F5F2B2820275F1F7B4A75A1626F379524DB9AB16998356FB5C9B800C36917DA8617EE830FE05B117BCB01500EB166B65DA3C45B6F6312B7A89BF6F03025D9A40AB3B9510AAB8ECC7193038A393A94A0EA609E0F639E374B55F4562496A86DB52456DAAC8DF7074A33D4C7A2298367EDEBB2ED9DB487D2D80CE43A1298A951E699AE9CF5038829E53581BD657931E789AEB0AA5AB691576CF2146DFF7DDA6A9F0F447A566EFB8C2A0EF91D7795514513ACFFFC1D6080ACBAC07646E1BB99A02CB20077BD84EBD60BDAA0235CAC86E937BE8F1C1B5CC41879E0C11EA3C2CA429BB54AEED13E03C1F02932DE74368C06B2A4272E6BC8F31DF198A988E932A1C51EC4D76E94BDA28B5415DBD3758ACFE7B6AEE0F634EF04B88E66F91FE0787F2DD256B48DD83F74F28837992962577450032EDD4FFD63CAC0DBD53ECDB23BEC96112DC445A4FA631C947F3FCCE8FACF5F00927B9C8501101C0D3220EC12CBD47EF4B7133CB313B912C4089CFCB276960521FB92E57CE4CC03C21399AB0BA8A89397403B5F024493AA504FCB30A382728CFD245D8087D87D6206884E5EEA62CD71E9256E908E4877D18C523B48A9E535EAC919677F8048B275F4891AA4885091CD2B8974B79F4842217274533BC8ED76E623FFCE63D1F79F1A02944F86B90C6091A391496217F33D3458DD2E2674F7F23898276D187063F49CA4011C844196014E77D46674D8BDEC0BED36418349DCE5BC0A5D084989BED5491EB01FB91280E556A72D18607A0CD21F885FD893B3D2C64FDEAD67FF5DE2C85D56101382E9639B392595751253A238B0713D2AAA70EFF6BF902972C811EA228CDD05B263526C13BE72FDE5C8FA05791B52E5D36A8E9D6BFF6613AF373119325ECD3D469224873E15FD34C703DBE7C94DFAD955D4C5104837DDE43AC28DFF71E37A4ED9EF2BE03A820422394DE6376992B58C931B35CBD712E96BE06B02E5D3571E82EFF16AED11B0E8C69FA18485CCFBF62DC29FF112D9AF45A0043948FD42B0D33EB974D30BA7518E51B527FF121E76562F3FFE1F47117D9BE4B90000 , N'6.2.0-61023')

