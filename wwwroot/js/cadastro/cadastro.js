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

function OcultarSenhaConfirm() {
    let password = document.getElementById('confirmarSenha');
    
    if(password.type === "password"){
        password.type = "text";
        iconeOlhoConfirm.style.display = "none";
        iconeOlhoSlashConfirm.style.display = "block";
        
    } else {
        password.type = "password"
        iconeOlhoConfirm.style.display = "block"
        iconeOlhoSlashConfirm.style.display = "none";
    }

}