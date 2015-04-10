SalesforceNet
=============

Salesforce REST wrapper for .NET.

SalesforceNet is **lightweight and entity oriented** Salesforce wrapper. It automatically extracts fields from entities and builds required SOQL requests. Some methods can be called without using entities, for example: Create, Update and Delete. 

SalesforceNet is extremely flexible. ExtractRecursively attribute allows you to get **related data**, for example Contact Account. Or User Account. Ignore attributes allow to **filter what fields** should participate to Get or Create operations.

For first you should to define your Salesforce entity derived from BasedEntity:

```CSharp
public class Contact : BaseEntity
{
	#region Public Properties

	/// <summary>
    /// Gets or sets first name.
	/// </summary>	
	public string FirstName { get; set; }
	
	/// <summary>
	/// Gets or sets title.
	/// </summary>	
	public string Title { get; set; }

	/// <summary>
    /// Gets or sets last name.
    /// </summary>	
	public string LastName { get; set; }

	#endregion
}
```

After that you can get, create, update and delete contacts, for ex:

Getting
-------

```CSharp 
List<Contact> contacts = _salesforce.GetMany<Contact>(whereClause, orderClause); 
```

Creating
--------

```CSharp
var contact = new Contact
{
	FirstName = "Ivan", Title = "Programmer", LastName = "Drago"
};

_salesforce.Create<Contact>(contact);
```

Same for Update and Delete ...

OAuth
-----

Password, authorization code and token flows supported.