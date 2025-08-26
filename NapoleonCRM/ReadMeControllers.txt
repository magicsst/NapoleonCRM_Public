������ ���������
1.	��� NapoleonCRM.Shared.Models
	1.1 ��� ��� ��������, ���� ���� ��� Add-> Class
		2.1.1 ��������� �� ����������� ��� class Address.cs ��� ���� ��� ����������� �������
	1.2 ��� ������ �� ��������� ��������, ���� 2� ���� ��� <���>.cs ��� ���� �������
		�� ��� Address.cs
2.	��� NapoleonCRM.Controllers
	2.1	��� ��� ��������, ���� ���� ��� Add-> Controller-> API-> API Controller (Empty)
		1.1.1 ��������� �� ����������� ��� class AddressController.cs ��� ���� ��� ����������� �������
	2.2 ��� ������ �� ��������� ��������, ���� 2� ���� ��� <���>Controller.cs ��� ���� �������
3.	��� NapoleonCRM.Data
	3.1 ��� ApplicationDbContext.cs, ����� ��� �������� ��� ���� �� ��������
		��. ��� ��� �������� Address, �� �������� public DbSet<Address> Addresses { get; set; }
	3.2 ��� OnModelCreating, ������ ��� ������� ������ ��� ���������
		��. ��� ��� �������� Address, �� ��������:
		modelBuilder.Entity<Address>()
			.HasOne(a => a.Customer)
			.WithMany(c => c.Addresses)
			.HasForeignKey(a => a.CustomerId);
4.	��� NapoleonCRM.Program.cs
	4.1 �������� �� ���������� 
		modelBuilder.EntitySet<Address>("Address");

5.	��� NapoleonCRM.Shared.Blazor
	5.1 ��� Layout.NavMenu.razor 
		�������� �� ���������� link ��� ��� �������� ���� ��� �� ��������
6.	��� NapoleonCRM.Shared.Blazor
	6.1 ��� Pages, ���� ���� ���� ��� Add-> Razor Component-> 
		Add<XXX>.razor �� AddAddress.razor
		List<XXX>.razor �� ListAddress.razor
		Update<XXX>.razor �� UpdateAddress.razor
7. ��� NapoleonCRM.Shared.Blazor
	7.1 ��� Services-> AppService.cs, ���� ��� ����� set ������ (copy / paste) ���� �� #region "Address Sample"

��� ���� ����� ��� ���������		
	��� ApplicationDbContextFactory.cs, �������� ��� �������� ��� OnConfiguring
		��. ��� ��� �������� Address, �� ��������:
		optionsBuilder.UseSqlServer(connectionString, options => options.MigrationsAssembly("NapoleonCRM.Data"));
	��� NapoleonCRM.Data.Migrations
		��� Package Manager Console, ����� add-migration <MigrationName>
		��� Package Manager Console, ����� update-database
		