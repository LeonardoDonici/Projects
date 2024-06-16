    const express = require('express');
    const expressLayouts = require('express-ejs-layouts');
    const bodyParser = require('body-parser')
    const app = express();
    const port = 6789;
    const cookieParser = require('cookie-parser');
    const sqlite3 = require('sqlite3').verbose();
    var session = require('express-session');
    // directorul 'views' va conține fișierele .ejs (html + js executat la server)
    app.set('view engine', 'ejs');
    // suport pentru layout-uri - implicit fișierul care reprezintă template-ul site-ulueste views/layout.ejs
    app.use(expressLayouts);
    // directorul 'public' va conține toate resursele accesibile direct de către client (e.g., fișiere css, javascript, imagini)
    app.use(express.static('public'))
    // corpul mesajului poate fi interpretat ca json; datele de la formular se găsesc în format json în req.body
    app.use(bodyParser.json());
    // utilizarea unui algoritm de deep parsing care suportă obiecte în obiecte
    app.use(bodyParser.urlencoded({ extended: true }));
    // la accesarea din browser adresei http://localhost:6789/ se va returna textul 'Hello World'
    // proprietățile obiectului Request - req - https://expressjs.com/en/api.html#req
    // proprietățile obiectului Response - res - https://expressjs.com/en/api.html#res
   // app.get('/', (req, res) => res.send('Hello World'));

var listaIntrebari;
//citire din json
app.use(cookieParser());
app.set('view engine', 'ejs');
app.use(expressLayouts);
app.use(express.static('public'))
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

app.use(session({
    secret:'secret',
    resave:false,
    saveUninitialized:false,
    cookie:{
    maxAge:10000
    }}));

app.use((req, res, next) => {
    req.session.username = req.cookies.username;
    req.session.userType = req.cookies.userType;
    res.locals.username = req.session.username;
    next();
});

var incercari_gresite = 0;
var incercari_gresite_termen_lung = 0;

var block = {};
var res_gresit = 0;
var ok = 0;

app.get('/', (req, res) => {

    res.clearCookie('mesajEroare');
    console.log(req.cookies['user']);
    var produse = null;
    if (req.cookies['produse'] != null) {
        produse = req.cookies['produse'];
    }
    var rol = null;
    if (req.cookies['rol'] != null) {
        rol = req.cookies['rol'];
    }
    if (req.cookies['blocat'] != null && (resTime - (new Date().getTime())<=0)) {
        res.clearCookie('blocat');
    }

    console.log("cookie: " + req.cookies['blocat']);
    console.log("res_gresit main: " + res_gresit);

    if (req.cookies['user']) {
        let db = new sqlite3.Database('cumparaturi.db');

        // Verificăm dacă tabela 'produse' există
        db.get("SELECT name FROM sqlite_master WHERE type='table' AND name='produse'", (err, table) => {
            if (err) {
                return console.error(err.message);
            }

            if (table) {
                // Tabela 'produse' există, continuăm cu selectarea datelor
                db.all("SELECT * FROM produse", [], (err, rows) => {
                    if (err) {
                        throw err;
                    }

                    res.render('index', { produse: rows, user: req.cookies['user'], rol: req.cookies['rol'] });

                    db.close((err) => {
                        if (err) {
                            return console.error(err.message);
                        }
                        console.log('Conexiunea la baza de date a fost închisă.');
                    });
                });
            } else {
                // Tabela 'produse' nu există, renderizăm pagina fără produse
                res.render('index', { produse: [], user: req.cookies['user'], rol: req.cookies['rol'] });

                db.close((err) => {
                    if (err) {
                        return console.error(err.message);
                    }
                    console.log('Conexiunea la baza de date a fost închisă.');
                });
            }
        });
    } else {
        let db = new sqlite3.Database('cumparaturi.db');

        // Verificăm dacă tabela 'produse' există
        db.get("SELECT name FROM sqlite_master WHERE type='table' AND name='produse'", (err, table) => {
            if (err) {
                return console.error(err.message);
            }

            if (table) {
                // Tabela 'produse' există, continuăm cu selectarea datelor
                db.all("SELECT * FROM produse", [], (err, rows) => {
                    if (err) {
                        throw err;
                    }

                    res.render('index', { produse: rows, user: undefined, rol: undefined });

                    db.close((err) => {
                        if (err) {
                            return console.error(err.message);
                        }
                        console.log('Conexiunea la baza de date a fost închisă.');
                    });
                });
            } else {
                // Tabela 'produse' nu există, renderizăm pagina fără produse
                res.render('index', { produse: [], user: undefined, rol: undefined });

                db.close((err) => {
                    if (err) {
                        return console.error(err.message);
                    }
                    console.log('Conexiunea la baza de date a fost închisă.');
                });
            }
        });
    }
});


//citire din json
const fs = require('fs');
var listaIntrebari;
var utilizatori;
fs.readFile('intrebari.json', (err, data) => {
    if (err) throw err;
    listaIntrebari = JSON.parse(data);

});
fs.readFile('utilizatori.json', (err, data) => {
    if (err) throw err;
    utilizatori = JSON.parse(data);
    //console.log(utilizatori);

});


// la accesarea din browser adresei http://localhost:6789/chestionar se va apela funcția specificată
app.get('/chestionar', (req, res) => {
    const ip = req.ip;

    if (block[ip]) {
        // Verificați dacă utilizatorul este blocat
        const blockTime = block[ip];
        const currentTime = new Date().getTime();
        const remainingTime = blockTime - currentTime;
        if (remainingTime > 0) {
            // Utilizatorul este încă blocat
            return res.render("paginaRestrictionata", { remainingTime });
        } else {
            // Dacă a trecut perioada de blocare, eliminați utilizatorul din lista de utilizatori blocați
            delete block[ip];
            res_gresit = 0; 
        }
    }

    const u = req.cookies['user'];
    // în fișierul views/chestionar.ejs este accesibilă variabila 'intrebari' care conține vectorul de întrebări
    res.render('chestionar', { intrebari: listaIntrebari, utilizator: u });
});

app.post('/rezultat-chestionar', (req, res) => {
    console.log(req.body);
    var nrCorecte = 0;
    var i = 0;
    const u = req.session.user;

    for (i in req.body) {
        if (req.body[i] == listaIntrebari[parseInt(i.substring(1))].corect) {
            nrCorecte++;
        }
    }
    //res.send("Raspunsuri corecte: " + JSON.stringify(nrCorecte));
    res.render('rezultat-chestionar', { raspunsuri: nrCorecte, utilizator: u });

});

const blockedUsers = {};
var intervalS = new Date().getTime() + 60000;
var intervalL = new Date().getTime() + 60000 * 4;

app.get('/autentificare', (req, res) => {

    const ip = req.ip;

    if (blockedUsers[ip]) {
        // Verificați dacă utilizatorul este blocat
        const blockTime = blockedUsers[ip];
        const currentTime = new Date().getTime();
        const remainingTime = blockTime - currentTime;
        if (remainingTime > 0) {
            // Utilizatorul este încă blocat
            return res.render("paginaRestrictionata", { remainingTime });
        } else {
            // Dacă a trecut perioada de blocare, eliminați utilizatorul din lista de utilizatori blocați
            delete blockedUsers[ip];
            if(intervalS - (new Date().getTime()) <=0 ){
                intervalS = new Date().getTime() + 60000;
                incercari_gresite = 0;
            }
            if(intervalL - (new Date().getTime())<=0){
                intervalL = new Date().getTime() + 60000*5;
                incercari_gresite_termen_lung = 0;
            }
            
        }
    }
    res.render('autentificare', { mesajEroare: req.cookies.mesajEroares });

});

app.post('/verificare-autentificare', (req, res) => {

    //console.log(req.body);


    for (i in utilizatori) {
        if (req.body.utilizator == utilizatori[i].user && req.body.password == utilizatori[i].parola) {
            ok = 1;
            console.log("aici");
            res.cookie('rol', utilizatori[i].rol);
            req.session.nume = utilizatori[i].nume;
            req.session.prenume = utilizatori[i].prenume;
            req.session.varsta = utilizatori[i].varsta;
        }
    }

    if(intervalS - (new Date().getTime()) <=0 ){
        intervalS = new Date().getTime() + 60000;
        incercari_gresite = 0;
    }
    if(intervalL - (new Date().getTime())<=0){
        intervalL = new Date().getTime() + 60000*5;
        incercari_gresite_termen_lung = 0;
    }

    console.log(intervalS - (new Date().getTime()));
    console.log(intervalL - (new Date().getTime()));


    console.log(ok);
    if (ok == 1) {
        //mesaj=null;
        res.cookie('user', req.body.utilizator);
        res.clearCookie('mesajEroare');
        req.session.user = req.body.utilizator;
        incercari_gresite = 0;
        incercari_gresite_termen_lung = 0;
        //console.log(req.session);
        ok = 0;
        //utilizator = "user";
        res.redirect('/');
    } else {

        res.cookie('mesajEroare', "Utilizator sau parola gresita!");
        res.clearCookie('user');
        incercari_gresite = incercari_gresite + 1;
        incercari_gresite_termen_lung = incercari_gresite_termen_lung + 1;
       
        if (incercari_gresite >= 3 && !blockedUsers[req.ip]) {
            // Dacă utilizatorul a depășit numărul maxim de încercări greșite, blocați-l pentru 15 secunde
            const blockTime = new Date().getTime() + 15000; // Adăugați 15 secunde la timpul curent
            blockedUsers[req.ip] = blockTime;
            incercari_gresite = 0;
            return res.render("paginaRestrictionata", { remainingTime: 15000 });
          }

          if (incercari_gresite_termen_lung >= 6 && !blockedUsers[req.ip]) {
            // Dacă utilizatorul a depășit numărul maxim de încercări greșite, blocați-l pentru 15 secunde
            const blockTime = new Date().getTime() + 20000; // Adăugați 15 secunde la timpul curent
            blockedUsers[req.ip] = blockTime;
            incercari_gresite_termen_lung = 0;
            return res.render("paginaRestrictionata", { remainingTime: 20000 });
          }
          
        console.log("gresit:" + incercari_gresite);
        console.log("gresit termen lung: " + incercari_gresite_termen_lung );
        //console.log("time: "+ remainingTime);

        //mesaj = "Utilizator sau parola gresita"; 
        res.redirect('/autentificare');
    }
    //res.send("Raspunsuri corecte: " + JSON.stringify(nrCorecte));

});

var resTime;
app.use((req,res,next)=>{
    const resource = req.path;
    const ip = req.ip;
    console.log("res_gresit:" + res_gresit);
    if(!existaResursa(resource)){
        if(res_gresit == 3){
            resTime = new Date().getTime() + 15000; // Adăugați 15 secunde la timpul curent
            block[req.ip] = resTime;
            const currentTime = new Date().getTime();
            const remainingTime = resTime - currentTime;
            res.cookie('time',remainingTime);
            res.cookie('blocat',1);
            res_gresit = 0;
            return res.render("paginaRestrictionata", { remainingTime: 15000 });
        }else{
            res_gresit = res_gresit + 1;
            res.clearCookie('blocat');
        }
        res.redirect('/');
    }
    
    next();
})

function existaResursa(resource){
    console.log("resursa ceruta: " + resource);
    const resources = [
        '/',
        '/chestionar',
        '/rezultat-chestionar',
        '/autentificare',
        '/verificare-autentificare',
        '/logout',
        '/verificare-adaugare',
        '/favicon.ico',
        '/vizualizare-cos',
        '/adauga-bd',
        '/adaugare-cos',
        '/sterge-din-cos',
        '/creare-bd',
        '/inserare-bd',
    ];
    console.log("rezultat: " + resources.includes(resource));
    return resources.includes(resource);
}

app.get('/logout', (req, res) => {
    res.clearCookie('user');
    res.clearCookie('produse');
    req.session.destroy();
    res.redirect('/');
});


app.get('/creare-bd', (req, res) => {
    let db = new sqlite3.Database('cumparaturi.db', (err) => {
        if (err) {
            console.error(err.message);
            throw err;
        }
        console.log('Connected to the SQLite database.');
    });

    db.serialize(() => {
        db.run(`CREATE TABLE IF NOT EXISTS produse (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            denumire TEXT,
            pret INTEGER,
            stoc INTEGER
        )`, (err) => {
            if (err) {
                console.error(err.message);
                throw err;
            }
            console.log('Table created or already exists.');
        });
    });

    db.close((err) => {
        if (err) {
            console.error(err.message);
        }
        console.log('Closed the database connection.');
    });

    res.redirect('/');
});

// Insert products into the database
app.get('/inserare-bd', (req, res) => {
    let db = new sqlite3.Database('cumparaturi.db', (err) => {
        if (err) {
            console.error(err.message);
            throw err;
        }
        console.log('Connected to the SQLite database.');
    });

    var produse = [
        ["Ciocan", 25, 10],
        ["Surubelnita", 15, 25],
        ["Bormasina", 150, 5],
        ["Dalta", 30, 20],
        ["Ferestrau", 50, 15],
        ["Chei fixe", 40, 30],
        ["Cleste", 20, 20]
    ];

    db.serialize(() => {
        var SQLinstr = `INSERT INTO produse (denumire, pret, stoc) 
                        SELECT ?, ?, ? 
                        WHERE NOT EXISTS (SELECT 1 FROM produse WHERE denumire = ?)`;

        produse.forEach(pr => {
            db.run(SQLinstr, [pr[0], pr[1], pr[2], pr[0]], function(err) {
                if (err) {
                    if (err.code === 'SQLITE_CONSTRAINT') {
                        console.log("Inregistrarea deja exista!");
                    } else {
                        console.log("Eroare la inserarea in tabela " + err.code);
                    }
                } else {
                    console.log("S-a inregistrat cu succes!!");
                }
            });
        });
    });

    db.close((err) => {
        if (err) {
            console.error(err.message);
        }
        console.log('Closed the database connection.');
    });

    res.redirect('/');
});

app.get('/adauga-bd', (req, res) => {
    res.render("adaugareBD");
});

app.get('/afisare-bd', (req, res) => {
    const sql = "SELECT * FROM produse";
    db.all(sql, [], (err, rows) => {
        if (err) {
            console.log("Eroare la extragere date " + err.message);
            return;
        }
        console.log("Date extrase cu succes");
        res.cookie('produse', rows);
        res.redirect('/');
    });
});

app.get('/adaugare-cos', (req, res) => {
    const produsId = req.query.id;
    if (!req.session.cos) {
        req.session.cos = [];
    }

    let db = new sqlite3.Database('cumparaturi.db');
    db.get('SELECT stoc FROM produse WHERE id = ?', [produsId], (err, row) => {
        if (err) {
            console.error(err.message);
            return res.status(500).send("Eroare la verificarea stocului.");
        }

        if (!row) {
            return res.status(404).send("Produsul nu a fost găsit.");
        }

        if (row.stoc <= 0) {
            return res.status(400).send("Produsul nu este în stoc.");
        }

        // Dacă produsul este în stoc, continuăm cu adăugarea în coș și scăderea stocului
        req.session.cos.push(produsId);

        db.run('UPDATE produse SET stoc = stoc - 1 WHERE id = ?', [produsId], function(err) {
            if (err) {
                console.error(err.message);
                return res.status(500).send("Eroare la actualizarea stocului.");
            }
            console.log(`Stocul pentru produsul cu ID-ul ${produsId} a fost actualizat.`);
            res.redirect('/');
        });
    });
});


app.get('/vizualizare-cos', (req, res) => {
    if (!req.session.cos || req.session.cos.length === 0) {
        return res.render('vizualizare-cos', { cos: [] });
    }
    
    let db = new sqlite3.Database('cumparaturi.db');
    const placeholders = req.session.cos.map(() => '?').join(',');
    const query = `SELECT * FROM produse WHERE id IN (${placeholders})`;
    
    db.all(query, req.session.cos, (err, rows) => {
        if (err) {
            throw err;
        }
        
        // Calculăm numărul de produse din coș
        let produseCos = {};
        req.session.cos.forEach(id => {
            produseCos[id] = (produseCos[id] || 0) + 1;
        });
        
        // Adăugăm numărul de produse în obiectele din rows
        rows.forEach(row => {
            row.cantitate = produseCos[row.id];
        });
        
        res.render('vizualizare-cos', { cos: rows });
    });
    
    db.close((err) => {
        if (err) {
            return console.error(err.message);
        }
        console.log('Conexiunea la baza de date a fost închisă.');
    });
});


app.post('/verificare-adaugare', (req, res) => {
    var denumire = req.body.denumire;
    var pret = req.body.pret;
    var stoc = req.body.stoc;

    let db = new sqlite3.Database('cumparaturi.db', (err) => {
        if (err) {
            console.error(err.message);
            return res.status(500).send("Database connection error");
        }
    });

    const SQLinstr = `INSERT INTO produse (denumire, pret, stoc) 
                      SELECT ?, ?, ? 
                      WHERE NOT EXISTS (SELECT 1 FROM produse WHERE denumire = ?)`;

    db.run(SQLinstr, [denumire, pret, stoc, denumire], function(err) {
        if (err) {
            if (err.message.includes('UNIQUE constraint failed')) {
                console.log("Inregistrarea deja exista!");
                return res.status(400).send("Inregistrarea deja exista!");
            } else {
                console.log("Eroare la inserarea in tabela " + err.message);
                return res.status(500).send("Eroare la inserarea in tabela");
            }
        } else {
            console.log("S-a inregistrat cu succes!!");
            res.redirect('/');
        }
    });

    db.close((err) => {
        if (err) {
            console.error(err.message);
        }
        console.log('Conexiunea la baza de date a fost închisă.');
    });
});

app.get('/sterge-din-cos', (req, res) => {
    const produsId = req.query.id;
    if (!req.session.cos) {
        return res.redirect('/vizualizare-cos');
    }
    
    // Eliminăm produsul din coș
    const index = req.session.cos.indexOf(produsId);
    if (index > -1) {
        req.session.cos.splice(index, 1);
    }

    let db = new sqlite3.Database('cumparaturi.db');
    db.run('UPDATE produse SET stoc = stoc + 1 WHERE id = ?', [produsId], function(err) {
        if (err) {
            console.error(err.message);
            return res.status(500).send("Eroare la actualizarea stocului.");
        }
        console.log(`Stocul pentru produsul cu ID-ul ${produsId} a fost actualizat.`);
        res.redirect('/vizualizare-cos');
    });

    db.close((err) => {
        if (err) {
            console.error(err.message);
        }
        console.log('Conexiunea la baza de date a fost închisă.');
    });
});




app.listen(port, () => console.log(`Serverul rulează la adresa http://localhost:6789/`));