const harburguer = document.querySelector(".hamburger")
const sidebar = document.querySelector(".sidebar")

const btEditar = document.getElementById('editarPerfil')
const nome = document.getElementById('nome')
const foto = document.getElementById('foto')

harburguer.addEventListener("click", ()=> {
    sidebar.classList.toggle("active-side")
    harburguer.classList.toggle("active-button")

})


btEditar.addEventListener('click', () => {
    const isEditing = nome.getAttribute('contenteditable') === 'true';
    
    
    if(isEditing){
        alert.apply();
        nome.setAttribute = prompt('contenteditable', 'false')
        btEditar.textContent = "Editar Perfil";
        alert('Perfil Editado com sucesso')
    }
    else{
        nome.setAttribute('contenteditable', 'true');
        nome.focus();
        btEditar.textContent = 'Salvar';
    }
})