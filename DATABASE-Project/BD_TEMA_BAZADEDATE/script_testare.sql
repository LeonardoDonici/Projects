--Afisam toti autorii
SELECT * FROM autori;

--Afisam toate cardurile de biblioteca
SELECT * FROM carduri_de_biblioteca;

--Afisam toate cartile
SELECT * FROM carti;

--Afisam toate categoriile
SELECT * FROM categorii;

--Afisam toate imprumuturile
SELECT * FROM imprumuturi;

--Afisam toti membrii
SELECT * FROM membri;


-- UPDATE nr telefon
UPDATE Membri SET nr_telefon = '0723456780' WHERE id_membru = 1;

--UPDATE autori
UPDATE Autori SET nume_autor = 'NumeNou' WHERE id_autor = 1;

-- UPDATE LIMBA UNEI CARTI CARE ESTE NULL INITIAL
UPDATE Carti SET limba = 'ENG' WHERE id_carte = 2;

--UPDATE BIOGRAFIE AUTOR CARE E INITIAL NULL
UPDATE Autori SET biografie = 'BIOGRAFIEAUTOR' WHERE id_autor = 2;

--UPDATE GEN IN MEMBRI CARE E INITIAL NULL
UPDATE Membri SET gen = 'MASCULIN' WHERE id_membru = 2;

--UPDATE datadata_scadentei
UPDATE Imprumuturi SET data_scadentei = TO_DATE('2024-12-15', 'YYYY-MM-DD') WHERE id_imprumut = 1;

--UPDATE pentru prelungirea termenului de returnare pentru un împrumut:
UPDATE Imprumuturi SET data_scadentei = TO_DATE('2024-01-20', 'YYYY-MM-DD') WHERE id_imprumut = 3;


--DELETE pentru stergerea unui membru  a împrumuturilor sale si a cardului sau:
DELETE FROM Imprumuturi WHERE membri_id_membru IN (SELECT id_membru FROM Membri WHERE nume = 'Popescu');
DELETE FROM carduri_de_biblioteca WHERE membri_id_membru IN (SELECT id_membru FROM Membri WHERE nume = 'Popescu');
DELETE FROM Membri WHERE nume = 'Popescu';

--Membri si numãrul de împrumuturi efectuate:
SELECT id_membru, nume, prenume, (SELECT COUNT(*) FROM Imprumuturi WHERE Imprumuturi.membri_id_membru = Membri.id_membru) AS nr_imprumuturi FROM Membri;

--Cãrti împrumutate de membri cu vârsta între 18 si 50 de ani:
SELECT id_carte, titlu
FROM Carti WHERE id_carte IN (SELECT id_carte FROM Imprumuturi WHERE membri_id_membru IN (SELECT id_membru FROM Membri WHERE  EXTRACT(YEAR FROM SYSDATE) - EXTRACT(YEAR FROM data_nasterii) BETWEEN 18 AND 50));

--verificare constrangere nr telefone sa inceapa cu 07 
UPDATE Membri SET nr_telefon = '0123456780' WHERE id_membru = 2;

-- Exemplu pentru verificarea vârstei membrilor
INSERT INTO Membri ( nume, prenume, nr_telefon, data_inregistrarii, data_nasterii, gen)
VALUES ( 'Alberto', 'Relu', '0723456789', TO_DATE('2023-01-01', 'YYYY-MM-DD'), TO_DATE('2008-01-01', 'YYYY-MM-DD'), 'M');

-- Exemplu pentru verificarea datei de returnare
INSERT INTO Imprumuturi (id_imprumut, id_carte, data_imprumutului, data_scadentei, data_returnarii, membri_id_membru)
VALUES (1, 1, TO_DATE('2023-01-01', 'YYYY-MM-DD'), TO_DATE('2023-01-10', 'YYYY-MM-DD'), TO_DATE('2023-01-05', 'YYYY-MM-DD'), 1);

-- Exemplu pentru verificarea anului de publicare
INSERT INTO Carti ( titlu, autor, editura, an_publicare, nr_exemp_impr, limba, categorii_id_categorie) VALUES ( 'TitluCarte2', 'Autor1 Prenume1', 'Editura1',to_date ('2024-02-10', 'YYYY-MM-DD'), 5, 'Romana', 4);

-- Exemplu pentru verificarea datei de returnare
INSERT INTO Imprumuturi ( id_carte, data_imprumutului, data_scadentei, data_returnarii, membri_id_membru) VALUES ( 3, TO_DATE('2023-01-01', 'YYYY-MM-DD'), TO_DATE('2023-01-10', 'YYYY-MM-DD'), '', 1);


--Afisarea tuturor membrilor cu informatii despre carduri si împrumuturi:
SELECT m.id_membru, m.nume, m.prenume, m.nr_telefon, m.data_inregistrarii, m.data_nasterii, m.gen,
       cd.id_card, cd.data_expirarii,
       i.id_imprumut, i.id_carte, i.data_imprumutului, i.data_scadentei, i.data_returnarii
FROM Membri m
LEFT JOIN Carduri_de_biblioteca cd ON m.id_membru = cd.membri_id_membru
LEFT JOIN Imprumuturi i ON m.id_membru = i.membri_id_membru
ORDER BY m.id_membru, cd.id_card, i.id_imprumut;

--Informatii despre carti pe baza autorilor
SELECT c.id_carte, c.titlu, c.autor, c.editura, c.an_publicare,
       a.nume_autor, a.prenume_autor
FROM Carti c
JOIN Autori a ON c.id_carte = a.carti_id_carte;

--testare 2 membri sa imprumute aceeasi carte membrul cu id 5 imprumuta cartea cu id 4 care e deja imprumutata deci avem eroare
insert into imprumuturi(id_carte,data_imprumutului,data_scadentei,data_returnarii,membri_id_membru) values (4,to_date('02.12.2023','DD.MM.YYYY'),to_date('15.12.2024','DD.MM.YYYY'),'',5);
    
--VIZUALIZARE TABELE DUPA MODIFICARI
SELECT * FROM Autori ORDER BY id_autor;
SELECT * FROM Carti ORDER BY id_carte;
SELECT * FROM Categorii ORDER BY id_categorie;
SELECT * FROM Carduri_de_biblioteca ORDER BY id_card;
SELECT * FROM Imprumuturi ORDER BY id_imprumut;
SELECT * FROM Membri ORDER BY id_membru;




