// Script para o menu hamburger
const hamburger = document.querySelector('.hamburger');
const sidebar = document.querySelector('.sidebar');

if (hamburger && sidebar) { // Verificando os dois
    hamburger.addEventListener('click', () => {
        sidebar.classList.toggle('active-side');
        hamburger.classList.toggle('active-button');
    });
}

// --- Script para o Modal de Resenha ---

// Seleciona TODOS os botões que abrem o modal
const showReviewModalButtons = document.querySelectorAll(".review-btn"); 
const closeReviewModal = document.getElementById("closeModal");
const modalReview = document.getElementById("modalReview");

// Verifica se os elementos do modal existem
if (modalReview && closeReviewModal && showReviewModalButtons.length > 0) {

    // Adiciona o evento de clique para CADA botão "Resenha"
    showReviewModalButtons.forEach(button => {
        button.addEventListener("click", () => {
            modalReview.showModal();
        });
    });

    // Adiciona o evento para fechar o modal
    closeReviewModal.addEventListener("click", () => {
        modalReview.close();
    });
}