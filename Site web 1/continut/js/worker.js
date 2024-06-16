onmessage = function (e){
  console.log("Worker: s-a primit mesajul de la script");
  var nume = e.data[0];
  var cant = e.data[1];
  if (nume === "" || cant === "") {
    var errorMessage = "Completati toate campurile!!!!";
    this.postMessage({error: errorMessage});
  } else {
    this.postMessage("Campurile sunt completate corect");
  }
}