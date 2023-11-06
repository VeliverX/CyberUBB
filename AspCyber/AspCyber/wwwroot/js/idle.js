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
            console.log("U�ytkownik zosta� wylogowany po klikni�ciu przycisku LogoutBtn.");
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

document.addEventListener("mousemove", function () {
    resetTimeout();
    logInactiveTime();
});

document.addEventListener("keydown", function () {
    resetTimeout();
    logInactiveTime();
});

resetTimeout();
