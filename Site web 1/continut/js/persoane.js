function incarcaPersoane() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            var xmlDoc = this.responseXML;
            var table = "<table><tr><th>Nume</th><th>Prenume</th><th>Vârstă</th><th>Locuri de Muncă</th><th>Strada</th><th>Număr</th><th>Localitate</th><th>Județ</th><th>Țară</th><th>Telefon</th><th>Studii</th></tr>";
            var x = xmlDoc.getElementsByTagName('persoana');
            for (i = 0; i < x.length; i++) {
                table += "<tr><td>" + x[i].getElementsByTagName("nume")[0].textContent + "</td><td>" +
                    x[i].getElementsByTagName("prenume")[0].textContent + "</td><td>" +
                    x[i].getElementsByTagName("varsta")[0].textContent + "</td><td>" +
                    x[i].getElementsByTagName("WorkExperience")[0].textContent + "</td><td>" +
                    x[i].getElementsByTagName("strada")[0].textContent + "</td><td>" +
                    x[i].getElementsByTagName("numar")[0].textContent + "</td><td>" +
                    x[i].getElementsByTagName("localitate")[0].textContent + "</td><td>" +
                    x[i].getElementsByTagName("judet")[0].textContent + "</td><td>" +
                    x[i].getElementsByTagName("tara")[0].textContent + "</td><td>" +
                    x[i].getElementsByTagName("PhoneNumber")[0].textContent + "</td><td>" +
                    x[i].getElementsByTagName("Liceu")[0].textContent + ", " +
                    x[i].getElementsByTagName("Facultate")[0].textContent + "</td></tr>";
            }
            table += "</table>";
            document.getElementById("incarcare").innerHTML = "";
            document.getElementById("tabel_persoane").innerHTML = table;
        }
    };
    xhttp.open("GET", "resurse/persoane.xml", true);
    xhttp.send();
}