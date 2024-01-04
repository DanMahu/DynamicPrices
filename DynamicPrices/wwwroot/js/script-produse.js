function showTipProduse() {
    document.getElementById("p-dropdown").classList.toggle("show");
    document.getElementById("p-dropdown").parentElement.classList.toggle("open");
}

function showElectronice() {
    document.getElementById("tip-dropdown").classList.toggle("show");
    document.getElementById("tip-dropdown").parentElement.classList.toggle("open");
}

function showProduse() {
    document.getElementById("istoric-dropdown").classList.toggle("show");
    document.getElementById("istoric-dropdown").parentElement.classList.toggle("open");
}

function showMenu(menu_id) {
    //hideAltMenu();
    const allMenus = document.querySelectorAll('.dropdown-tip-produse');
    allMenus.forEach(function (menu) {
        if (menu.id === menu_id) {
            menu.style.display = 'block';
        }
    })
}

function hideAltMenu() {
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
                        <th class="p-h-1">Nr.</th>
                        <th class="p-h-2">Nume Produs</th>
                        <th class="p-h-3">Cost Producere</th>
                        <th class="p-h-4">Preț Recomandat</th>
                        <th class="p-h-5">Descriere</th>
                        <th class="p-h-6">Preț Curent</th>
                    </tr>
                `;
                table.appendChild(thead);

                let nr = 1;
                data.forEach(produs => {
                    const tr = document.createElement('tr');
                    tr.classList.add('p-row')
                    tr.innerHTML = `
                        <td>${nr}</td>
                        <td>${produs['nume_produs']}</td>
                        <td>${produs['cost_producere'].toFixed(2)}</td>
                        <td>${produs['pret_recomandat'].toFixed(2)}</td>
                        <td class="descriere-produs">${produs['descriere']}</td>
                        <td class="pret-curent-produs">${produs['pret_curent'].toFixed(2)}</td>
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

    //afisare meniu de istoric preturi
    showMenu('istoric-preturi');
    fetch('ListaDeProduse?tipProdus=' + tipProdus)
        .then(Response => Response.json())
        .then(data => {
            //alert(JSON.stringify(data));
            const div_a = document.querySelector('.istoric-dropdown-content');
            div_a.innerHTML = '';
            div_a.innerHTML = '<input type="text" class="search-bar" id="search-bar" placeholder="Caută produs..." oninput="searchProducts()">';
            if (data && data.length > 0) {
                data.forEach(produs => {
                    const a = document.createElement('a');
                    a.href = '#' + produs.id_produs;
                    a.classList.add('product-item');
                    a.onclick = function () {
                        showPriceHistory(produs.id_produs, produs.nume_produs)
                    };
                    a.innerHTML = produs.nume_produs;
                    div_a.appendChild(a);
                });
            } else {
                div_a.innerHTML = "<a>-EROARE-</a>";
            }
        })
}

function searchProducts() {
    const searchTerm = document.getElementById('search-bar').value.toLowerCase();
    const productItems = document.querySelectorAll('.product-item');

    productItems.forEach(item => {
        const productName = item.innerHTML.toLowerCase();

        if (productName.includes(searchTerm)) {
            item.style.display = 'block';
        } else {
            item.style.display = 'none';
        }
    });
}

function showPriceHistory(product_id, product_name) {
    fetch('/DB/IstoriePreturiDupaProdus?product_id=' + product_id)
        .then(Response => Response.json())
        .then(data => {
            const container = document.querySelector('.container-info');
            container.innerHTML = '';

            if (data && data.length > 0) {
                const table = document.createElement('table');
                table.className = 'tabel-istoric';

                const thead = document.createElement('thead');
                thead.innerHTML = `
                    <tr>
                        <th class="phy-h-name" colspan="3">${product_name}</th>
                    </tr>
                    <tr class="phy-h-main">
                        <th class="phy-h-1">Preț Vechi</th>
                        <th class="phy-h-2">Preț Nou</th>
                        <th class="phy-h-3">Data Modificare</th>
                    </tr>
                `;
                table.appendChild(thead);

                data.forEach(produs => {
                    const tr = document.createElement('tr');
                    tr.classList.add('phy-row')
                    tr.innerHTML = `
                        <td>${produs['pret_vechi'].toFixed(2)}</td>
                        <td>${produs['pret_nou'].toFixed(2)}</td>
                        <td>${produs['data_modificare']}</td>
                    `;
                    table.appendChild(tr);
                });

                container.appendChild(table);
            } else {
                container.innerHTML = '<p>Acest produs nu a avut modificări de preț.</p>';
            }
        })

        .catch(error => {
            console.error('Eroare: ', error);
        });
}