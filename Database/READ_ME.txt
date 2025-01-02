[SQL Server]
1. If using Windows Authentication, no changes are needed. Otherwise, follow these steps: 
	1.1. 7_ChineseProject -> Model -> App.config
	then update the connectionString with Server=yourServerName;Database=Chinese;User Id=yourUserName;Password=yourPassword;
	1.2. 7_ChineseProject -> Project -> Web.config
	and make the same update as in step 1.1.

2. Ctrl + O (or File -> Open -> File) -> 7_ChineseProject -> Database -> choose .sql file

3. F5 (or Query -> Execute)