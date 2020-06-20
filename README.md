# TaxServices
API for storing tax data for different municipalities
with MongoDatabase by Giedre Usaviciute

Details:
  - It has mongo database support, implemented with Repository pattern
  - User can:
   - GET all records
   - POST an entity to save into database
   - POST a JSOn file to save all entities into database
   - GET a Tax value for a specific municipality on a specific date
   
 TODO:
  - add support for all RESTful endpoints
  - improve validation
  - add unit tests for controller

  
  
  Task details for reference: 
  
  Full requirements for the application (choose the priority of tasks in the way that when the time ends
    up you would have working application, not necessarily with all functionality):
    - It has its own database where municipality taxes are stored 
    - Taxes should have ability to be scheduled (yearly, monthly ,weekly ,daily) for each municipality
    - Application should have ability to import municipalities data from file (choose one data format
you believe is suitable)
    - Application should have ability to insert new records for municipality taxes (one record at a
time)
    - User can ask for a specific municipality tax by entering municipality name and date
    - Errors needs to be handled i.e. internal errors should not to be exposed to the end user
    -You should ensure that application works correctly
 
Application has no visible user interface, requests are given directly to application as a service
(producer service). Also, there should be a consumer service created to demonstrate how the
producer service can be used.
