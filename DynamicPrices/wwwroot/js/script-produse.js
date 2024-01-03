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
function showProductsByType(tipProdus) {
    fetch('/DB/ProduseElectronice?tipProdus=' + tipProdus)
        .then(Response => Response.json())
        .then(data => {
            const container = document.querySelector('.container-info');
            container.innerHTML = '';

            if (data && data.length > 0) {
                const table = document.createElement('table');
                table.className = 'tabel-produse';

                const thead = document.createElement('thead');
                thead.innerHTML = `
                    <tr>
                        <td>Nr.</td>
                        <td>Nume Produs</td>
                        <td>Cost Producere</td>
                        <td>Preț Recomandat</td>
                        <td>Descriere</td>
                    </tr>
                `;
                table.appendChild(thead);

                let nr = 1;
                data.forEach(produs => {
                    const tr = document.createElement('tr');
                    tr.innerHTML = `
                        <td class="nr-produs">${nr}</td>
                        <td>${produs['nume_produs']}</td>
                        <td>${produs['cost_producere']}</td>
                        <td>${produs['pret_recomandat']}</td>
                        <td class="descriere-produs">${produs['descriere']}</td>
                    `;
                    table.appendChild(tr);
                    nr++;
                });

                container.appendChild(table);
            } else {
                container.innerHTML = '<p>Niciun produs disponibil.</p>';
            }
        })

    .catch(error => {
        console.error('Eroare: ', error);
    });
}