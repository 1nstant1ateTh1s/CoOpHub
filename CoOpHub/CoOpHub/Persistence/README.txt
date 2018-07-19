DESIGN NOTES:

Anything under the Persistence folder is tightly coupled to an external framework, in this case, Entity Framework.

*Note about the "EntityConfigurations" folder --> these classes are part of the persistence because they are the detail of the model class abstractions.
		- For configuring relationships where there are multiple model classes involved, the convention being used here is to put the configuration in the "parent" config. class 
			(Ex. --> modelBuilder.Entity<Attendance>() 
						.HasRequired(a => a.Coop)
						.WithMany(g => g.Attendances)
						.WillCascadeOnDelete(false);

			... will go into the CoopConfiguration class.)

	**With this approach, all of the configurations that are specific to the persistence framework, or database, are in one place, & we have 1 consistent way to apply these configurations, 
		as opposed to some configurations with data annotations and others with Fluent API.