DESIGN NOTES:

Here, we have all of the classes that define the "core" of our application in one place.

Anything under the Core folder is completely decoupled from any presentation or persistence frameworks.

These classes provide the "abstraction" between the presentation/controllers layer and the persistence/EF layer.
	*According to the Dependency Inversion (DI) definition, we rely on abstractions - These abstractions are not just interfaces, 
		- Entities, 
		- Dtos, 
		- & ViewModels
	are also classified as "abstractions" in terms of the DI definition.   