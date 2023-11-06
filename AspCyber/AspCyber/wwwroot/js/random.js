document.getElementById("generatePasswordBtn").addEventListener("click", function () {
    var email = document.getElementById("email").value;
    var emailLength = email.length;
    var randomX = Math.random() / 4;
    var result = Math.exp( -emailLength * randomX).toFixed(15);
    document.getElementById("generatedPassword").value = result;
    document.getElementById("generatedPasswordx").value = randomX;

    // Logowanie do konsoli
    console.log("Email Length:", -emailLength );
    console.log("Random X:", randomX);
    console.log("Generated Password:", result);
});
