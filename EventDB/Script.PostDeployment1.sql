Insert into themes(theme) values('Fantastique');
Insert into themes(theme) values('Horreur');
Insert into themes(theme) values('Médiéval');
Insert into themes(theme) values('Mangas');
Insert into themes(theme) values('Cinema');
Insert into themes(theme) values('Supers-Héros');
Insert into themes(theme) values('Livres');

Insert into Roles(Role) values('Admin');
Insert into Roles(role) values('Utilisateur');
Insert into Roles(role) values('Participant');

Insert into Events(Title, BeginDate, EndDate,Address,Status) values('Made In Asia','2024-03-04','2024-03-05','Tour & Taxis','1')
Insert into Events(Title, BeginDate, EndDate,Address,Status) values('Trolls et légendes','2021-03-04','2021-03-05','Tour & Taxis','1')
Insert into Events(Title, BeginDate, EndDate,Address,Status) values('Comicon','2020-03-04','2020-03-05','Tour & Taxis','1')
Insert into Events(Title, BeginDate, EndDate,Address,Status) values('GamesWeek','2022-03-04','2022-03-06','Tour & Taxis','1')

Insert into Persons(LastName, FirstName, Email, password, roleId) values('dark','vador','darkVador@etoile.com','test1234',2);
Insert into Persons(LastName, FirstName, Email, password, roleId) values('Luke','sky','lukeSky@xwing.com','test1235',1);
Insert into Persons(LastName, FirstName, Email, password, roleId) values('leia','sky','leiaSky@andor.com','test1236',3);

Insert into Exposants(date, gsm, comments, status, personId, eventId,Name) values('2022-03-04','497889977','Je n aime pas les aiguilles',1,3,2,'La cantina');
Insert into Exposants(date, gsm, comments, status, personId, eventId,Name) values('2022-03-05','497889977','Je n aime pas les aiguilles',1,3,2,'Buffet de l étoile de la mort');

Insert into Comments(title, date, message, personId, eventId) values('Trop génial','2022-03-10','ça m a fait plaisir de rencontrer une princesse.',1,2);

Insert into EventTheme(eventId, themeId) values(2,2);
Insert into eventTheme(eventId, themeId) values(2,2);
Insert into EventTheme(eventId, themeId) values(2,3);
Insert into EventTheme(eventId, themeId) values(3,4);