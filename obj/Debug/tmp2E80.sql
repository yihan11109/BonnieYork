DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.CustomerReserves')
AND col_name(parent_object_id, parent_column_id) = 'StaffName';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CustomerReserves] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[CustomerReserves] ALTER COLUMN [StaffName] [nvarchar](max) NULL
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202212060927315_editCustomerReserve3', N'BonnieYork.Migrations.Configuration',  0x1F8B0800000000000400ED5D5B6FE3BA117E2FD0FF20F8A92DF6C449160BB481730EB2CEA627EDEE2688B3E7B44F012DD18E10597225394D50F497F5A13FA97FA1D49D972145EA663B6BEC4B5624BFE16538331CD233FFFBCF7F273FBDAC3CEB1987911BF8E7A393A3E391857D3B705C7F793EDAC48B1FFE38FAE9C7DFFE66F2C959BD58BF14F5DE27F5484B3F3A1F3DC6F1FA6C3C8EEC47BC42D1D1CAB5C3200A16F1911DACC6C809C6A7C7C77F1A9F9C8C318118112CCB9ADC6DFCD85DE1F43FE4BFD3C0B7F13ADE20EF4BE0602FCABF9392598A6A7D452B1CAD918DCF471F03DF77F1DF83F0E928AB3CB22E3C17918ECCB0B71859C8F78318C5A49B67DF223C8BC3C05FCED6E403F2EE5FD798D45B202FC279F7CFAAEABA23393E4D4632AE1A1650F6268A839521E0C9FB7C6AC67CF346133C2AA78E4CDE2732C9F16B32EA7402C9DC6D22D7C75174ED2F82709553E1E99E4DBD3069034CF51100F0CEAAAABD2B39843052F2EF9D35DD78F126C4E73EDEC421F2DE59B79BB9E7DA7FC5AFF7C113F6CFFD8DE7D19D26DD2665CC07F2E9360CD6388C5FEFF0221FCAB533B2C66CBB31DFB06C46B5C94676EDC7EF4F47D657421CCD3D5CF204350BB33808F19FB18F431463E716C5310EFD0403A7B32A50E768DD13F6265470F88CBC822A6146B2AD46D617F4F219FBCBF8F17CF481ECA32BF7053BC587BC23DF7C976C42D2260E37B88ED6AF183F39E87516A3304EE80E44EF93EF0C48ED6388D1533AC421099231F64DEEE7C073875CBD9CDE40AB97531B6EF5688203AC5E26CE72A20A62A78D887D45CFEE3295471CD9543A5DE218B944BADC612FAD133DBAEB4C0D1E51E50FA0D4BF0A83D55DE0B15050D5877B142E71B26C815EFD59B0096D6E289371A58B941A8A19978966A21A1E34124CCB77C87C86AF024D753360890D112E6C3B20F69E62739C1CEBED0E61766AF6268AA27F06A14A06F444395DAAE4CFF6E2C790F234E58481895EBA84B15CBB03D96E48F8C27142C29BC3CF32F6BCF563E0E3AF9BD51C872A06FBD00B677749BB8E95D16271EFC69E8A979B69379E8B706487EE3AD34F5252E4CF0E687D2472158744863EF64EEA8A1C5BE741F0F4D9F59F7A2776ED47315A866835083542040F42E8678C9CD963A012301D11CA0CB94BA261BBA725B5E6400BAD5FABAEB0D274ADBAC20A341E528C57113818A6C60363F855C3905612CC52794DC8205575BD3094C05E1785B20E43E5425FC14AA6DD4C05B3F2085096CB3A2BA90298FC70BD56A63EC7218DDC5049D383B90FD24A9BD713AC512764826BCCD8937E0CE835AEF54FF443F99618B2C353CD6C9F790F4A872374875728EC5F61DF92D300D975FD18586A91F82B910372ADC35679E08410271A25F560F928AB6C2ED7D5AE9D967A9357FCF51AB6A123875D0A335F0EDDF620DF415ACCBAB595F3E98C770252A32D7AB39CFBDDF0A2B5AC211D1AED9BCAF834D93145ABC35E8169E5D3338C47AEB1646F74B6E059537900692ECA9BF9E4CB8607C69488CC0E8CF4EFD2BF5EAF6A347D928694FF12CC077186EA7B983B2147382F0C9C8D3D88F3F5E011DD658FA8FA7455DE74CB4CADBCC203A33538330BA8039FA9A08A5D9FA75AF8C940B350E14F6BAE7ECB6937D6BF79CB8302962B12C3FBEC6C95D16B74B3580CBDFDEAF8B8E9EE033959B54D1BB1F234655E1C363126D9B6077606697D87A660C117DB796DA16DA5E94A03C30E7C74C3F8F152F9F2AC9B5775C39B1BC5C2DEE10887CF1894795C9D075EC054924F5D53B03E6AAAB7BAF81206D6440CE68D0F72502914BAB8FF7AEBBED53E5C123977D2CF2892BFB30BBD6658CCE3E1B660D4C3607DA8164E685E9C48BDD0CA8A8295A6AE6DFA6483179E7DCADBBA9148C4B38EBCBD88A2C076D39E8B2740F0D90C3B3F84372CC33734D98EE45EE890BD49A4AFBB26F296F4F27CF4076125F42995EAA9A204BEFF61291E1F1D9D08448910C76122459137256B4BD482EBC7A2C4777DDB5D23CFAC7F1C8CA6EA4896B224C8975CE2E42500E9AFD99AE8F444F2B259EC5CD9074EE3D54DE5644CF1621D8B2A6EA9E45CA373654533277B276BC29F3AB7E1007342844EF8D599DCF897D8C331B62EECEC87735314D9C8114530D9F24E27EC5C3F9C4178B97EFD8C18B9B80ADE0A0BCBDF25C8B84AE39182264BA97857E309A196FCDE3EDBD68E64009EAD5D339D3E9436E6561815BC6695718FFACEB5629CEADE5E4307D7C09BF1A5C8FB8D984B35D001F84A35115AE4A95F376D4F8343EE57A54E55FA6239CD5DDE0118EA6DE56D0B470366B12E0D49758786D2BA8A79D71360F9997E7BAC06DC5829B940757DA5C504B57CA67A48BE271AB6661C4371A77CAD765FBBAA5D1D3206D2F47B546C243851F59955CF6BB23FA719ADF10CC0BA5A6BA8D58FDCEBBA13FCCBB9B874994AE6EFEA858365971922B15D15BC7A23DA0213C3EBA8D311FA0E62004ECE9CA1A40DE9A78FC3E2505EDE0E5DCE9332FC1203F74DDF229C5F3945F92501CF7909F60CC7AADF2B56DE58958752E0691699B10E0444A6B4068913DAF2DE65E5B5FD625D6740D7D80A3578D5715140AA8A74FAA4982BAA5407A93C5EC05065710D162F690434BE82265E2926A580650D0E91DA3020A3C13FA5A55A69FEFA96DFE3E61709E5D8B98D20880FF39B030A5A675F8ED949D39A50D5EF2CA0D9D4F5789BFBBC997954ED4B732737348FE08E379F41C50FBBC4E9D3F4B61AFA5B754767E860D5E3EB065306FF62429CAD7A979FBED38F1A8C4C54EBBBF87A9B19F96B40C956AC755D9939AFF82D28511F66DE2A1EB4DBD982DE004B26ABCEF962E47ED11B9591BFA537B6AA79C8204E97813BA08143801A678D15D0C003D0A3A8AF7B47513F8FAA6369938369473329398902E8C62C59BCF7288F3865D9649C8589CD3F4CC69278B2932F68BD76FD25155F36FF62CDB2E0B2D31F66E661575719C6D866F8993F909594C87E444BCC9512D2A4A7576E18C59728467394BC3F9A3A2BA11A74A09318D00545F9994D5CD7C2B42EDA267FF32749453058E0609C235D9141AF9263762A8F6AAC5101C54A2201230F85C09BC269E06D56BEFCF02F6FCDC668A571D8127D4431122B8D2A961A2397513B01DCB2CC18958ECE0900D3C5CDB0D3F77632E4B4501F578C974A038BA5C6C8E01CF365C6A8B239068A9B610B732C14EAE372514D6954AE48C49C8CB92D2FF8BE043123B8145909A625DFD4B698895C532069C83365EB7EE4187DF7CDA028EEC4E56892677134B0E6CB39398DF2772A346AF9D1804FCB9F9D302C5A7ED5479A55113B6928EAB33E561683938699026FD6550855404D1AA5FA6A30D345844C66A68B8F0663E27FE9C20C8F2F34584119E66D333C3A5C25BB90D5778375A04352324B411718EC2D2AEC24B3A1A8EFFA68ECEFA8693CB6C44492303F9666850953A48F59FD249A86ABBE1A68BBF247488C9A2BBF1AEBCDECF70980CECC0A7646B7D51D9A1B59ED109689BD0EB7EF47C3954F1A04D96CA829CB78764C5FCAAF063DAA02D4317DAA3E1BC8C02CE41C23FDB24FA6926A8E21313537422A22C4D138C5378331D101E09891D1053BB3C3B86B82B606A40A4CCB865403F4B3C98467E4A0C9277B632EC72D1FCB09FAB8010E64A2959F77869DCA8B81768C2483D1602179D37ECF208074654A766689D4F71CC6DBBDC57951D17AB7B5E92E9EE2CC45840CAB8AF44443555FB77F76628336B17B8E2E399C28BA3B516C535A154EB70EC495044A575E499BF725B03AB420CAC83D025859B233CB5E7BCB67B2F06A308DA5AF03E867F1774FCBB021671861CE946C5F4354E161185BBEFCFA0625257F7BDDCD9E91A0196C1A29423FBB867E7D0C71A8B9DF44E88FE495BE1C659B07C1BE6D6126E209EB3AA10A8CF1800B43B6C41851B826A4BF0FBC8B85271D7C95927AFEA5FC7FF9A4237F4E519F3758785F91551959649A9E5D27795B317B8D084F1F25158E66FFF0A69E8B13155354F8827C7781A3380B9D343A3D3E39E5720FEF4E1EE07114391EF01C05CABF228D84324028283799E0595DB027C3203F50EE5DFF1985F6230A85D06815769B54BBDDC273B959BB051753B1F6804F0558EA085D9626B75BF87E665E9A04B707FCEE671E4C710B629FD660374A03FB366492987D352523FC148BD4C32FE7A37FA5CDCEACEBBF3D542DDF593721D14367D6B1F56FD34554A671D5ED0A08D2A6575CBC5290A9D2B8A12DC39176064CBD07D1DE63C6795BBBC2E4D3B27685CB655DED6C0AE060AAF0DA7DE82267AA0674B314A98DA423B0806246D402F9772BF4F27B632120643D6D0507C5F16F0508C6EA6F85C8C7E36F05C607C16D0726661A35C06B9865F16DE8D2F2786EA6BDF266327DD52821A244B1B4CF77D8153093CEB02B503E5B61AB8DC066246C0505641DEC634FA99E67ECEDA692E49333360DB3E6AD3619173139ED42DB78C97DF001FCC6626F3900CA92D6CCB66B945AEC6D4CE21675D31E1EA6249B546136374AD2D591396E724031478752701DCCF361CCF366C998DE8AC412B5AD96C44A9BB5F1FE408994FA58346508AF7D5DB6BD93F650A29E815C47023335CAADD395B37E0031A57C74B0B72C2F6675D1155655CBD6A7FF060EFCB4D53E9F88F4CCDCF639621CF277DC558E98B660D4459A2654FF1949C0D05D50503BC3A420603CB1810390EB8593D0115456C38422DF47D6102E960DC43DBDC7B995055D6B95AEA47D4E85E1937EBCE9FC1E0D64494F5CD690E73BE2315311D3658A8E3D8818DE281F4717C937B6A7EB143FECDBBA82DBD34C1AE03A9A65B4802311B648C4D13607C1D0E930DE64EE8B5DD1410DB8743FF58F29036F57FB34CB57B15B46B410B191EA8F719A81FD30A3EB7F980390DCE3BC1280E01830A7C32EB15AEDCF187782D77622FB831853945FD6CEF23A64BFA6391F39F380F044E622032AEA645AD0CE0001D1A42AD4D332CC11A11C633F6924E021769F6A02A29397BA58735C7AA928A423D25D34A36415526A798D7A72C6F92C20926C1D7DA206492F5464F34A22DDEDA7C650881CDD6415727B9F095BD07974FDFE935D80F2C9303B032C72347263EC62068B06ABDBC584EE5E660AF33414036E8C9ED34E808330C89BC0E98EDA1C15BB974FA2DD26683089BB9C29A20B2129D1B73AE910F623FB03B0DCEA440C034C8F414207F157FEE4ACB4F1933BF9EC7F9738729715C48460FAD8664E49659DC494280E6C5C8F8A2ADC8B802F648A1C7284BA08637781EC9814DB84AF5C7F39B27E41DE8654F9B49A63E7DABFD9C4EB4D4C868C57738F9124C9A14F453FCD5AC1F6797293FEF42BEA6208A49B6EF28CE1C6FFB8713DA7ECF715F08C4102919C26F3D73CC95AC6C9AB9EE56B89F435F03581F2E92B0FC1F778B5F6085874E3CF50C242E67DFB16E1CF7889ECD72258831CA47E21D8699F5CBAE9A3D728C7A8DA93FF121E76562F3FFE1FA12A73C14ABB0000 , N'6.2.0-61023')
