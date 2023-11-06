document.getElementById("generatePasswordBtn").addEventListener("click", function () {
    console.log("Przycisk zosta� klikni�ty");
    fetch("/Identity/Account/Register?handler=GeneratePassword", {
        method: "POST"
    })
        .then(response => response.text())
        .then(data => {
            document.getElementById("GeneratedPassword").value = data;
        })
        .catch(error => {
            console.error("B��d:", error);
        });
});