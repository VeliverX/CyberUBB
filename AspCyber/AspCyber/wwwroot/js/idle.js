let inactivityTimeout = 5 * 1000; // 10 sekund w milisekundach
let timeoutId;

function resetTimeout() {
    clearTimeout(timeoutId);
    timeoutId = setTimeout(logoutUser, inactivityTimeout);
}

function logoutUser() {
    var form = document.getElementById("LogoutBtn"); // Zamiast "YourFormId" podaj w�a�ciwe ID formularza
    if (form) {
        console.log("Znaleziono formularz.");

        form.addEventListener("submit", function (event) {
            // Zapobiegaj domy�lnemu zachowaniu formularza (zapobiega przekierowaniu na inn� stron�)
            event.preventDefault();

            // Tutaj mo�esz doda� kod wylogowania u�ytkownika lub inny odpowiedni kod
            console.log("U�ytkownik zosta� wylogowany po klikni�ciu przycisku LogoutBtn.");

            // Na przyk�ad przekieruj u�ytkownika na inn� stron�
            // window.location.href = '/Identity/Account/Logout';
        });
    } else {
        console.log("Nie znaleziono formularza.");
    }
}

function logInactiveTime() {
    const currentTime = new Date();
    const lastActivityTime = new Date(currentTime - inactivityTimeout);
    console.log(`Czas nieaktywno�ci: ${inactivityTimeout / 1000} sekund. Ostatnia aktywno��: ${lastActivityTime}`);
}

// Dodaj obs�ug� zdarze� dla aktywno�ci myszki i klawiatury
document.addEventListener("mousemove", function () {
    resetTimeout();
    logInactiveTime();
});

document.addEventListener("keydown", function () {
    resetTimeout();
    logInactiveTime();
});

// Inicjalizuj timer po za�adowaniu strony
resetTimeout();
