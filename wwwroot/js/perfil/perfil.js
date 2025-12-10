const modal = document.querySelector(".modal-overlay");
const btnAbrirModal = document.querySelector(".edit-profile-btn");
const btnFecharModal = document.querySelector(".btn-cancel");

btnAbrirModal.addEventListener("click", () => {
    modal.classList.add("active");
});

btnFecharModal.addEventListener("click", () => {
    modal.classList.remove("active");
});
