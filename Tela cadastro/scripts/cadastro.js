function OcultarSenha() {
    let password = document.getElementById('senha');
    
    if(password.type === "password"){
        password.type = "text";
        iconeOlho.style.display = "none";
        iconeOlhoSlash.style.display = "block";
        
    } else {
        password.type = "password"
        iconeOlho.style.display = "block"
        iconeOlhoSlash.style.display = "none";
    }

}