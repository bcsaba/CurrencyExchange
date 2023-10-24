# Introduction

This is an initial version of a C# application which provides the possibilities for the users to check currency excange rates based on MNB's open APIs.
At the current state it provides the below functionalities:

- User login - through the usual ASP.NET Core Identity management system.
- Conversion from HUF to EUR based on the current exchange rate queried directly from the MNB's open API endpoints.
- Display a list of the exchange rates of all the MNB handled currencies for the last banking date.
- Possibility for the user to save any of the above rates with a comment.
- View and edit all the saved exchange rate comments.

## Technologies

- .Net 7, C#
- EF Core v7.0
- PostgreSQL
- Angular v15.2
- Bootstrap 5.2.3

The live database should be generated through code-first migrations (`ExchangeRateDbContext` context). A separate test database for the tests can be generated through the `TestExchangeRateDbContext` DB context. This is needed to successfully run the tests on the backend.

# Architecture

The solution is divided into the folloing projects:

- `CurrencyExchange`
This contains the API endpoints of the backend project.
It also contains the client side Angular appliation in the `ClientApp` folder.

- `CurrencyExchange.Application`
This is the business logic layer of the soution.

- `CurrencyExchange.Application.Intergration.Tests`
This contains integration tests for the `CurrencyExchange.Appliation` business logic project. These tests are connected to the MNB services to verify that they are working and providing the expected data for our solution.
- `CurrencyExchange.Application.Tests`
This project contains the unit tests for the `CurrencyExchange.Appliation` business logic project.
- `CurrencyExchange.Persistence`
This is the DB access layer. Contains the database contexts: one for the live applicaiton and another one for running the tests. There are the code-first model classes and the generated migrations for those two contexts.
- `CurrencyExchange.Tests`
This project contains unit tests for the `CurrencyExchange` web application.

## CQRS for data manipulations

For mediating all the data requests in the applicaiton I used the `MediatR` library. The data queries and commands can be found in separate folders and all the handler classes are again have their own folder too.





# TODOs

Following is a list of improvement possibilities for the current version of the code:

- Testing, validation
  * Increase back-end code coverage by writing tests for the missing parts. (They were not implemented everywhere because of the time constraint for this current version.)
  * Add additional validation for the comment field. There is only a client side limit set up in the inputs currently. A proper validation with error messages needed on the backend too.
  * Write tests also for the front-end part. Those were not implemented at all for the current version.
  
- CQRS fixes
  * At the very first requests a simple service was used instead of the CQRS approach. Those service based calls should be converted to proper CQRS implementation for consitency reasons.
  
- Caching, improved data access
  * Currently every single page load goes to the MNB's site for the exchange rate information. We can add caching options to the solution, so if the data was already queried for the current day, then we just load ot from the cache instead of going to the MNB site all the time. We need to figure out the proper cache lifetime for this, not to miss an update.
  * For the HUF -> EUR conversion, we can potantially try to use local data first if that is present in the saved data for the current day already. Only query the MNB data if necessary. (Again some consideration is needed here, when is it correct to use the local data.)
  * In general we can optimize the local data vs MNB data usage in the application. We have to keep in mind though that the currency list we use is not i[dated during weekends or bank holidays. We have to think carfully for these edge cases for the proper, correct caching implementations.

- UI imprevements:
  * Better navigation handling. Do not show or make inactive of the navigation links that require authentication. Currently the user can click anywhere, if she is not authenticated she'll be only redirected to the Login page. But everything is available for click. This can be changed to a stricter control.
  * There are no loading indication at the moment for the saved rate adminitration page for the individual commnet updates. Only the toast information when those operations are finished. We can add a line-by-line spinner implementation there.
  * At the moment the .NET scaffolding generated start page is used, which should go away for sure. We have to figure out what would be the best landing page for the initial load of the application. The two candidates I can see:
    - HUF => EUR conversion pages: This maybe does not even require a login, since there is no saved data used here. Just the openly available MNB information.
	- The MNB Exchange rates page: The initial load here also only uses public data. For the save of a comment we need to login the user. Which is kind of a break of a workflow a bit to navigate away for authetnication. Probably the previous one is a better option for a starting page.


