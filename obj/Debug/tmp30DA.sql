ALTER TABLE [dbo].[CustomerReserves] ADD [StaffName] [int] NOT NULL DEFAULT 0
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202212060858043_editCustomerReserve2', N'BonnieYork.Migrations.Configuration',  0x1F8B0800000000000400ED5D5B6FE3BA117E2FD0FF20F8A92DF6C449160BB481730EB2C9A627EDEE2688B3E7B44F012DD18E10597225394D50F497F5A13FA97FA1D49D972145EA66391BEC4B5624BFE16538331CD233FFFBCF7F673F3DAF3DEB0987911BF8A793A383C389857D3B705C7F753AD9C6CB1FFE38F9E9C7DFFE66F6C9593F5BBF14F5DE27F5484B3F3A9D3CC4F1E6643A8DEC07BC46D1C1DAB5C3200A96F1811DACA7C809A6C787877F9A1E1D4D318198102CCB9ADD6EFDD85DE3F43FE4BFE7816FE34DBC45DE97C0C15E947F2725F314D5FA8AD638DA201B9F4E3E06BEEFE2BF07E1E3415679629D792E221D99636F39B190EF07318A49374FBE45781E8781BF9A6FC807E4DDBD6C30A9B7445E84F3EE9F54D5754772789C8C645A352CA0EC6D14076B43C0A3F7F9D44CF9E68D2678524E1D99BC4F6492E39764D4E90492B9DB46AE8FA3E8CA5F06E13AA7C2D33D39F7C2A40D30D50700C03BABAAF6AEE410C248C9BF77D6F9D68BB7213EF5F1360E91F7CEBAD92E3CD7FE2B7EB90B1EB17FEA6F3D8FEE34E93629633E904F3761B0C161FC728B97F950AE9C893565DB4DF9866533AA4D36B22B3F7E7F3CB1BE12E268E1E19227A85998C74188FF8C7D1CA2183B37288E71E82718389D55813A47EB8EB037A182C327E415540933926D35B1BEA0E7CFD85FC50FA7930F641F5DBACFD8293EE41DF9E6BB6413923671B8C575B47EC5F8D1412FF31885714277207A9F7C67406A1F438C1ED3210E49908CB16F723F079E3BE4EAE5F4065ABD9CDA70AB47131C60F53271961355103B6E44EC2B7A7257A93CE2C8A6D2E902C7C825D2E5167B699DE8C1DD646AF0802ABF07A5FE6518AC6F038F8582AADEDFA1708593650BF4EACF836D687343994D2B5DA4D450CCB84C3413D5F04D23C1B47C87CC67F822D054370396D810E1CCB60362EF2936C7D1A1DEEE1066A7666FA228FA6710AA64404F94D3A54AFE6C2F7E0C299FA79C3030D10B9730966B7720DB0D099F394E487873F859C69EB779087CFC75BB5EE050C5601F7AE1EC2E69D7B1325A2EEFDCD853F17233EDC673118EECD0DD64FA494A8AFCD901AD8F44AEE290C8D087DE495D9263EB22081E3FBBFE63EFC4AEFC2846AB10AD07A14688E04108FD8C91337F085402A6234299217741346CF7B4A4D61C68A1F56BD515569AAE55575881C6438AF13A0207C3D4B8670CBF6A18D24A82592AAF0919A4AAAE178612D8EBA250D661A85CE82B58C9B49BA960561E01CA725967255500931FAED7CAD4E738A4911B2A69FA66EE83B4D2E6F5046BD40999E01A33F6A81F037A836BFD13FD50BE2186ECF05433DB67D183D2E108DDE2350AFB57D837E43440765D3F06965A24FE4AE4805CEBB055EE3921C48946493D583ECA2A9BCB75B56BA7A5DEE4157FBD866DE8C86197C2CC9743B77D93EF202D66DDDACAF974C63B01A9D116BD59CEFD6E78D15AD6900E8DF64D657C9AEC98A2D5DB5E8169E5D3338C47AEB1646F74B6E059537900692ECA9BF9E4CB866F8C2911991D18E9DFA57FBD5ED568FA240D29FF25580CE20CD5F73077428E705E18385B7B10E7EB9B4774CC1E51F5E9AABCE996995A79857B466B70661650073E534115BB3E4FB5F0938166A1C29FD65CFD96D36EAC7FF3966F0A58AE480CEFB3B355462FD1F57239F4F6ABE3E3A6BB0FE464D5366DC4CAE729F3E2B08931C9B67D636790D677680A167CB19BD716DA569AAE3430ECC047378C1F2E942FCFBA795537BCB9512CEC2D8E70F8844199C7D5B9E7054C25F9D43505EBA3A67AAB8B2F61604DC460DEF84D0E2A854217F75FE3F4AD3685E9C0DF90B31EFD4622F93BBBAD6B86C5BC0C6E0B46BDFAD5876AE161E66585D4C5ACAC289860EADAA6EF3178C9D8A730AD1B8944F6EA08D3B3280A6C37EDB978BC03DFC4B0F34378C3327C2093E93BEEF90DD17C44B4BA1B224C492F4F277F1056429F52A97B2A4AE0E31E96E2E1C1C191409448681C26221279E7646D89CC77FD5814E7AE6FBB1BE499F58F83D1D40BC9529604F9920B9C5CF393FE9AAD894E4F24CF96C5CE957DE0D459DD54CEA6142FD6B1A8E20A4ACE353AF7513473B217AE26FCA973D50D302744E8885F9DD9B57F813D1C63EBCCCE7E15778E221B39A208265BDEE9849DEB8733082FD7AF9F112317F7BC3B6161F9A303195769BC40D0642915EF6ABC0FD492DFBB67DBDA910CC0B3B56BA6D387D2C6DC09A38277A832EE515FA8568C535DCA6BE8E01A7833BE1479BF1173A9063A005FA926428B3CF5D3A5DD6970C8B7AAD4A94A472BA7B94B07BFA1DE565EA570346016EBD29054776828ADAB98773D01961FD877C76AC07594920B5477535A4C50CB67AA57E27BA2616BC6311477CAD76AFCDA55EDEA903190A6DFA36223C143AACFAC7A5E93FD39CD688D6700D6D55A43AD7EE42ED551F02FE7E2D2652A99BFAB170E96DD5488C4C62A78F546B4032686D751A723F405C3009C9C3943491BD24F1F87C5A1BCBCFAB9582465F839062E93BE4538BF4F8AF22B389EF312EC398E553F46ACBCB12A0FA5C0D32C32631D08884C690D1227B4E5BDCBCA6BFBC5BACE80AEB1156AF0AAE3A2805415E9F449315754A90E5279BC80A1CAE21A2C5ED208687C054DBC524C4A01CB1A1C22B5614046837F274BB5D2FC692DBFC7CD2F12CAB1731B41101FE6370714B4CEBE9CB293A635A1AA1F5140B3A9EBF136F77933F3A8DA97E64E6E681EC11D6F3E838A5F6D89D3A7E96D35F4B7EA8ECED0C1AAC7D70DA60CFE3984385BF52E3F7DA71F351899A8D677F1F53633F2A77E92AD58EBBA32735EF15B50A23ECCBC553C68B7B3053DF0954C569DF3C5C8FDA2372A237F4B6F6C55F390419C2E03774003870035CE1A2BA08107A047515FF78EA27E1E55C7D22607D38E6652721205D08D59B278EF511E71CAB2D9348B019B7F984D25C162675FD066E3FA2B2A786CFEC59A679163CF7F989BC7545D6718539BE167FE40565222FB11AD30574A48939E5EBA61145FA0182D50F2FEE8DC590BD5A0039DC4802E28CACF6CE2BA16A675D136F99B3F492A22BD0207E31CE9920C7A9D1CB3537954638D0A285612E6177928041E0C9E07DE76EDCB0FFFF2D66C00561A872DD14714C3ACD2A862A93172199213C02DCB8C51E9D09B00305DDC0C3B7D6F27434E0BF571C560A834B0586A8C0CCE315F668C2A9B63A0B819B630C742A13E2E17B29446E58A44CCD994DBF282EF4B1033824B9195605AF24D6D8B99C8350592863C53B6EE478ED177DF0C8AE24E5C8E26791647036BBE9C93D3287F8442A3961F0DF8B4FC4D09C3A2E5577DA479158E9386A23EEB63650136699873E041BA0AA18A9649A3545F0D66BA087FC9CC74F1D1604CFCCF5898E1F185062B28C3BC698647C7A26417B2FA6EB00E74BC496629E80283BD45C594643614F55D1F8DFD91348DC796984812E697D0AC30618AF431ABDF3BD370D557036D57FEC2885173E55763BD99FD3E01D09959C168745BDDA1B991D50E6199D8EB70FB7E345CF9A44190CD869AB20C56C7F4A5FC6AD0A32AFA1CD3A7EAB3810CCCE2C931D22FFB642AA9161812530B23A422FC1B8D537C3318131DDD8D19195D309A1DC65D13B4352055605A36A41AA09F4D263C23074D3ED91B73396EF9584ED0C70D702013ADFC3C1A762A2F06DA31920C468385E44DFB3D8300D2952919CD12A9EF398CB77B8BF3A2A2F5B8B5E9184F71E6224286558571A2A1AAAFBB3F3BB11199D83D4797BC9D28BA3B51EC525A154EB70EC495044A575E499BF725B03AB420CAB03C025859329A65AFBDE53359783598C6D2D701F4B3F8E3D3326C3C1946983325BBD71055EC17C6962FBFBE4249C9DF5E77B3672468069B468AD0CFAEA15F1F431C6AEE3711FA2379A52F47D9E541B06F5B988978C2BA4EA802633CE0C2902D314614AE09E9EF03EF62E149075FA5A49E7F29FF5F3EE9C89F53D4270516DE576455261699A627D749DE56CC5F22C2D307498583F93FBC73CFC5898A292A7C41BEBBC4519CC5459A1C1F1E1D738985C793E4771A458E073C478192AB4823A10C10E7C94D26785E17C9C930C80F9458D77F42A1FD804221EE5985DD268F6EB7F05CE2D56EC1C53CAB3DE05301963A4297E5C0ED16BE9F999766B8ED01BFFB9907F3D782D8C735D88D72BCBE0E9924A6564DC9083FC522F5F0F3E9E45F69B313EBEA6FF755CB77D67548F4D0897568FDDB741195395A75BB0282B4E915178C1464AA342868CB58A39D0153EF41B4F7987152D6AE30F99CAB5DE17229553B9B0238522ABC761FBA4888AA01DD2CFF6923E9082CA098EEB440FEDD1A3DFFDE580808294D5BC14141FA5B018281F85B21F2C1F65B81F1116EDB818969440DF01AA6507C1DBAB43C9E9B69AFBC994C5F35CA7628512CED93197605CCE42AEC0A944F45D86A23B0E9065B41012905FBD853AAE7197BBBA924C9E28C4DC3AC79AB4DC685434EBBD03618721F7C00BFB1D85B0E8052A035B3ED1AE50D7B1D93B843DDB4878729C9265598CD8D327075648E9B1C50CCD1A1FC5A6FE6F930E679B34C4BAF456289DA564B62A5CDDA787FA02C497D2C9A3284D7BE2EDBDE497B280BCF40AE2381991A25CEE9CA593F8098523E3AD85B961753B6E80AABAA65EBD37F03077EDA6A7427A26620A275DB3E358C43FE8EBB4A0DD3168CBA3FD384EA3F110918B10B8A6567980B040C233670DC71BD28123AF2C96A9847E4FB4816C285B081B8A7F7F0B6B2586BADB294B44FA5307CAE8F579DD6A3812CE989CB1AF27C473C662A62BACCCCB10781C21BA5E1E822E7C6EE749DE2F77C3B57707B9A40035C47B344167000C216F937DAA61E183A0BC6AB4C7931161DD4804BF753FF9832F06EB54FB33415E332A285408D547F8CB30BEC87195DFF7B1C80E41EA7930004C780A91CC6C46AB5BF5E1C05AF8D22E983184A945FD6CED239643FA2399D388B80F044E622032AEA2458D04EFC00D1A42AD4D3324C0DA11C633FD923E021769F6102A29397BA58735C7A1928A423D25D34A31C15526A798D7A72C6692C20926C1D7DA206B92E5464F34A22DDDD67C450881CDD1C15727B9F8956D07950FDFE735C80F2C93029032C723452628C31714583D5ED6242C79790C23CFBC4801BA3E76C13E0200CD22570BAA33635C5F8D248B4DB040D2671CC0922BA1092127DAB9305613F923E00CBADCEBF30C0F418E471107FDC4FCE4A5B3FB993CFFE778123775541CC08A68F6DE69454D6494C89E2C0C6F5A8A8C2BD08F842A6C82147A8B3307697C88E49B14DF8CAF55713EB17E46D49954FEB0576AEFCEB6DBCD9C664C878BDF01849921CFA54F4D364156C9F67D7E92FBEA22E8640BAE926CF18AEFD8F5BD773CA7E5F02CF182410C969327FC493AC659C3CE659BD94485F035F13289FBEF2107C87D71B8F8045D7FE1C252C64DEB76F11FE8C57C87E296234C841EA17829DF6D9859BBE758D728CAA3DF92FE16167FDFCE3FF01B80801CF1EBB0000 , N'6.2.0-61023')
