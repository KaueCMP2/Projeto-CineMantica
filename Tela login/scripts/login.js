function OcultarSenha() {
    let password = document.getElementById('senha');
    
    if(password.type === "password"){
        password.type = "text";
        iconeOlho.style.display = "none";
        iconeOlhoSlash.style.display = "block";
        
    } else {
        password.type = "password";
        iconeOlho.style.display = "block";
        iconeOlhoSlash.style.display = "none";
    }
}

function Login() {
    let email = document.getElementById('email').value;
    let senha = document.getElementById('senha').value;

    if(email == "admin" && senha == "1234"){
        window.location.href = "index.html";
        
    } else {
        alert("errado");
    }
}