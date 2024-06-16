//sectiunea 1
function Data() {
    const d = new Date();
    var luni = ["Ianuarie", "Februarie", "Martie", "Aprilie", "Mai", "Iunie", "Iulie", "August", "Septembrie", "Octombrie", "Noiembrie", "Decembrie"];
    var dataElement = document.getElementById("data");
    if(dataElement)
    {
        dataElement.innerHTML = d.getDate() + " " + luni[d.getMonth()] + " " + d.getFullYear();
    }
}

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        document.getElementById("locatie").innerHTML = "Geolocation is not supported by this browser.";
    }
}

function showPosition(position) {
    var latitude = position.coords.latitude;
    var longitude = position.coords.longitude;
    document.getElementById("locatie").innerHTML = "Latitude: " + position.coords.latitude + "<br>Longitude: " + position.coords.longitude;
}



function url() {
    var url = window.location.href;
    document.getElementById("url").innerHTML = url;
}

function SistemOperare() {
    var os = "Unknown OS";
    if (navigator.userAgent.indexOf("Win") != -1) os = "Windows";
    if (navigator.userAgent.indexOf("Mac") != -1) os = "MacOS";
    if (navigator.userAgent.indexOf("Linux") != -1) os = "Linux";
    document.getElementById("nume_versiune_so").innerHTML = "Sistem de operare: " + os;
}

function Browser() {
    var x = "Nume Browser: " + navigator.appCodeName + "  <br> Versiune: " + navigator.appVersion;
    document.getElementById("nume_versiune_browser").innerHTML = x;

}
function functieSectiune1(){
    const button = document.getElementById("button1");
    button.onclick = function(){
        window.location.href = "#sectiunea1";
    }
    
}
function functieSectiune2(){
    const button = document.getElementById("button2");
    button.onclick = function(){
        window.location.href = "#sectiunea2";
    }
    
}function functieSectiune3(){
    const button = document.getElementById("button3");
    button.onclick = function(){
        window.location.href = "#sectiunea3";
    }
    
}

function DisplayFunction() {

    Data();
    url();
    Browser();
    getLocation();
    SistemOperare();
    
}


function myTimer() {
let myVar = setInterval(myTimer ,1000);
  const d = new Date();
  document.getElementById("clock").innerHTML = d.toLocaleTimeString();
}

var isDrawing = false;
var lastX = 0;
var lastY = 0;

function draw(event) {
    let canvas = document.getElementById("canvas");
    let ctx = canvas.getContext("2d");
    let x = event.offsetX;
    let y = event.offsetY;

    switch (event.type) {
        case 'mousedown':
            isDrawing = true;
            lastX = x;
            lastY = y;
            break;
        case 'mousemove':
            if (isDrawing) {
                ctx.strokeStyle = document.getElementById('contur').value;
                ctx.lineWidth = 3;
                ctx.lineJoin = 'round';
                ctx.lineCap = 'round';
                ctx.beginPath();
                ctx.moveTo(lastX, lastY);
                ctx.lineTo(x, y);
                ctx.stroke();
                lastX = x;
                lastY = y;
            }
            break;
        case 'mouseup':
            isDrawing = false;
            break;
    }
}

// Adaugă ascultători de evenimente pentru mouse pentru a începe, continua și opri desenarea
canvas.addEventListener('mousedown', draw);
canvas.addEventListener('mousemove', draw);
canvas.addEventListener('mouseup', draw);

function Curatare() {
    const canvas = document.getElementById("canvas");
    const ctx = canvas.getContext("2d");

    canvas.width = 500;
    canvas.height = 300;

    ctx.fillRect(0, 0, canvas.width, canvas.height);
    ctx.clearRect(0, 0, canvas.width, canvas.height);

}


//sectiunea 3

function inserareColoana() {
    var poz = document.getElementById("poz").value;
    var tbl = document.getElementById('tabel');
    var i;
    for (i = 0; i < tbl.rows.length; i++) {
        creareColoana(tbl.rows[i].insertCell(poz), i);
    }
}


function inserareRand() {
    var poz = document.getElementById("poz").value;
    var tbl = document.getElementById('tabel');
    var row = tbl.insertRow(poz);
    if (poz == 0) {
        for (i = 0; i < tbl.rows[1].cells.length; i++) {
            creareColoana(row.insertCell(i), i);
        }
    }
    else {
        for (i = 0; i < tbl.rows[0].cells.length; i++) {
            creareColoana(row.insertCell(i), i);
        }
    }
}

function creareColoana(cell, text) {
    var div = document.createElement('div');
    var txt = document.createTextNode(text);
    div.appendChild(txt);
    cell.appendChild(div);
}

function schimbaCuloare_coloana() {
    var poz = document.getElementById("poz").value;
    var x = document.getElementById("tabel").getElementsByTagName("tr");
    for (var i = 0; i < x.length; i++) {
        var first_td = x[i].getElementsByTagName('td')[poz];
        first_td.style.backgroundColor = document.getElementById("culoare").value;
    }
}

function schimbaCuloare_linie() {
    var poz = document.getElementById("poz").value;
    var x = document.getElementById("tabel").getElementsByTagName("tr");
    x[poz].style.backgroundColor = document.getElementById("culoare2").value;
}



var timeInterval;
var showTime;
/*functia pentru modificarea continutului*/
function schimbaContinut(resursa, jsFisier, jsFunctie) {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById("continut").innerHTML =
                this.responseText;
            if (jsFisier) {
                var elementScript = document.createElement('script');
                elementScript.onload = function() {
                    //aveam aceste functii la onload, dar cum nu mai exista el. body 
                    //o sa le apelam la incarcarea paginii
                    if(resursa=="invat"){
                    DisplayFunction();
                    timeInterval=setInterval(showTime, 1000);
                    }
                    else{
                        clearInterval(timeInterval);
                    }
                    if (jsFunctie) {
                        window[jsFunctie]();
                    }
                };
                elementScript.src = jsFisier;
                document.head.appendChild(elementScript);
            } else {
                if (jsFunctie) {
                    window[jsFunctie]();
                }
            }
        }
    };
    xhttp.open("GET", resursa + ".html", true);
    xhttp.send();
}


function validare() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            var object = JSON.parse(this.responseText);
            var utilizatorInput = document.getElementById("utilizator").value;
            var parolaInput = document.getElementById("parola").value;
            var autentificareReusita = false;

            for (var i = 0; i < object.length; i++) {
                if (utilizatorInput == object[i].utilizator && parolaInput == object[i].parola) {
                    autentificareReusita = true;
                    break;
                }
            }

            if (autentificareReusita) {
                document.getElementById("nume_corect").innerHTML = "Nume utilizator corect";
                document.getElementById("parola_corecta").innerHTML = "Parola corecta";
            } else {
                document.getElementById("nume_corect").innerHTML = "Nume utilizator sau parola greșită";
                document.getElementById("parola_corecta").innerHTML = "Nume utilizator sau parola greșită";
            }
        }
    };

    xhttp.open("GET", "../../resurse/utilizatori.json", true);
    xhttp.send();
}

function validare_formular(event){
    event.preventDefault();
 
    //construim obiectul JSON
    var utilizator = {
        utilizator: document.getElementById("NumeUtilizator").value,
        parola: document.getElementById("Parola").value,
        nume: document.getElementById("Nume").value,
        prenume: document.getElementById("Prenume").value,
        email: document.getElementById("email").value,
        telefon: document.getElementById("Telefon").value,
        gen: document.querySelector('select').value,
        dataNasterii: document.getElementById('birthday').value,
        oraNasterii: document.getElementById('birthdayHour').value,
        varsta: document.getElementById('Varsta').value,
        adresaUrl: document.getElementById('AdresaPaginii').value,
        descriere: document.getElementById("Descriere").value
    };
    console.log(utilizator);
    //facem o cerere POST catre api/utilizatori
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "api/utilizatori", true);
    xhr.setRequestHeader("Content-Type", "application/json");
   
    xhr.send(JSON.stringify(utilizator));
    alert('Ai fost înregistrat cu succes');
    document.getElementById("inregistrare_formular").reset();
}

function updateValue(val){
    document.getElementById('varsta-tooltip').textContent = val;
    document.getElementById('varsta-tooltip').setAttribute('title', val);
}




