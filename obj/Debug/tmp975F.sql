ALTER TABLE [dbo].[CustomerReserves] ADD [ManualName] [nvarchar](max)
ALTER TABLE [dbo].[CustomerReserves] ADD [ManualCellphoneNumber] [nvarchar](max)
ALTER TABLE [dbo].[CustomerReserves] ADD [ManualEmail] [nvarchar](100)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202212070609021_addColumnToCustomerReserve', N'BonnieYork.Migrations.Configuration',  0x1F8B0800000000000400ED5D5B6FE3BA117E2FD0FF20F8A92DF6C449160BB481730EB2C9A627EDEE2688B3E7B44F812CD18E105D5C494E1314FD657DE84FEA5F2875E7654891D4C57636D897AC487EC3CB706638A467FEF79FFFCE7E7A0E7CEB09C5891785A793A383C38985422772BD70753AD9A4CB1FFE38F9E9C7DFFE66F6C90D9EAD5FAA7AEFB37AB865989C4E1ED2747D329D26CE030AECE420F09C384AA2657AE044C1D476A3E9F1E1E19FA647475384212618CBB266B79B30F50294FF07FFF73C0A1DB44E37B6FF2572919F94DF71C93C47B5BEDA014AD6B6834E271FA330F4D0DFA3F8F1A0A83CB1CE7CCFC61D99237F39B1EC308C523BC5DD3CF996A0791A47E16ABEC61F6CFFEE658D70BDA5ED27A8ECFE49535D752487C7D948A64DC30ACAD924691468021EBD2FA766CA36379AE0493D7578F23EE1494E5FB251E71388E76E9378214A92AB7019C5414985A57B72EEC7591B60AA0F0080775653ED5DCD219891B27FEFACF38D9F6E62741AA24D1ADBFE3BEB66B3F03DE7AFE8E52E7A44E169B8F17DB2D3B8DBB88CFA803FDDC4D11AC5E9CB2D5A9643B97227D6946E37651BD6CD8836C5C8AEC2F4FDF1C4FA8A89DB0B1FD53C41CCC23C8D62F46714A2D84E917B63A7298AC30C03E5B3CA516768DD61F6C65450FC64FB1555CC8C785B4DAC2FF6F36714AED287D3C907BC8F2EBD67E4561FCA8E7C0B3DBC09719B34DEA0365ABF22F4E8DA2FF3D48ED38CEE48F43E85EE88D43EC6C87ECC873826413CC6A1C9FD1CF9DE98AB57D21B69F54A6AE3AD1E497084D52BC459495442ECD888D857FBC95BE5F288219B4BA70B94DA1E962EB7C8CFEB240FDEBA50830744F93D28F52FE328B88D7C1A0AAA7A7F67C72B942D5BA4567F1E6D628719CA6CDAE822A986A2C6A5A39988866F1A09A615BA783EE3178EA6BC19B0C49A08678E13617B4FB2398E0ED57607373B2D7BD34E927F46B14C060C44395FAAECCFEEE24793F279CE092313BDF03063794E0FB25D93F099EBC69837C79F65E4FBEB8728445F37C102C53206FB300867F749BB8D95EDE5F2CE4B7D192F9B6937968B50E2C4DEBAD04F4252F8CF1E687DC47215C558863E0C4EEA121F5B1751F4F8D90B1F0727761526A9BD8AED60146A98081A85D0CFC876E70F914CC0F444A830E42EB086ED9F96D09A032DB461ADBACA4A53B5EA2A2B507B48290A127030548D7BCAF06B8621ACC499A5E29A90412AEB7A652881BDAE0A451D86CAB9BE829574BB990B66E911A02E177556500530F9E17A9D4C7D86438CDC5059D337731FA495376F27D8A24EF004B798B147C318D06BD4EA9F1886F20D3664C7A75AD83E8B01940E43E81605763CBCC2BEC1A701BCEB8631B0E422F1572C07C45A87AE72CF082146340AEAC1F25154595FAECB5D3B1DF526ABF8DB35ACA123875E0A3D5F0ED9F64DBE83B4A875EB2AE7F319EF05A4455B0C66390FBBE1796B59413A18ED9BC6F8D4D93155ABB7BD02D32AA7671C8F9CB164373A5BB0AC293D80988B72339F7CDDF08D310522B30723FDBBF4AFB7AB1A459FA426E5BF448B519CA1EA1EE65EC861CE8B2377E38CE27C7DF388EEB247547EBAAA6FBA45A65659E19ED21A8C9905D481CF5450C5BECF531DFC64A05928F1A799ABDF7ADAB5F56FD9F24D018B1589E67D76B1CAF64B72BD5C8EBDFDDAF8D874F7819C2CDBA646AC7C9E332F8A4D8C49BAED1B3B83B4BE4353B0E28BEDBCB650B6D254A58166073E7A71FA70217D79D6CFABBAF1CD8D6A616F5182E22704CA3CA6CE3D2B601AC927AFC9591F2DD53B5D7C710333118365E3373928150A9A9A3DF3DFBD7657EA101E889219C95713D9DFC5FD9D1916F556B82B18F10EB8877E0DF1348421F6C50E37B63F0A0314A4FA57642A543F05B994EE6C32F4775BC0CA7DE17581B422674ECB6BEBBEAD61B5DC908AB16D24023DAAA218CF922472BCBCE7FC511D7CDF44CF0FDED596E663A782D198A75498E5B09AF4D65831E25E9E4EFEC0AD843AA5DA8E6828810FB5688A870707471C51AC6D519CA93BBC3BF1DA62FDED8529AF9ABDD0F1D6B6AFD73F064651C7674B5913644B2E50F66403F7576F4D547A227882CE77AEEE03639AB44DE56C4AF0621B8B4AAE13C55CA372B74832277D79AEC39F2ACF1600E684081DB1AB33BB0E2F908F52649D39C52F1CCFEDC4B15D5E04E32DEFF6C2CEEDC3198597DBD74F8B91AB3BFBADB0B0F8018988AB145E9328B2948C7715DE7A2AC9EFEDB36DEB4846E0D9D63553E9437D3AD80AA382F7E122EE915F8E378CD33CB050D0C12DF07A7CC9F3BE1173C9063A025FC92642893CF133B4ED6970C84F2ED5A952A739A3B9EBCB1A4DBD2DBD166368C02CD6A72129EFD0585A5732EF6A02ACF4C66C8FD580AB452917C8EE199598A095CF642FFEF744C3B68C632CEE14AFD5EE6B57B9AB43C4408A7E8F868D386FB73AB3AA794DF6E734A3349E115857690D95FA51FACB77827F1917972A5389FC5D8370B0E8D6892736B47E57EBD91698115E0F958E90973E237064E1D4C46D703F43145787EBFA3AEE629195A1E714B8E0FB96A0F28E2F297DD82C0765D87394CA7E20DA7855659E468E3769644ACB738854690B12237CC5BD2BCA5BFB45BBC080AED1155AF09A631F87D414A9F449325744A90A527D4C80A1EAE2162C566270686C0545BC5ADC0901EB1A0C22B1614046837FBB4CB4E2B9AFFD3AC0E842A01E3BB31138F1A17F034040ABECCB293D694A132AFB610B349BAA9E6B7DDF35358FB27DA9EFAC86E611DCF1FA3328F9251D3F7D8A5E534DBFA9EAE8341DA56A7C6D3065F04F54F8D96A77DDA93BEF88C18844B5BAAB6EB099113FBF146CC55617949E138ADD8202F5A1E7756241FB9D2DE8D1B560B2DA9C285A6E14B55169F94D0663AB960709FC74691CEB0D0EF6C4385BAC008393FC80A2BEED3D44FB3CCA8E972607CC9E665270A204D0B559B27AB7511F71EAB2D9B488CB5B7E984D05017C675FECF5DA0B574440DFF28B352FA2F99EFF30D78F731B14185387E267F6405653C2FBD15E21A61493C63DBDF4E224BDB0537B6167CF8ACEDD80AB061DE804067445517C66E3D7B532ADABB6D9DFEC4952127D17381897489778D04176CCCEE5518B35CAA15859E865DBB763E011E779E46F82507CF817B7A683E2923874893A221FFA9644E54BB591EB30A9006E5DA68D4A86430580C96233ECFCC5A308392F54C7E503D492C07CA9363238C76C9936AA688E8162336C6E8EB942755C268C2C89CA14F198B329B3E539DF17276638F7362DC194E49BDC16D3916B12240579266D3D8C1C23EFB02914C9DDB6184DF0BC8D04567C0127A651FF308844AD3F6AF069FD3B1F8A45EBAFEA48F326442A09457C56C72A829E9230E7C08F0464084D045312A5F9AA31D35548526AA6AB8F1A63625F6453C3630B3556508479638647C607A517B2F9AEB10E640C506A29C8028DBD45C4F9A43614F15D1D8DFEE13A894797E84812EAD7E9B430A18AD4319BDFA09370CD570D6D57FFEA8B5273F5576DBD59FC4204D09945C1CEE8B6B643B391D50E61E9D8EB70FB61345CFD348193CD9A9AB20E2048F5A5FEAAD1A3262220D5A7E6B3860C2C62FC51D2AFF8A42BA9160812530B2DA42A241F89537DD3181319718F1A1959B0333B8CB926E86A40CAC0946C4839C0309B8C7B0E0E9A7CA2B7E262DCFAD11BA78F0D702013ADFEBC33EC545F0C746324118C020B899B0E7B0601A42B55B2334B24BFE7D0DEEE1DCE8B92D6BBAD4D77F114A72F2244584D682D12AAF9BAFDB3131D258BDE7364C9DB89A2BF13C536A555E574EB415C09A054E595B0F95002AB470BA20E95C481D5253BB3ECADB77C3A0B2F075358FA368061167FF7B40C1DE38712E654C9F63544138F87B2E5EBAFAF5052B2B7D7FDEC19019AC6A611220CB36BC8D7C71087EAFB4DB8FE085EDB8B51B679101CDA16A662CED0AE13A2401B0FB830A44BB411B96B42F2BB49FF04032E4BD411C92033241EF95D174D2A570555746994516378E4B26064C9C83D9361ABD4D4CB2FF5FFEB6732E51395F6E4D7DC9B95A2CAC4C213F5E4B9D97B95F94B82E5C44156E160FE0FFFDCF750A6B6AB0A78A6BC254AD222FED7E4F8F0E89849A0BD3BC9ACA749E2FAC0131F288990304ACC08F1CCBC6C82E76D11CB34E34D4109A4C3273B761EEC988BEFD76077C917DD2F3C9360B85F703E9FF000F844D8B09ED045B99EFB851F66E685999C07C0EF7FE6C13CCD20F6710BB6512EE3D72193F814C23919EEE76DB81E7A3E9DFC2B6F76625DFDEDBE69F9CEBA8EB11E3AB10EAD7FEB2EA23417B16A5740902EBD6282EE824C9547B2EB1853B73760E28D8DF21ED34E3EDC17269B5BB82F5C2675706F5300075284D7EE431F897F15A0CDF2FC1A49476001F9B4BE15F2EF02FBF9F7DA42804BDDDB090E4A46D109104C38D109914D2AD1098C8DE4DC0D8C4F97AB8167982AF475E8D2DAE5A1A7BDCA66227D6594D553A058BA27EDEC0B98CAC9D917289B72B3D346A0D36A76820252670EB1A7644F5EF676530992226A9B8645F34E9B8C89039E77A16B14F021F8007EB7B2B71C00A5FA33B3ED8CF2E3BD8E49DCA26EDAC3C39460934ACC66A34C733D99E33A07147D74288FDC9B793E8E796E9651ECB5482C5EDB2A49ACBC5917EF0F940D6C8845938637DBD765DB3B690F659B1AC975C431935182A8BE9CF5238829E9438EBD65793E3591AAB06A5A769157748E2365FF7DDE6A9F0F446A566EF7C4472EFE3BED2BF1515730E21EAD877E1939F978403EB9510F70BAC2D33049918A22511779FD65C00143CC1121FB4C93D08071EF460E78AF16F64445F85B86096CBE8F2C354CCC25887B068FAB2C0A0ED8293D4EF71C1EE3279979D5F9640C64C9405C66C8F33DF198AE88E93325CC1E44A837CAFFD247B297EDE93AC90F50B7AEE0F634730BB88E7A1954E088991D12BF74CD793176FA9557996B655774900197EEA7FED165E0ED6A1FB3FC28BB654473914589FE68A7B5D80F33BAFD076400C93DCE6302080E831C22BBC432AD3F9BDD099ED9896C237C0C5B76597BCB2352FCD2E874E22E22CC1385A70BA8A892D94339E3084493A8D04E4B332789748CC3A42D8187D87F6A13884E59EA21C571A9A53E118E4875D1B492A308A99535DAC969E74F8148D275D4896A245991912D2BF174B79F8A4522725493A388ED762A4C46EFD91C864FAE02CA27CD6C20B0C851C8C5B28B19530C56B78F09DDBD4C28FA694F46DC1803A7390107A191A783D11DAD3951762F7F49B74D603089BB9C99A40F2129D0B72AE937F623DB08B0DCF2C41F234C8F4602113E02023E2B6DC2ECC142F1BF0B9478AB0662863143E450A7A4BA4E664A540736A6475515F6A61F4F918B8F506771EA2D6D27C5C50EE62B2F5C4DAC5F6C7F83AB7C0A16C8BD0AAF37E97A93E221A360E15392243BF4C9E8E75952E83ECFAEF39FC5257D0C0177D3CBDE785C871F379EEFD6FDBE045EB60B20B2D364F9D2295BCB347BF1B47AA991BE46A12250397DF521F80E056B1F8325D7E1DCCE5848BF6FDF12F419AD6CE7A50A642106695F087ADA67175EFE203829319AF6F8BF9887DDE0F9C7FF03B2FD5DB62BBF0000 , N'6.2.0-61023')

