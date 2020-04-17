# avtogradshina
dotnet ef database update --context ApplicationContext 
dotnet ef database update --context AppIdentityDbContext



dotnet ef database drop --context ApplicationContext
dotnet ef database drop --context AppIdentityDbContext

dotnet ef migrations add Initial --context ApplicationContext
dotnet ef migrations add Initial --context AppIdentityDbContext

dotnet sql-cache create "Data Source=localhost\SQLEXPRESS; Database=avtogradshinaProducts;Persist Security Info=False; MultipleActiveResultSets=True; Trusted_Connection=True" dbo SessionData




