const clickButton = document.querySelector('.toggle-icon');
clickButton.addEventListener('click', () => {
    document.querySelector('nav').classList.toggle('show-nav');
});