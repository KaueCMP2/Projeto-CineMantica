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

    // Script para o menu hamburger
    const hamburger = document.querySelector('.hamburger');
    const sidebar = document.querySelector('.sidebar');

    if (hamburger) {
        hamburger.addEventListener('click', () => {
            sidebar.classList.toggle('active-side');
            hamburger.classList.toggle('active-button');
        });
    }

    // Script para o carrossel "Em Destaque"
    const carouselContainer = document.querySelector('.carousel-container');
    const nextArrow = document.querySelector('.carousel-arrow.next');
    const prevArrow = document.querySelector('.carousel-arrow.prev');

    if (prevArrow) {
        prevArrow.classList.add('hidden');
    }

    if (nextArrow && carouselContainer && prevArrow) {
        nextArrow.addEventListener('click', () => {
            const scrollAmount = carouselContainer.querySelector('.movie-card').offsetWidth + 20; // Largura do card + gap
            carouselContainer.scrollBy({
                left: scrollAmount,
                behavior: 'smooth'
            });
            if (prevArrow.classList.contains('hidden')) {
                prevArrow.classList.remove('hidden');
            }
        });

        prevArrow.addEventListener('click', () => {
            const scrollAmount = carouselContainer.querySelector('.movie-card').offsetWidth + 20; // Largura do card + gap
            carouselContainer.scrollBy({
                left: -scrollAmount,
                behavior: 'smooth'
            });
        });
    }
});

