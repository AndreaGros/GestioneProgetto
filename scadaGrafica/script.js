document.addEventListener("DOMContentLoaded", () => {
    const btnConnect = document.getElementById("btnConnect");
    const btnDisconnect = document.getElementById("btnDisconnect")
    const btnMan = document.getElementById("btnMan");
    const btnStart = document.getElementById("btnStart");
    const btnSlittaCentroSu = document.getElementById("btnSlittaCentroSu")
    const btnSlittaCentroGiu = document.getElementById("btnSlittaCentroGiu")
    const btnPistoneAvanti = document.getElementById("btnPistoneAvanti")
    const btnPistoneIndietro = document.getElementById("btnPistoneIndietro")
    const btnSlittaLatSu = document.getElementById("btnSlittaLatSu")
    const btnSlittaLatGiu = document.getElementById("btnSlittaLatGiu")
    const btnReset = document.getElementById("btnReset")

    btnAggiorna.addEventListener("click", aggiornaPezzi)
    btnConnect.addEventListener("click", connect);
    btnDisconnect.addEventListener("click", disconnect)
    btnMan.addEventListener("click", man);
    btnStart.addEventListener("click", startNastro);
    btnSlittaCentroSu.addEventListener("click", slittaCentroSu)
    btnSlittaCentroGiu.addEventListener("click", slittaCentroGiu)
    btnPistoneAvanti.addEventListener("click", pistoneAvanti)
    btnPistoneIndietro.addEventListener("click", pistoneIndietro)
    btnSlittaLatSu.addEventListener("click", slittaLatSu)
    btnSlittaLatGiu.addEventListener("click", slittaLatGiu)
    btnReset.addEventListener("click", reset)
});

async function connect() {
    try {
        const response = await fetch('https://localhost:7181/api/PLC/connect');
        const data = await response.text();
        console.log("Risposta PLC:", data);
    } catch (error) {
        console.error("Errore connessione PLC:", error);
    }
}

async function disconnect() {
    try {
        const response = await fetch('https://localhost:7181/api/PLC/disconnect');
        const data = await response.text();
        console.log("Risposta PLC:", data);
    } catch (error) {
        console.error("Errore connessione PLC:", error);
    }
}

async function man() {
    try {
        const response = await fetch('https://localhost:7181/api/PLC/btnMan');
        const data = await response.text();
        console.log("Risposta PLC:", data);
    } catch (error) {
        console.error("Errore connessione PLC:", error);
    }
}

// Funzione per il comando Nastro
async function startNastro() {
    alert("Comando Nastro Avanti inviato!");
    try {
        const response = await fetch('https://localhost:7181/api/PLC/startNastro');

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        const data = await response.json();
        
        if (data.success) {
            console.log("Nastro avviato! Numero pezzi:", data.nPezzi);
            nPezzi = data.nPezzi
        } else {
            console.log("Errore: Nastro non avviato");
        }
    } catch (error) {
        console.error("Errore connessione PLC:", error);
    }
}

async function slittaCentroSu() {
    try {
        const response = await fetch('https://localhost:7181/api/PLC/slittaCentraleSu');
        const data = await response.text();
        console.log("Risposta PLC:", data);
    } catch (error) {
        console.error("Errore connessione PLC:", error);
    }
}

async function slittaCentroGiu() {
    try {
        const response = await fetch('https://localhost:7181/api/PLC/slittaCentraleGiu');
        const data = await response.text();
        console.log("Risposta PLC:", data);
    } catch (error) {
        console.error("Errore connessione PLC:", error);
    }
}

async function pistoneAvanti() {
    try {
        const response = await fetch('https://localhost:7181/api/PLC/pistoneAvanti');
        const data = await response.text();
        console.log("Risposta PLC:", data);
    } catch (error) {
        console.error("Errore connessione PLC:", error);
    }
}

async function pistoneIndietro() {
    try {
        const response = await fetch('https://localhost:7181/api/PLC/pistoneIndietro');
        const data = await response.text();
        console.log("Risposta PLC:", data);
    } catch (error) {
        console.error("Errore connessione PLC:", error);
    }
}

async function slittaLatSu() {
    try {
        const response = await fetch('https://localhost:7181/api/PLC/slittaLatSu');
        const data = await response.text();
        console.log("Risposta PLC:", data);
    } catch (error) {
        console.error("Errore connessione PLC:", error);
    }
}

async function slittaLatGiu() {
    try {
        const response = await fetch('https://localhost:7181/api/PLC/slittaLatGiu');
        const data = await response.text();
        console.log("Risposta PLC:", data);
    } catch (error) {
        console.error("Errore connessione PLC:", error);
    }
}

async function emergenza() {
    try {
        const response = await fetch('https://localhost:7181/api/PLC/emergenza');
    } catch (error) {
        console.error("Errore connessione PLC:", error);
    }
}

async function reset() {
    try {
        const response = await fetch('https://localhost:7181/api/PLC/reset');
    } catch (error) {
        console.error("Errore connessione PLC:", error);
    }
}

async function aggiornaPezzi(){
    try {
        const response = await fetch('https://localhost:7181/api/PLC/aggiorna');
    } catch (error) {
        console.error("Errore connessione PLC:", error);
    }
}