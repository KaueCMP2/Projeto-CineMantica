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

    // --- Login/Logout Logic in Header ---
    const isLoggedIn = localStorage.getItem('isLoggedIn');
    if (isLoggedIn === 'true') {
        if (userDropdown) {
            userDropdown.innerHTML = `
                <a href="../Tela Perfil/index.html">Meu Perfil</a>
                <a href="#" id="logout-btn">Sair</a>
            `;

            const logoutBtn = document.getElementById('logout-btn');
            if (logoutBtn) {
                logoutBtn.addEventListener('click', (e) => {
                    e.preventDefault();
                    localStorage.removeItem('isLoggedIn');
                    localStorage.removeItem('currentUser');
                    // Optional: remove userProfile if you want to clear settings
                    window.location.href = "../Tela login/login.html";
                });
            }
        }
    }

    // --- Sidebar Profile/Following Link Logic ---
    const sidebarLinks = document.querySelectorAll('.sidebar-nav a');
    sidebarLinks.forEach(link => {
        const linkText = link.textContent.trim();
        if (linkText.includes('Perfil') || linkText.includes('Seguindo')) {
            if (isLoggedIn !== 'true') {
                link.parentElement.style.display = 'none';
            }
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

    // Script para Carrosséis (Genérico para múltiplos carrosséis)
    const carouselWrappers = document.querySelectorAll('.carousel-content-wrapper');

    carouselWrappers.forEach(wrapper => {
        const container = wrapper.querySelector('.carousel-container');
        const nextArrow = wrapper.querySelector('.carousel-arrow.next');
        const prevArrow = wrapper.querySelector('.carousel-arrow.prev');

        if (!container || !nextArrow || !prevArrow) return;

        // Inicializa estado da seta anterior
        prevArrow.classList.add('hidden');

        // Scroll Next
        nextArrow.addEventListener('click', () => {
            const card = container.querySelector('.movie-card');
            if (card) {
                const scrollAmount = card.offsetWidth + 20; // Largura do card + gap
                container.scrollBy({
                    left: scrollAmount,
                    behavior: 'smooth'
                });
                // Mostra a seta anterior após o primeiro scroll
                if (prevArrow.classList.contains('hidden')) {
                    prevArrow.classList.remove('hidden');
                }
            }
        });

        // Scroll Prev
        prevArrow.addEventListener('click', () => {
            const card = container.querySelector('.movie-card');
            if (card) {
                const scrollAmount = card.offsetWidth + 20; // Largura do card + gap
                container.scrollBy({
                    left: -scrollAmount,
                    behavior: 'smooth'
                });

                // Opcional: Esconder seta anterior se voltar ao início (precisaria de lógica de scroll event)
            }
        });

        // Adiciona listener de scroll para gerenciar visibilidade das setas (Opcional, mas bom para UX)
        container.addEventListener('scroll', () => {
            if (container.scrollLeft <= 0) {
                prevArrow.classList.add('hidden');
            } else {
                prevArrow.classList.remove('hidden');
            }
        });
    });

    // Script para o Modal de Resenha e Detalhes do Filme
    const reviewModal = document.getElementById('reviewModal');
    const reviewButtons = document.querySelectorAll('.review-tag');
    const closeModalBtn = document.getElementById('closeModalBtn');
    const modalTitle = document.getElementById('modalMovieTitle');
    const modalOverview = document.getElementById('modalMovieOverview');
    const modalRating = document.getElementById('modalMovieRating');
    const modalPoster = document.getElementById('modalMoviePoster');

    function openMovieModal(movie) {
        if (!reviewModal) return;

        if (modalTitle) modalTitle.textContent = movie.title;
        if (modalOverview) modalOverview.textContent = movie.overview || 'Sinopse não disponível.';

        // Atualiza nota geral
        if (modalRating) {
            modalRating.innerHTML = `${movie.vote_average.toFixed(1)} <i class="far fa-star" style="color: red;"></i>`;
        }

        // Atualiza nota de amigos (fixo em 0 por enquanto)
        const modalFriendsRating = document.getElementById('modalFriendsRating');
        if (modalFriendsRating) {
            modalFriendsRating.innerHTML = `0 <i class="far fa-star" style="color: red;"></i>`;
        }

        if (modalPoster) {
            const posterSrc = movie.poster_path
                ? `${IMAGE_BASE_URL}${movie.poster_path}`
                : 'https://placehold.co/500x750?text=Sem+Imagem';
            modalPoster.src = posterSrc;
        }

        // Configura botões de avaliação
        const modalBtns = document.querySelectorAll('.modal-btn');
        if (modalBtns.length >= 2) {
            const newReviewBtn = modalBtns[0];
            const viewReviewsBtn = modalBtns[1];

            const posterSrc = movie.poster_path ? `${IMAGE_BASE_URL}${movie.poster_path}` : '';
            const params = new URLSearchParams({
                title: movie.title,
                poster: posterSrc
            }).toString();

            newReviewBtn.onclick = () => {
                const isUserLoggedIn = localStorage.getItem('isLoggedIn') === 'true';
                if (isUserLoggedIn) {
                    window.location.href = `../Tela Avaliacoes/nova_avaliacao.html?${params}`;
                } else {
                    alert("Você precisa estar logado para fazer uma avaliação!");
                }
            };

            viewReviewsBtn.onclick = () => {
                window.location.href = `../Tela Avaliacoes/ver_avaliacoes.html?${params}`;
            };
        }

        reviewModal.classList.add('active');
    }

    if (reviewModal && closeModalBtn) {
        // Evento para botões de resenha (Página Seguindo)
        if (reviewButtons) {
            reviewButtons.forEach(btn => {
                btn.addEventListener('click', (e) => {
                    e.preventDefault(); // Previne comportamento padrão se for link
                    reviewModal.classList.add('active');
                });
            });
        }

        closeModalBtn.addEventListener('click', () => {
            reviewModal.classList.remove('active');
        });

        window.addEventListener('click', (e) => {
            if (e.target === reviewModal) {
                reviewModal.classList.remove('active');
            }
        });
    }


    // --- INTEGRAÇÃO COM A API DO TMDB ---
    const API_KEY = 'd1f9bb73b6a11d4041713d4e2f755ea3'; // Substitua pela sua chave da API
    const BASE_URL = 'https://api.themoviedb.org/3';
    const IMAGE_BASE_URL = 'https://image.tmdb.org/t/p/w500';

    async function fetchMovies(endpoint, containerId, type) {
        try {
            const separator = endpoint.includes('?') ? '&' : '?';
            const response = await fetch(`${BASE_URL}${endpoint}${separator}api_key=${API_KEY}&language=pt-BR`);
            const data = await response.json();
            displayMovies(data.results, containerId, type);
        } catch (error) {
            console.error('Erro ao buscar filmes:', error);
        }
    }

    function displayMovies(movies, containerId, type) {
        const container = document.getElementById(containerId);
        if (!container) return;

        container.innerHTML = ''; // Limpa o container

        // Limita o grid a 4 itens para manter o layout CSS, carrossel pode ter mais
        const itemsToDisplay = type === 'grid' ? movies.slice(0, 4) : movies;

        itemsToDisplay.forEach(movie => {
            const movieElement = document.createElement('div');
            const posterSrc = movie.poster_path
                ? `${IMAGE_BASE_URL}${movie.poster_path}`
                : 'https://placehold.co/500x750?text=Sem+Imagem';

            if (type === 'carousel') {
                movieElement.classList.add('movie-card');
                movieElement.innerHTML = `
                    <img src="${posterSrc}" alt="${movie.title}" class="movie-poster">
                    <div class="movie-info">
                        <h3 class="movie-title">${movie.title}</h3>
                        <div class="movie-rating-icons">
                            ${getStarRating(movie.vote_average)}
                        </div>
                        <p class="movie-description">${movie.overview ? movie.overview.substring(0, 100) + '...' : 'Sem descrição.'}</p>
                    </div>
                `;
            } else if (type === 'grid') {
                movieElement.classList.add('grid-item');
                movieElement.innerHTML = `
                    <img src="${posterSrc}" alt="${movie.title}">
                    <div class="grid-item-info">
                        <h4 class="grid-movie-title">${movie.title}</h4>
                        <div class="grid-movie-rating">
                            <i class="fas fa-star"></i>
                            <span>${movie.vote_average.toFixed(1)}</span>
                        </div>
                    </div>
                `;
            }

            // Adiciona evento de clique para abrir o modal
            movieElement.addEventListener('click', () => {
                openMovieModal(movie);
            });

            // Adiciona cursor pointer para indicar clicável
            movieElement.style.cursor = 'pointer';

            container.appendChild(movieElement);
        });
    }

    function getStarRating(vote) {
        const stars = Math.round(vote / 2); // Converte de 0-10 para 0-5
        let starHtml = '';
        for (let i = 0; i < 5; i++) {
            if (i < stars) {
                starHtml += '<i class="fas fa-star"></i>';
            } else {
                starHtml += '<i class="far fa-star"></i>';
            }
        }
        return starHtml;
    }

    // --- Sidebar Following List Logic ---
    function updateSidebarFollowingList() {
        const followingListContainer = document.querySelector('.sidebar .following-list');
        if (!followingListContainer) return;

        const currentUser = localStorage.getItem('currentUser');
        if (!currentUser) {
            followingListContainer.innerHTML = '<li style="padding: 10px; color: #aaa;">Faça login para ver quem você segue.</li>';
            return;
        }

        const followingListKey = `userFollowing_${currentUser}`;
        const followingList = JSON.parse(localStorage.getItem(followingListKey)) || [];

        if (followingList.length > 0) {
            followingListContainer.innerHTML = '';
            followingList.forEach(userId => {
                const userProfile = JSON.parse(localStorage.getItem(`userProfile_${userId}`)) || {
                    name: 'Usuário',
                    avatar: 'https://placehold.co/40x40/e0e0e0/000?text=User'
                };

                // Calculate watched count (mock logic or real if available)
                // For now, let's count reviews by this user
                const allReviews = JSON.parse(localStorage.getItem('reviews')) || [];
                const userReviewsCount = allReviews.filter(r => r.userId === userId || r.user === userProfile.name).length;

                const li = document.createElement('li');
                li.className = 'following-item';
                li.innerHTML = `
                    <a href="../Tela Perfil/index.html?user=${userId}" style="display: flex; align-items: center; text-decoration: none; color: inherit; width: 100%;">
                        <img src="${userProfile.avatar}" alt="Avatar">
                        <span>${userProfile.name}</span>
                        <span class="watched-count" title="Avaliações">${userReviewsCount}</span>
                    </a>
                `;
                followingListContainer.appendChild(li);
            });
        } else {
            followingListContainer.innerHTML = '<li style="padding: 10px; color: #aaa;">Você ainda não segue ninguém.</li>';
        }
    }

    // Call it on load
    updateSidebarFollowingList();

    // Chamadas iniciais
    if (API_KEY !== 'YOUR_API_KEY') {
        // Home Page
        fetchMovies('/movie/now_playing', 'featured-carousel', 'carousel');
        fetchMovies('/movie/popular', 'trending-grid', 'grid');

        // Destaques Page (Categorias)
        // Comédia: 35, Terror: 27, Ação: 28, Romance: 10749, Ficção: 878, Animação: 16
        // Filtros: Mais bem avaliados (vote_average.desc) e com mínimo de votos (vote_count.gte=300) para evitar desconhecidos
        const filterParams = '&sort_by=popularity.desc';

        fetchMovies(`/discover/movie?with_genres=35${filterParams}`, 'comedy-carousel', 'carousel');
        fetchMovies(`/discover/movie?with_genres=27${filterParams}`, 'horror-carousel', 'carousel');
        fetchMovies(`/discover/movie?with_genres=28${filterParams}`, 'action-carousel', 'carousel');
        fetchMovies(`/discover/movie?with_genres=10749${filterParams}`, 'romance-carousel', 'carousel');
        fetchMovies(`/discover/movie?with_genres=878${filterParams}`, 'scifi-carousel', 'carousel');
        fetchMovies(`/discover/movie?with_genres=16${filterParams}`, 'animation-carousel', 'carousel');
    } else {
        console.warn('API Key não configurada. Adicione sua chave no script.js');
    }
});
