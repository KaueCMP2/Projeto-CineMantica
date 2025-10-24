const harburguer = document.querySelector(".hamburger")
const sidebar = document.querySelector(".sidebar")

const showReviewModal = document.querySelector(".review-btn")
const closeReviewModal = document.getElementById("closeModal")
const modalReview = document.getElementById("modalReview")

harburguer.addEventListener("click", ()=> {
    sidebar.classList.toggle("active-side")
    harburguer.classList.toggle("active-button")

})

// Função para abrir o modaL

showReviewModal.addEventListener("click", () => {
    modalReview.showModal();
})

closeReviewModal.addEventListener("click", ()=> {
    modalReview.close();
})