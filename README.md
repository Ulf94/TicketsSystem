# Ticket System Application

Application created to manage company's requests from a customer. A customer can log in (if the user is created) and create a ticket. Then, some programmer from a team can pick up a ticket and take care of the task. The ticket can be sent back to the creator to close it (if all requirements are fulfilled).

Like in all my projects:
- Backend is done with .Net. I used here CQRS model to manage structure of implementation,
- Fronted is based on Angular 13,
- Entity Framework Core - to communicate with database, store and read all tickets and to register, login and get all users,
- MediatR - helps to manage CQRS,
- JWT - authorization and authentication,
- CORS - combines front and backend

Below, I represent pieces of my code. Just to show an overview of the whole project. 

<h2>User management</h2>
Here, I used the same concept like the one used in repository "LiveChat", so there is no point to describe it again. You can refere to thie repository.
https://github.com/Ulf94/LiveChat_CompleteProject

<h2> Ticket management</h2>
Ticket can be created by users with assigned role "Admin", "Manager" or "User".

Here is POST request - adding a new ticket to a system. This request triggers MediatR and calls proper function
![image](https://user-images.githubusercontent.com/79094141/195041133-80523455-890e-46ce-8480-39a09c390a91.png)

![image](https://user-images.githubusercontent.com/79094141/195068256-1299fa18-b690-4590-adea-13ade381a9df.png)

More/less similarly work all other functions for tickets.

Tickets are stored in database with some foreign keys relations. Column "ResponsibleUserID" is by default value. 
![image](https://user-images.githubusercontent.com/79094141/195069359-731fda42-3abf-4664-a4a7-49cda1a34538.png)

