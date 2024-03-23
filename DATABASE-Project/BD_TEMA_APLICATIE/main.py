from flask import Flask, render_template, jsonify, request, redirect
import cx_Oracle
import os
from datetime import datetime

app = Flask(__name__)
with open(app.root_path + '\config.cfg', 'r') as f:
    app.config['ORACLE_URI'] = f.readline()
import os

cx_Oracle.init_oracle_client(os.path.join(os.getcwd(), "instantclient_21_7"))
dsn_tns = cx_Oracle.makedsn('bd-dc.cs.tuiasi.ro', '1539', 'orcl')
con = cx_Oracle.connect("bd007", "parolaBD2023", dsn_tns)

@app.route('/')

@app.route('/authors')
def authors():
    authors = []

    cur = con.cursor()
    cur.execute('select * from autori')  # Modificați cu numele real al tabelului autori
    for result in cur:
        author = {}
        author['id_autor'] = result[0]
        author['carti_id_carte'] = result[1]
        author['nume_autor'] = result[2]
        author['prenume_autor'] = result[3]
        author['biografie'] = result[4]

        authors.append(author)
    cur.close()
    return render_template('autori.html', authors=authors)

@app.route('/addAuthor')
def add_author():
    return render_template('addAuthor.html')

@app.route('/saveAuthor', methods=['POST'])


def save_author():
    error = None
    if request.method == 'POST':
        # Conectarea la baza de date și codul pentru inserarea datelor
        emp = 0
        cur = con.cursor()
        cur.execute('SELECT MAX(id_autor) FROM autori')
        for result in cur:
            emp = result[0]
        cur.close()

        emp += 1
        cur = con.cursor()
        values = []
        values.append("'" + str(emp) + "'")
        values.append("'" + request.form['carti_id_carte'] + "'")
        values.append("'" + request.form['nume_autor'] + "'")
        values.append("'" + request.form['prenume_autor'] + "'")
        values.append("'" + request.form['biografie'] + "'")

        fields = ['id_autor', 'carti_id_carte', 'nume_autor', 'prenume_autor', 'biografie']
        query = 'INSERT INTO autori (%s) VALUES (%s)' % (', '.join(fields), ', '.join(values))

        cur.execute(query)
        con.commit()


    return redirect('/authors')  # Aici redirectionăm înapoi la pagina autorilor după adăugare

@app.route('/delAuthor', methods=['POST'])
def del_author():
    emp = request.form['author_id']
    cur = con.cursor()
    cur.execute('delete from autori where id_autor=' + emp)
    cur.execute('commit')
    return redirect('/authors')


@app.route('/books')
def books():
    books = []

    cur = con.cursor()
    cur.execute('SELECT * FROM carti')  # Modificați cu numele real al tabelului carti
    for result in cur:
        book = {
            'id_carte': result[0],
            'titlu': result[1],
            'autor': result[2],
            'editura': result[3],
            'an_publicare': result[4],
            'nr_exemplare_impr': result[5],
            'limba': result[6],
            'categorii_id_categorie': result[7]
        }
        books.append(book)
    cur.close()
    return render_template('carti.html', books=books)

@app.route('/addBook')
def add_book():
    return render_template('addBook.html')

@app.route('/saveBook', methods=['POST'])
def save_book():
    error = None
    if request.method == 'POST':
        # Conectarea la baza de date și codul pentru inserarea datelor
        emp = 0
        cur = con.cursor()
        cur.execute('SELECT MAX(id_carte) FROM carti')
        for result in cur:
            emp = result[0]
        cur.close()

        emp += 1
        cur = con.cursor()
        values = []
        values.append("'" + str(emp) + "'")
        values.append("'" + request.form['titlu'] + "'")
        values.append("'" + request.form['autor'] + "'")
        values.append("'" + request.form['editura'] + "'")
        values.append("'" + request.form['an_publicare'] + "'")
        values.append("'" + request.form['nr_exemp_impr'] + "'")
        values.append("'" + request.form['limba'] + "'")
        values.append("'" + request.form['categorii_id_categorie'] + "'")

        fields = ['id_carte', 'titlu', 'autor', 'editura', 'an_publicare', 'nr_exemp_impr', 'limba', 'categorii_id_categorie']
        query = 'INSERT INTO carti (%s) VALUES (%s)' % (', '.join(fields), ', '.join(values))

        cur.execute(query)
        con.commit()

    return redirect('/books')  # Aici redirectionăm înapoi la pagina autorilor după adăugare

@app.route('/delBook', methods=['POST'])
def del_book():
    emp = request.form['book_id']
    cur = con.cursor()
    cur.execute('delete from autori where carti_id_carte=' +emp)
    cur.execute('delete from carti where id_carte=' + emp)
    cur.execute('commit')
    return redirect('/books')


@app.route('/library_cards')
def library_cards():
    cards = []

    cur = con.cursor()
    cur.execute('SELECT * FROM carduri_de_biblioteca')  # Modificați cu numele real al tabelului carduri_de_biblioteca
    for result in cur:
        card = {
            'id_card': result[0],
            'membri_id_membru': result[1],
            'data_expirarii': result[2],
        }
        cards.append(card)
    cur.close()
    return render_template('carduri_de_biblioteca.html', cards=cards)


@app.route('/addLibraryCard')
def add_carduri():
    return render_template('addCarduri.html')

@app.route('/saveLibraryCard', methods=['POST'])
def save_cards():
    error = None
    if request.method == 'POST':
        # Conectarea la baza de date și codul pentru inserarea datelor
        emp = 0
        cur = con.cursor()
        cur.execute('SELECT MAX(id_card) FROM carduri_de_biblioteca')
        for result in cur:
            emp = result[0]
        cur.close()

        emp += 1
        cur = con.cursor()
        values = []
        values.append("'" + str(emp) + "'")
        values.append("'" + request.form['membri_id_membru'] + "'")
        values.append("'" + request.form['data_expirarii'] + "'")


        fields = ['id_card', 'membri_id_membru', 'data_expirarii']
        query = 'INSERT INTO carduri_de_biblioteca (%s) VALUES (%s)' % (', '.join(fields), ', '.join(values))

        cur.execute(query)
        con.commit()

    return redirect('/library_cards')

@app.route('/deleteLibraryCard', methods=['POST'])
def del_card():
    emp = request.form['card_id']
    cur = con.cursor()
    cur.execute('delete from carduri_de_biblioteca where id_card=' + emp)
    cur.execute('commit')
    return redirect('/library_cards')



@app.route('/categories')
def categories():
    categories = []

    cur = con.cursor()
    cur.execute('SELECT * FROM categorii')  # Modificați cu numele real al tabelului categorii
    for result in cur:
        category = {
            'id_categorie': result[0],
            'nume_categorie': result[1],
        }
        categories.append(category)
    cur.close()
    return render_template('categorii.html', categories=categories)

@app.route('/addCategory')
def add_category():
    return render_template('addCategory.html')

@app.route('/saveCategory', methods=['POST'])
def save_category():
    error = None
    if request.method == 'POST':
        # Conectarea la baza de date și codul pentru inserarea datelor
        emp = 0
        cur = con.cursor()
        cur.execute('SELECT MAX(id_categorie) FROM categorii')
        for result in cur:
            emp = result[0]
        cur.close()

        emp += 1
        cur = con.cursor()
        values = []
        values.append("'" + str(emp) + "'")
        values.append("'" + request.form['nume_categorie'] + "'")

        fields = ['id_categorie', 'nume_categorie']
        query = 'INSERT INTO categorii (%s) VALUES (%s)' % (', '.join(fields), ', '.join(values))

        cur.execute(query)
        con.commit()

    return redirect('/categories')  # Aici redirectionăm înapoi la pagina categoriilor după adăugare

@app.route('/deleteCategory', methods=['POST'])
def del_category():
    emp = request.form['category_id']
    cur = con.cursor()
    # Șterge autorii legați de cărți cu categorii_id_categorie egal cu emp
    # Șterge autorii legați de cărți cu categorii_id_categorie egal cu emp
    cur.execute(
        'DELETE FROM autori WHERE carti_id_carte IN (SELECT id_carte FROM carti WHERE categorii_id_categorie = :1)',
        (emp,))

    # Șterge cărțile cu categorii_id_categorie egal cu emp
    cur.execute('DELETE FROM carti WHERE categorii_id_categorie = :1', (emp,))

    cur.execute('delete from categorii where id_categorie=' + emp)
    cur.execute('commit')
    return redirect('/categories')


@app.route('/loans')
def loans():
    loans = []

    cur = con.cursor()
    cur.execute('SELECT * FROM IMPRUMUTURI')  # Modificați cu numele real al tabelului imprumuturi
    for result in cur:
        loan = {
            'id_imprumut': result[0],
            'id_carte': result[1],
            'membri_id_membru': result[2],
            'data_imprumutului': result[3],
            'data_scadentei': result[4],
            'data_returnarii': result[5],
        }
        loans.append(loan)
    cur.close()
    return render_template('imprumuturi.html', loans=loans)


@app.route('/addLoan')
def add_imprumut():
    return render_template('addImprumut.html')

@app.route('/saveImprumut', methods=['POST'])
def save_loans():
    error = None
    if request.method == 'POST':
        # Conectarea la baza de date și codul pentru inserarea datelor
        emp = 0
        cur = con.cursor()
        cur.execute('SELECT MAX(id_imprumut) FROM imprumuturi')
        for result in cur:
            emp = result[0]
        cur.close()

        emp += 1
        cur = con.cursor()
        values = []
        values.append("'" + str(emp) + "'")
        values.append("'" + request.form['id_carte'] + "'")
        values.append("'" + request.form['membri_id_membru'] + "'")
        values.append("'" + request.form['data_imprumutului'] + "'")
        values.append("'" + request.form['data_scadentei'] + "'")
        values.append("'" + request.form['data_returnarii'] + "'")

        fields = ['id_imprumut', 'id_carte', 'membri_id_membru', 'data_imprumutului', 'data_scadentei',
                  'data_returnarii']
        query = 'INSERT INTO imprumuturi (%s) VALUES (%s)' % (', '.join(fields), ', '.join(values))

        cur.execute(query)
        con.commit()

    return redirect('/loans')

@app.route('/deleteLoan', methods=['POST'])
def del_loan():
    emp = request.form['loan_id']
    cur = con.cursor()
    cur.execute('delete from imprumuturi where id_imprumut=' + emp)
    cur.execute('commit')
    return redirect('/loans')

@app.route('/members')
def members():
    members = []

    cur = con.cursor()
    cur.execute('SELECT * FROM membri')  # Modificați cu numele real al tabelului membri
    for result in cur:
        member = {
            'id_membru': result[0],
            'nume': result[1],
            'prenume': result[2],
            'nr_telefon': result[3],
            'data_inregistrarii': result[4],
            'data_nasterii': result[5],
            'gen': result[6],
        }
        members.append(member)
    cur.close()
    return render_template('membri.html', members=members)

@app.route('/addMember')
def add_member():
    return render_template('addMember.html')

@app.route('/saveMember', methods=['POST'])
def save_member():
    error = None
    if request.method == 'POST':
        # Conectarea la baza de date și codul pentru inserarea datelor
        emp = 0
        cur = con.cursor()
        cur.execute('SELECT MAX(id_membru) FROM membri')
        for result in cur:
            emp = result[0]
        cur.close()

        emp += 1
        cur = con.cursor()
        values = []
        values.append("'" + str(emp) + "'")
        values.append("'" + request.form['nume'] + "'")
        values.append("'" + request.form['prenume'] + "'")
        values.append("'" + request.form['nr_telefon'] + "'")
        values.append("'" + request.form['data_inregistrarii'] + "'")
        values.append("'" + request.form['data_nasterii'] + "'")
        values.append("'" + request.form['gen'] + "'")

        fields = ['id_membru', 'nume', 'prenume', 'nr_telefon', 'data_inregistrarii', 'data_nasterii', 'gen']
        query = 'INSERT INTO membri (%s) VALUES (%s)' % (', '.join(fields), ', '.join(values))

        cur.execute(query)
        con.commit()

    return redirect('/members')  # Aici redirectionăm înapoi la pagina autorilor după adăugare

@app.route('/deleteMember', methods=['POST'])
def del_member():
    emp = request.form['member_id']
    cur = con.cursor()
    cur.execute('delete from imprumuturi where membri_id_membru ='+emp)
    cur.execute('delete from membri where id_membru=' + emp)
    cur.execute('commit')
    return redirect('/members')

# regions end code
# -----------------------------------------#


# main
if __name__ == '__main__':
    app.run(debug=True)
    con.close()
