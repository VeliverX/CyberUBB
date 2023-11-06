document.getElementById("generatePasswordBtn").addEventListener("click", function () {
    console.log("Przycisk zosta³ klikniêty");
    fetch("/Identity/Account/Register?handler=GeneratePassword", {
        method: "POST"
    })
        .then(response => response.text())
        .then(data => {
            document.getElementById("GeneratedPassword").value = data;
        })
        .catch(error => {
            console.error("B³¹d:", error);
        });
});