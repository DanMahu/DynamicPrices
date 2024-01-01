function showTipProduse() {
    document.getElementById("p-dropdown").classList.toggle("show");
    document.getElementById("p-dropdown").parentElement.classList.toggle("open");
}

function showElectronice() {
    document.getElementById("tip-dropdown").classList.toggle("show");
    document.getElementById("tip-dropdown").parentElement.classList.toggle("open");
}

function showMenu(menu_id) {
    hideCurrentMenu();
    const allMenus = document.querySelectorAll('.dropdown-tip-produse');
    allMenus.forEach(function (menu) {
        if (menu.id === menu_id) {
            menu.style.display = 'block';
        }
    })
}

function hideCurrentMenu() {
    const allMenus = document.querySelectorAll('.dropdown-tip-produse');
    allMenus.forEach(function (menu) {
        menu.style.display = 'none';
    })
}