insert into categorii(nume_categorie) values('Povesti');
insert into categorii(nume_categorie) values('Poezii');
insert into categorii(nume_categorie) values('Sci-fi');
insert into categorii(nume_categorie) values('Disney');
insert into categorii(nume_categorie) values('Scolaresti');

--SELECT * from categorii;


insert into carti(titlu,autor,editura,an_publicare,nr_exemp_impr,limba,categorii_id_categorie) values ('Amintiri din copilarie','Ion Creanga','Libri',to_date('5.09.1999','DD.MM.YYYY'),2,'',1);
insert into carti(titlu,autor,editura,an_publicare,nr_exemp_impr,limba,categorii_id_categorie) values ('Luceafarul','Eminescu','Coral',to_date('5.10.2001','DD.MM.YYYY'),3,'',2);
insert into carti(titlu,autor,editura,an_publicare,nr_exemp_impr,limba,categorii_id_categorie) values ('Flori de mucegai','Eminescu','Coral',to_date('10.05.1995','DD.MM.YYYY'),1,'',2);
insert into carti(titlu,autor,editura,an_publicare,nr_exemp_impr,limba,categorii_id_categorie) values ('Ultima noapte de dragoste','Camil Petrescu','Libri',to_date('15.12.1996','DD.MM.YYYY'),5,'Romana',5);
insert into carti(titlu,autor,editura,an_publicare,nr_exemp_impr,limba,categorii_id_categorie) values ('Ion','Liviu Rebreanu','Aloha',to_date('11.02.1994','DD.MM.YYYY'),2,'',3);

--SELECT * from carti;


insert into autori(id_autor,carti_id_carte,nume_autor,prenume_autor,biografie) values (1,1,'Creanga','Ion','');
insert into autori(id_autor,carti_id_carte,nume_autor,prenume_autor,biografie) values (2,2,'Eminescu','Mihai','');
insert into autori(id_autor,carti_id_carte,nume_autor,prenume_autor,biografie) values (2,3,'Eminescu','Mihai','AUTOR INTELIGENT');
insert into autori(id_autor,carti_id_carte,nume_autor,prenume_autor,biografie) values (3,5,'Rebreanu','Liviu','');
insert into autori(id_autor,carti_id_carte,nume_autor,prenume_autor,biografie) values (4,4,'Petrescu','Camil','AUTOR PUTERNIC');
insert into autori(id_autor,carti_id_carte,nume_autor,prenume_autor,biografie) values (5,1,'Petrescu','Camil','');

--SELECT * from autori;



insert into membri(nume,prenume,nr_telefon,data_inregistrarii,data_nasterii,gen) VALUES ('Popescu','Ion','0721921688',to_date('13.12.2023','DD.MM.YYYY'),to_date('13.12.2002','DD.MM.YYYY'),'');
insert into membri(nume,prenume,nr_telefon,data_inregistrarii,data_nasterii,gen) VALUES ('Alex','Vlad','0721921622',to_date('01.12.2023','DD.MM.YYYY'),to_date('05.11.2001','DD.MM.YYYY'),'');
insert into membri(nume,prenume,nr_telefon,data_inregistrarii,data_nasterii,gen) VALUES ('Popa','Gheorghe','0721921712',to_date('01.10.2023','DD.MM.YYYY'),to_date('11.12.2000','DD.MM.YYYY'),'');
insert into membri(nume,prenume,nr_telefon,data_inregistrarii,data_nasterii,gen) VALUES ('Alecu','Marius','0721927722',to_date('01.06.2023','DD.MM.YYYY'),to_date('06.08.1999','DD.MM.YYYY'),'');
insert into membri(nume,prenume,nr_telefon,data_inregistrarii,data_nasterii,gen) VALUES ('Marcu','Caron','0721941822',to_date('11.08.2023','DD.MM.YYYY'),to_date('02.06.1997','DD.MM.YYYY'),'M');

--SELECT * FROM membri;

insert into carduri_de_biblioteca(data_expirarii,membri_id_membru) values (to_date('16.12.2025','DD.MM.YYYY'),1);
insert into carduri_de_biblioteca(data_expirarii,membri_id_membru) values (to_date('11.10.2025','DD.MM.YYYY'),2);
insert into carduri_de_biblioteca(data_expirarii,membri_id_membru) values (to_date('09.08.2025','DD.MM.YYYY'),3);
insert into carduri_de_biblioteca(data_expirarii,membri_id_membru) values (to_date('12.11.2025','DD.MM.YYYY'),4);
insert into carduri_de_biblioteca(data_expirarii,membri_id_membru) values (to_date('11.09.2026','DD.MM.YYYY'),5);

--select * from carduri_de_biblioteca;

insert into imprumuturi(id_carte,data_imprumutului,data_scadentei,data_returnarii,membri_id_membru) values (1,to_date('11.12.2023','DD.MM.YYYY'),to_date('16.02.2024','DD.MM.YYYY'),'',1);
insert into imprumuturi(id_carte,data_imprumutului,data_scadentei,data_returnarii,membri_id_membru) values (3,to_date('14.12.2023','DD.MM.YYYY'),to_date('15.04.2024','DD.MM.YYYY'),'',2);
insert into imprumuturi(id_carte,data_imprumutului,data_scadentei,data_returnarii,membri_id_membru) values (2,to_date('11.12.2023','DD.MM.YYYY'),to_date('06.07.2024','DD.MM.YYYY'),'',3);
insert into imprumuturi(id_carte,data_imprumutului,data_scadentei,data_returnarii,membri_id_membru) values (5,to_date('02.12.2023','DD.MM.YYYY'),to_date('15.12.2024','DD.MM.YYYY'),'',4);
insert into imprumuturi(id_carte,data_imprumutului,data_scadentei,data_returnarii,membri_id_membru) values (4,to_date('02.12.2023','DD.MM.YYYY'),to_date('15.12.2024','DD.MM.YYYY'),'',3);

--SELECT * from imprumuturi;
