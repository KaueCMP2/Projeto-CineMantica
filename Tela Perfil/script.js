const harburguer = document.querySelector(".hamburger")
const sidebar = document.querySelector(".sidebar")

harburguer.addEventListener("click", ()=> {
    sidebar.classList.toggle("active-side")
    harburguer.classList.toggle("active-button")

})