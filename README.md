# **Web Application - 21.03.2023**

### Back-End
<ul style="list-style-type:disc">
  <li>USE AdventureWorksLT2019</li>
  <li>Creare una WebApp back-end chiamata <b>BETACOMARE</b>.</li>
  <li>Implementare la completa gestione del DataBase per le seguenti tabelle:</li>
    <ul style="list-style-type:circle">
      <li>Customer</li>
      <li>Address</li>
      <li>Product</li>
    </ul>
  <li>Il back-end va realizzato secondo tutte le best practices che si conoscono.</li>
  <li>Implementare la gestione dei Logs.</li>
  <li>Collaudo generale (Postman o altri) o validazione positiva funzionalità e chiamate HTTP.</li>
</ul>

### Front-End
<ul>
  <li>Implementare front-end tramite Angular.</li>
  <li>Implementare il Login con l'autenticazione Basic.</li>
</ul>

---

### Prerequisiti di Funzionalità

La WebApp *Betacomare* dovrà consentire la gestione completa dei Clienti e dei Prodotti (per la tab Product), ossia deve poter eseguire in modo semplice ed intuitivo:
<ul style="list-style-type:square">
  <li>
    <details>
      <summary><b>LogIn</b></summary>
      <ul>
        <li>Determinare se l'utente è un utente Cliente oppure Admin</li>
        <li>Se <b>Admin</b> consente l'utilizzo di tutti i metodi <abbr title="Create-Read-Update-Delete">CRUD</abbr> su tutte le tabelle, altrimenti, il cliente viene abilitato SOLAMENTE per operazioni di lettura sul suo Cliente e operazioni CRUD per eseguire eventuali ordini (FACOLTATIVO).
        </li>
      </ul>
    </details>
  </li>
  <li>Tutte le operazioni CRUD possono essere eseguite da Utente di livello Admin.</li>
  <li>Implementare modulo per l’inserimento di un nuovo Cliente (Registrazione Account)
    <blockquote>Il Cliente viene guidato per creare un nuovo account, con il quale poi eseguire l’accesso al sito.</blockquote>
  </li>
  <li>
    <b>LogOut</b>
    <blockquote>Gestire al meglio il meccanismo di Logout dal sito (fare molta attenzione).</blockquote>
  </li>
</ul>