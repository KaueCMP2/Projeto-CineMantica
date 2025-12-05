document.addEventListener('DOMContentLoaded', function() {
    const mainBanner = document.querySelector('.main-banner');
    const carouselContainer = document.querySelector('.background-carousel');
    const imageUrls = [
        'Styles.css/superman-2025-7680x4320-20308.jpg',
        'Styles.css/spider-man-across-the-spider-verse-1920x1080-v0-ki4gcq26iw4b1.png',
        'Styles.css/demon-slayer-3840x2160-23615.jpg',

    ];

    let currentImageIndex = 0;

    function createAndAppendImage(src, isActive = false) {
        const img = document.createElement('img');
        img.src = src;
        img.alt = 'Background image';
        if (isActive) {
            img.classList.add('active');
        }
        carouselContainer.appendChild(img);
        return img;
    }

    // Carregar todas as imagens no DOM, a primeira como ativa
    const imagesElements = imageUrls.map((url, index) => 
        createAndAppendImage(url, index === 0)
    );

    function showNextImage() {
        // Remove a classe 'active' da imagem atual
        imagesElements[currentImageIndex].classList.remove('active');

        // Incrementa o índice, voltando para 0 se for a última imagem
        currentImageIndex = (currentImageIndex + 1) % imageUrls.length;

        // Adiciona a classe 'active' à próxima imagem
        imagesElements[currentImageIndex].classList.add('active');
    }

    // Inicia o carrossel, alternando a cada 5 segundos (5000 milissegundos)
    setInterval(showNextImage, 5000);
    
    mainBanner.classList.add('loaded');
});