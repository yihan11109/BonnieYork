ALTER TABLE [dbo].[CustomerReserves] ADD [ReserveEnd] [datetime] NOT NULL DEFAULT '1900-01-01T00:00:00.000'
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202212061230086_editCustomerReserve5', N'BonnieYork.Migrations.Configuration',  0x1F8B0800000000000400ED5D5B6FE3BA117E2FD0FF20F8A92DF6C449160BB481730EB2CEA627EDEE2688B3E7B44F012DD18E10597225394D50F497F5A13FA97FA1D49D972145EA663B6BEC4B5624BFE16538331CD233FFFBCF7F273FBDAC3CEB1987911BF8E7A393A3E391857D3B705C7F793EDAC48B1FFE38FAE9C7DFFE66F2C959BD58BF14F5DE27F5484B3F3A1F3DC6F1FA6C3C8EEC47BC42D1D1CAB5C3200A16F1911DACC6C809C6A7C7C77F1A9F9C8C318118112CCB9ADC6DFCD85DE1F43FE4BFD3C0B7F13ADE20EF4BE0602FCABF9392598A6A7D452B1CAD918DCF471F03DF77F1DF83F0E928AB3CB22E3C17918ECCB0B71859C8F78318C5A49B67DF223C8BC3C05FCED6E403F2EE5FD798D45B202FC279F7CFAAEABA23393E4D4632AE1A1650F6268A839521E0C9FB7C6AC67CF346133C2AA78E4CDE2732C9F16B32EA7402C9DC6D22D7C75174ED2F82709553E1E99E4DBD3069034CF51100F0CEAAAABD2B39843052F2EF9D35DD78F126C4E73EDEC421F2DE59B79BB9E7DA7FC5AFF7C113F6CFFD8DE7D19D26DD2665CC07F2E9360CD6388C5FEFF0221FCAB533B2C66CBB31DFB06C46B5C94676EDC7EF4F47D657421CCD3D5CF204350BB33808F19FB18F431463E716C5310EFD0403A7B32A50E768DD13F6265470F88CBC822A6146B2AD46D617F4F219FBCBF8F17CF481ECA32BF7053BC587BC23DF7C976C42D2260E37B88ED6AF183F39E87516A3304EE80E44EF93EF0C48ED6388D1533AC421099231F64DEEE7C073875CBD9CDE40AB97531B6EF5688203AC5E26CE72A20A62A78D887D45CFEE3295471CD9543A5DE218B944BADC612FAD133DBAEB4C0D1E51E50FA0D4BF0A83D55DE0B15050D5877B142E71B26C815EFD59B0096D6E289371A58B941A8A19978966A21A1E34124CCB77C87C86AF024D753360890D112E6C3B20F69E62739C1CEBED0E61766AF6268AA27F06A14A06F444395DAAE4CFF6E2C790F234E58481895EBA84B15CBB03D96E48F8C27142C29BC3CF32F6BCF563E0E3AF9BD51C872A06FBD00B677749BB8E95D16271EFC69E8A979B69379E8B706487EE3AD34F5252E4CF0E687D2472158744863EF64EEA8A1C5BE741F0F4D9F59F7A2776ED47315A866835083542040F42E8678C9CD963A012301D11CA0CB94BA261BBA725B5E6400BAD5FABAEB0D274ADBAC20A341E528C57113818A6C60363F855C3905612CC52794DC8205575BD3094C05E1785B20E43E5425FC14AA6DD4C05B3F2085096CB3A2BA90298FC70BD56A63EC7218DDC5049D383B90FD24A9BD713AC512764826BCCD8937E0CE835AEF54FF443F99618B2C353CD6C9F790F4A872374875728EC5F61DF92D300D975FD18586A91F82B910372ADC35679E08410271A25F560F928AB6C2ED7D5AE9D967A9357FCF51AB6A123875D0A335F0EDDF620DF415ACCBAB595F3E98C770252A32D7AB39CFBDDF0A2B5AC211D1AED9BCAF834D93145ABC35E8169E5D3338C47AEB1646F74B6E059537900692ECA9BF9E4CB8607C69488CC0E8CF4EFD2BF5EAF6A347D928694FF12CC077186EA7B983B2147382F0C9C8D3D88F3F5E011DD658FA8FA7455DE74CB4CADBCC203A33538330BA8039FA9A08A5D9FA75AF8C940B350E14F6BAE7ECB6937D6BF79CB8302962B12C3FBEC6C95D16B74B3580CBDFDEAF8B8E9EE033959B54D1BB1F234655E1C363126D9B6077606697D87A660C117DB796DA16DA5E94A03C30E7C74C3F8F152F9F2AC9B5775C39B1BC5C2DEE10887CF1894795C9D075EC054924F5D53B03E6AAAB7BAF81206D6440CE68D0F725029140C357BE2BF7BEBAED43E3C103933D2AF2692BFB3FBBB6658CC5BE1B660D43B607DA8163E675E7A489DCECA8A8251A6AE6DFA428397957D8AD7BA9148A4B18E78BD88A2C076D39E8B073EF0950C3B3F84372CC32733D98EE41EE490BD4984ADBB26E295F4F27CF4076125F42995DAA8A2043EF761291E1F1D9D084489CCC66122349137256B4BB480EBC7A280777DDB5D23CFAC7F1C8CA6A64896B224C8975CE2E4E29FF4D76C4D747A2279C82C76AEEC03A7E0EAA67232A678B18E4515975272AED1B9A1A29993BD8235E14F9DCB6F8039214227FCEA4C6EFC4BECE1185B1776F63BB9298A6CE48822986C79A71376AE1FCE20BC5CBF7E468C5CDCFC6E8585E5CF10645CA5F1264193A554BCABF162504B7E6F9F6D6B473200CFD6AE994E1F4A1B732B8C0ADEAACAB8477DC55A314E754DAFA1836BE0CDF852E4FD46CCA51AE8007CA59A082DF2D48F99B6A7C1216FAB52A72A5DAF9CE62E5DFE867A5B79B9C2D18059AC4B4352DDA1A1B4AE62DEF504587EA6DF1EAB0117544A2E50DD566931412D9FA9DE8DEF8986AD19C750DC295FABDDD7AE6A57878C8134FD1E151B093E537D66D5F39AECCF69466B3C03B0AED61A6AF523F7BAEE04FF722E2E5DA692F9BB7AE160D9DD8548AC6FFDAED7B32D3023BC1E3A1DA1AF0E06E0C8CCA949DA907EFA382C0ED7E5A5CEE53C29C32F31704DF42DC2F94D51943BFB790E4AB0673856FDCCB0F2AAAA3C8D026FB2C88C96171099D21A244EF8CA7B9795D7F68B7581015D632BD4E055C73E01A92AD2E99362AEA8521DA4F298004395C53558BCC410D0F80A9A78A5B89302963538446AC3808C06FF02966AA5F9A3597E8F9B5F089463E73682203ECC6F0028689D7D3966274D6B42553F8F806653D7736DEEBB66E651B52FCD9DD5D03C823BDE7C0615BFC712A74FD36B6AE837D51D9DA1A3548FAF1B4C19FC430771B6EA5D77FACE3B6A303251ADEFAAEB6D66E48FF8245BB1D60565E684E2B7A0447D98799D78D06E670B7ABA2B99AC3A278A911B456F54467E93DED8AAE64182385D06C7FA06077B6A9C35564083937C8FA2BEEE3D44FD3CAA8E974D0E981DCDA4E44409A01BB364F16EA33CE29465937116DD35FF30194BC2C04EBEA0F5DAF5975458D8FC8B35CB62C24E7F9899474B5D6518639BE167FE40565222FB112D31574A48939E5EB961145FA218CD51F28E68EAAC846AD0814E62401714E56736715D0BD3BA689BFCCD9F2415315C8183718E744506BD4A8ED9A93CAAB14605142B09E08B3C14024F01A781B759F9F2C3BFBC351B5A95C6614BF411C500AA34AA586A8C5C06DB0470CB3263543AA826004C1737C34EDFCDC990D3427D5C31CC290D2C961A238373CC9719A3CAE618286E862DCCB150A88FCB0523A551B922117332E6B6BCE0FB12C48CE0DE662598967C53DB6226724D81A421CF94ADFB9163F41D3683A2B8DB96A3499EB7D1C09A2FE0E434CA9F97D0A8E547033E2D7F2DC2B068F9551F695605DAA4A1A8CFFA5859E84C1A660A3C3557215471306994EAABC14C17812D99992E3E1A8C89FF810A333CBED060056598B7CDF0E82893EC4256DF0DD6818E24C92C055D60B0B7A86891CC86A2BEEBA3B13F7FA6F1D8121349C2FCC69915264C913E66F54B661AAEFA6AA0EDCADF0E316AAEFC6AAC37B3DF19003A332BD819DD5677686E64B5435826F63ADCBE1F0D573E4D1064B3A1A62CC3D0317D29BF1AF4A88A2BC7F4A9FA6C2003B348718CF4CB3E994AAA3986C4D4DC08A908EC46E314DF0CC644C76D63464617ECCC0EE3AE09DA1A902A302D1B520DD0CF26139E8383269FECADB81CB77CF426E8E306389089567EDE19762A2F06DA31920C468385E44DFB3D8300D29529D9992552DF73186FF716E74545EBDDD6A6BB788A33171132AC2A40130D557DDDFED9898DB5C4EE39BAE470A2E8EE44B14D695538DD3A105712285D79256DDE97C0EAD0822803EE086065C9CE2C7BED2D9FC9C2ABC13496BE0EA09FC5DF3D2DC3468A61843953B27D0D514575616CF9F2EB1B9494FCED75377B468266B069A408FDEC1AFAF531C4A1E67E13A13F92D7F672946D1E04FBB68599C825ACEB842A30C6032E0CD9126344E19A90FE3EF02E169E74F0554AEAF997F2FFE5938EFC39457DBA5FE17D4556656491697A769DE46DC5EC35223C7D9454389AFDC39B7A2E4E544C51E10BF2DD058EE22CE2D1E8F4F8E4944B19BC3BE97BC751E478C07314286D8A34A2C900119CDC64826775319A0C83F5402973FD6714DA8F2814229A55D86D32E4760BCFA554ED165CCCA0DA033E1528A923745976DB6EE1FB997969EEDA1EF0BB9F7930332D887D5A83DD287BEBDB904962D2D4948CF0532C520FBF9C8FFE95363BB3AEFFF650B57C67DD84440F9D59C7D6BF4D1751997D55B72B20489B5E71614641A64AC37DB68C22DA1930F51E447B8F19A75BED0A93CFA6DA152E972CB5B3298063A0C26BF7A18B54A71AD0CD329B36928EC0028A894C0BE4DFADD0CBEF8D858090ACB4151C147EBF15201862BF15221F46BF15181FBBB61D989820D400AF6172C4B7A14BCBE3B999F6CA9BC9F455A33C8612C5D23E4D6157C04C16C2AE40F92483AD36029B48B01514902CB08F3DA57A9EB1B79B4A9206CED834CC9AB7DA645CE4E3B40B6DE31EF7C107F01B8BBDE50028B95933DBAE5146B0B731895BD44D7B7898926C5285D9DC28B75647E6B8C901C51C1DCA9C7530CF8731CF9BE5507A2B124BD4B65A122B6DD6C6FB03E53FEA63D194A1B8F675D9F64EDA43F97506721D09CCD428254E57CEFA01C494F2D1C1DEB2BC988C455758552DDBC82B36AB8BB6FF3E6DB5CF07223D2BB77DAA1787FC1D7795EAA52D18758FA609D57F621130721714D3CE30B707184E6CE038E27AD12474E494D5302FC8F791FC830B6503714FEFE16A6531D75A651D699F1A61F8DC1D6F3A4D470359D2139735E4F98E78CC54C4749969630F027F374AABD1450E8DEDE93AC5EFFAB6AEE0F6342106B88E668929E040842DF269B44D253074568B3799C262577450032EDD4FFD63CAC0DBD53ECDD24EEC96112D046CA4FA639C2D603FCCE8FADFE50024F7383D0420381AA466D82596A9FD35E24EF0CC4E2471104383F2CBDA597A86EC4731E723671E109EC85C5D40459D8409DA891C209A54857A5A86A91E9463EC271B043CC4EE33464074F252176B8E4B2FA3847444BA8B669473424A2DAF514FCE382D054492ADA34FD42077858A6C5E49A4BBFD0C170A91A39B73426EB733D1073A0F92DF7FCE0A503E19265980458E468A8B5D4C44D16075BB98D0DD4B30619E4D62C08DD173F608701006E90F38DD519B6A62F7D242B4DB040D267197133E74212425FA5627ABC17E247100965B9D4F6180E931C8CB20FE589F9C95367E72B79EFDEF1247EEB28298104C1FDBCC29A9AC939812C5818DEB515185BBD9FF42A6C82147A88B307617C88E49B14DF8CAF59723EB17E46D48954FAB3976AEFD9B4DBCDEC464C87835F71849921CFA54F4D3E4136C9F2737E92FB8A22E8640BAE926CF116EFC8F1BD773CA7E5F01CF112410C969327F9493AC659C3CCE59BE96485F035F13289FBEF2107C8F576B8F804537FE0C252C64DEB76F11FE8C97C87E2D622EC841EA17829DF6C9A59BBE5D8D728CAA3DF92FE16167F5F2E3FF01EE522BF9C8BA0000 , N'6.2.0-61023')
