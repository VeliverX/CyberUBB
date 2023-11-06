let inactivityTimeout = 5 * 1000;
let timeoutId;

function resetTimeout() {
    clearTimeout(timeoutId);
    timeoutId = setTimeout(logoutUser, inactivityTimeout);
}

function logoutUser() {
    var form = document.getElementById("LogoutBtn"); 
    if (form) {
        console.log("Znaleziono formularz.");

        form.addEventListener("submit", function (event) {
            
            event.preventDefault();   
            console.log("U¿ytkownik zosta³ wylogowany po klikniêciu przycisku LogoutBtn.");
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

document.addEventListener("mousemove", function () {
    resetTimeout();
    logInactiveTime();
});

document.addEventListener("keydown", function () {
    resetTimeout();
    logInactiveTime();
});

resetTimeout();
