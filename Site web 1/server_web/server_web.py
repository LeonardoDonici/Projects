import json
import socket
import os
import threading
import time

# creeaza un server socket
serversocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
# specifica ca serverul ca rula pe portul 5678, accesibil de pe orice ip al serverului
serversocket.bind(('', 5678))
# serverul poate accepta conexiuni; specifica cati clienti se asteapta la coada
serversocket.listen(5)


def prelucrare_cerere(clientsocket, par):
    # asteapta conectarea unui client la server
    # metoda accept este blocanta => clientsocket, care reprezinta socket-ul corespunzator clientului conectat
    print('S-a conectat un client')
    # se proceseaza cererea si se citeste prima linie de text
    cerere = ''
    linieDeStart = ''
    while True:
        data = clientsocket.recv(1024)
        cerere = cerere + data.decode()
        print('S-a citit mesajul: \n-----------' + cerere + '---------\n')
        if(cerere == ""):
            time.sleep(1)
        pozitie = cerere.find('\r\n')
        if (pozitie > -1):
            linieDeStart = cerere[0:pozitie]
            print('S-a citit linia de start din cerere: ##### ' +
                  linieDeStart + '#####')
            break
    print('S-a terminat citirea')
    elemente = linieDeStart.split()
    resursa = elemente[1]
    print(resursa)

    if (resursa == "/api/utilizatori"):
        # to do - aici se va face prelucrarea in acest caz
        print("aici")
        print("cererea in if" + cerere)
        #cautam inceputul JSON ului
        start = cerere.find("{")
        #extragem JSON ul
        utilizator_nou = cerere[start:]
        print("utilizator nou "+ utilizator_nou)

        with open('../continut/resurse/utilizatori.json', 'r') as f:
            utilizatori_vechi = json.load(f)
            print("utilizator vechi")
            print(utilizatori_vechi)

        utilizatori_vechi.append(json.loads(utilizator_nou))
        print("utilizator actualizat")
        print(utilizatori_vechi)

        with open('../continut/resurse/utilizatori.json', 'w') as f:
            json.dump(utilizatori_vechi, f)
        print('s-a scris in fisier')
    # prima cerinta - hello world + resursa necesara
    # mesaj = "Hello World! " + resursa
    # linieDeRaspuns = 'HTTP/1.1 200 OK\r\nContent-Length:' + str(len(mesaj)) + '\r\n Content-Type:text/html\r\nAccept-Encoding: gzip\r\n Transfer-Encoding:gzip\r\n Server:server_web\r\n\r\n' + mesaj + '\r\n'
    # clientsocket.sendall(linieDeRaspuns.encode('utf-8'))
    else:
        path = '../continut' + elemente[1]
        print(path)

        if os.path.isfile(path):
            with open(path, 'rb') as f:
                continut = f.read()

        # determinam tipul de fisier
            extensie = resursa[resursa.rfind('.')+1:]
            tipResursa = {
                'html': 'text/html; charset=utf-8',
                'css': 'text/css; charset=utf-8',
                'js': 'application/js; charset=utf-8',
                'png': 'text/png',
                'jpg': 'text/jpeg',
                'jpeg': 'text/jpeg',
                'gif': 'text/gif',
                'jfif': 'text/jfif',
                'ico': 'image/x-icon',
                'xml': 'application/xml; charset=utf-8',
                'json': 'application/json; charset=utf-8'
            }
            tip = tipResursa.get(extensie, 'text/plain; charset=utf-8')
            linieDeRaspuns = b'HTTP/1.1 200 OK\r\nContent-Length:' + str(
                len(continut)).encode('utf-8') + b'\r\n Content-Type:' + tip.encode('utf-8') + b'\r\n'+b'Accept-Encoding: gzip\r\n Transfer-Encoding:gzip\r\n Server:server_web\r\n\r\n'
            clientsocket.sendall(linieDeRaspuns + continut)
        else:
            string = 'Nu s-a gasit fisierul!'
            linieDeRaspuns = b'HTTP/1.1 404 NOT FOUND \r\nContent-Length:' + \
                         str(string).encode(
                             'utf-8') + b'\r\n Content-Type:text/html\r\n Server:server_web\r\n\r\n'
            clientsocket.sendall(linieDeRaspuns+string)
        clientsocket.close()


while True:
    print('###################################')
    print('Serverul asculta potentiali clienti')

    try:
        clientsocket, address = serversocket.accept()
    except KeyboardInterrupt:
        print("Serverul s-a inchis!")
        break
    try:
        threading.Thread(target=prelucrare_cerere,
                         args=(clientsocket, 1)).start()
    except:
        print("Eroare la pornirea threadului")

    print('S-a terminat comunicarea cu clientul')
