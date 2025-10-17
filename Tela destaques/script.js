document.addEventListener('DOMContentLoaded', function () {
    // Script para o dropdown de usuário
    const userIcon = document.getElementById('user-icon');
    const userDropdown = document.getElementById('user-dropdown');

    if (userIcon) {
        userIcon.addEventListener('click', function (event) {
            event.stopPropagation();
            userDropdown.classList.toggle('show');
        });
    }

    window.addEventListener('click', function (event) {
        if (userDropdown && userDropdown.classList.contains('show')) {
            userDropdown.classList.remove('show');
        }
    });

    // Script para o menu hamburger e sidebar
    const hamburger = document.querySelector('.hamburger');
    const sidebar = document.querySelector('.sidebar');

    if (hamburger && sidebar) {
        hamburger.addEventListener('click', () => {
            sidebar.classList.toggle('active-side');
            hamburger.classList.toggle('active-button');
        });
    }

    // --- LÓGICA DO CARROSSEL ---
    const carousels = document.querySelectorAll('.carousel-container');

    carousels.forEach(container => {
        const carousel = container.querySelector('.movie-carousel');
        const prevButton = container.querySelector('.prev');
        const nextButton = container.querySelector('.next');
        let currentIndex = 0;

        // Função para atualizar a posição do carrossel e o estado dos botões
        function updateCarousel() {
            const cardWidth = carousel.querySelector('.movie-card').offsetWidth;
            const gap = parseInt(window.getComputedStyle(carousel).gap);
            const offset = -currentIndex * (cardWidth + gap);
            carousel.style.transform = `translateX(${offset}px)`;

            // Lógica para desabilitar/habilitar botões
            if (prevButton) {
                prevButton.classList.toggle('disabled', currentIndex === 0);
            }
            if (nextButton) {
                const totalCards = carousel.children.length;
                const containerWidth = container.offsetWidth;
                const visibleCards = Math.floor(containerWidth / (cardWidth + gap));
                const maxIndex = totalCards - visibleCards;
                nextButton.classList.toggle('disabled', currentIndex >= maxIndex);
            }
        }

        if (nextButton) {
            nextButton.addEventListener('click', () => {
                const totalCards = carousel.children.length;
                const cardWidth = carousel.querySelector('.movie-card').offsetWidth;
                const gap = parseInt(window.getComputedStyle(carousel).gap);
                const containerWidth = container.offsetWidth;
                const visibleCards = Math.floor(containerWidth / (cardWidth + gap));
                const maxIndex = totalCards - visibleCards;

                if (currentIndex < maxIndex) {
                    currentIndex++;
                    updateCarousel();
                }
            });
        }

        if (prevButton) {
            prevButton.addEventListener('click', () => {
                if (currentIndex > 0) {
                    currentIndex--;
                    updateCarousel();
                }
            });
        }

        // Atualiza o carrossel ao carregar a página e ao redimensionar a janela
        window.addEventListener('resize', updateCarousel);
        updateCarousel(); // Chamada inicial
    });
});

