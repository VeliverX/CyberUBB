let inactivityTimeout = 5 * 1000; // 10 sekund w milisekundach
let timeoutId;

function resetTimeout() {
    clearTimeout(timeoutId);
    timeoutId = setTimeout(logoutUser, inactivityTimeout);
}

function logoutUser() {
    var form = document.getElementById("LogoutBtn"); // Zamiast "YourFormId" podaj w³aœciwe ID formularza
    if (form) {
        console.log("Znaleziono formularz.");

        form.addEventListener("submit", function (event) {
            // Zapobiegaj domyœlnemu zachowaniu formularza (zapobiega przekierowaniu na inn¹ stronê)
            event.preventDefault();

            // Tutaj mo¿esz dodaæ kod wylogowania u¿ytkownika lub inny odpowiedni kod
            console.log("U¿ytkownik zosta³ wylogowany po klikniêciu przycisku LogoutBtn.");

            // Na przyk³ad przekieruj u¿ytkownika na inn¹ stronê
            // window.location.href = '/Identity/Account/Logout';
        });
    } else {
        console.log("Nie znaleziono formularza.");
    }
}

function logInactiveTime() {
    const currentTime = new Date();
    const lastActivityTime = new Date(currentTime - inactivityTimeout);
    console.log(`Czas nieaktywnoœci: ${inactivityTimeout / 1000} sekund. Ostatnia aktywnoœæ: ${lastActivityTime}`);
}

// Dodaj obs³ugê zdarzeñ dla aktywnoœci myszki i klawiatury
document.addEventListener("mousemove", function () {
    resetTimeout();
    logInactiveTime();
});

document.addEventListener("keydown", function () {
    resetTimeout();
    logInactiveTime();
});

// Inicjalizuj timer po za³adowaniu strony
resetTimeout();
