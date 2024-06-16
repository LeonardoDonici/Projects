localStorage.clear();

class Product {
  constructor(id, name, quantity) {
    this.id = id;
    this.name = name;
    this.quantity = quantity;
  }
}

//clasa de baza
class Storage {
    //metoda prin care citim din storage
    getItem() {
      throw new Error("Abstract get method");
    }
    //metoda prin care punem in storage
    setItem(products) {
      throw new Error("Abstract set method");
    }
  
  }

//clasa pentru localStorage
class LocalStorage extends Storage {
    getItem(resolve) {
      //citesc continutul din localStorage
      let content = localStorage.getItem('products')
      //si il returnez prin intermediul unei promisiuni - resolve
      
      if (content != null)
        return resolve(content)
      else return resolve([])
    }
  
    setItem(products) {
      //adaug in localStorage sub forma de json
      localStorage.setItem("products", JSON.stringify(products));
    }
  
  }

  //clasa pentru baza de date
//clasa pentru baza de date
class IndexedDB extends Storage {
  constructor() {
    super();
    var openRequest = indexedDB.open('produse', 3);
    openRequest.onupgradeneeded = function (e) {
      var db = e.target.result;
      console.log('running onupgradeneeded');
      //daca nu exista baza de date o cream
      if (!db.objectStoreNames.contains('products')) {
        db.createObjectStore('products', { keyPath: 'id' });
      }
      else {
        // object store exists
        console.log("Object store products exists");
        // perform transaction on object store here
      }
    };
  };


  setItem(produse) {
    var DBOpenRequest = indexedDB.open('produse', 3);

    DBOpenRequest.onsuccess = function (e) {
      var db = DBOpenRequest.result;
      var transaction = db.transaction(['products'], 'readwrite');
      var objectStore = transaction.objectStore('products');

      produse.forEach(function (produs) {

        var request = objectStore.put({ id: produs.id, value: produs });
        request.onsuccess = function (event) {
          console.log("Succesfull Insertion")
        };
      });
    }
    DBOpenRequest.onerror = function (e) {
      console.log("Error opening the database for insertion");
    }
  };

  getItem(resolve) {

    //citim inregistrarile din baza de date
    self = this;
    var DBOpenRequest = indexedDB.open('produse', 3);
    DBOpenRequest.onsuccess = function (e) {
      var db = DBOpenRequest.result;
      var transaction = db.transaction(['products'], 'readwrite');
      var objectStore = transaction.objectStore('products');

      //acest array va contine elementele din baza de date
      var db_items = [];
      //vom itera prin toate inregistrarile din tabela pentru a le pune in array
      objectStore.openCursor().onsuccess = function (event) {
        var cursor = event.target.result;
        if (cursor) {
          //daca inca mai sunt elemente in baza de date va continua iteratia
          db_items.push(cursor.value.value)
          cursor.continue();
        } else {
          //daca nu e nimic va returna array ul gol folosind promisiunea
          if (db_items.length == 0)
            resolve(db_items);
          else
            //daca avem elemente va returna ca o promisiune folosind json
            resolve(JSON.stringify(db_items))

        }
      };
    }
  }

}


  var myStorage=new LocalStorage();
  var myIndexDB=new IndexedDB() ;
  var storage_type = 0;//default e pentru localStorage

//functie pentru a schimba tipul stocarii
function changeStorage() {

  const selectElement = document.querySelector('select');
  const selectedOption = selectElement.options[selectElement.selectedIndex].value;

  if (selectedOption == 'local') {
    storage_type = 0;
    console.log(selectedOption);
   
  }
  else if(selectedOption=='db'){
    storage_type = 1;
    console.log(selectedOption);
   
  }
  //de fiecare data cand schimb tipul de stocare se va afisa tabelul corespunzator
  showTable();

}

var selectedStorage;

//functia principala
function buy(event) {
  console.log(storage_type);
  if (storage_type == 0) {
    selectedStorage=myStorage = myStorage;
  } else {
    selectedStorage=myIndexDB = myIndexDB;
  }

    const worker = new Worker("js/worker.js");
    let name = document.getElementById("nume").value;
    let quantity = document.getElementById("cantitate").value;
    //trimitem cei doi termeni care se vor verifica in worker
    worker.postMessage([name, quantity]);
  
    //primeste mesajul de la worker
    worker.onmessage = function (event) {
      if (event.data.hasOwnProperty('error')) {
        //daca e ceva in neregula cu parametrii se va primi un mesaj de eroare de la worker
        document.getElementById("eroare").innerHTML = event.data.error;
        event.preventDefault();
          return;
      } else {
        document.getElementById("eroare").innerHTML = "";
        console.log("Message received from worker.js");
  
      var id;
      //cream o promisiune care va primi itemele din myStorage
      var promise = new Promise(function (resolve) {
        selectedStorage.getItem(resolve)
      });
      promise.then(function (content) {
        if (content.length == 0) {
          id = 0;
        }
        else {
          content = JSON.parse(content);
          id = content[content.length - 1].id
        }
  
        id++;
        let product = new Product(id, name, quantity);
  
  
        let table = document.getElementById("tabel_cump");
        let row = table.insertRow();
        row.innerHTML = `<td>${product.id}</td><td>${product.name}</td><td>${product.quantity}</td>`;
  
        content.push(product);
        selectedStorage.setItem(content)
  
        document.getElementById("nume").value = "";
        document.getElementById("cantitate").value = "";
        event.preventDefault();
      });
    }
  };
  
    event.preventDefault();
}
  
  //stergem baza de date la refresh sau la iesire din fereastra
window.addEventListener('beforeunload', function (event) {
  event.preventDefault();
  var request = indexedDB.deleteDatabase('produse');
  request.onsuccess = function () {
    console.log('Baza de date a fost ștearsă cu succes.');
  };
  request.onerror = function () {
    console.log('Eroare la ștergerea bazei de date.');
  };
});


window.showTable = function show() {
    var table = document.getElementById("tabel_cump");

    // Get all the rows in the table except the first one
    if (table) {
        let rows = table.getElementsByTagName("tr");
        while (rows.length > 1) {
            table.deleteRow(1)
        }
    }

    if (storage_type == 0) {
        console.log("Instanta LocalStorage");
        // Afișați conținutul pentru localStorage
        var promise = new Promise(function(resolve) {
            myStorage.getItem(resolve);
        });
        promise.then(function(products) {
            if (products.length != 0) {
                products = JSON.parse(products);
            }
            let table = document.getElementById("tabel_cump");
            if (table) {
                for (var i = 0; i < products.length; i++) {
                    let row = table.insertRow();
                    row.innerHTML = `<td>${products[i].id}</td><td>${products[i].name}</td><td>${products[i].quantity}</td>`;
                }
                event.preventDefault();
            }
        });
    } else {
        console.log("Instanta IndexedDB");
        // Afișați conținutul pentru IndexedDB
        var promise = new Promise(function(resolve) {
            myIndexDB.getItem(resolve);
        });
        promise.then(function(products) {
            if (products.length != 0) {
                products = JSON.parse(products);
            }
            let table = document.getElementById("tabel_cump");
            if (table) {
                for (var i = 0; i < products.length; i++) {
                    let row = table.insertRow();
                    row.innerHTML = `<td>${products[i].id}</td><td>${products[i].name}</td><td>${products[i].quantity}</td>`;
                }
                event.preventDefault();
            }
        });
    }
}



  
  
 
  
  
  
  
  
  