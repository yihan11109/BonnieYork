DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.StaffDetails')
AND col_name(parent_object_id, parent_column_id) = 'StaffWorkItems';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[StaffDetails] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[StaffDetails] DROP COLUMN [StaffWorkItems]
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202212051613030_updateStaffWorkItems', N'BonnieYork.Migrations.Configuration',  0x1F8B0800000000000400ED5DDB6EE3BA157D2FD07F10FCD416397192C1006DE09C838C33E9493B3309C69973DAA780966847882EAE24A7098A7E591FFA49FD8552775E362952373B99202F8E48AECDCBE6DE9B172DFDEF3FFF9DFDF4E47BD6238E62370CCE26C78747130B0776E8B8C1FA6CB24D563FFC71F2D38FBFFDCDECA3E33F59BF94F9DEA5F948C9203E9BDC27C9E6743A8DED7BECA3F8D077ED288CC355726887FE1439E1F4E4E8E84FD3E3E329261013826559B3AFDB20717D9CFD43FE9D87818D37C916799F43077B71F19CA42C3254EB0BF271BC41363E9B7C0883C0C57F0FA387C33CF3C43AF75C442AB2C0DE6A62A120081394906A9E7E8BF12289C260BDD89007C8BB7DDE60926F85BC1817D53FADB3EBB6E4E8246DC9B42E5842D9DB38097D43C0E37745D74CF9E2AD3A7852751DE9BC8FA49393E7B4D5590792BEDBC66E80E3F82A5885915F48E1E59ECEBD282D0374F521007060D5D90E2A0D218A94FE1D58F3AD976C237C16E06D1221EFC0BAD92E3DD7FE2B7EBE0D1F7070166C3D8FAE34A93649631E90473751B8C151F2FC15AF8AA65C39136BCA969BF205AB625499BC655741F2EE64627D21C2D1D2C3954E50BDB048C208FF1907384209766E5092E028483170D6AB82744ED62D516F2205478FC82BA5126524D36A627D464F9F70B04EEECF26EFC93CBA749FB0533E282AF22D70C924246592688B9B64FD8AF183839E17098A9254EE48F23E06CE88D23E44183D644D1C532069E3D0E27E0E3D77CCD12BE48D347A85B4F1468F1638C2E8E5E6AC10AA1076D24AD817F4E8AE337BC489CDACD3054E904BACCB57EC6579E27B7793BBC1432AFD0EB4FA9751E87F0D3D160ACA7A778BA2354E872DD4CBBF08B791CD356536AD7D91D24331ED32F14C54C1378F04CB0A1CD29FD1B320535D0C1862438473DB0E49BCA7981CC7477AB343E89D86B989E2F89F61A4B2010349CE862AFDD9DDFC184A9E679A30B2D00B9728966BF760DB0D059F3B4E447473FC5EC69EB7B90F03FC65EB2F71A452B0F78368769FB29B5419AD56B76EE2A974B99D77E3B508C776E46E72FF2415457EF620EB03B1AB382236F47E70519764D9BA0CC3874F6EF030B8B0AB204ED03A42FE28D288103C8AA09F317216F7A1CAC0F424280FE42E8887ED5F96349A0323B461A3BA324AD38DEACA28D0B84909F663B0314C8E3B26F0AB9B21CD2484A5F29C5040AAAA7A192881B52E13651586D285BA82994CAB991966E512A04A975556920508F9E17C9D427D4E435A6D43A545DFC27D505656BC5960833B211DDC10C61E0F13406F70E3FEC430926F48203BBED43CF6590EE07438415FB18FA2E11DF60D590D9059374C80A53689BF123B20F73A6C963BCE0871A651920FB68FB2CCE6765DBDB5D3D16FF28EBFD9C3B6DCC86187C26C2F872EFB66DF4159CCB875B5F3598FF702D2E02D068B9C879DF062B4AC611D5ACD9B3AF834993165A9B7B902CB2ABA679C1DB9D696BDD5DA82574DE502A4BD296FB7275F157C534C89C9EC2148FF2EF7D79B5D8DE69EA4A1E4BF84CB513643F577987B1147342F0A9DAD3DCAE6EBDB8EE83EEF88AA5757D549B72CD42A32DC315E830BB3803CF09A0ACAD8F77AAAC33E1918162AF6D3DABBDFAADB8DFD6F51F2CD01CB1D89E179763ECAE839BE5EADC69E7E4D7ADC76F6819AAC9AA6AD54799E292F8EDA04936CD9377506657D87A160A917BBB96DA11DA5E95A03C30A7C70A3E4FE4279F3AC9F5B75C3841B32CB711EC7A1ED665342F4E2E0D1275BFB8F8163199E83E6CDE24E59490389257137C476905A9E4DFE20F493BEA42AC0A9258167B8ACC4A3C3C36341283148384A2D02F2E6C4071013E7068968BDDCC07637C833AB1F07A36906D3A1AC04F22917383DCD21F5351B139D9A486EA78995ABEAC059EFA6AE9C4D295D6C5251C54EA35C6B74B61D69E564F7D54DF453E74403504E48D0313F3AB3EBE0027B38C1D6B99DBFFC3047B18D1CD1B29029EFF4A2CECDCD1945979BC7CF4891CBEDFC9DA8B0FC6C49A6551A074D9A2AA5D25D8D6B205AF67BF76ADBD8921174B671CC74EA506D5DEE4451C1AD7299F6A8F7CD6BC5A9CF5E347C7003BC995E8ABADF4AB9540D1D41AF541DA1259EBAA1BE3B0F0E2DA1953E55B99EE63C77B58F63E8B7953B669C0C58C5FA0C24D5151ACBEB2AFA5DCF80155B3FBB533560D751A905AA2D482D2568D433D565C017E2611BDA319676CAC76ADFBC6BBE0427654825031C95A160B5BF76B14CD3F053026CD87D8B71B1671717EB7B5EBD52EC054E54379DEB3D00D5BA58505C1699D1490191496D40E2A25479EDF2F4C67AB10B36A06A6C8606BC3A481190EA249D3A29FA8A4AD541AA9C1A0C55253760F1BBC3021A9F81C3A3D41B540BF8CA3C554AF3963D3F23CD379BAA96736A2B4C76F3DD250A5A67164DD94ED3EA50D57D2AA837757745CCF745987E54CD22F38D10A81FC1F969DE838A0B9C62F769AEC80DD7E4BAAD335C84EBE9758B2E836F4689BDD5BC2CD45F18528D911956FD65E0603D233FF5934CC5C6E58DD902879F8212636FB6A2E141FBED2DE8AC5FD2594D01BA5188AED72AA398BCB35A95073055F457A5CDA639F74EF160369590F4CC3EA3CDC60DD614694FF1C45AE48C3DF31F16E65C367E8E31B51927C1C7AA9524D278B4C65C2A114D6A7AE94671728112B444E969D4DCF1856C50AC2B89554A89F270561CCD328C29CBA6BFF9205BC1B0032C180AA44BD2683F5D776483DFE0FA05142BA557421E8A8003EB79E86DFD40BE1A929766896F681C36451F51A4B7A151C55463E48A0A05C0ADD28C5169CA1300984E6E879DB19BC890B3447D5C9184860616538D91C13EE6D38C51657D0C24B7C316FA5848D4C7E5A86268542E49C49C4DB9292F6C0B086646D85C612D98967D533B3E13BBA640D2B067CAD2C3D8317A339A41516C52CBD124E7D434B0E651B65C4675F98746AD1E1AE86975978751D1EAA93E1245834243518FF5B17262131A660E5C885221D42C25344AFDD4A0A74BDA11A6A7CB87066DE2AF0F31CDE3130D46508679D30E8FE6006107B27E6E300E34CF0733147482C1DCA2B83C9809453DD747632FA7D3786C8A8925616EA0B3C68449D2C7ACEF99D370F553036F57DDEC62DC5CF5D4D86FE6DC1980CFCC13F6C6B735ED44B58ADA212C93781D2E3F8C87ABCE1804DB6CE8292B9200A62ED553831AD56FFD3375AA1F1BD8C0FC3D7EC6FAE58F4C2DD51243666A698454BE764FE394CF0CDA44BF55CFB48C4ED89B19C6EDC9760D2055605A31A41A60984926DCEB02433ED9A52F396E757A2DF8E316385088563DDE1B75AA7661BB29920C464385E445875D8300D69549D99B21526F2A1B4FF70EEB4545E9FDF6A6FBB88A33371132ACFAF5591AAA7EBAFBB513FB262C3BE7E894B715457F2B8A5D5AAB72D3AD07732581D2B557D2E24319AC1E2388EA754801AC4AD99B61E7AE91741B783598C6D037010C33F8FBE765D8F7F81863CEA4ECDE43D4EFDC31B17CF5F4C55A4AE13098CF5249AF0E85B9C3DF597110DBFC1917E16436CF32B148173DBA4E7A2ABB788EC9BAE830CD70B8F88737F75C9C2A6799E1330ADC158E93FC8DDEC9C9D1F109F72998FDF92CCB348E1D0F38C886E830A52F358EF086B29B7670E33BC886EFA8429F42091E5164DFA3487853B5C6EEF2E5937EE1B94F65F40B2E7E1963007CEA43183DA1CBBE5AD22FFC303D2FFD26C900F8FDF73CF8C51110FBA401BBD557395E874D123F86918911EEB7937CF8E96CF2AFACD8A975F5B7BBBAE481751D113F746A1D59FF361D44E5573574AB028274A915471F012A5546E3D0911DA23760E13B1B1A73CCF8331A7D61F25FC9E80B97FB08466F5D00735BC063F7BE8F4F586840B7FB62452BEB080CA0F8818A12F9773E7AFABDB111103E42D1090EA255EB040852A77542E4E9D13A81F19C24DDC0C40F3F18E0B524BD7F1DBE9423B1D4F55E453199BF6AC54F2F712CDDE9E7FB0266D8E5FB02E5C9E33B4D049620BE131440023FC49C521DECBED84925A1F7360E0DF3E29D2619C7919755A12B5FEB107A009FCEBE580D8048ABDBC576AD989E5F4727EED037BDC0C59464922AC2E6569CC93D85E3260B1473748811F92D3C1F273C6FC78DFB5A2C96E86DB52C5656ACCBEE0FC46B3BC4A0A94E3C5FECB0BD386B0FF1A68EB4752428532BAAD3BE36EBFB3753FDB19782140E3D108882BC12239395E9BDE9AA33F9AD96E4A3DF07C32847F0B013864F19F946276AD3EEFC8BE31384BE6A2ED016B664202D6BA9F33DE998A989E993CE73EFD8C57AE2EEEC83A87377BE4EF1CEC1CE1DDC0B65DD04C7D18CFD1266A4E940DAD995AF706CEACC57C993B92F3EA88596BE4CFF63AAC0BBE7B6146981F841EC8DB532BFD67A36719621D1827C250964D4E191D4E6B7846452199A651932602ADB380C4926DCC4FE8934213945AA8B35DBA547B4296D91EEA01951714AA515399AC519B3754222D93CA2D0DD537A2AA6BE2EC9A6DC3F49186E042FB3AF249DA09D30649584A7BE06A7E73E326FB618DD3E3A74FF1835CDE933479C1803D365828D30E07BE46C7823B7E6FEF160769B042D3AB11F864BF1E52512796E49693FDFDE27916FECAE6B8819C10CB0CDC49C559ED42194B12F57A3320B7712F19934CD2101E97994B82B642724D926333EFBCEDA2FC8DB922C1FFD2576AE82EB6DB2D926A4C9D85F7A8C3EA421B44A7E46E3C9D679769DDD688DFB6802A926D1D0045F071FB6AEE754F5BE042EA54820D2D8BC38974BC73249CFE7D6CF15D29730D0042ABAAF5A52DC627FE311B0F83A58A047DCA66EDF62FC09AF91FD5CBE832607691E08B6DB67176E76961F17187579F22FD161C77FFAF1FFFB543E97B09D0000 , N'6.2.0-61023')

