
        document.addEventListener('DOMContentLoaded', function () {
            // Script original para o dropdown de usuário
            const userIcon = document.getElementById('user-icon');
            const userDropdown = document.getElementById('user-dropdown');

            userIcon.addEventListener('click', function (event) {
                event.stopPropagation();
                userDropdown.classList.toggle('show');
            });

            window.addEventListener('click', function (event) {
                if (userDropdown.classList.contains('show')) {
                    userDropdown.classList.remove('show');
                }
            });

            // Novo script para o menu hamburger
            const hamburger = document.querySelector('.hamburger');
            const sidebar = document.querySelector('.sidebar');

            hamburger.addEventListener('click', () => {
                sidebar.classList.toggle('active-side');
                hamburger.classList.toggle('active-button');
            });
        });
