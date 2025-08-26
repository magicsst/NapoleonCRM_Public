Βήματα ενεργειών
1.	Στο NapoleonCRM.Shared.Models
	1.1 Για νέα οντότητα, δεξί κλίκ και Add-> Class
		2.1.1 Αντιγράφω το περιεχόμενο της class Address.cs και κάνω τις απαραίτητες αλλαγές
	1.2 Για αλλαγή σε υπάρχουσα οντότητα, κάνω 2ο κλίκ στο <ΧΧΧ>.cs και κάνω αλλαγές
		πχ στο Address.cs
2.	Στο NapoleonCRM.Controllers
	2.1	Για νέα οντότητα, δεξί κλίκ και Add-> Controller-> API-> API Controller (Empty)
		1.1.1 Αντιγράφω το περιεχόμενο της class AddressController.cs και κάνω τις απαραίτητες αλλαγές
	2.2 Για αλλαγή σε υπάρχουσα οντότητα, κάνω 2ο κλίκ στο <ΧΧΧ>Controller.cs και κάνω αλλαγές
3.	Στο NapoleonCRM.Data
	3.1 Στο ApplicationDbContext.cs, γράφω την οντότητα που θέλω να προσθέσω
		Πχ. για την οντότητα Address, θα προσθέσω public DbSet<Address> Addresses { get; set; }
	3.2 Στο OnModelCreating, δηλώνω τις σχέσεις μεταξύ των οντοτήτων
		Πχ. για την οντότητα Address, θα προσθέσω:
		modelBuilder.Entity<Address>()
			.HasOne(a => a.Customer)
			.WithMany(c => c.Addresses)
			.HasForeignKey(a => a.CustomerId);
4.	Στο NapoleonCRM.Program.cs
	4.1 Προσθέτω το αντίστοιχο 
		modelBuilder.EntitySet<Address>("Address");

5.	Στο NapoleonCRM.Shared.Blazor
	5.1 Στο Layout.NavMenu.razor 
		προσθέτω το αντίστοιχο link για την οντότητα όπως και τα υπόλοιπα
6.	Στο NapoleonCRM.Shared.Blazor
	6.1 Στο Pages, κάνω δεξί κλίκ και Add-> Razor Component-> 
		Add<XXX>.razor πχ AddAddress.razor
		List<XXX>.razor πχ ListAddress.razor
		Update<XXX>.razor πχ UpdateAddress.razor
7. Στο NapoleonCRM.Shared.Blazor
	7.1 Στο Services-> AppService.cs, βαζω ένα εξτρα set κώδικα (copy / paste) όπως το #region "Address Sample"

Δεν κάνω ακόμα ότι ακολουθεί		
	Στο ApplicationDbContextFactory.cs, προσθέτω την οντότητα στο OnConfiguring
		Πχ. για την οντότητα Address, θα προσθέσω:
		optionsBuilder.UseSqlServer(connectionString, options => options.MigrationsAssembly("NapoleonCRM.Data"));
	Στο NapoleonCRM.Data.Migrations
		Στο Package Manager Console, γράφω add-migration <MigrationName>
		Στο Package Manager Console, γράφω update-database
		