CREATE TABLE [dbo].[BusinessItems] (
    [Id] [int] NOT NULL IDENTITY,
    [StoreId] [int] NOT NULL,
    [ItemName] [nvarchar](10) NOT NULL,
    [SpendTime] [nvarchar](10) NOT NULL,
    [Price] [nvarchar](10) NOT NULL,
    [Describe] [nvarchar](max),
    [Remark] [nvarchar](max),
    [PicturePath] [nvarchar](max),
    CONSTRAINT [PK_dbo.BusinessItems] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_StoreId] ON [dbo].[BusinessItems]([StoreId])
ALTER TABLE [dbo].[BusinessItems] ADD CONSTRAINT [FK_dbo.BusinessItems_dbo.StoreDetails_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[StoreDetails] ([Id]) ON DELETE CASCADE
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202212031650202_AddBusinessItemsTable', N'BonnieYork.Migrations.Configuration',  0x1F8B0800000000000400ED5C5B6F1BBB117E2FD0FFB0D0535BF858B68300AD219F03478E7BDC26B11139A7ED5340ED52F2C27BD1D9A55C1B457F591FFA93FA17CABD71795F726F9212C32FF292FC861C0E67869799FFFDE7BFB39F9EC3C0798249EAC7D1C5E4F4F864E2C0C88D3D3F5A5F4CB668F5C31F273FFDF8DBDFCCDE7BE1B3F34B55EF4D560FB78CD28BC903429BF3E934751F6008D2E3D07793388D57E8D88DC329F0E2E9D9C9C99FA6A7A75388212618CB71669FB711F24398FF83FF9DC7910B37680B828FB10783B4FC8E4B1639AAF3098430DD00175E4CDEC551E4C37FC4C9E3715179E25C063EC01D59C06035714014C50820DCCDF32F295CA0248ED68B0DFE0082FB970DC4F556204861D9FDF3BABAE9484ECEB2914CEB861594BB4D511C5A029EBE295933E59BB762F084B00E33EF3D66327AC9469D3310F36E9BFA114CD39B681527614985A77B3E0F92AC8D84D5C7128023A7AE764424040B52F677E4CCB701DA26F022825B9480E0C8B9DB2E03DFFD2B7CB98F1F6174116D8380EE34EE362E633EE04F7749BC81097AF90C57E5506EBC893365DB4DF986A419D5A618D94D84DE9C4D9C4F9838580690C804C585058A13F86718C10420E8DD01846012651830E7AA409DA3758FC51B5381C913082AAA5818F1B29A381FC1F30718ADD1C3C5E42D5E47D7FE33F4AA0F6547BE443E5E84B80D4AB6B089D6DF207CF4C0CB028104657447A2F73EF246A4F62E81E0710C7A3FC7813F26374B7A2371B3A4361A378BE55E52D5103B6B45EC1378F2D7F97AE5C8E6ABF70A22E0E3D5F71906799DF4C1DF1466E2982AFF2AD58AD7491C7E8E03164A56F5EB3D48D610E1A1C566F517F13671B9A1CCA6B5AED66A70665C369A9B6AF8AAB1E5B4220FF333791168EA9B49A6D812E1D27563EC0F6916C7E989D9EA10B8D3B036419AFE334EBCF129E75395FDECAE7E2C29CF73491899E8958F05CB7775533C0CE14BCF4BB06C8ECF6518049B8738829FB6E112263A017B3B8864F749BB4994C16A75EFA34027CBEDAC1B2F453075137F53D8272529FCB3075AEFB05E8509D6A10F8393BAC6DBBA651C3F7EF0A3C7C189DD442902EB0484A350C344E028847E86C05B3CC43A05D39290D2C3927A4DC37A5A95E764EA69559E99F590100C53E960981A5F1967AC1E86B292E02AAA6BCA9C445DD72BE745DAEBAA50D56159B9D0576925DB6EE6CA52EB96937255671555246EB8BC5E27F79B939056472759D357175C4A2B6FDE4CB041C5630637B896A7C338B51BD8B8871F86F21D762EC7A75AF8234B1DE17EACDB6718826478237A873D74BCEA86717A5A9F54743439BCCD6C364EAD14636D7F6C7462D5EA551DCA6995EC1967A3DC5A425BB917BC5C6A7D9096476594AF617754461ABE0AA694562F76FABB3CF6C292D5B09A0D8F0A2C29FF255E8E7246617EF0D30B392C7949EC6DDD51CE445E0F2A0EF2A0225F74E4024AB5E92C2B7C65AC06B7EB94D4916F3B6515EDB7CA4D3758ADB7CAE2398A7E4BDDDEFC12B65BDBDFB2E5AB01561B12CB6BA66296C14B7ABB5A8DBDFC9AE4B8EDEA934AB26E99B612E5792EBC3069E34CB26D5FC5594AEB3B74052BB9D8CD25A8B19766AA0D2C3BF0CE4FD0C395F641483FAF5D867137549AE3324D63D7CF978468C5A5B71F6CEFDF479E637915520C8BBB68C103C49AC4DF60DD817B7931F983C027734AC4C1A92949AF71588A27C7C7A70251AC906092690410CCB10DC02ACE8F90A8BDFCC8F53720B0EB1F0763A806B3A92404F9922B981DE8E2FEDACD89494F148F46C4CE913E70DABB8995B329258B7A11551F30AA44C6E0B451222CC59589B9601A5CA31909FF293F23B3DBE80A061041E7D22DDEE9CE41EA024FD42678997B3D8870E3484610DEC63933E90339F7D989A04ACF1955D2A33F74AC05A73EB83650600DF0767229CA7E2BE1D20D7404B9D231C2883CF5EA6E2752A5DC7FA80D66D366849E7E7A136C63949B8E1B381A7211EBD30AEB3B348AF9D5F3DD4C8195FBE6DD899AE4C8462B05BAF31B2321689433DD638A03B1B00DE3184B3AD573B56FD6B5D8BFE036B89378D75EB982E470E26A9995C1672439EDF892C2F2C0232D3747BC7865D80B88742FC5EA0D946E5321082E8BCCC8A480C8943620715EAABA774579035AED5408407551E3D8A8152D191B556A82448C901C8A143760F14761021A5F81C3A3C4513A8DF22782542BC35785FC0AB2DF5993917362262C4EFBAD34056D22F5539669060CD5BC19115969B8FFB3DC01CAC6285B3B965B3EB35969C132F92306915BCD9B10F36D083518955A30DF740CC619F501BD6C599A38D376EE343330B5AAB2F39F79D07EB925BB965330ABC91DB47208CD4665E5017616ABEAAC94F81AA46C362DA2BBCB0FB3A9220C7CF6116C367EB4A6C2C2CB2FCEA288099FFFB0B08F960E0B8CA99B4A82A6496F09253C78B0865C29268D7B7AED2729BA02082C4176703CF742A19ACCB35258DA8AA2DA791267B332C255DBEC37EFD26962B825EE6989748D071D665E6E3EF90D864B4071B2007E10804472B7348F836D18A97D6F756B36B49AC6614BCC11C5006A1A552CB54626C1BD125C52668D4A05F14A70A952736431F89946164BAD91A59CE0CBAC51159C104BCD91B9C0651A962B123167536EF9081B3A61C90ADB62561B18E90ABD11B1D1111A2403DDA06D3D8C4EA08F111914CDF1A21A4D713D43031BDEE0A869903B6F1A957CB490537285CD8828F96A8E4405E5D250D46773AC22CC9686994BDE01E810EA98591AA5FE6AC1E92A0896E174F5D1624CFCAD39333CBED06206559877EDF0E888547622EBEF16F340479D3253411758AC2D2AB2945950D4777334F64D268DC796D86812E6E125AB4C982273CCFA79250D577FB5B077E4410363E7C8D7BDB145ECA6BF278F558665E3ABCADB0F6391C869AEA04B2D2D1B096763FA42BE5AF4A88E4F63FA547FB6D05945C419A3AD8A4FB69A6509656A656985540588D138D5378B31D1F15FCCC8E882BD5961E4C8A8DBE252C118AC2B75D3619D3CC972604AF6668AF42760760EB912C9C821D7B4DE6FF5B78F6E3209E211BC2B5B955C87E5D050F5D7DD3BA76C840DBBE6E8925797EDE05D36E674BD0775A58032D557CAE64329ACF26988B0A85BE090300B018C94ECCDB47337B6DD265E0F6630F54D00C34CFEFE5919363E8051E64CC9EE2D44FD969FD9CE93AF07AB29859B2BBE0AA14E6EB0B89BAA59796BD49CD558B8462AAA4C1CCCA227DFCBAE90162F29DEF41D67158E17BF06F3C087997056153E82C85FC11415914293B393D3332E33F2FE64299EA6A917486EDD1A5215B3533742E4939F31B831B6C932F6459619387A0289FB00122102A6C6EE9208B85F782E536DBFE04262DA9EE055597DFB851F8635AA9CBD3DC14B53F44AB1CF1AB05BA5B1FD3656B5983D3627233C9EC4F5E0F3C5E45F79B373E7E6EF5FEB9647CE6D8235F9B973E2FCDB7612B569684DBB2205E9D22B2EB0532A54798065C7B8CDDE8085C4B4066BCC3AEF6C5F987C5AD9BE70B9ACB1BDB1401E752A9FBBB77DE47C35806E97E2B59576944CA098D1B542FE5D089E7F6FAD0484ACAD9DE064094F3A014A939A7442E413977402E3A3852DC05A6690FC366C1F970ECAD4DA94CD54F6A555B2478521E89ECBB12F602655635FA07C26C64EAB80CDB6D8094A925171883525BF873AD8E5244BFBD7CE06B7CA95F76D3071873AE9009D5E2E2D9D817BD32AEB5C4F6E938D23698F2ECB29F7EA46ED991BA5BE213A608DC5E4F432D75879B32EBB745966B021264D77B773B0D37670DA5E96796AA42DBE204CAD9245F575AADABF9AEA2FFF93342EB487144CD260D5913396980520992C7EA765FAA6EF2047538BE9ED358D8322A8B74B6AA8AEE9C3464FB3B477591F7ACAA9D44702A5DDA91FCD83D79DEB9C03CD86249D47BBAC44F2D8ED0EC994BAE691193BA5D13799BF685F6C500B293D4CFB632BC0BBCF392406D0F393D85B36A1E24DD5C5C45BC6580A0AE75E52B139BF8F45DE21194DAA42332DCBCC44DA31F697BC4846A62CF5A109038D931BC919482A18D2324D7FA4A456D66826679D21494692AD2312DD7D1A25CD32344D6CA4B61583E5B2193C31528B61E8364BA65994F62FE1917D76A37124608C6C46D24158A4E3E1945563EAA3FD4B53D46D11B460623F0988C4E7DAD8DDD9E2D66171CC87DDADD45FD710338C1941977174489D4CF3550E17D7A3AA0A7722F9110FCDC35ED06582FC1570112E76F18ACF33D6FF02822DAEF23E5C42EF26BADDA2CD16E121C3701930F290F96D3AFA799625B6CFB3DBFC0552DAC7107037B18422781BBDDBFA8147FA7D2DB99C5640640E61793E9FCD25CACEE9D72F04E9531C190295EC237EEC3D0C3701064B6FA30578826DFAF625851FE01AB82FD5AB7B3548F344B06C9F5DF9F99D5E5A62D4EDF1BF5886BDF0F9C7FF031C29E146B1890000 , N'6.2.0-61023')

