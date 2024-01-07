function showDetails(div_id) {
    document.getElementById(div_id).classList.toggle("show");
    document.getElementById(div_id).parentElement.classList.toggle("open");
}

function showMenu(menu_id) {
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
    showMenu('detalii-produse');
    fetch('ListaDeProduse?tipProdus=' + tipProdus)
        .then(Response => Response.json())
        .then(data => {
            //alert(JSON.stringify(data));
            const div_istoric = document.querySelector('.detalii-dropdown-content');
            div_istoric.innerHTML = '';
            div_istoric.innerHTML = `<input type="text" class="search-bar" id="search-bar1" placeholder="Caută produs..." oninput="searchProducts('search-bar1')">`;
            if (data && data.length > 0) {
                data.forEach(produs => {
                    const a = document.createElement('a');
                    a.href = '#' + produs.id_produs;
                    a.classList.add('product-item');
                    a.onclick = function () {
                        showOptions(produs.id_produs, produs.nume_produs);
                    };
                    a.innerHTML = produs.nume_produs;
                    div_istoric.appendChild(a);
                });
            } else {
                div_istoric.innerHTML = "<a>-EROARE-</a>";
            }
        })
        .catch(error => {
            console.error('Eroare: ', error);
        });
}

function searchProducts(search_id) {
    const searchTerm = document.getElementById(search_id).value.toLowerCase();
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
                        <th class="phy-h-3">Dată Modificare</th>
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
                container.innerHTML = '<p class="else-message">Acest produs nu a avut modificări de preț.</p>';
            }
        })
        .catch(error => {
            console.error('Eroare: ', error);
        });
}

function showMinMaxStoc(idProdus, numeProdus) {
    fetch('MinMaxStoc?idProdus=' + idProdus)
        .then(Response => Response.json())
        .then(data => {
            const container = document.querySelector('.container-info');
            container.innerHTML = '';

            if (data && data.length > 0) {
                const table = document.createElement('table');
                table.className = 'tabel-stoc';

                const thead = document.createElement('thead');
                thead.innerHTML = `
                    <tr>
                        <th class="stoc-h-name" colspan="3">${numeProdus}</th>
                    </tr>
                    <tr class="stoc-h-main">
                        <th class="stoc-h-1">Cantitate</th>
                        <th class="stoc-h-2">Stoc Minim</th>
                        <th class="stoc-h-3">Stoc Maxim</th>
                    </tr>
                `;
                table.appendChild(thead);

                const tr = document.createElement('tr');
                tr.classList.add('stoc-row')
                tr.innerHTML = `
                    <td>${data[0]}</td>
                    <td>${data[1]}</td>
                    <td>${data[2]}</td>
                `;
                table.appendChild(tr);
                container.appendChild(table);
            } else { container.innerHTML = '<p class="else-message">Acest produs nu a are stoc.</p>'; }
        })
        .catch(error => {
            console.error('Eroare: ' + error);
        });
}

function fetchData(url) {
    return fetch(url)
        .then(Response => {
            if (!Response.ok) {
                throw new Error('Cererea catre server a esuat!');
            }
            return Response.json();
        })
        .catch(error => {
            console.error('Eroare: ', error);
            return null;
        });
}

let lastId = undefined;
function showOptions(idProdus, numeProdus) {
    if (lastId != idProdus) {
        lastId = idProdus;
        const options = document.getElementById('multiple-options');
        const div = document.querySelector('.multiple-options');
        div.innerHTML = '';

        if (!options.classList.contains('afisat')) {
            options.classList.add('afisat');
        } else {
            options.classList.remove('afisat');
            setTimeout(() => {
                options.classList.add('afisat');
            }, 500);
        }
        Promise.all([
            fetchData('PretDupaProdus?idProdus=' + idProdus),
            fetchData('StocDupaProdus?idProdus=' + idProdus)
        ])
            .then(([pret, stoc]) => {
                if (pret != null) {
                    const aPret = document.createElement('a');
                    aPret.innerHTML = `Preț Curent (${pret.toFixed(2)})`;
                    setTimeout(() => {
                        div.appendChild(aPret);
                    }, 500);
                }

                if (stoc != null) {
                    const aStoc = document.createElement('a');
                    aStoc.href = '#';
                    aStoc.classList.add('a-option');
                    aStoc.onclick = function () {
                        showMinMaxStoc(idProdus, numeProdus);
                    };
                    aStoc.innerHTML = `Stoc (${stoc}) &#11208`;
                    setTimeout(() => {
                        div.appendChild(aStoc);
                    }, 550);
                }

                const istoricPreturi = document.createElement('a');
                istoricPreturi.href = '#';
                istoricPreturi.classList.add('a-option');
                istoricPreturi.onclick = function () {
                    showPriceHistory(idProdus, numeProdus);
                };
                istoricPreturi.innerHTML = 'Istoric Prețuri &#11208';
                setTimeout(() => {
                    div.appendChild(istoricPreturi);
                }, 600);
            });
    }
}

const options = document.getElementById('multiple-options');
var timer;
options.addEventListener('mouseover', function () {
    options.classList.add('afisat');
    clearTimeout(timer);
});

options.addEventListener('mouseleave', function () {
    timer = setTimeout(() => {
        options.classList.remove('afisat');
    }, 900);
    lastId = undefined;
});